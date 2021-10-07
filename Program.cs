﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace TextAdventure.src
{
    class Program
    {
        static BG bg = new BG();

        public static void Main(string[] args)
        {
            bg.Create(16,8);
            UPDStatus();

            while (true)
            {
                PlayerMove();
            }

        }

        public static void UPDStatus() //키보드에서 입력을 받아 위치 초기화
        {
            Console.Clear();

            DrawBG();
        }

        public static void DrawBG()
        {


            foreach (ArrayList bg_Line in bg.Frame)
            {
                foreach (ArrayList bg_Text in bg_Line)
                {
                    Console.Write(bg_Text);
                }
                Console.WriteLine();
            }
        }

        public static void PlayerMove()
        {
            Cursor Location = new Cursor();

            Location.posX = 0;
            Location.posY = 0;


            ConsoleKeyInfo resault = Console.ReadKey();

            if (resault != null) //키보드에서 입력을 받았을 때 
            {
                UPDStatus();
                Console.WriteLine(resault.Key);

                switch (resault.Key)
                {
                    case ConsoleKey.UpArrow: //위쪽 방향키
                        Console.WriteLine("true");
                        break;

                    case ConsoleKey.DownArrow: //아래쪽 방향키
                        Console.WriteLine("true");
                        break;

                    case ConsoleKey.LeftArrow: //왼쪽 방향키
                        Console.WriteLine("true");
                        break;

                    case ConsoleKey.RightArrow: //오른쪽 방향키
                        Console.WriteLine("true");
                        break;

                }

                if (Location.posX < 0)  Location.posX = 0;
                if (Location.posY < 0)  Location.posY = 0;
                if (Location.posX > bg.Line.Count) Location.posX = bg.Line.Count;
                if (Location.posY > bg.Frame.Count) Location.posY = bg.Frame.Count; //커서가 배경 밖으로 빠져나올경우 위치 고정

                object temp = bg.Frame[Location.posX];
            }
        }
    }
}
