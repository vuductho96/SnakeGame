using System;

namespace SnakeGame
{
    class Program
    {
        const int ScreenWidth = 60;
        const int ScreenHeight = 20;

        static void Main(string[] args)
        {
            var game = new Game(ScreenWidth, ScreenHeight);
            game.Run();
        }
    }

    class Game
    {

        int[] snakeX = { 10, 10  };
        int[] snakeY = { 5, 6  };
        int foodX;
        int foodY;
        int score;
        int dx = 1;
        int dy = 1;
        bool isOver;
        readonly Random _rand = new Random();
         
    
        readonly int screenWidth;
        readonly int screenHeight;

        public Game(int screenWidth, int screenHeight)
        {
           this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            foodX = _rand.Next(1, screenWidth - 1);
            foodY = _rand.Next(1, screenHeight - 1);
            
        }
        void IncrementScore()
        {
            score++;
            Console.SetCursorPosition(0, Console.WindowHeight-1);
            Console.WriteLine("Score: " + score);
        }
        void DrawSnake()
        {
            for (int i = 0; i < snakeX.Length; i++)
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                Console.Write("0");
            }
        }
        public void Run()
        {
            Console.WriteLine("Press any key to play game");
            while (!isOver)
            {
                if (snakeX[0] == foodX && snakeY[0] == foodY)
                {
                    score++;
                    GenerateFood();

                }
               
                Direction();
                MoveSnake();
                CheckCollisions();
                DrawBorder();
                DrawSnake();
                DrawFood();
                DrawSnake();
              
            }

            Console.Clear();

        }
        void DrawFood()
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.Write("@");
        }

        void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < screenWidth; i++)
            {
                Console.Write("#");
            }

            for (int i = 1; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("#");
            }

            Console.SetCursorPosition(0, screenHeight);
            for (int i = 0; i < screenWidth; i++)
            {
                Console.Write("#");
            }
        }

        void Direction()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    dx = -1;
                    dy = 0;
                    break;
                case ConsoleKey.RightArrow:
                    dx = 1;
                    dy = 0;
                    break;
                case ConsoleKey.UpArrow:
                    dx = 0;
                    dy = -1;
                    break;
                case ConsoleKey.DownArrow:
                    dx = 0;
                    dy = 1;
                    break;
            }
        }
        void MoveSnake()
        {
            Console.SetCursorPosition(snakeX[snakeX.Length - 1], snakeY[snakeY.Length - 1]);
            Console.Write(" ");
            // Move the body of the snake
            for (int i = snakeX.Length - 1; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }

            // Move the head of the snake in the current direction
            snakeX[0] += dx;
            snakeY[0] += dy;

            // Wrap the snake around the screen
            if (snakeX[0] < 0) snakeX[0] = screenWidth - 1;
            if (snakeX[0] >= screenWidth) snakeX[0] = 0;
            if (snakeY[0] < 0) snakeY[0] = screenHeight - 1;
            if (snakeY[0] >= screenHeight) snakeY[0] = 0;
        }
        void CheckCollisions()
        {
            // Check collision with food
            if (snakeX[0] == foodX && snakeY[0] == foodY)
            {
                score++;

                // Add new segment to the snake's body
                Array.Resize(ref snakeX, snakeX.Length + 1);
                Array.Resize(ref snakeY, snakeY.Length + 1);

                // Set the position of the new segment to the last segment of the snake's body
                snakeX[snakeX.Length - 1] = snakeX[snakeX.Length - 2];
                snakeY[snakeY.Length - 1] = snakeY[snakeY.Length - 2];

                GenerateFood();
                IncrementScore();
            }

            // Check collision with walls
            if (snakeX[0] == 0 || snakeX[0] == screenWidth - 1 || snakeY[0] == 0 || snakeY[0] == screenHeight)
            {
                isOver = true;
            }

            // Check collision with body
            for (int i = 1; i < snakeX.Length; i++)
            {
                if (snakeX[i] == snakeX[0] && snakeY[i] == snakeY[0])
                {
                    isOver = true;
                }
            }
        }

        void GenerateFood()
        {
            foodX = _rand.Next(1, screenWidth - 1);
            foodY = _rand.Next(1, screenHeight - 1);

           
            for (int i = 0; i < snakeX.Length; i++)
            {
                if (snakeX[i] == foodX && snakeY[i] == foodY)
                {
                   
                    GenerateFood();
                    return;
                }
            }

            DrawFood();
        }
      
    }
}