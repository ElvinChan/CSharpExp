using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 此处是重定向数据库文件的位置，详见http://www.cnblogs.com/rupeng/archive/2010/05/01/1725772.html
            string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            if (dataDir.EndsWith(@"\bin\Debug\") || dataDir.EndsWith(@"\bin\Release\"))
            {
                dataDir = System.IO.Directory.GetParent(dataDir).Parent.Parent.FullName;
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
            }
            #endregion
            string[] books = System.IO.File.ReadAllLines("jk的书.txt", Encoding.Default);
            //对books进行split，不需要除掉空项
            //0--书 目 	
            //1--版 次	
            //2--分 类	
            //3--作 者	
            //4--属 性	
            //5--出版社	
            //6--出版时间	
            //7--价 格	
            //8--注 释

            string connStr = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                #region Step1 添加表
                Console.Write("是否添加表？(Y/N)：");
                if (Console.ReadKey().Key.ToString().ToUpper() == "Y")
                {
                    using (cmd)
                    {
                        cmd.CommandText = @"CREATE TABLE MyBooks
                                            (
	                                            Fid int identity(1,1) primary key,
	                                            FName nvarchar(100),
	                                            FEdit nvarchar(100),
	                                            FClass nvarchar(100),
	                                            FAuthor nvarchar(100),
	                                            FProperty nvarchar(100),
	                                            FPublic nvarchar(100),
	                                            FPTime datetime,
	                                            FPrice money,
	                                            FNote nvarchar(max),
	                                            FIsDel	bit default(0),
	                                            FCreate datetime default(getDate())
                                            )";
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine();
                #endregion

                #region Step2 添加数据
                Console.Write("是否添加数据？(Y/N)：");
                if (Console.ReadKey().Key.ToString() == "Y")
                {
                    foreach (string temp in books)
                    {
                        #region 处理字符串
                        //拼接插入数据的SQL语句，并执行
                        //将时间的年月改为-，日去掉
                        string[] temps = temp.Split('\t');

                        if (temps[6] == "")
                        {
                            temps[6] = DateTime.Now.ToString();
                        }
                        else
                        {
                            temps[6] = temps[6].Replace("年", "-").Replace("月", "-1");
                        }

                        if (temps[7] == "")
                        {
                            temps[7] = "0";
                        }
                        using (cmd)
                        {
                            #region 方法一：直接拼接字符串
                            //cmd.CommandText = string.Format(
                            //                        "INSERT INTO MyBooks(FName, FEdit, FClass, FAuthor, FProperty, FPublic, FPTime, FPrice, FNote) VALUES(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',CAST('{6}' AS DATETIME),CAST('{7}' AS MONEY),N'{8}')",
                            //                        temps);
                            //cmd.ExecuteNonQuery();
                            #endregion

                            #region 方法二：用SqlParameter参数方式（推荐，因为安全）
                            cmd.CommandText = @"INSERT INTO MyBooks(FName, FEdit, FClass, FAuthor, FProperty, FPublic, FPTime, FPrice, FNote) VALUES (@FName, @FEdit, @FClass, @FAuthor, @FProperty, @FPublic, @FPTime, @FPrice, @FNote)";
                            SqlParameter[] sqlParams = {
                                                           new SqlParameter("@FName",temps[0]),
                                                           new SqlParameter("@FEdit",temps[1]),
                                                           new SqlParameter("@FClass",temps[2]),
                                                           new SqlParameter("@FAuthor",temps[3]),
                                                           new SqlParameter("@FProperty",temps[4]),
                                                           new SqlParameter("@FPublic",temps[5]),
                                                           new SqlParameter("@FPTime",Convert.ToDateTime(temps[6])),
                                                           new SqlParameter("@FPrice",Convert.ToDecimal(temps[7])),
                                                           new SqlParameter("@FNote",temps[8])
                                                       };
                            cmd.Parameters.AddRange(sqlParams);
                            cmd.ExecuteNonQuery();
                            //因为是循环执行此段代码，一行一行的插入数据，所以如果不清空参数就会报错：参数已经定义
                            cmd.Parameters.Clear();
                            #endregion

                        #endregion
                        }
                    }
                }
                Console.WriteLine();
                #endregion

                #region Step3 查询数据
                Console.Write("是否显示数据？(Y/N)：");
                if (Console.ReadKey().Key.ToString() == "Y")
                {
                    using (cmd)
                    {
                        cmd.CommandText = @"SELECT * FROM MyBooks";
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    //拿到一行的数据后，对于reader有三种处理方法：

                                    //①得到指定列的数据，比如书名
                                    //string bookName = reader["FName"].ToString(); 

                                    //②得到一行的全部内容
                                    //FieldCount表示列数
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i].ToString() + "\t");
                                    }

                                    //③得到对应格式的数据，0表示第0列
                                    //int id = reader.GetInt32(0);
                                    //string bookName = reader.GetString(1);

                                    //常见的操作是将数据存入数组或集合
                                    //List<Book> bookList = new List<Book>();
                                    //bookList.Add(new Book(reader[0],reader[1],...));
                                }
                            }
                        }
                    }
                }
                Console.WriteLine();
                #endregion

                #region Step4 删除表
                Console.Write("是否删除表？(Y/N)：");
                if (Console.ReadKey().Key.ToString().ToUpper() == "Y")
                {
                    using (cmd)
                    {
                        cmd.CommandText = @"DROP TABLE MyBooks";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine();
                        Console.WriteLine("删除成功！");
                    }
                }
                Console.ReadKey();
                #endregion
            }
        }
    }
}
