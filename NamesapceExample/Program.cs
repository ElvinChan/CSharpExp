using System;
using Introduction = Wrox.ProCSharp.Basics;
using System.Linq;
using System.Text;

namespace Basic
{
    class Program
    {
        static int Main(string[] args)
        {
            //：：命名空间别名限定符运算符。直接查找名为Introduction的命名空间
            Introduction::NamespaceExample NSEx = new Introduction::NamespaceExample();

            //以下代码获取this下的NamespaceExample子类
            NamespaceExample NSExFake = new NamespaceExample();

            Console.WriteLine(NSEx.GetNamespace());

            Console.WriteLine(NSExFake.GetNamespace());
            Console.ReadKey();
            return 0;
        }
    }
    class NamespaceExample
    {
        public string GetNamespace()
        {
            return this.GetType().Namespace;
        }
    }
}

namespace Wrox.ProCSharp.Basics
{
    class NamespaceExample
    {
        public string GetNamespace()
        {
            return this.GetType().Namespace;
        }
    }
}
