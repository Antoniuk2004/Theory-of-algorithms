using System;

namespace Task1
{
    class Program
    {
        public static void Main()
        {
            Queue q = new Queue();
            q.Create();
            q.Add(10);
            q.Add(11);
            int value = q.Poll();
            Console.WriteLine(value);
            Console.WriteLine(q.GetSize());
        }
    }
    class Queue
    {
        public int[] queue;

        public int[] Create()
        {
            queue = new int[0];
            return queue;
        }

        public int[] Add(int value)
        {
            int[] newQueue = new int[queue.Length + 1];
            for (int i = 0; i < queue.Length; i++)
            {
                newQueue[i] = queue[i];
            }
            newQueue[queue.Length] = value;
            queue = newQueue;
            return queue;

        }
        public int Poll()
        {
            int[] newQueue = new int[queue.Length - 1];
            int value = queue[0];
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