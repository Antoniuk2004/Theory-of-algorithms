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
            int expectedValue1 = 5;
            string expectedValue2 = "hello";
            int expectedValue3 = 123;
            double expectedValue4 = 11.5;
            char expectedValue5 = 'p';
            double expectedValue6 = Math.PI;

            bool result = false;
            result = q.GetSize().Equals(expectedValue1);
            Console.WriteLine("Is first value equal to expected value(5): " + result);
            result = q.Pop().Equals(expectedValue2);
            Console.WriteLine("Is first second equal to expected value(\"hello\"): " + result);
            result = q.Pop().Equals(expectedValue3);
            Console.WriteLine("Is first third equal to expected value(123): " + result);
            result = q.Pop().Equals(expectedValue4);
            Console.WriteLine("Is first fourth equal to expected value(11.5): " + result);
            result = q.Pop().Equals(expectedValue5);
            Console.WriteLine("Is fifth value equal to expected value('p'): " + result);
            result = q.Pop().Equals(expectedValue6);
            Console.WriteLine("Is sixth value equal to expected value(Pi): " + result);
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