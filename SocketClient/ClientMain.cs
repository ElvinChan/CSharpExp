using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace SocketClient
{
    public partial class ClientMain : Form
    {
        public Socket ClientSocket { get; set; }

        public ClientMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public delegate bool TakesAWhileDelegate(IPAddress ip, int port, int tryTimes, TextBox t, ClientMain c);
        TakesAWhileDelegate d = tryConnect;

        private void btnStart_Click(object sender, EventArgs e)
        {
            //1 创建Socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ClientSocket = socket;

            //2 尝试连接

            IAsyncResult ar = d.BeginInvoke(IPAddress.Parse(txtIP.Text), 53666, 1, this.txtLog, this, null, null);
            //while (!ar.IsCompleted)
            //{
            //    Thread.Sleep(50);
            //}


            //if (!tryConnect(IPAddress.Parse(txtIP.Text), 53666, 1))
            //{
            //    return;
            //}
            //AppendTextToTxtLog(string.Format("已连接到服务器：{0}", ClientSocket.RemoteEndPoint.ToString()));
            ////3 创建后台线程，接收消息
            //Thread thread = new Thread(new ParameterizedThreadStart(ReceiveData));
            //thread.IsBackground = true;
            //thread.Start(ClientSocket);


        }

        public static bool tryConnect(IPAddress ip, int port, int tryTimes, TextBox t, ClientMain c)
        {
            bool flag = false;
            try
            {
                if (tryTimes == 1)
                {
                    AppendTextToTxtLog(string.Format("正在连接..."), t);
                }
                else if (tryTimes < 10)
                {
                    AppendTextToTxtLog(string.Format("连接失败，正在尝试第{0}次连接...", tryTimes), t);
                }
                else
                {
                    AppendTextToTxtLog(string.Format("连接失败，请检查网络!"), t);
                    return false;
                }
                c.ClientSocket.Connect(ip, port);
                return true;
            }
            catch (Exception)
            {
                Thread.Sleep(500);
                flag = tryConnect(ip, port, tryTimes + 1, t, c);
                if (flag)
                {
                    if (tryTimes == 1)
                    {
                        AppendTextToTxtLog(string.Format("已连接到服务器：{0}", c.ClientSocket.RemoteEndPoint.ToString()), t);
                        //3 创建后台线程，接收消息
                        Thread thread = new Thread(new ParameterizedThreadStart(c.ReceiveData));
                        thread.IsBackground = true;
                        thread.Start(c.ClientSocket);
                    }
                }
                return flag;
            }
        }

        #region 窗口关闭
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //判断是否已连接，如果已连接就关闭连接
            StopConnect();
        }
        #endregion

        #region 客户端接收数据
        public void ReceiveData(object socket)
        {
            var proxSocket = socket as Socket;
            byte[] data = new byte[1024 * 1024];
            while (true)
            {
                int len = 0;
                try
                {
                    len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception)
                {
                    //异常退出
                    AppendTextToTxtLog(string.Format("服务器端：{0}非正常退出", proxSocket.RemoteEndPoint.ToString()));
                    StopConnect();
                    return;
                }
                if (len <= 0)
                {
                    //客户端正常退出
                    AppendTextToTxtLog(string.Format("服务器端：{0}正常退出", proxSocket.RemoteEndPoint.ToString()));
                    StopConnect();
                    return;
                }


                //把接收到的数据放到文本框上去
                //接收的数据中第一个字节如果是1是字符串，如果是2是闪屏，如果是3是文件

                #region 接收到的是字符串
                if (data[0] == 1)
                {
                    string strMsg = ProcessReceiveString(data, len);
                    AppendTextToTxtLog(string.Format("接收到服务端：{0}的消息是：{1}", proxSocket.RemoteEndPoint.ToString(), strMsg));
                }
                #endregion

                #region 接收闪屏
                else if (data[0] == 2)
                {
                    Shake();
                }
                #endregion

                #region 接收文件
                else if (data[0] == 3)
                {
                    ProcessReceiveFile(data, len);
                }
                #endregion
            }

        }
        #endregion

        #region 处理接收到的字符串
        public string ProcessReceiveString(byte[] data, int len)
        {
            string str = Encoding.Default.GetString(data, 1, len - 1);
            return str;
        }
        #endregion

        #region 执行闪屏
        public void Shake()
        {
            Point oldLocation = this.Location;
            Random r = new Random();

            for (int i = 0; i < 50; i++)
            {
                this.Location = new Point(r.Next(oldLocation.X - 5, oldLocation.X + 5), r.Next(oldLocation.Y - 5, oldLocation.Y + 5));
                Thread.Sleep(10);
                this.Location = oldLocation;
            }
        }
        #endregion

        #region 处理接收到的文件
        public void ProcessReceiveFile(byte[] data, int len)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                byte[] fileData = new byte[len - 1];
                Buffer.BlockCopy(data, 1, fileData, 0, len - 1);
                File.WriteAllBytes(sfd.FileName, fileData);
            }
        }
        #endregion

        #region 记录消息
        public void AppendTextToTxtLog(string str)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke(new Action<string>(s =>
                    {
                        this.txtLog.Text = string.Format("{0}\r\n{1}", s, txtLog.Text);
                    }), str);
            }
            else
            {
                txtLog.Text = string.Format("{0}\r\n{1}", str, txtLog.Text);
            }
        }

        public static void AppendTextToTxtLog(string str, TextBox t)
        {
            if (t.InvokeRequired)
            {
                t.BeginInvoke(new Action<string>(s =>
                    {
                        t.Text = string.Format("{0}\r\n{1}", s, t.Text);
                    }), str);
            }
            else
            {
                t.Text = string.Format("{0}\r\n{1}", str, t.Text);
            }
        }
        #endregion

        #region 关闭连接
        public void StopConnect()
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close(100);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 发送模块
        #region 发送字符串
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (ClientSocket.Connected)
            {
                //原始的字符串转换的字节数组
                byte[] data = Encoding.Default.GetBytes(txtMsg.Text);

                //对原始的数据数组加上协议的头部字节
                byte[] result = new byte[data.Length + 1];

                //设置当前的协议头部字节是1:1代表字符串
                result[0] = 1;

                Buffer.BlockCopy(data, 0, result, 1, data.Length);

                ClientSocket.Send(result, 0, result.Length, SocketFlags.None);
                // ClientSocket.Send(data, 0, data.Length, SocketFlags.None);
            }
        }
        #endregion

        #region 发送闪屏
        private void btmSendShake_Click(object sender, EventArgs e)
        {
            if (ClientSocket.Connected)
            {
                ClientSocket.Send(new byte[] { 2 }, SocketFlags.None);
            }
        }
        #endregion

        #region 发送文件
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ClientSocket.Connected)
                {
                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    byte[] data = File.ReadAllBytes(ofd.FileName);
                    byte[] result = new byte[data.Length + 1];
                    result[0] = 3;
                    Buffer.BlockCopy(data, 0, result, 1, data.Length);
                    ClientSocket.Send(result, SocketFlags.None);
                }
            }
        }
        #endregion

        private void txtMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSendMsg_Click(sender, null);
                txtMsg.Text = "";
            }
        }
        #endregion


    }
}
