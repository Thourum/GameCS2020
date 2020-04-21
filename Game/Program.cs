using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using GameLib.Logic;
using GameLib.Model;
using GameLib.State;

namespace FallingRocks
{
    class FallingRocks
    {
        const int WindowWidth = 100;
        const int WindowHeight = 50;
        const int GameMenuWidth = 40;
        const int MaximumClusterSize = 3;
        const int MaximumNumberOfRocks = 64; //Approximate number of maximum rocks currently in the game.
        const int CostOfLife = 10_000;

        private static bool exitGame = false;

        const double MaximumGameSpeed = 120d;
        const double Acceleration = 0.3d;
        static double gameSpeed = 0d;

        private static List<Rock> rocks = new List<Rock>();
        
        static Random random = new Random();
        static int NoTimesPlayerDied = 0;


        private static void InitializeConsoleSettings()
        {
            Console.BufferHeight = Console.WindowHeight = WindowHeight;
            Console.BufferWidth = Console.WindowWidth = WindowWidth;
            Console.CursorVisible = false;
        }

        private static void InitializeGame()
        {
            Game game = GameSingleton.Instance;
            game.World = new World(WindowHeight, WindowWidth - GameMenuWidth);
            game.Player = new Player();
            game.Player.Position = new Point(game.World.SizeX - 1, game.World.SizeY / 2);
            game.Speed = 0d;
            game.Player.Score = 0;
        }

        private static void GetUserInput()
        {
            var g = GameSingleton.Instance;
            var playerPosition = g.Player.Position;
            var world = g.World;
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                switch (pressedKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (playerPosition.Y - 1 > 0)
                        {
                            playerPosition.Y--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerPosition.Y + 1 < world.SizeY - 1)
                        {
                            playerPosition.Y++;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerPosition.X + 1 < world.SizeX - 1)
                        {
                            playerPosition.X++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (playerPosition.X - 1 > 1)
                        {
                            playerPosition.X--;
                        }
                        break;
                    default:
                        break;
                }
                DetermineCollisions();
            }
        }

        private static void MoveRocks()
        {
            List<int> rocksToRemove = new List<int>();
            for (int i = 0; i < rocks.Count; ++i)
            {
                rocks[i].MoveDown();

                if (rocks[i].Visible == false)
                {
                    rocksToRemove.Add(i);
                }
            }

            // Add score to the player, Could add score based on the size of ther rock
            // TODO: implement diferent rock sizes
            GameSingleton.Instance.Player.Score += (int)Math.Floor((gameSpeed * rocksToRemove.Count) * 0.5);

            gameSpeed += rocksToRemove.Count * Acceleration;
            gameSpeed = Math.Min(gameSpeed, MaximumGameSpeed);

            foreach (int rockIndex in rocksToRemove)
            {
                rocks.RemoveAt(rockIndex);

            }
        }

        private static void GenerateNewRocks()
        {
            // Max rock guard
            if (rocks.Count() >= MaximumNumberOfRocks) return;

            //75% change of generating new rock
            if (random.Next(0, 4) != 0)
            {
                //33.3% chance of generating cluster.
                if (random.Next(0, 3) == 0)
                {
                    Rock newRock = new Rock();
                    int clusterSize = random.Next(0, Math.Min(newRock.Position.X, MaximumClusterSize)) + 1;

                    if (clusterSize >= 2)
                    {
                        rocks.Add(newRock);
                        for (int i = 1; i < clusterSize; ++i)
                        {
                            rocks.Add(new Rock(newRock.Position.X - i, newRock.Symbol, newRock.Color));
                        }
                    }
                }
                else
                {
                    rocks.Add(new Rock()); //generate single rock.
                }
            }
        }

        private static void DetermineCollisions()
        {
            var player = GameSingleton.Instance.Player;
            foreach (Rock rock in rocks)
            {
                if (rock.Position == player.Position)
                {
                    if (GameSingleton.Instance.Player.Score > 10000)
                    {
                        GameSingleton.Instance.Player.Score -= CostOfLife;
                        NoTimesPlayerDied++;
                        player.Status = UnitStatus.Hit;
                    } 
                    else
                    {
                        player.Status = UnitStatus.Dead;
                    }
                    
                    rocks.Clear();
                    break;
                }
            }
        }

        private static void DrawGameMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < WindowHeight; i++)
            {
                Console.SetCursorPosition(GameSingleton.Instance.World.SizeY + 1, i);
                Console.Write("|");
            }
            Console.SetCursorPosition(WindowWidth - GameMenuWidth + 10, (WindowHeight / 2) - 6);
            Console.Write("Difficulty: " + (int)((gameSpeed / 10d) + 1));

            Console.SetCursorPosition(WindowWidth - GameMenuWidth + 10, (WindowHeight / 2) - 3);
            Console.Write("Your score: " + GameSingleton.Instance.Player.Score + (NoTimesPlayerDied * CostOfLife));

            Console.SetCursorPosition(WindowWidth - GameMenuWidth + 10, (WindowHeight / 2));
            Console.Write("Extra lives: " + GameSingleton.Instance.Player.Score / CostOfLife);
        }

        private static void PaintGameOver()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(WindowWidth / 3, WindowHeight / 2);
            Console.Write(" _____                  _____");
            Console.SetCursorPosition(WindowWidth / 3, (WindowHeight / 2) + 1);
            Console.Write("|   __|___ _____ ___   |     |_ _ ___ ___ ");
            Console.SetCursorPosition(WindowWidth / 3, (WindowHeight / 2) + 2);
            Console.Write("|  |  | .'|     | -_|  |  |  | | | -_|  _|");
            Console.SetCursorPosition(WindowWidth / 3, (WindowHeight / 2) + 3);
            Console.Write("|_____|__,|_|_|_|___|  |_____|\\_/|___|_| ");


            Console.SetCursorPosition((WindowWidth / 2) - 4, WindowHeight - (WindowHeight / 3));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Your Score:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((WindowWidth / 2), WindowHeight - (WindowHeight / 3) + 1);
            Console.Write(GameSingleton.Instance.Player.Score);
            Console.ReadKey();
            exitGame = true;
        }

        private static void Repaint()
        {
            var player = GameSingleton.Instance.Player;
            if (player.Status != UnitStatus.Dead)
            {
                Console.Clear();

                DrawGameMenu();

                DrawPlayer(player);

                for (int i = 0; i < rocks.Count; ++i)
                {
                    rocks[i].Draw();
                }

                if (player.Status == UnitStatus.Hit)
                {
                    player.Status = UnitStatus.Alive;
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep((int)(125 - gameSpeed));
                }
            }
            else
            {
                PaintGameOver();
            }
        }

        private static void DrawPlayer(Player p)
        {
            if (p.Status == UnitStatus.Hit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(p.Position.Y -1, p.Position.X);
                Console.Write("xXx");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(p.Position.Y -1, p.Position.X);
                Console.Write("<0>");
            }
        }

        static void Main(string[] args)
        {
            InitializeConsoleSettings();
            InitializeGame();

            while (true)
            {
                GetUserInput();
                MoveRocks();
                GenerateNewRocks();
                DetermineCollisions();
                Repaint();

                if (exitGame)
                {
                    break;
                }
            }
        }
    }
}