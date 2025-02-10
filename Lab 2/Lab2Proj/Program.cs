using System;

namespace Lab2Proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Monster m = new Monster
            {
                Name = "Normal Mon"
            };
            Console.WriteLine(m.ToString());
            m.Draw();


            LevitatedMonster levitated = new LevitatedMonster
            {
                Name = "Levitated Mon",
                Hight = 5
            };
            Console.WriteLine(levitated.ToString());
            levitated.Draw();

            
            ConfusedMonster confused = new ConfusedMonster();
            confused.Name = "Confiused Mon";
            Console.WriteLine(confused.ToString());
            confused.Draw();
        }
    }
}
