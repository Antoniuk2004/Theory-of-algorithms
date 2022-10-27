﻿using System;
using System.Collections;
using System.Text;

namespace Task1
{
    class Program
    {


        public static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            char[][] grid = GridInput();
            Point start = FindPoints(grid, 'S');
            List<List<Point>> listOfPaths = new List<List<Point>>();
            bool finish;
            List<Point> path = FindPath(grid, start, out listOfPaths);
            OutputSteps(listOfPaths, grid);
        }

        public static void OutputSteps(List<List<Point>> list, char[][] grid)
        {

            for (int i = 0; i < list.Count; i++)
            {
                char[][] temp = new char[grid.Length][];
                for (int p = 0; p < grid.Length; p++)
                {
                    temp[p] = new char[grid[0].Length];
                }
                for (int p = 0; p < grid.Length; p++)
                {
                    for (int l = 0; l < grid[p].Length; l++)
                    {
                        temp[p][l] = grid[p][l];
                    }
                }


                for (int j = 0; j < list[i].Count - 1; j++)
                {
                    temp[list[i][j].firstCoord][list[i][j].secondCoord] = grid[list[i][j].firstCoord][list[i][j].secondCoord];
                    temp[list[i][j].firstCoord][list[i][j].secondCoord] = '■';
                }

                temp[list[i][list[i].Count - 1].firstCoord][list[i][list[i].Count - 1].secondCoord] = '█';
                GridOutput(temp);
                Console.WriteLine();
            }
        }


