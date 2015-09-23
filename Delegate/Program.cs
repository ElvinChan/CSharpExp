using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegate
{
    class Program
    {
        #region 基本委托定义
        public delegate void MyDelegate(string name);
        #endregion

        static void Main(string[] args)
        {
            #region 基本委托
            MyDelegate mdStatic = new MyDelegate(StaticMyDelegateFunc);
            MyDelegate md = new MyDelegate(new Program().MyDelegateFunc);

            mdStatic("StaticMyDelegate");
            md("MyDelegate");

            Console.ReadKey();
            #endregion
        }

        #region 基本委托调用的方法
        public static void StaticMyDelegateFunc(string name)
        {
            Console.WriteLine("Hello,{0}", name);
        }

        public void MyDelegateFunc(string name)
        {
            Console.WriteLine("Hello,{0}", name);
        }
        #endregion

    }
}
