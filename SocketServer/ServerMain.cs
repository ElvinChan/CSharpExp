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

namespace SocketServer
{
    public partial class ServerMain : Form
    {
        List<Socket> ClientProxSocketList = new List<Socket>();

        public ServerMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        #region 窗口关闭时通知客户端退出
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var socket in ClientProxSocketList)
            {
                StopConnect(socket);
            }
        }
        #endregion

        #region 启动服务
        private void btnStart_Click(object sender, EventArgs e)
        {
            //1 创建Socket对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //2 绑定端口IP
            socket.Bind(new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text)));

            //3 开启侦听
            //等待链接的队列：同时来了100链接请求，只能处理一个链接，队列里面放10个等待链接的客户端，其他的返回错误消息
            socket.Listen(20);

            //4 开始接受客户端连接
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), socket);
        }
        #endregion

        #region 接收客户端连接
        private void AcceptClientConnect(object socket)
        {
            var serverSocket = socket as Socket;

            this.AppendTextToTxtLog("服务器开始接受客户端的连接。");

            while (true)
            {
                var proxSocket = serverSocket.Accept();
                this.AppendTextToTxtLog(string.Format("客户端：{0}连接上了", proxSocket.RemoteEndPoint.ToString()));

                ClientProxSocketList.Add(proxSocket);
                // 不断的接收当前连接的客户端发来的消息
                // 完整写法:（语法糖）
                // ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveData), proxSocket);
                ThreadPool.QueueUserWorkItem(ReceiveData, proxSocket);
            }
        }
        #endregion

        #region 接收客户端的消息
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
                    AppendTextToTxtLog(string.Format("客户端：{0}非正常退出", proxSocket.RemoteEndPoint.ToString()));
                    ClientProxSocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;
                }

                if (len <= 0)
                {
                    AppendTextToTxtLog(string.Format("客户端：{0}正常退出", proxSocket.RemoteEndPoint.ToString()));
                    ClientProxSocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    //让方法结束，终结当前接受客户端数据的异步线程
                    return;
                }

                //把接收到的数据放到文本框上去
                //接收的数据中第一个字节如果是1是字符串，如果是2是闪屏，如果是3是文件
                #region 接收字符串
                if (data[0] == 1)
                {
                    string strMsg = ProcessReceiveString(data, len);
                    AppendTextToTxtLog(string.Format("接收到客户端：{0}的消息是：{1}", proxSocket.RemoteEndPoint.ToString(), strMsg));
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
        public string ProcessReceiveString(byte[] data,int len)
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

        #region 断开连接
        private void StopConnect(Socket proxSocket)
        {
            try
            {
                if (proxSocket.Connected)
                {
                    proxSocket.Shutdown(SocketShutdown.Both);
                    proxSocket.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 往日志的文本框上追加数据
        public void AppendTextToTxtLog(string txt)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke(new Action<string>(s =>
                    {
                        this.txtLog.Text = string.Format("{0}\r\n{1}", s, txtLog.Text);
                    }), txt);
            }
            else
            {
                this.txtLog.Text = string.Format("{0}\r\n{1}", txt, txtLog.Text);
            }
        }
        #endregion

        #region 发送模块
        #region 发送字符串
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            foreach (var proxSocket in ClientProxSocketList)
            {
                if (proxSocket.Connected)
                {
                    //原始的字符串转换的字节数组
                    byte[] data = Encoding.Default.GetBytes(txtMsg.Text);

                    //对原始的数据数组加上协议的头部字节
                    byte[] result = new byte[data.Length + 1];

                    //设置当前的协议头部字节是1:1代表字符串
                    result[0] = 1;

                    //把原始的数据放到最终的字节数组里去
                    Buffer.BlockCopy(data, 0, result, 1, data.Length);
                    proxSocket.Send(result, 0, result.Length, SocketFlags.None);
                }
            }
        }
        #endregion

        #region 发送闪屏
        private void btmSendShake_Click(object sender, EventArgs e)
        {
            foreach (var proxSocket in ClientProxSocketList)
            {
                if (proxSocket.Connected)
                {
                    proxSocket.Send(new byte[] { 2 }, SocketFlags.None);
                }
            }
        }
        #endregion

        #region 发送文件
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                byte[] data = File.ReadAllBytes(ofd.FileName);
                byte[] result = new byte[data.Length + 1];
                result[0] = 3;
                Buffer.BlockCopy(data, 0, result, 1, data.Length);

                foreach (var proxSocket in ClientProxSocketList)
                {
                    if (!proxSocket.Connected)
                    {
                        continue;
                    }
                    proxSocket.Send(result, SocketFlags.None);
                }
            }
        }
        #endregion

        #endregion

        private void txtMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSendMsg_Click(sender, null);
                txtMsg.Text = "";
            }
        }
    }
}
