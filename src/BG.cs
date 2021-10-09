using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.src
{
    class BG
    {
        public List<string> Line = new List<string>();
        public ArrayList Frame = new ArrayList();

        public void Create(int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                Line.Add("0");
            }
            for (int i = 0; i < width; i++)
            {
                Frame.Add(Line);
            }
        }
    }
}
