using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lambda
{
    delegate int AddDel(int a, int b);

    delegate int AddDel1(int a, int b);

    class Program
    {
        static void Main(string[] args)
        {
            //一、Lambda表达式的演变

            #region 1.基本的委托
            //缺点：既要定义委托类型，又要定义委托指向的方法
            //构造一个委托变量
            AddDel del = new AddDel(AddStaticFunc);
            Program p = new Program();

            //将委托指向方法
            del += p.AddInstanceFunc;

            //如果是多播委托，则拿到的委托返回值是最后一个委托指向的方法的执行结果
            int result = del(3, 4);
            Console.WriteLine(result);
            #endregion

            #region 2.泛型委托
            //缺点：还要定义一个委托指向的方法
            Func<int, int, int> funcDemo = new Func<int, int, int>(AddStaticFunc);
            int result1 = funcDemo(3, 4);
            Console.WriteLine(result1);
            #endregion

            #region 3.匿名委托
            //缺点：形式上有点繁琐
            Func<int, int, int> funcDemo2 = delegate(int a, int b) { return a + b; };
            int result2 = funcDemo2(3, 4);
            Console.WriteLine(result2);
            #endregion

            #region 4.Lambda语句
            //缺点：形式上还有点繁琐
            Func<int, int, int> funcDemo3 = (int a, int b) => { return a + b; };
            int result3 = funcDemo3(3, 4);
            Console.WriteLine(result3);
            #endregion

            #region 5.Lambda表达式
            //缺点：传入参数还要手动指定类型
            Func<int, int, int> funcDemo4 = (int a, int b) => a + b;
            int result4 = funcDemo4(3, 4);
            Console.WriteLine(result4);
            #endregion

            #region 6.Lambda表达式

            //如果只有一个传入参数就还可以省略小括号
            //比如Fun<int, int> funcDemo6 = a => a + a;

            Func<int, int, int> funcDemo5 = (a, b) => a + b;
            int result5 = funcDemo5(3, 4);
            Console.WriteLine(result5);
            #endregion

            //Action没有返回值
            //Func最后一个参数是返回值
            //为什么微软要提供这两种方法？

            //答:因为指向相同方法签名（方法名 + 参数）的不同委托类型是不能相互转化的，在实际使用中可能为相同功能定义了多个不同的委托类型，而调用的时候不知道该用哪个，所以可能会产生很多重复和混乱，所以微软预定义了这两种委托类型，使得编码标准化，减少了重复了混乱

            Console.WriteLine("=====================================================================");

            #region Where以及自定义扩展方法
            List<string> strList = new List<string>()
            {
                "3","9","32","7"
            };
            //把集合中字符串小于"6"，查询出来然后打印

            //Where方法内部：遍历strList集合，把strList中的每个元素传到后面那个匿名委托去执行，如果结果为true，就把这个元素选择出来，最终的结果，把所有选择出来的元素返回给temp
            //var temp = strList.Where(delegate(string a) { return a.CompareTo("6") < 0; });

            //自定义的扩展方法
            //var temp = strList.MyWhere(delegate(string a) { return a.CompareTo("6") < 0; });

            //简化为Lambda表达式
            //方法泛型的约束（传入参数类型）可以省略，但是如果你是显式的约束，就必须满足约束，比如可以写成
            //var temp = strList.MyWhere((string a) => a.CompareTo("6") < 0);
            var temp = strList.MyWhere(a => a.CompareTo("6") < 0);

            foreach (var item in temp)
            {
                Console.WriteLine(item);
            }
            #endregion

            Console.Read();
        }

        static int AddStaticFunc(int a, int b)
        {
            return a + b;
        }

        public int AddInstanceFunc(int a, int b)
        {
            return a + b + 1;
        }
    }
}
