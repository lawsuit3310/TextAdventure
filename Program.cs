using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TextAdventure.src
{
    class Program
    {
        #region 변수
        static int x_Count_Pos = 1;
        static int y_Count_Pos = 1;

        static int x_Count_Portal = 1;
        static int y_Count_Portal = 1;

        static int x_Count_Enemy = 1;
        static int y_Count_Enemy = 1;

        static int x_Count_item = 1;
        static int y_Count_item = 1;

        static int score = 0;



        static Boolean isOnPortal = false;
        static Boolean isFacingEnemy = false;
        static Boolean isOnBattle = false;
        static Boolean isBattleAble = false;

        static BG bg = new BG();

        public static Cursor Location = new Cursor(10);
        static Cursor Portal = new Cursor();

        static Enemy Enemy = new Enemy(10);

        static Item item = new Item();

        static Random rand = new Random();

        #endregion 
        public static void Main(string[] args)
        {
            Console.WriteLine("엔터키를 누르면 시작합니다.");
            Start();
            score = 0;
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
            Console.WriteLine("점수 : {0}", score);

            do
            {
                x_Count_Pos = 0;
                y_Count_Pos = 0;

                x_Count_Portal = 0;
                y_Count_Portal = 0;

                x_Count_Enemy = 0;
                y_Count_Enemy = 0;

                x_Count_item = 0;
                y_Count_item = 0;
            } while (false);

            foreach (List<string> bg_Line in bg.Frame)
            {
                x_Count_Pos = 0;
                x_Count_Portal = 0;
                x_Count_Enemy = 0;
                x_Count_item = 0;

                ++y_Count_Portal;
                ++y_Count_Pos;
                ++y_Count_Enemy;
                ++y_Count_item;

                foreach (string bg_Text in bg_Line)
                {
                    ++x_Count_Enemy;
                    ++x_Count_Pos;
                    ++x_Count_Portal;
                    ++x_Count_item;

                    if (x_Count_Pos - 1 == Location.posX && y_Count_Pos - 1 == Location.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Y");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    } //플레이어 위치 생성

                    if (x_Count_Portal - 1 == Portal.posX && y_Count_Portal - 1 == Portal.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("P");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    } //포탈 위치 생성

                    if (x_Count_Enemy - 1 == Enemy.posX && y_Count_Enemy - 1 == Enemy.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (Location.posX == Portal.posX && Location.posY == Portal.posY)
                        {
                            isOnPortal = true;
                        }
                        else isOnPortal = false;

                        if (
                            (Math.Abs(Location.posX - Enemy.posX) == 1 || Location.posX == Enemy.posX) &&
                            (Math.Abs(Location.posY - Enemy.posY) == 1 || Location.posY == Enemy.posY)
                            )
                        {
                            isFacingEnemy = true;
                        }
                        else isFacingEnemy = false;
                        continue;
                    } //적 생성

                    if (x_Count_item - 1 == item.posX && y_Count_item - 1 == item.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("I");
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

            if (resault != null && !isOnBattle) //키보드에서 입력을 받았을 때 
            {
                EnemyMove();

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

                if (Location.posX < 0) Location.posX = 0;
                if (Location.posY < 0) Location.posY = 0;
                if (Location.posX >= bg.Line.Count)
                {
                    Location.posX = bg.Line.Count - 1;
                }
                if (Location.posY >= bg.Frame.Count) Location.posY = bg.Frame.Count - 1; //커서가 배경 밖으로 빠져나올경우 위치 고정

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

                if (isFacingEnemy && isBattleAble)
                {
                    Console.WriteLine("앗! 적과 마주했다!");
                    isOnBattle = true;
                    onBattle();

                }

                if (CallerName != "PlayerMove" && !isOnBattle) //스스로에게 호출되거나 전투 중 일경우 좌표를 출력하지 않음.
                {
                    Console.WriteLine("{0}, {1}", Location.posX, Location.posY);
                    Console.WriteLine("{0}, {1}", Enemy.posX, Enemy.posY);
                }
            }
        }

        static void EnemyMove()
        {
            if (Enemy.moveChance < 0)
            {
                Enemy.moveChance++;
                return;
            }
            else
            {
                Enemy.moveChance = 0;
                if (Math.Abs(Enemy.posX - Location.posX) > Math.Abs(Enemy.posY - Location.posY)) //y방향으로 더 가까울때
                {
                    if (Math.Abs(Enemy.posX) > Math.Abs(Location.posX))
                    {
                        Enemy.posX--;
                    }
                    else
                    {
                        Enemy.posX++;
                    }
                }
                else if (Math.Abs(Enemy.posX - Location.posX) < Math.Abs(Enemy.posY - Location.posY)) //x방향으로 더 가까울때
                {
                    if (Math.Abs(Enemy.posY) > Math.Abs(Location.posY))
                    {
                        Enemy.posY--;
                    }
                    else
                    {
                        Enemy.posY++;
                    }
                }
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
            bg.Create(rand.Next(1, 30), rand.Next(1, 30));
            Portal.Create(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));

            Enemy.Create(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));

            isBattleAble = true;

            do
            {
                item.Create(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));
            } while (Portal.posX != item.posX && Portal.posY != item.posY);

            score++;
        }

        static void onBattle()
        {
            ConsoleKeyInfo result = Console.ReadKey();
            while (true)
            {
                if (result != null)
                {
                    Console.Clear();

                    printEnemy();
                    printDialog();
                    Enemy.printStatus();

                    result = Console.ReadKey();


                    switch (result.Key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            printEnemy();
                            Enemy.Damaged(Location);
                            
                            
                            break;
                        case ConsoleKey.D2:
                            break;
                        default:
                            break;
                    }
                    
                    if (Enemy.HP <= 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        printEnemy();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("전투에서 승리했다!");
                        ExitBattle();
                        break;
                    }


                }
            }
        }

        static void printEnemy()
        {
            foreach (string Line in Enemy.Image)
            {
                Console.WriteLine(Line);
            }

        }

        static void printDialog()
        {
            Console.WriteLine("=========적이 싸움을 걸어왔다!==========");
            Console.WriteLine("\n무엇을 할까?");

            Console.WriteLine("1.싸운다 \t\t 2. 아이템");
        }

        static void ExitBattle()
        {
            isBattleAble = false;
            isOnBattle = false;
        }


    }
}
