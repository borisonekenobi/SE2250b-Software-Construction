using System;

namespace Lab3App
{
    class Diamond : Treasure
    {
        public Diamond(string description, int score)
        {
            Description = description;
            Score = score;
        }

        public override void Display()
        {
            Console.WriteLine($"{nameof(Diamond)} {Description} is displayed");
        }
    }
}
