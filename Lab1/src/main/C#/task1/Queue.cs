namespace Task1
{
    class Program
    {
        public static void Main()
        {
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