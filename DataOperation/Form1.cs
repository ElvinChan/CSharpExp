using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace DataOperation
{
    public partial class Form1 : Form
    {
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlCitysOfShanxi"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //先执行city.sql确保数据存在
            string connStr1 = System.Configuration.ConfigurationManager.ConnectionStrings["SqlMaster"].ConnectionString;

            #region 在C#中执行sql脚本以确认数据源存在
            //Microsoft.SqlServer.ConnectionInfo.dll
            //Microsoft.SqlServer.Smo.dll
            //Microsoft.SqlServer.Management.Sdk.Sfc.dll(这个一定要考到你的程序目录,但你可以不引用)

            if (File.Exists("city.sql"))
            {
                FileInfo file = new FileInfo("city.sql");
                string script = file.OpenText().ReadToEnd();
                try
                {
                    SqlConnection conn1 = new SqlConnection(connStr1);
                    using (conn1)
                    {
                        Server server = new Server(new ServerConnection(conn1));
                        int i = server.ConnectionContext.ExecuteNonQuery(script);
                        if (i != 0)
                        {
                            //MessageBox.Show("执行成功！");
                        }
                        else
                        {
                            MessageBox.Show("执行失败！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("初始化数据库失败！");
                return;
            }
            #endregion

            #region 给ComboBox绑定数据源
            SqlConnection conn2 = new SqlConnection(connStr);
            using (conn2)
            {
                SqlCommand cmd = new SqlCommand(@"select id, name from city where fk=0", conn2);
                using (cmd)
                {
                    if (conn2.State == ConnectionState.Closed)
                    {
                        conn2.Open();
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cbxCity1.Items.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                            }
                        }
                    }
                }
            }
            cbxCity1.DisplayMember = "Value";
            cbxCity1.ValueMember = "Key";
            #endregion
        }

        private void cbxCity1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxCity2.Items.Clear();
            //KeyValuePair<int,string> kvl = (KeyValuePair<int,string>)cbxCity1.SelectedItem;
            int id = Convert.ToInt32(cbxCity1.SelectedIndex) + 1;
            string sql = @"SELECT id, name FROM city WHERE fk=" + id;
            SqlConnection conn = new SqlConnection(connStr);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                using (cmd)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cbxCity2.Items.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                            }
                        }
                    }
                }
            }
            cbxCity2.DisplayMember = "Value";
            cbxCity2.ValueMember = "Key";
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet("CityOfShanxi");
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM City", connStr);
                sda.Fill(ds);
                ds.WriteXml(@"Export.xml");
                MessageBox.Show("导出成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"导出失败！");
            }
        }
    }
}