        public static char[][] InputPathToGrid(char[][] grid, List<Point> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                grid[path[i].firstCoord][path[i].secondCoord] = 'x';
            }
            return grid;
        }

        public static Point FindPoints(char[][] grid, char C)
        {
            Point value = new Point(0, 0);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == C)
                    {
                        value.firstCoord = i;
                        value.secondCoord = j;
                    }
                }
            }
            return value;
        }

        public static void GridOutput(char[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }

        public static char[][] GridInput()
        {
            char[][] arr = new char[8][];
            arr[0] = "#.S.....".ToCharArray();
            arr[1] = "#.###.##".ToCharArray();
            arr[2] = new String('.', 8).ToCharArray();
            arr[3] = ".####.#.".ToCharArray();
            arr[4] = ".....#..".ToCharArray();
            arr[5] = "..#.F#..".ToCharArray();
            arr[6] = "..####..".ToCharArray();
            arr[7] = "#.......".ToCharArray();
            return arr;
        }


        public static List<Point> FindPath(char[][] grid, Point start, out List<List<Point>> listOfPaths)
        {

            Queue q = new Queue();
            q.Create();
            q.Add(start, new List<Point>());
            bool[][] available = CreateBoolArr(grid);
            List<Point> currentDirections = new List<Point>();
            listOfPaths = new List<List<Point>>();
            while (q.GetSize() > 0)
            {
                Data currentState = q.Pop();
                Point currentPoint = currentState.P;
                currentDirections = currentState.Directions;
                if (IsAvailable(grid, new Point(currentPoint.firstCoord + 1,
                    currentPoint.secondCoord), available))//down
                {
                    if (grid[currentPoint.firstCoord + 1][currentPoint.secondCoord] == 'F')
                    {
                        break;
                    }
                    available[currentPoint.firstCoord + 1][currentPoint.secondCoord] = false;
                    List<Point> listOfPath = new List<Point>();
                    for (int i = 0; i < currentDirections.Count; i++)
                    {
                        listOfPath.Add(currentDirections[i]);
                    }
                    listOfPath.Add(new Point(currentPoint.firstCoord + 1, currentPoint.secondCoord));
                    q.Add(new Point(currentPoint.firstCoord + 1, currentPoint.secondCoord), listOfPath);

                    listOfPaths.Add(listOfPath);
                }
                if (IsAvailable(grid, new Point(currentPoint.firstCoord - 1,
                    currentPoint.secondCoord), available))//up
                {
                    if (grid[currentPoint.firstCoord - 1][currentPoint.secondCoord] == 'F')
                    {
                        break;
                    }
                    available[currentPoint.firstCoord - 1][currentPoint.secondCoord] = false;
                    List<Point> listOfPath = new List<Point>();
                    for (int i = 0; i < currentDirections.Count; i++)
                    {
                        listOfPath.Add(currentDirections[i]);
                    }
                    listOfPath.Add(new Point(currentPoint.firstCoord - 1, currentPoint.secondCoord));
                    q.Add(new Point(currentPoint.firstCoord - 1, currentPoint.secondCoord), listOfPath);

                    listOfPaths.Add(listOfPath);
                }
                if (IsAvailable(grid, new Point(currentPoint.firstCoord, currentPoint.secondCoord + 1),
                    available))//right
                {
                    if (grid[currentPoint.firstCoord][currentPoint.secondCoord + 1] == 'F')
                    {
                        break;
                    }
                    available[currentPoint.firstCoord][currentPoint.secondCoord + 1] = false;
                    List<Point> listOfPath = new List<Point>();
                    for (int i = 0; i < currentDirections.Count; i++)
                    {
                        listOfPath.Add(currentDirections[i]);
                    }
                    listOfPath.Add(new Point(currentPoint.firstCoord, currentPoint.secondCoord + 1));
                    q.Add(new Point(currentPoint.firstCoord, currentPoint.secondCoord + 1), listOfPath);

                    listOfPaths.Add(listOfPath);
                }
                if (IsAvailable(grid, new Point(currentPoint.firstCoord, currentPoint.secondCoord - 1),
                    available))//left
                {
                    if (grid[currentPoint.firstCoord][currentPoint.secondCoord - 1] == 'F')
                    {
                        break;
                    }
                    available[currentPoint.firstCoord][currentPoint.secondCoord - 1] = false;
                    List<Point> listOfPath = new List<Point>();
                    for (int i = 0; i < currentDirections.Count; i++)
                    {
                        listOfPath.Add(currentDirections[i]);
                    }
                    listOfPath.Add(new Point(currentPoint.firstCoord, currentPoint.secondCoord - 1));
                    q.Add(new Point(currentPoint.firstCoord, currentPoint.secondCoord - 1), listOfPath);

                    listOfPaths.Add(listOfPath);
                }
            }
            return currentDirections;
        }

        static bool IsAvailable(char[][] grid, Point p, bool[][] available)
        {
            if (p.firstCoord >= 0
                && p.secondCoord >= 0
                && p.firstCoord < grid.Length
                && p.secondCoord < grid[0].Length)
            {
                if ((grid[p.firstCoord][p.secondCoord] == '.') && (available[p.firstCoord][p.secondCoord] == true))
                {
                    return true;
                }
                else if (grid[p.firstCoord][p.secondCoord] == 'F')
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        static bool[][] CreateBoolArr(char[][] grid)
        {
            bool[][] arr = new bool[grid.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new bool[grid[i].Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = true;
                }
            }
            return arr;
        }

    }

    public struct Point
    {
        public int firstCoord;
        public int secondCoord;

        public Point(int firstCoord, int secondCoord)
        {
            this.firstCoord = firstCoord;
            this.secondCoord = secondCoord;
        }
    }

    public class Data
    {
        public Point P { get; set; }
        public List<Point> Directions { get; set; }

        public Data(Point p, List<Point> list)
        {
            P = p;
            Directions = list;
        }
    }

    public class Queue
    {
        public Data[] queue;

        public Data[] Create()
        {
            queue = new Data[0];
            return queue;
        }

        public Data[] Add(Point point, List<Point> list)
        {
            Data[] newQueue = new Data[queue.Length + 1];
            for (int i = 0; i < queue.Length; i++)
            {
                newQueue[i] = queue[i];
            }
            newQueue[queue.Length] = new Data(point, list);
            queue = newQueue;
            return queue;

        }
        public Data Pop()
        {
            Data[] newQueue = new Data[queue.Length - 1];
            Data value = queue[0];
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