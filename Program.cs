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
        static Boolean isSkip = false;
        static Boolean isOnItem = false;

        static BG bg = new BG();

        public static Player Location = new Player(10);
        static Cursor Portal = new Cursor();

        static Enemy Enemy = new Enemy(5);

        static List<Item> items = new List<Item>();

        static List<int[]> itemPos = new List<int[]>();
 
        static ConsoleKeyInfo result;
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

            itemPos.Clear();

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
                    
                    isSkip = false;

                    if (x_Count_Pos - 1 == Location.posX && y_Count_Pos - 1 == Location.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Y");
                        Console.ForegroundColor = ConsoleColor.White;
                        
                        foreach (int[] pos in itemPos)
                        {
                            if (pos.Equals(new int[] { Location.posX, Location.posY }))
                                isOnItem = true;
                            else
                                isOnItem = false;
                        }
                        continue;
                    } //플레이어 위치 생성

                    if (x_Count_Portal - 1 == Portal.posX && y_Count_Portal - 1 == Portal.posY)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("P");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;


                    } //포탈 위치 생성

                    if (x_Count_Enemy - 1 == Enemy.posX && y_Count_Enemy - 1 == Enemy.posY && isBattleAble)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;

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

                    foreach (Item item in items)
                    {
                        if (x_Count_item - 1 == item.posX && y_Count_item - 1 == item.posY)
                        {
                            itemPos.Add(new int[] {item.posX, item.posY });
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("I");
                            Console.ForegroundColor = ConsoleColor.White;
                            isSkip = true;
                            continue;
                        }
                    }

                    if (Location.posX == Portal.posX && Location.posY == Portal.posY)
                    {
                        isOnPortal = true;
                    }
                    else isOnPortal = false;



                    if (!isSkip) Console.Write(bg_Text);
                }
                Console.WriteLine();
            }
        }

        public static void PlayerMove([CallerMemberNameAttribute] string CallerName = "") //이 함수를 호출한 메소드의 이름을 가져옴.
        {
            ConsoleKeyInfo resault = Console.ReadKey();

            if (resault != null && !isOnBattle && Location.HP > 0) //키보드에서 입력을 받았을 때 
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

                else if (isOnItem)
                {
                    Console.WriteLine("아이템 위에 있습니다.");
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

                foreach (int[] po in itemPos)
                {
                    Console.WriteLine(po);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("큭.... 체력이 다 떨어졌다...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Enter를 눌러 계속하십시요.");
                ConsoleKeyInfo result = Console.ReadKey();

                while (true)
                { 
                    result = Console.ReadKey();
                    if (result.Key == ConsoleKey.Enter)
                        Environment.Exit(0);
                }
            }
        }

        static void EnemyMove()
        {
            if (!isOnBattle)
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
            else
            {
                ConsoleKeyInfo result = Console.ReadKey();

                if (result != null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    printEnemy();
                    Console.WriteLine("==============================================\n" +
                        "큭...적에게 공격당해 {0}의 데미지를 입었다!", Enemy.Atk);
                    Console.ForegroundColor = ConsoleColor.White;
                    Location.Damaged(Enemy);
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
            int i = 0;

            items.Clear();

            Location.posX = 0;
            Location.posY = 0;
            bg.Create(rand.Next(1, 30), rand.Next(1, 30));
            Portal.Create(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));

            Enemy.Create(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));
            Enemy.HP = Enemy.Health;

            isBattleAble = true;
            int itemCnt = rand.Next(1, 4);
            while (i < itemCnt)
            {
                Item item = new Item();

                do
                {
                    item.Create(rand.Next(bg.Line.Count - 1), rand.Next(bg.Frame.Count - 1));
                } while (Portal.posX != item.posX && Portal.posY != item.posY);
                items.Add(item);
                i++;
            }

            itemPos.Clear();

            score++;
        }

        static void onBattle()
        {
            result = Console.ReadKey();
            
            Console.Clear();
            printEnemy();
            Console.WriteLine("앗! 적이 싸움을 걸어왔다");
 
            while (true)
            {
                if (result != null)
                {
                    result = Console.ReadKey();

                    if (result.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        printEnemy();
                        Enemy.printStatus();
                        printDialog();
                        Location.printStatus();
                    }
                    else continue;

                    result = Console.ReadKey();
                    switch (result.Key)
                    {
                        case ConsoleKey.D1:
                            
                            Console.Clear();
                            printEnemy();
                            if (Location.HP <= 0)
                            {
                                ExitBattle();
                                break;
                            }
                            Console.WriteLine("==============================================\n" +
                                "적에게 공격하여 {0}의 피해를 입혔다!",Location.Atk);
                            Enemy.Damaged(Location);
                            
                            if (Enemy.HP <= 0) ExitBattle();
                            
                            //ExitBattle();

                            else EnemyMove();
                            break;
                        case ConsoleKey.D2:
                            break;
                        default:
                            break;
                    }

                    if (Location.HP <= 0) break;

                    if (Enemy.HP <= 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
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
