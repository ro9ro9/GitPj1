namespace ts2
{
    internal class Program
    {
        struct Position
        {
            public int x;
            public int y;
        }

        static void Main(string[] args)
        {
            bool gameOver = false;
            bool gameOver2 = false;
            Position playerPos;
            char[,] map;
            Start(out playerPos, out map);
            while (gameOver == false)
            {
                Render(playerPos, map);
                ConsoleKey key = Input();
                Update(key, ref playerPos, map, ref gameOver);
            }
            while (gameOver2 == false)
            {
                Render(playerPos, map);
                ConsoleKey key = Input();
                Update2(key, ref playerPos, map, ref gameOver2);
            }
            End();
        }

        static void Start(out Position playerPos, out char[,] map)
        {
            // 게임 설정
            Console.CursorVisible = false;
            // 플레이어 위치 설정
            playerPos.x = 4;
            playerPos.y = 4;
            // 맵 설정
            map = new char[8, 8]
            {
            { '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒' },
            { '▒', ' ', ' ', ' ', ' ', ' ', ' ', '▒' },
            { '▒', ' ', '■', ' ', ' ', '■', ' ', '▒' },
            { '▒', '□', ' ', ' ', ' ', ' ', '◇', '▒' },
            { '▒', '□', ' ', ' ', ' ', ' ', '◇', '▒' },
            { '▒', ' ', '■', ' ', ' ', '■', ' ', '▒' },
            { '▒', ' ', ' ', ' ', ' ', ' ', ' ', '▒' },
            { '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒' },
            };
        }

        static void Render(Position playerPos, char[,] map)
        {
            Console.SetCursorPosition(0, 0);
            PrintMap(map);
            PrintPlayer(playerPos);
        }

        static void PrintMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }
        }

        static void PrintPlayer(Position playerPos)
        {
            Console.SetCursorPosition(playerPos.x, playerPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('⊙');
            Console.ResetColor();
        }

        static ConsoleKey Input()
        {
            return Console.ReadKey(true).Key;
        }

        static void Update(ConsoleKey key, ref Position playerPos, char[,] map, ref bool gameOver)
        {
            Move(key, ref playerPos, map);
            bool isClear = IsClear(map);

            if (isClear)
            {
                gameOver = true;
            }

        }
        static void Update2(ConsoleKey key, ref Position playerPos, char[,] map, ref bool gameOver2)
        {
            Move(key, ref playerPos, map);
            bool isClear2 = IsClear2(map);

            if (isClear2)
            {
                gameOver2 = true;
            }
        }

        static void Move(ConsoleKey key, ref Position playerPos, char[,] map)
        {
            Position targetPos;
            Position overPos;
            switch (key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    targetPos.x = playerPos.x - 1;
                    targetPos.y = playerPos.y;
                    overPos.x = playerPos.x - 2;
                    overPos.y = playerPos.y;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    targetPos.x = playerPos.x + 1;
                    targetPos.y = playerPos.y;
                    overPos.x = playerPos.x + 2;
                    overPos.y = playerPos.y;
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    targetPos.x = playerPos.x;
                    targetPos.y = playerPos.y - 1;
                    overPos.x = playerPos.x;
                    overPos.y = playerPos.y - 2;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    targetPos.x = playerPos.x;
                    targetPos.y = playerPos.y + 1;
                    overPos.x = playerPos.x;
                    overPos.y = playerPos.y + 2;
                    break;
                default:
                    return;
            }
            // 승리 //
            if (map[targetPos.y, targetPos.x] == '■')
            {
                if (map[overPos.y, overPos.x] == '□')
                {
                    map[overPos.y, overPos.x] = '▣';
                    map[targetPos.y, targetPos.x] = ' ';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
                else if (map[overPos.y, overPos.x] == ' ')
                {
                    map[overPos.y, overPos.x] = '■';
                    map[targetPos.y, targetPos.x] = ' ';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
            }
            else if (map[targetPos.y, targetPos.x] == '□')
            {
                playerPos.x = targetPos.x;
                playerPos.y = targetPos.y;
            }

            else if (map[targetPos.y, targetPos.x] == '▣')
            {

                if (map[overPos.y, overPos.x] == '□')
                {
                    map[overPos.y, overPos.x] = '▣';
                    map[targetPos.y, targetPos.x] = '□';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }

                else if (map[overPos.y, overPos.x] == ' ')
                {
                    map[overPos.y, overPos.x] = '■';
                    map[targetPos.y, targetPos.x] = '□';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
            }
            else if (map[targetPos.y, targetPos.x] == ' ')
            {
                playerPos.x = targetPos.x;
                playerPos.y = targetPos.y;
            }
            else if (map[targetPos.y, targetPos.x] == '▒')
            {

            }

            // 패배 //
            if (map[targetPos.y, targetPos.x] == '■')
            {
                if (map[overPos.y, overPos.x] == '◇')
                {
                    map[overPos.y, overPos.x] = '◆';
                    map[targetPos.y, targetPos.x] = ' ';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
                else if (map[overPos.y, overPos.x] == ' ')
                {
                    map[overPos.y, overPos.x] = '■';
                    map[targetPos.y, targetPos.x] = ' ';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
            }
            else if (map[targetPos.y, targetPos.x] == '◇')
            {
                playerPos.x = targetPos.x;
                playerPos.y = targetPos.y;
            }

            else if (map[targetPos.y, targetPos.x] == '◆')
            {

                if (map[overPos.y, overPos.x] == '◇')
                {
                    map[overPos.y, overPos.x] = '◆';
                    map[targetPos.y, targetPos.x] = '◇';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }

                else if (map[overPos.y, overPos.x] == ' ')
                {
                    map[overPos.y, overPos.x] = '■';
                    map[targetPos.y, targetPos.x] = '◇';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
            }
            else if (map[targetPos.y, targetPos.x] == ' ')
            {
                playerPos.x = targetPos.x;
                playerPos.y = targetPos.y;
            }
            else if (map[targetPos.y, targetPos.x] == '▒')
            {

            }

        }

        static bool IsClear(char[,] map)
        {
            int goalCount = 0;
            foreach (char tile in map)
            {
                if (tile == '□')
                {
                    goalCount++;
                    break;
                }

            }
            if (goalCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool IsClear2(char[,] map)
        {
            int goalCount2 = 0;
            foreach (char tile in map)
            {
                if (tile == '◇')
                {
                    goalCount2++;
                    break;
                }
            }
            if (goalCount2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static void End()
        {
            Console.Clear();
            Console.WriteLine("클리어 했습니다!");
     

        }
    }
}
