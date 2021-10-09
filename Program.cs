using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;

namespace TextAdventure.src
{
    class Program
    {

        static int x_Count_Pos = 1;
        static int y_Count_Pos = 1;

        static int x_Count_Portal = 1;
        static int y_Count_Portal = 1;

        static Boolean isOnPortal = false;

        static BG bg = new BG();
        
        static Cursor Location = new Cursor();
        static Cursor Portal = new Cursor();
        
        static Random rand = new Random();


        public static void Main(string[] args)
        {
            Console.WriteLine("엔터키를 누르면 시작합니다.");
            Start();
            PlayerMove();

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
                x_Count_Pos = 0;
                y_Count_Pos = 0;
                x_Count_Portal = 0;
                y_Count_Portal = 0;

            } while (false);

            foreach (List<string> bg_Line in bg.Frame)
            {
                x_Count_Pos = 0;
                x_Count_Portal = 0;

                ++y_Count_Portal;
                ++y_Count_Pos;
                foreach (string bg_Text in bg_Line)
                {
                    ++x_Count_Pos;
                    ++x_Count_Portal;

                    if (x_Count_Pos - 1 == Location.posX && y_Count_Pos - 1 == Location.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Y");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (Location.posX == Portal.posX && Location.posY == Portal.posY)
                        {
                            isOnPortal = true;
                        }
                        else isOnPortal = false;

                        continue;
                    }

                    if (x_Count_Portal - 1 == Portal.posX && y_Count_Portal - 1 == Portal.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }


                    Console.Write(bg_Text);
                }
                Console.WriteLine();
            }
        }

        public static void PlayerMove([CallerMemberNameAttribute] string CallerName = "") //이 함수를 호출한 메소드의 이름을 가져옴.
        {
            ConsoleKeyInfo resault = Console.ReadKey();

            if (resault != null) //키보드에서 입력을 받았을 때 
            {
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
                if (Location.posX >= bg.Line.Count)
                {
                    Location.posX = bg.Line.Count-1;
                }
                if (Location.posY >= bg.Frame.Count) Location.posY = bg.Frame.Count-1; //커서가 배경 밖으로 빠져나올경우 위치 고정
                
                UPDStatus();

                if (isOnPortal)
                {
                    Console.WriteLine("포탈 위에 있습니다.");
                    Console.WriteLine("다음 지역으로 넘어가시겠습니까? Y/N");

                    if (Check())
                    {
                        Console.WriteLine("알겠습니다.");
                        Console.WriteLine("아무키나 입력하면 넘어갑니다..");
                        
                        Start();
                        
                        PlayerMove();

                        
                    }
                    else
                    {
                        Console.WriteLine("");
                    }

                }

                if (CallerName != "PlayerMove") Console. WriteLine("{0}, {1}", Location.posX, Location.posY); //스스로에게 호출되면 좌표를 출력하지 않음.

            }
        }

        static bool Check()
        {
            ConsoleKeyInfo resault = Console.ReadKey();

            if (resault.Key == ConsoleKey.N || resault.Key == ConsoleKey.Escape) return false;
            else return true;
        }

        static void Start()
        {
            Location.posX = 0;
            Location.posY = 0;
            bg.Create(rand.Next(30), rand.Next(30));
            Portal.CreatePortal(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));
        }

    }
}
