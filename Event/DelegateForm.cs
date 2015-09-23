using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//此项目参考:传智播客C#基础教程——C#基础加强-15职位抓取,委托,排序,委托连
namespace Event
{
    public partial class DelegateForm : Form
    {
        public DelegateForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTripleClick1.myTripleClickDelegate = Fun;
            //myTripleClick1.myTripleClickDelegate += Fun;
            //myTripleClick1.myTripleClickDelegate += Fun;
            //myTripleClick1.myTripleClickDelegate += Fun;
        }

        private void Fun()
        {
            MessageBox.Show("我被点了三下！");
        }

        private void myTripleClick1_Click(object sender, EventArgs e)
        {

        }

        //盗用委托
        private void myTripleClick2_Click(object sender, EventArgs e)
        {
            myTripleClick1.myTripleClickDelegate();
        }
        
        //为什么不用委托实现三连击？
        //此处联系黑马第9期——42_2013年11月21日 委托多线程——上午6 事件的方式实现 窗体间传值.avi

        //1、无法避免盗链：由于委托变量需要在其他类中赋值（将方法赋值给委托变量），所以委托必须是public的
        //   由于委托是public的，其他类中的方法就可以调用委托

        //2、委托链的问题：无法限制委托不使用=，以防清空整个委托链

        //解决方法：只需要在委托变量定义时加上event关键字

        //另注：
        //①事件只能使用+=不能使用=，事件是私有的，不能在类外调用
        //②委托的实质就是一个私有的委托变量，加上两个方法(add、remove)，通过+=和-=(运算符重载)进行赋值和清空等操作
        //③委托是类型，事件是变量

        private void userControlEvent1_UcEvent(object sender, EventArgs e)
        {
            MyEventArgs myE = (MyEventArgs)e;
            TextBox t = (TextBox)sender;

            MessageBox.Show(t.Text + "\r\n" + myE.i + "\r\n" + myE.str + "\r\n");
        }
    }
}
