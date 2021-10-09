using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.src
{
    class Player : Cursor
    {
        Player()
        {
            HP = Health;
        }
        public Player(int Atk) : this()
        {
            this.Atk = Atk;
        }

        public void Damaged(Enemy obj)
        {
            HP -= obj.Atk;
        }
        
        public void printStatus()
        {
            Console.WriteLine("\n현재 체력 : {0}/{1}", HP, Health);
        }
    }
}
