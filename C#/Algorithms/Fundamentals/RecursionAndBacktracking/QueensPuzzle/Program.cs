using System;
using System.Collections.Generic;

namespace QueensPuzzle
{
    class Program
    {
        private static bool[,] board = new bool[8, 8];
        private static HashSet<int> attackedRows = new HashSet<int>();
        private static HashSet<int> attackedCols = new HashSet<int>();
        private static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        private static HashSet<int> attackedRightDiagonals = new HashSet<int>();

        static void Main(string[] args)
        {
            PrintBoardSolutions(0);
        }

        private static void PrintBoardSolutions(int row)
        {
            if (row >= board.GetLength(0))
            {
                PrintSolution();
                return;
            }

            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (!IsAttacked(row, col))
                {
                    InsertQueen(row, col);
                    PrintBoardSolutions(row + 1);
                    RemoveQueen(row, col);
                }
            }
        }

        private static void InsertQueen(int row, int col)
        {
            attackedRows.Add(row);
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(row + col);
            attackedRightDiagonals.Add(row - col);

            board[row, col] = true;
        }

        private static void RemoveQueen(int row, int col)
        {
            attackedRows.Remove(row);
            attackedCols.Remove(col);
            attackedLeftDiagonals.Remove(row + col);
            attackedRightDiagonals.Remove(row - col);

            board[row, col] = false;
        }

        private static bool IsAttacked(int row, int col)
        {
            return attackedRows.Contains(row) ||
                    attackedCols.Contains(col) ||
                    attackedLeftDiagonals.Contains(row + col) ||
                    attackedRightDiagonals.Contains(row - col);
        }

        private static void PrintSolution()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    char sign = board[row, col] ? '*' : '-';
                    Console.Write(sign + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
