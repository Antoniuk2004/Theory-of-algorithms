namespace Task1
{
    class Program
    {
        public static void Main()
        {
            Queue<object> q = new Queue<object>();
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
            Console.WriteLine("Is the value equal to expected value(5): " + result);
            result = q.Poll().Equals(expectedValue2);
            Console.WriteLine("Is the second equal to expected value(\"hello\"): " + result);
            result = q.Poll().Equals(expectedValue3);
            Console.WriteLine("Is the third equal to expected value(123): " + result);
            result = q.Poll().Equals(expectedValue4);
            Console.WriteLine("Is the fourth equal to expected value(11.5): " + result);
            result = q.Poll().Equals(expectedValue5);
            Console.WriteLine("Is the value equal to expected value('p'): " + result);
            result = q.Poll().Equals(expectedValue6);
            Console.WriteLine("Is the value equal to expected value(Pi): " + result);
        }
    }
    class Queue<T>
    {
        public T[] queue;

        public Queue()
        {
            queue = new T[0];
        }

        public void Add(T value)
        {
            Array.Resize<T>(ref queue, queue.Length + 1);
            queue[queue.Length - 1] = value;
        }
        public object Poll()
        {
            T[] newQueue = new T[queue.Length - 1];
            T value = queue[0];
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