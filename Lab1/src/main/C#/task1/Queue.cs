using System;

namespace Task1
{
    class Program
    {
        public static void Main()
        {
            Queue queueInstance = new Queue();
            double[] queue = queueInstance.Create();
            queueInstance.Add(ref queue, 7);
            queueInstance.Add(ref queue, 10);
            Console.WriteLine(queueInstance.Poll(ref queue));
            Console.WriteLine(queueInstance.Poll(ref queue));

            Console.WriteLine(queueInstance.GetSize(queue));

        }
    }
    class Queue
    {
        public double[] Create()
        {
            double[] queue = new double[0];
            return queue;
        }
        public void Add(ref double[] queue, double value)
        {
            double[] newQueue = new double[queue.Length + 1];
            if(queue.Length != 0)
            {
                for (int i = 0; i < queue.Length; i++)
                {
                    newQueue[i] = queue[i];
                }
            }
            newQueue[queue.Length] = value;
            queue = newQueue;
        }
        public double Poll(ref double[] queue)
        {
            double value = 0;
            try
            {
                value = queue[0];
                double[] newQueue = new double[queue.Length - 1];
                for (int i = 1; i < queue.Length; i++)
                {
                    newQueue[i - 1] = queue[i];
                }
                queue = newQueue;
            }
            catch
            {
                throw new Exception("Queue is empty.");
            }
            return value;
        }
        public int GetSize(double[] queue)
        {
            return queue.Length;
        }
    }
}