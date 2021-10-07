using System;
using System.Collections;
using System.Text;

namespace TextAdventure.src
{
    class BG
    {
        int height = 0;
        int width = 0;

        Queue Line = new Queue();
        public Queue Frame = new Queue();

        public void Create(int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                Line.Enqueue("0");
            }
            for (int i = 0; i < width; i++)
            {
                Frame.Enqueue(Line);
            }
        }
    }
}
