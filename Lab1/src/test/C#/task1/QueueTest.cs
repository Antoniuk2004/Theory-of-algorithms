using System;

namespace Task1
{
    class Program
    {
        public static void Main()
        {
            Queue q = new Queue();
            q.Create();
            q.Add("hello");
            q.Add(123);
            q.Add(11.5);
            q.Add('p');
            q.Add(Math.PI);
            q.Add(Math.PI);
            Console.WriteLine("Size: " + q.GetSize());//Size: 6
            Console.WriteLine("Value: " + q.Poll());//Value: hello
            Console.WriteLine("Value: " + q.Poll());//Value: 123
            Console.WriteLine("Value: " + q.Poll());//Value: 11.5
            Console.WriteLine("Value: " + q.Poll());//Value: p
            Console.WriteLine("Value: " + q.Poll());//Value: 3.141592653589793
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
        public object Poll()
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