using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XmlEditor
{
    public partial class Form1 : Form
    {
        private XmlDocument document;
        int count = 0;
        int indent = 5;
        const int SPACE = 5;

        public Form1()
        {
            InitializeComponent();
            ResizeControls();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (document != null)
            {
                try
                {
                    Update(document.DocumentElement.SelectNodes("."));
                    paintControls(document.DocumentElement.SelectNodes(@"/*/*"));
                }
                catch (Exception err)
                {
                    txtXml.Text = err.Message;
                }
            }
            else
            {
                openOToolStripMenuItem_Click(sender, e);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = "Extensible Markup Language Document(*.xml)|*.xml";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                document = new XmlDocument();
                document.Load(ofd.FileName);
                try
                {
                    Update(document.DocumentElement.SelectNodes("."));
                    paintControls(document.DocumentElement.SelectNodes(@"/*/*"));
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message.ToString());
                }
            }
        }

        #region 调整控件尺寸
        protected void ResizeControls()
        {
            this.txtXml.Width = this.ClientSize.Width / 2 - 30;
            this.pnl.Width = this.ClientSize.Width / 2 - 53;
        }
        #endregion

        #region 结果输出到txtXml文本框
        private void Update(XmlNodeList nodes)
        {
            if (nodes == null || nodes.Count == 0)
            {
                txtXml.Text = "The query yielded no results";
                return;
            }
            string text = "";
            foreach (XmlNode item in nodes)
            {
                text = FormatText(item, text, "") + "\r\n";
            }
            txtXml.Text = text;
        }
        #endregion

        #region XML文本解析方法
        private string FormatText(XmlNode node, string text, string indent)
        {
            if (node is XmlText)
            {
                text += node.Value;
                return text;
            }

            if (string.IsNullOrEmpty(indent))
                indent = "";
            else
            {
                text += "\r\n" + indent;
            }

            //直接取出XML注释
            if (node is XmlComment)
            {
                text += node.OuterXml;
                return text;
            }

            //直接取出XML属性
            if (node is XmlAttribute)
            {
                text += node.OuterXml;
                return text;
            }

            //分层取出XML元素
            text += "<" + node.Name;
            if (node.Attributes.Count > 0)
            {
                AddAttributes(node, ref text);
            }
            if (node.HasChildNodes)
            {
                text += ">";
                foreach (XmlNode child in node.ChildNodes)
                {
                    text = FormatText(child, text, indent + "  ");
                }
                if (node.ChildNodes.Count == 1 &&
                   (node.FirstChild is XmlText || node.FirstChild is XmlComment))
                    text += "</" + node.Name + ">";
                else
                    text += "\r\n" + indent + "</" + node.Name + ">";
            }
            else
                text += " />";
            return text;
        }
        #endregion

        #region 在XML的元素中的属性单独处理
        private void AddAttributes(XmlNode node, ref string text)
        {
            foreach (XmlAttribute xa in node.Attributes)
            {
                text += " " + xa.Name + "='" + xa.Value + "'";
            }
        }
        #endregion

        private void paintControls(XmlNodeList nodeList)
        {
            gbxElements.Controls.Clear();
            if (analysisXmlStruct(nodeList) > 15)
            {
                Label lblError = new Label();
                lblError.Text = "The Xml structure is too complicated to analysis!";
                lblError.Location = new System.Drawing.Point(5, 20);
                lblError.AutoSize = true;
                lblError.Name = "lbl_Error";
                this.gbxElements.Controls.Add(lblError);
            }
            else
            {
                paintControlsLoop(nodeList[0], false);

                //调整label标签位置
                foreach (Control item in gbxElements.Controls)
                {
                    if (item is Label)
                    {
                        item.Location = new System.Drawing.Point(item.Location.X, item.Location.Y + 3);
                    }
                }
            }
        }

        //获取上一个控件的位置
        private int[] getLastLocation()
        {
            int[] loc = new int[4];
            if (gbxElements.Controls != null && gbxElements.Controls.Count != 0)
            {
                int count = gbxElements.Controls.Count;
                loc[0] = gbxElements.Controls[count - 1].Location.X;
                loc[1] = gbxElements.Controls[count - 1].Location.Y;
                loc[2] = gbxElements.Controls[count - 1].Size.Width;
                loc[3] = gbxElements.Controls[count - 1].Size.Height;
            }
            else
            {
                loc[0] = 0;
                loc[1] = 0;
                loc[2] = 0;
                loc[3] = 0;
            }
            return loc;
        }

        //循环绘制节点
        private void paintControlsLoop(XmlNode node, bool isChildNode)
        {
            int[] loc = new int[4];
            
            #region Step1 绘制注释节点
            if (!isChildNode)
            {
                //开始标记
                Label lblCommentStart = new Label();
                lblCommentStart.Text = @"<!--";
                adjustLabelSize(lblCommentStart);
                loc = getLastLocation();
                lblCommentStart.Location = new System.Drawing.Point(indent, loc[1] + 20);
                lblCommentStart.Name = "lbl_CommentStart";
                this.gbxElements.Controls.Add(lblCommentStart);

                //注释文本框
                TextBox tbComment = new TextBox();
                loc = getLastLocation();
                tbComment.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
                this.gbxElements.Controls.Add(tbComment);

                //结束标记
                Label lblCommentEnd = new Label();
                lblCommentEnd.Text = @"-->";
                adjustLabelSize(lblCommentEnd);
                loc = getLastLocation();
                lblCommentEnd.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
                lblCommentEnd.Name = "lbl_CommentEnd";
                this.gbxElements.Controls.Add(lblCommentEnd);
            }
            #endregion

            #region Step3 绘制文本节点
            if (node.NodeType == XmlNodeType.Text)
            {
                TextBox tbText = new TextBox();
                loc = getLastLocation();
                tbText.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
                this.gbxElements.Controls.Add(tbText);
                return;
            }
            #endregion

            #region Step2 绘制普通节点的开始标记
            //绘制有属性的元素的开始标记
            if (node.Attributes.Count > 0)
            {
                //开始标记的前半部分
                Label lblCommonStartBefore = new Label();
                lblCommonStartBefore.Text = @"<" + node.Name + " ";
                adjustLabelSize(lblCommonStartBefore);
                loc = getLastLocation();
                lblCommonStartBefore.Location = new System.Drawing.Point(indent, loc[1] + loc[3] + SPACE);
                this.gbxElements.Controls.Add(lblCommonStartBefore);

                for (int i = 1; i <= node.Attributes.Count; i++)
                {
                    Label lblAttribute = new Label();
                    lblAttribute.Text = node.Attributes[i - 1].Name + "=";
                    adjustLabelSize(lblAttribute);
                    loc = getLastLocation();
                    lblAttribute.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
                    this.gbxElements.Controls.Add(lblAttribute);

                    TextBox tbAttribute = new TextBox();
                    loc = getLastLocation();
                    tbAttribute.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
                    this.gbxElements.Controls.Add(tbAttribute);
                }

                //开始标记的后半部分
                Label lblCommonStartAfter = new Label();
                lblCommonStartAfter.Text = @">";
                adjustLabelSize(lblCommonStartAfter);
                loc = getLastLocation();
                lblCommonStartAfter.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
                this.gbxElements.Controls.Add(lblCommonStartAfter);
            }
            else
            {
                //绘制没有属性的元素的开始标记
                Label lblCommonStart = new Label();
                lblCommonStart.Text = @"<" + node.Name + ">";
                adjustLabelSize(lblCommonStart);
                loc = getLastLocation();
                lblCommonStart.Location = new System.Drawing.Point(indent, loc[1] + loc[3] + SPACE);
                this.gbxElements.Controls.Add(lblCommonStart);
            }
            #endregion

            #region Step4 以树形结构的先序遍历方式遍历子节点
            if (node.HasChildNodes && node.ChildNodes.Count > 0)
            {
                indent += 15;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    paintControlsLoop(node.ChildNodes[i], true);
                }
                indent -= 15;
            }
            #endregion

            #region Step5 绘制结束标记
            Label lblCommonEnd = new Label();
            lblCommonEnd.Text = @"</" + node.Name + @">";
            adjustLabelSize(lblCommonEnd);
            loc = getLastLocation();
            if (node.HasChildNodes && node.FirstChild.NodeType == XmlNodeType.Element)
            {
                lblCommonEnd.Location = new System.Drawing.Point(indent, loc[1] + loc[3] + SPACE);
            }
            else
            {
                lblCommonEnd.Location = new System.Drawing.Point(loc[0] + loc[2], loc[1]);
            }
            this.gbxElements.Controls.Add(lblCommonEnd);
            #endregion

            #region Step6 绘制兄弟节点，如果要这样写就需要不遍历子节点，只遍历FirstChild节点
            //if (node.NextSibling != null && isChildNode)
            //{
            //    paintControlsLoop(node.NextSibling, true);
            //}
            #endregion

        }

        private void adjustLabelSize(Label label)
        {
            string text = label.Text;
            Graphics g = label.CreateGraphics();
            SizeF oSize = g.MeasureString(text, label.Font);
            int nWidth = (int)oSize.Width + 5;
            int nHeight = 20;
            label.Width = nWidth;
            label.Height = nHeight;
        }

        private int analysisXmlStruct(XmlNodeList loopNodeList)
        {
            //XmlNodeList loopNodeList = document.DocumentElement.SelectNodes(@"/*/*");
            //分析
            if (document.DocumentElement.SelectNodes(@"/*/*/*/*/*") != null)
            {
                analysisLoop(loopNodeList[0], true);
            }
            return count;
        }

        private void analysisLoop(XmlNode node, bool isRoot)
        {
            count++;
            if (node.HasChildNodes && node.ChildNodes[0].NodeType == XmlNodeType.Element)
            {
                XmlNode nodeFirst = node.FirstChild;
                analysisLoop(nodeFirst, false);
            }
            if (node.NextSibling != null && !isRoot)
            {
                XmlNode nodeNext = node.NextSibling;
                analysisLoop(node.NextSibling, false);
                node = nodeNext;
            }
            if (node.Attributes != null && node.Attributes.Count != 0)
            {
                analysisLoop(node.Attributes[0], false);
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (document != null)
            {
                try
                {
                    //执行XPath命令，生成节点列表
                    XmlNodeList nodes = document.DocumentElement.SelectNodes(txtQuery.Text);
                    Update(nodes);
                }
                catch (Exception err)
                {
                    txtXml.Text = err.Message;
                }
            }
            else
            {
                MessageBox.Show("还没有载入XML文档", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnExecute_Click(sender, e);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string add = "";
            foreach (Control control in gbxElements.Controls)
            {
                add += control.Text;
            }
            MessageBox.Show(add);
        }
    }
}
