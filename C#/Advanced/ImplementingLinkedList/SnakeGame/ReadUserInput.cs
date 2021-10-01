using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    public static class ReadUserInput
    {
        public static Position GetMovement(ConsoleKey key)
        {
            Position position = new Position(0, 0);

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    position =  new Position(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    position = new Position(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    position = new Position(-2, 0);
                    break;
                case ConsoleKey.RightArrow:
                    position = new Position(2, 0);
                    break;
            }

            return position;
        }
    }
}
