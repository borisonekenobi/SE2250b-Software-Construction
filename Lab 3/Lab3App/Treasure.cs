using System;
using System.Collections.Generic;

namespace Lab3App
{
    abstract class Treasure : Collectable
    {
        public int Score { get; set; }

        private void UpdateTotalScore()
        {
            Board.TotalScore += Score;
            Console.WriteLine($"Total Score is updated to: {Board.TotalScore}");
        }

        public override void AddMe(List<Collectable> list)
        {
            base.AddMe(list);
            UpdateTotalScore();
        }
    }
}
