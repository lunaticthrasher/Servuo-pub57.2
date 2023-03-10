using Server.Items;
using System;

namespace Server.Engines.Quests
{
    public class Jamal : MondainQuester
    {
        [Constructable]
        public Jamal()
            : base("Jamal", "the fisherman")
        {
        }

        public Jamal(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[] { typeof(VilePoisonQuest) };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Hue = 0x83FB;
            HairItemID = 0x2049;
            HairHue = 0x45E;
        }

        public override void InitOutfit()
        {
            SetWearable(new Backpack());
            SetWearable(new ThighBoots(), 0x901, 1);
            SetWearable(new ShortPants(), 0x730, 1);
            SetWearable(new Shirt(), 0x1BB, 1);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}