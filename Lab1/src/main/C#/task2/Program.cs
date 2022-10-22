using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        public static void Main()
        {
            char[][] matrix = CreateMatrix();
            int[] start = FindPoints(matrix, 'S');
            MatrixOutput(matrix);
            FindPath(matrix, start);
            MatrixOutput(matrix);
        }

        public static int[] FindPoints(char[][] matrix, char C)
        {
            int[] value = new int[2];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == C)
                    {
                        value = new int[] { i, j};
                    }
                }
            }
            return value;
        }

        public static void MatrixOutput(char[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j]);
                }
                Console.WriteLine();
            }
        }

        public static char[][] CreateMatrix()
        {
            char[][] arr = new char[8][];
            arr[0] = "#.S.....".ToCharArray();
            arr[1] = "##.#.###".ToCharArray();
            arr[2] = new String('.', 8).ToCharArray();
            arr[3] = ".####.#.".ToCharArray();
            arr[4] = ".....#..".ToCharArray();
            arr[5] = "..#.F#..".ToCharArray();
            arr[6] = "..####..".ToCharArray();
            arr[7] = "#.......".ToCharArray();
            return arr;
        }

        public static bool[][] FindPath(char[][] matrix, int[] start)
        {
            Queue q = new Queue();
            q.Create();
            int index = 0;
            q.Add(start[1], start[0], index);
            bool[][] available = CreateBoolArr(matrix);

            while (q.GetSize() > 0)
            {
                int[] currentPoint = q.Poll();
                try
                {
                    if (matrix[currentPoint[1] + 1][currentPoint[0] == '.' && available[currentPoint[1] + 1][currentPoint[0])
                    {
                        q.Add(currentPoint[1] + 1, currentPoint[0], index);
                        available[currentPoint[1] + 1][currentPoint[0]] = false;
                    }
                    else if (matrix[currentPoint[1] + 1][currentPoint[0]] == 'F')
                    {
                        break;
                    }
                }
                catch { }

                try
                {
                    if (matrix[currentPoint.Y - 1][currentPoint.X] == '.' && available[currentPoint.Y - 1][currentPoint.X])
                    {
                        q.Add(new Points(currentPoint.Y - 1, currentPoint.X));
                        available[currentPoint.Y - 1][currentPoint.X] = false;
                    }
                    else if (matrix[currentPoint.Y - 1][currentPoint.X] == 'F')
                    {
                        break;
                    }
                }
                catch { }

                try
                {
                    if (matrix[currentPoint.Y][currentPoint.X + 1] == '.' && available[currentPoint.Y][currentPoint.X + 1])
                    {
                        q.Add(new Points(currentPoint.Y, currentPoint.X + 1));
                        available[currentPoint.Y][currentPoint.X + 1] = false;
                    }
                    else if (matrix[currentPoint.Y][currentPoint.X + 1] == 'F')
                    {
                        break;
                    }
                }
                catch { }

                try
                {
                    if (matrix[currentPoint.Y][currentPoint.X - 1] == '.' && available[currentPoint.Y][currentPoint.X - 1])
                    {
                        q.Add(new Points(currentPoint.Y, currentPoint.X - 1));
                        available[currentPoint.Y][currentPoint.X - 1] = false;
                    }
                    else if (matrix[currentPoint.Y][currentPoint.X - 1] == 'F')
                    {
                        break;
                    }
                }
                catch { }
            }
            return available;
        }

        static bool[][] CreateBoolArr(char[][] matrix)
        {
            bool[][] arr = new bool[matrix.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new bool[matrix[i].Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = true;
                }
            }
            return arr;
        }
    }

    class Queue
    {
        public int[][] queue;

        public int[][] Create()
        {
            queue = new int[0][];
            return queue;
        }

        public int[][] Add(int x, int y, int index)
        {
            int[][] newQueue = new int[queue.Length + 1][];
            for (int i = 0; i < queue.Length; i++)
            {
                newQueue[i] = queue[i];
            }
            newQueue[queue.Length] = new int[3] { x, y, index };
            queue = newQueue;
            return queue;

        }
        public int[] Poll()
        {
            int[][] newQueue = new int[queue.Length - 1][];
            int[] value = queue[0];
            for (int i = 0; i < newQueue.Length; i++)
            {
                newQueue[i] = queue[i + 1];
            }
            queue = newQueue;
            return value;
        }
        public int GetSize()
        {
            return queue.Length;
        }
    }
}