using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class GameEngine
    {
        bool isStarted = false;
        List<IDrawable> gameItems = new List<IDrawable>();

        public GameEngine()
        {
            Snake = new Snake(new Position(30, 20));
            gameItems.Add(Snake);
        }

        public Snake Snake { get; set; }

        public void Start()
        {
            isStarted = true;
            Position movement = new Position(0, 0);
            while (isStarted == true)
            {
                Snake.Move(movement);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(false).Key;
                    movement = ReadUserInput.GetMovement(key);
                    
                }
                Thread.Sleep(10);
                Console.Clear();
                gameItems.ForEach(i => i.Draw());
            }

        }

        public void Stop()
        {
            isStarted = false;
        }
    }
}
