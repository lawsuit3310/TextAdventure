using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.src
{
    class Cursor
    {
        public int posX { get; set; }
        public int posY { get; set; }

        static Program main = new Program();

        public void CreatePortal(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

    }
}
