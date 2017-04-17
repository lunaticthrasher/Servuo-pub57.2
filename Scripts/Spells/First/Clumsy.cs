using System;
using Server.Targeting;
using System.Collections.Generic;

namespace Server.Spells.First
{
    public class ClumsySpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Clumsy", "Uus Jux",
            212,
            9031,
            Reagent.Bloodmoss,
            Reagent.Nightshade);
        public ClumsySpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }

        public static void RemoveEffects(Mobile m)
        {
            if (m_Table.ContainsKey(m))
                m_Table.Remove(m);
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }
        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckHSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, this.Caster, ref m);

				SpellHelper.AddStatCurse(this.Caster, m, StatType.Dex);
				int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, m, true) * 100);
				TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Clumsy, 1075831, length, m, percentage.ToString()));

				if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                m.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
                m.PlaySound(0x1DF);

                this.HarmfulSpell(m);

                m_Table[m] = Timer.DelayCall<Mobile>(length, RemoveEffects, m);
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly ClumsySpell m_Owner;
            public InternalTarget(ClumsySpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    this.m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}