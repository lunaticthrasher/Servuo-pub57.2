using Server.Items;
using System;

namespace Server.Engines.Quests
{
    public class Emilio : MondainQuester
    {
        [Constructable]
        public Emilio()
            : base("Emilio", "the tortured artist")
        {
        }

        public Emilio(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[] { typeof(UnfadingMemoriesOneQuest) };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Hue = 0x83EB;
            HairItemID = 0x2048;
            HairHue = 0x470;
            FacialHairItemID = 0x204C;
            FacialHairHue = 0x470;
        }

        public override void InitOutfit()
        {
            SetWearable(new Backpack());
            SetWearable(new Sandals(), 0x721, 1);
            SetWearable(new LongPants(), 0x51B, 1);
            SetWearable(new FancyShirt(), 0x517, 1);
            SetWearable(new FloppyHat(), 0x584, 1);
            SetWearable(new BodySash(), 0x13, 1);
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