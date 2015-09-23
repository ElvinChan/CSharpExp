using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace BasicWebClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WebClient client = new WebClient();
            Stream stream1 = client.OpenRead("http://www.baidu.cn");
            StreamReader sr = new StreamReader(stream1);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                listBox1.Items.Add(line);
            }

            stream1.Close();

            try
            {
                Stream stream2 = client.OpenWrite("http://localhost/accept/newfile.txt", "PUT");
                StreamWriter sw = new StreamWriter(stream2);
                sw.WriteLine("Hello Web");
                stream2.Close();
            }
            catch (System.Exception)
            {

            }

            //异步加载WebRequest
            WebRequest wr = WebRequest.Create("http://www.reuters.com");
            wr.BeginGetResponse(new AsyncCallback(OnResponse), wr);

        }

        protected static void OnResponse(IAsyncResult ar)
        {
            WebRequest wrq = (WebRequest)ar.AsyncState;
            WebResponse wrs = wrq.EndGetResponse(ar);
        }
    }
}
