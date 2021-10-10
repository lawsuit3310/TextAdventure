using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.src
{
    class Item : Cursor
    {
        public int itemType { get; set; }

        static Random rand = new Random();

        public Item()
        {
            itemType = rand.Next(2);
        }
    }
}
