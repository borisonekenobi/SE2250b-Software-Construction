using System;
using System.Linq;

namespace Lab2Proj
{
    class LevitatedMonster : Monster
    {
        private int height;

        public int Hight
        {
            get => height;
            set => height = value;
        }

        public override void Draw()
        {
            DrawMonster();
            DrawGap();
            DrawShadow();
        }

        private void DrawGap()
        {
            Console.Write(string.Concat(Enumerable.Repeat("\n", Hight)));
        }
    }
}
