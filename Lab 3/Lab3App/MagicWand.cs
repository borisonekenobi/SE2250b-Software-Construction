using System;

namespace Lab3App
{
    class MagicWand : Tool
    {
        public MagicWand(string description)
        {
            Description = description;
        }

        public override void Display()
        {
            Console.WriteLine($"{nameof(MagicWand)} {Description} is displayed");
        }

        public override void DoAction()
        {
            Console.WriteLine($"{nameof(MagicWand)} is Used");
        }
    }
}
