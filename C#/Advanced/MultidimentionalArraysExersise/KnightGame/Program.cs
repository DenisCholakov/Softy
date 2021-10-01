using System;
using System.Reflection.Metadata;

namespace KnightGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            char[,] board = new char[size, size];
            int maxAttacks = 0;
            int attackerCol = 0;
            int attackerRow = 0;
            int knightsRemoved = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                char[] boardLine = Console.ReadLine().ToCharArray();

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = boardLine[j];
                }
            }

            do
            {
                maxAttacks = 0;

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == 'K')
                        {
                            int attacks = FindAttackedCount(i, j, board);

                            if (attacks > maxAttacks)
                            {
                                maxAttacks = attacks;
                                attackerRow = i;
                                attackerCol = j;
                            }
                        }   
                    }
                }

                if (maxAttacks > 0)
                {
                    board[attackerRow, attackerCol] = '0';
                    knightsRemoved++;
                }

            } while (maxAttacks > 0);

            Console.WriteLine(knightsRemoved);
        }

        public static int FindAttackedCount(int row, int col, char[,] board)
        {
            int attackedCount = 0;

            if (IsAttacked(row - 2, col - 1, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row - 2, col + 1, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row - 1, col + 2, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row + 1, col  + 2, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row + 2, col - 1, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row + 2, col + 1, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row - 1, col - 2, board))
            {
                attackedCount++;
            }

            if (IsAttacked(row + 1, col - 2, board))
            {
                attackedCount++;
            }

            return attackedCount;
        }

        public static bool IsAttacked(int row, int col, char[,] board)
        {
            if (row >= 0 && row < board.GetLength(0) 
                    && col >= 0 && col < board.GetLength(1))
            {
                return board[row, col] == 'K';
            }

            return false;
        }
    }
}
