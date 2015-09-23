using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepClone
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 拷贝示例一
            Console.WriteLine("拷贝示例一：");
            Cloner mySource = new Cloner(5);
            //Cloner myTarget = (Cloner)mySource.GetCopy();
            Cloner myTarget = (Cloner)mySource.Clone();
            Console.WriteLine("MyTarget.MyContent.Val = {0}", myTarget.MyContent.Val);
            mySource.MyContent.Val = 2;
            Console.WriteLine("MyTarget.MyContent.Val = {0}", myTarget.MyContent.Val);
            Console.ReadKey();
            #endregion

            Console.WriteLine("\n拷贝示例二：");
            Person p1 = new Person("张三", 10, '男',"自行车");
            Person p2 = new Person();
            Person p3 = new Person();
            Person p4 = new Person();
            Person p5 = new Person();

            p2 = p1.ShallowCloneA();
            p3 = p1.ShallowCloneB();
            p4 = (Person)p1.Clone();
            p5 = p1.StreamClone();

            Console.WriteLine("复制前:");
            p1.SayHello();
            p1.Name = "李四";
            p1.Age = 18;
            p1.Sex = '女';
            p1.MyCar.Name = "三轮车";

            Console.WriteLine("复制后");
            p1.SayHello();

            Console.WriteLine("浅复制一");
            p2.SayHello();
            Console.WriteLine("浅复制二");
            p3.SayHello();
            Console.WriteLine("深复制一");
            p4.SayHello();
            Console.WriteLine("深复制二");
            p5.SayHello();
            Console.ReadKey();

        }
    }

    #region 拷贝示例一
    public class Cloner : ICloneable
    {
        //public int Val;
        public Content MyContent = new Content();

        public Cloner(int newVal)
        {
            MyContent.Val = newVal;
        }

        //浅复制
        public object GetCopy()
        {
            return MemberwiseClone();
        }

        //深复制，实现ICloneable接口中的Clone方法
        public object Clone()
        {
            Cloner clonedCloner = new Cloner(MyContent.Val);
            return clonedCloner;
        }
    }

    public class Content
    {
        public int Val;
    }
    #endregion

    #region 拷贝示例二
    [Serializable]
    public class Person : ICloneable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        private char sex;

        public char Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public Car MyCar;

        public Person()
        {

        }

        public Person(string name, int age, char sex)
        {
            Name = name;
            Age = age;
            Sex = sex;
        }

        public Person(string name, int age, char sex, string carName)
        {
            Name = name;
            Age = age;
            Sex = sex;
            MyCar = new Car();
            MyCar.Name = carName;
        }

        public void SayHello()
        {
            Console.WriteLine("大家好：我是{0}，今年{1}岁，我是{2}生，我有{3}", Name, Age, Sex, MyCar.Name);
        }

        /// <summary>
        /// 浅复制方法一：使用MemberwiseClone方法
        /// 此方法对于内部的变量赋值出现一些不可靠的因素
        /// </summary>
        /// <returns></returns>
        public Person ShallowCloneA()
        {
            return (Person)MemberwiseClone();
        }

        /// <summary>
        /// 浅复制方法二：使用复制对象的方法
        /// </summary>
        /// <returns></returns>
        public Person ShallowCloneB()
        {
            Person tempP = new Person();
            tempP = this;
            return tempP;
        }

        /// <summary>
        /// 深复制方法一：使用直接赋值的方法，继承自ICloneable接口的Clone方法——标准方法
        /// 此处使用ICloneable的方法容易导致方法名含义不清等问题，建议另起名字
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Person tempP = new Person(Name, Age, Sex, MyCar.Name);
            return tempP;
        }

        /// <summary>
        /// 深复制方法二：使用流复制的方法（最稳妥的方法）
        /// </summary>
        /// <returns></returns>
        public Person StreamClone()
        {
            Person tempP;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            using (ms)
            {
                bf.Serialize(ms, this);
                ms.Position = 0;
                tempP = (Person)bf.Deserialize(ms);
            }
            return tempP;
        }
    }

    [Serializable]
    public class Car
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
    #endregion
}
