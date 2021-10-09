using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.src
{
    class Cursor
    {
        #region 변수
        public int posX { get; set; }
        public int posY { get; set; }

        public const int Health = 100;
        public int HP { get; set; }
        public int Atk { get; set; }

        #endregion

        #region 생성자
        public Cursor()
        {
            HP = Health;
        }   
        public Cursor(int Atk) : this()
        {
            this.Atk = Atk;
        }
        #endregion
       
        public void Damaged(Enemy obj)
        {
            HP -= obj.Atk;
        }

        public void Create(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        

    }
}
