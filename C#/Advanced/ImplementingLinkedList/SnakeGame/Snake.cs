using ImplementingLinkedList;
using System;
using System.Text;

namespace SnakeGame
{
    public class Snake : IDrawable
    {
        public Snake(Position head)
        {
            this.SnakeBody = new LinkedList<Position>();
            this.SnakeBody.AddHead(new Node<Position>(head));

            for (int i = 1; i < 4; i++)
            {
                SnakeBody.AddLast(new Node<Position>(new Position(this.SnakeBody.Head.Value.X, this.SnakeBody.Head.Value.Y + i)));
            }
        }

        public LinkedList<Position> SnakeBody { get; set; }

        public void Draw()
        {
            SnakeBody.ForEach(n =>
            {
                Console.SetCursorPosition(n.Value.X, n.Value.Y);
                Console.Write("*");
            });
            
        }

        public void Move(Position position)
        {
            if (position.X == 0 && position.Y == 0)
            {
                return;
            }

            this.SnakeBody.Head.Value.ChangePosition(position);

            SnakeBody.ForEach(n =>
            {
               if (n.Previous != null)
               {
                   n.Value.X = n.Previous.Value.X;
                   n.Value.Y = n.Previous.Value.Y;
               }
            });
            
        }
    }
}
