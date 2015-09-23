using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Event
{
    public delegate void MyTripleClickDelegate();

    public class MyTripleClick : Button
    {
        int i = 0;

        public MyTripleClickDelegate myTripleClickDelegate;
        //public event MyTripleClickDelegate myTripleClickDelegate;

        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
            if (++i == 3 && myTripleClickDelegate != null)
            {
                myTripleClickDelegate();
                i = 0;
            }
        }
    }
}
