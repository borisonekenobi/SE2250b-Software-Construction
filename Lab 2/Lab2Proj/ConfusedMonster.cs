using System;
using System.Linq;

namespace Lab2Proj
{
    class ConfusedMonster : Monster
    {


        public override void Draw()
        {
            DrawConfusionLine();
            DrawMonster("?", "?");
            DrawConfusionLine();
            DrawShadow();
        }

        private void DrawConfusionLine()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("?", 12)));
        }
    }
}
