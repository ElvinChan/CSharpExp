using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Event
{
    public delegate void UcDelegate(object sender, EventArgs e);

    public partial class UserControlEvent : UserControl
    {
        public UserControlEvent()
        {
            InitializeComponent();
        }

        public event UcDelegate UcEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            UcEvent(textBox1, new MyEventArgs());
        }
    }

    public class MyEventArgs : EventArgs
    {
        public int i = 1;
        public string str = "我是一个e";
    }
}
