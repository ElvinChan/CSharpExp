using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lambda
{
    //扩展方法三要素：静态类，静态方法，this关键字（扩展的是哪个方法）
    public static class MyListExt
    {
        //list指向调用此方法的集合
        public static List<string> MyWhere(this List<string> list, Func<string, bool> funcWhere)
        {
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                if (funcWhere(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
