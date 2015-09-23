using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Wrox.ProCSharp.Threading
{
    class Program
    {
        static int TakesAWhile(int data, int ms)
        {
            Console.WriteLine("TakesAWhile Started");
            Thread.Sleep(ms);
            Console.WriteLine("TakesAWhile Completed");
            return ++data;
        }

        public delegate int TakesAWhileDelegate(int data, int ms);

        static void Main()
        {
            TakesAWhileDelegate d1 = TakesAWhile;
            TakesAWhileDelegate d2 = TakesAWhile;
            TakesAWhileDelegate d3 = TakesAWhile;
            TakesAWhileDelegate d4 = TakesAWhile;

            //异步委托方式一：Pooling轮询方式
            IAsyncResult ar1 = d1.BeginInvoke(1, 3000, null, null);
            while (!ar1.IsCompleted)
            {
                Thread.Sleep(50);
                Console.Write(".");
            }
            int result1 = d1.EndInvoke(ar1);
            Console.WriteLine("result: {0}", result1);

            //异步委托方式二：使用与IAsyncResult相关联的等待句柄
            IAsyncResult ar2 = d2.BeginInvoke(1, 3000, null, null);
            while (true)
            {
                if (ar2.AsyncWaitHandle.WaitOne(50, false))
                {
                    Console.WriteLine("Can get the result now");
                    break;
                }
                Console.Write(".");
            }
            int result2 = d2.EndInvoke(ar2);
            Console.WriteLine("result: {0}", result2);

            //异步委托方式三：使用与IAsyncResult相关联的等待句柄
            d3.BeginInvoke(1, 3000, TakesAWhileCompleted, d3);
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(50);
                Console.Write(".");
            }
            Console.WriteLine();

            //异步委托方法四：使用与IAsyncResult相关联的等待句柄
            d4.BeginInvoke(1, 3000,
                ar =>
                {
                    int result = d4.EndInvoke(ar);
                    Console.WriteLine("result: {0}", result);
                }, null);
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(50);
                Console.Write(".");
            }
        }

        static void TakesAWhileCompleted(IAsyncResult ar)
        {
            if (ar == null)
            {
                throw new ArgumentNullException("ar");
            }

            TakesAWhileDelegate d = ar.AsyncState as TakesAWhileDelegate;
            Trace.Assert(d != null, "Invoid object type");

            int result = d.EndInvoke(ar);
            Console.WriteLine("result: {0}", result);
        }
    }
}
