using System;
using System.Collections.Generic;

namespace Lab3App
{
    abstract class Collectable : Displayable
    {
        public CollectionBoard Board { get; set; }
        public string Description { get; set; }

        public abstract void Display();

        public virtual void AddMe(List<Collectable> list)
        {
            list.Add(this);
            Console.WriteLine($"{Description} Collected, Congrats!!!!");
        }
    }
}
