using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        public static void Main()
        {
            char[][] matrix = CreateMatrix();
            Points start = FindPoints(matrix, 'S');
            Points finish = FindPoints(matrix, 'F');
            MatrixOutput(matrix);
            
        }

        public static Points FindPoints(char[][] matrix, char C)
        {
            Points value = new Points(0,0);
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if(matrix[i][j] == C)
                    {
                        value = new Points(i, j);
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

        /*public static void FindPath(char[][] matrix, Points start)
        {
            Queue q = new Queue();
            Points current = new Points(start.Y, start.X);
            q.Add(current);

            bool isReached = false;
            int avaliable = 0;

            while (!isReached)
            {
                current
                if ((avaliable = IsAvailable(matrix, new Points(current.Y, current.X))) == 0)
                {
                    q.Add(new Points(current.Y + 1));
                }
                else if(avaliable == 1)
                {

                }
            }
        }

        public static int IsAvailable(char[][] matrix, Points current)
        {
            if (matrix[current.Y][current.X] == '#') return -1;
            else if (matrix[current.Y][current.X] == '.') return 0;
            else return 1;
        }*/


    }







    public struct Points
    {
        public int X;
        public int Y;

        public Points(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    class Queue
    {
        public Points[] queue;

        public Points[] Create()
        {
            queue = new Points[0];
            return queue;
        }

        public Points[] Add(Points value)
        {
            Points[] newQueue = new Points[queue.Length + 1];
            for (int i = 0; i < queue.Length; i++)
            {
                newQueue[i] = queue[i];
            }
            newQueue[queue.Length] = value;
            queue = newQueue;
            return queue;
            
        }
        public Points Poll()
        {
            Points[] newQueue = new Points[queue.Length - 1];
            Points value = queue[0];
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