using System;

namespace Lab2Proj
{
    internal class Monster
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public virtual void Draw()
        {
            DrawMonster();
            DrawShadow();
        }

        public void DrawMonster(string prefix = "", string sufix = "")
        {
            Console.WriteLine(prefix + "  \\     / " + sufix);
            Console.WriteLine(prefix + "   .---. " + sufix);
            Console.WriteLine(prefix + "  |o   o| " + sufix);
            Console.WriteLine(prefix + "  | \\_/ | " + sufix);
        }


        public void DrawShadow()
        {
            Console.WriteLine(" ===========");
        }

        public override string ToString()
        {
            return "''''''''''''''''''''''''''''''''''''''''''''''''''\n" +
                    $"{Name} \n" +
                    "''''''''''''''''''''''''''''''''''''''''''''''''''";
        }
    }
}
