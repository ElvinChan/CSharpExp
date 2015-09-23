using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSamples
{
    public struct Data
    {
        public string Message;
    }

    public class MyThread
    {
        private string data;

        public MyThread(string data)
        {
            this.data = data;
        }

        public void ThreadForeground()
        {
            Console.WriteLine("Running in a thread with foreground, data:{0}", data);
        }

        public void ThreadBackground()
        {
            Console.WriteLine("Running in a thread with background, data:{0}", data);
            Console.WriteLine("Thread {0} started", Thread.CurrentThread.Name);
            Thread.Sleep(3000);
            Console.WriteLine("Thread {0} completed", Thread.CurrentThread.Name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Lambda表达式简写创建线程
            var t1 = new Thread(() => Console.WriteLine("Running in a thread named 1, id: {0}", Thread.CurrentThread.ManagedThreadId));
            t1.Start();

            //标准方式创建线程
            var t2 = new Thread(ThreadSecond);
            t2.Start();
            Console.WriteLine("Running is the MainThread, id: {0}", Thread.CurrentThread.ManagedThreadId);

            //向线程传参数方式创建线程
            var d = new Data { Message = "Info" };
            var t3 = new Thread(ThreadThird);
            t3.Start(d);

            //封装在类MyThread里线程，调用对应方法创建线程
            var objForeground = new MyThread("info");
            var t4 = new Thread(objForeground.ThreadForeground);
            t4.Start();

            //封装在类MyThread里线程，调用对应方法创建线程，此线程为后台线程
            var objBackground = new MyThread("info");
            var t5 = new Thread(objBackground.ThreadBackground) { Name = "MyNewThread", IsBackground = true };
            //t5.IsBackground = true;
            t5.Start();

            Console.ReadKey();
        }

        static void ThreadSecond()
        {
            Console.WriteLine("Running in a thread named 2,id: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        static void ThreadThird(object o)
        {
            Data d = (Data)o;
            Console.WriteLine("Running in a thread named 3, received {0}", d.Message);
        }
    }
}
