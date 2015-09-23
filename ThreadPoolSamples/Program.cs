using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadPoolSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            int nWorkerThreads;
            int nCompletionPortThreads;
            ThreadPool.GetMaxThreads(out nWorkerThreads, out nCompletionPortThreads);
            Console.WriteLine("Max work threads: {0},I/O completion threads: {1}", nWorkerThreads, nCompletionPortThreads);

            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(JobForAThread);
            }
            Thread.Sleep(3000);

            Console.ReadKey();
        }

        static void JobForAThread(object o)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("loop {0}, running inside pooled thread {1}", i,
                    Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
            }
        }
    }
}
