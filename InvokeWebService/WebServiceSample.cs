using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InvokeWebService
{
    public partial class WebServiceSample : Form
    {
        public WebServiceSample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //同步调用WebService方法
            //var client = new WebServicesSample.SampleServiceSoapClient();
            //textBox2.Text = client.ReverseString(textBox1.Text.ToString());

            //异步调用WebService方法
            var client = new WebServicesSample.SampleServiceSoapClient();
            client.ReverseStringCompleted += client_ReverseStringCompleted;
            client.ReverseStringAsync(textBox1.Text);
        }

        void client_ReverseStringCompleted(object sender, WebServicesSample.ReverseStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                textBox2.Text = e.Result;
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
        }
    }
}
