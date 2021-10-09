using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace TextAdventure.src
{
    class Program
    {

        static int x_Count = 1;
        static int y_Count = 1;

        static Boolean debugStatus = false;

        static BG bg = new BG();
        static Cursor Location = new Cursor();


        public static void Main(string[] args)
        {
            bg.Create(16,8);

            Location.posX = 0;
            Location.posY = 0;

            PlayerMove();
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
            do {
                y_Count = 0;
                x_Count = 0;
            } while (false);

            foreach (List<string> bg_Line in bg.Frame)
            {
                x_Count = 0;
                ++y_Count;
                foreach (string bg_Text in bg_Line)
                {
                    ++x_Count;

                    if (x_Count-1 == Location.posX && y_Count-1 == Location.posY)
                    {
                        Console.Write("1");
                        continue;
                    }
                        
                     Console.Write(bg_Text);
                }
                Console.WriteLine();
            }
        }

        public static void PlayerMove()
        {
            ConsoleKeyInfo resault = Console.ReadKey();

            if (resault != null) //키보드에서 입력을 받았을 때 
            {

                if(debugStatus) UPDStatus();
                Console.WriteLine(resault.Key);

                switch (resault.Key)
                {
                    case ConsoleKey.UpArrow: //위쪽 방향키
                        Console.WriteLine("true");
                        Location.posY--;
                        break;

                    case ConsoleKey.DownArrow: //아래쪽 방향키
                        Console.WriteLine("true");
                        Location.posY++;
                        break;

                    case ConsoleKey.LeftArrow: //왼쪽 방향키
                        Console.WriteLine("true");
                        Location.posX--;
                        break;

                    case ConsoleKey.RightArrow: //오른쪽 방향키
                        Console.WriteLine("true");
                        Location.posX++;
                        break;

                }

                if (Location.posX < 0)  Location.posX = 0;
                if (Location.posY < 0)  Location.posY = 0;
                if (Location.posX >= bg.Line.Count) Location.posX = bg.Line.Count-1;
                if (Location.posY >= bg.Frame.Count) Location.posY = bg.Frame.Count-1; //커서가 배경 밖으로 빠져나올경우 위치 고정

                Console.WriteLine(bg.Frame.Count);

                Console.WriteLine(bg.Frame[Location.posY]);

                List<string> temp = (List<string>)bg.Frame[Location.posY];

                

                if (!debugStatus) UPDStatus(); 
                
                Console.WriteLine("{0}, {1}", Location.posX, Location.posY);



            }
        }
    }
}
