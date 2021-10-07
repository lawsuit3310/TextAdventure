using System;
using System.Collections;
using System.Threading;

namespace TextAdventure.src
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKeyInfo resault = Console.ReadKey();

                if (resault != null) //키보드에서 입력을 받았을 때
                    Console.WriteLine(resault.Key);
            }

        }

        public static void UPDStatus() //키보드에서 입력을 받아 위치 초기화
        {
            Console.Clear();

            DrawBG();
        }

        public static void DrawBG()
        {
            BG bg = new BG();

            bg.Create(16, 8);

            foreach (Queue bg_Line in bg.Frame)
            {
                foreach (string bg_Text in bg_Line)
                {
                    Console.Write(bg_Text);
                }
                Console.WriteLine();
            }
        }

        public static void PlayerMove()
        {
            Cursor Location = new Cursor();

            
        }
    }
}
