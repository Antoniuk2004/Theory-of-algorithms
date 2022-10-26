using System;

namespace Task1
{
    class Program
    {
        public static void Main()
        {
        }
    }
    class Queue
    {
        public object[] queue;

        public object[] Create()
        {
            queue = new object[0];
            return queue;
        }

        public object[] Add(object value)
        {
            object[] newQueue = new object[queue.Length + 1];
            for (int i = 0; i < queue.Length; i++)
            {
                newQueue[i] = queue[i];
            }
            newQueue[queue.Length] = value;
            queue = newQueue;
            return queue;

        }
        public object Pop()
        {
            object[] newQueue = new object[queue.Length - 1];
            object value = queue[0];
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