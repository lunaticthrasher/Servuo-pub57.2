using Server.Items;
using System;

namespace Server.Engines.Quests
{
    public class Belulah : MondainQuester
    {
        [Constructable]
        public Belulah()
            : base("Belulah", "The Scorned")
        {
        }

        public Belulah(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(AllSeasonAdventurerQuest)
                };
        public override void InitBody()
        {
            Female = true;
            Race = Race.Human;

            Hue = 0x83F7;
            HairItemID = 0x2046;
            HairHue = 0x463;
        }

        public override void InitOutfit()
        {
            SetWearable(new Backpack());
            SetWearable(new Boots(), dropChance: 1);
            SetWearable(new LongPants(), 0x6C7, 1);
            SetWearable(new FancyShirt(), 0x6BB, 1);
            SetWearable(new Cloak(), 0x59, 1);
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