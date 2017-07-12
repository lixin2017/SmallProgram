using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextReader
{
    public partial class Form1 : Form
    {
        string filename;
        int page=0;
        List<string> article = new List<string>();
        FileReadHelper fr = new FileReadHelper();

        bool Init()
        {
            if (article != null&&article.Count>0) return true;
            else return false;
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //载入阅读历史
            LoadReadHistory();
            MysqlHelper.MysqlOpen();
        }

        //打开文件按钮
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFile();

        }

        public bool bls()
        {
            if (page >= 0 && textBox1.Text != "" && page < article.Count - 1) return true;
            else return false;
        }
        //上一页
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (page > 0 && !string.IsNullOrEmpty(textBox1.Text))
            {    
                page--;
                textBox1.Text = article[page];
                toolStripTextBox1.Text = page + 1 + "/" + article.Count;
            }
        }
        //下一页
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (page < article.Count - 1 && !string.IsNullOrEmpty(textBox1.Text))
            {
                page++;                          
                textBox1.Text = article[page];
                toolStripTextBox1.Text = page + 1 + "/" + article.Count;               
            }
        }
        
        //清除按钮
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MysqlHelper.DeleteData();
            LoadReadHistory();
        }
        
        //菜单栏打开按钮
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        
        //退出按钮
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {        
            MysqlHelper.UpdatePage(filename, page);
            MysqlHelper.MysqlClose();
            Close();
        }
        //打开文件
       
        
        void OpenFile()
        {
            ofd.Title = "选择文件";
            ofd.Filter = "文本文件(*.txt)|*.txt";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if(Init()) article.Clear();
            page = 0;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MysqlHelper.InsertData(ofd.FileName,0);
                article = fr.ReadTxt(ofd.FileName);
                filename = ofd.FileName;
                if (Init())
                {
                    textBox1.Text = article[0];
                    toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                    SetCatalog(fr.GetCatalog(ofd.FileName));
                    fr.SaveReadHistory(ofd.FileName);
                    fr.SaveLastRead(filename, page);
                    LoadReadHistory();
                }
                else MessageBox.Show("此文档为空");
                
            }
        }       
        //载入文件菜单
        public void LoadReadHistory()
        {            
            IList<string> list = new List<string>();
            DataSet filedata = MysqlHelper.SelectData();
            foreach (DataRow row in filedata.Tables[0].Rows)
            {
                list.Add(row["path"].ToString());
            }
            toolStripMenuItem1.DropDownItems.Clear();
            toolStripMenuItem1.DropDownItems.Insert(0, 打开ToolStripMenuItem);
            toolStripMenuItem1.DropDownItems.Insert(1, toolStripSeparator1);
            toolStripMenuItem1.DropDownItems.Insert(2, 退出ToolStripMenuItem);
            toolStripMenuItem1.DropDownItems.Insert(3, 清除ToolStripMenuItem);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ToolStripMenuItem menu = new ToolStripMenuItem(list[i]);
                    toolStripMenuItem1.DropDownItems.Insert(i + 1, menu);
                    menu.Click += menu_Click;
                }
            }
        }

        //文件菜单按钮事件
        void menu_Click(object sender, EventArgs e)
        {
            MysqlHelper.UpdatePage(filename, page);
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            string str = menu.Text.Substring(menu.Text.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            str = str.Substring(0, str.IndexOf('.'));
            DataSet fileread = MysqlHelper.FileRead(str);           
            if (fileread!=null)
            {
                DataRow row = fileread.Tables[0].Rows[0];
                string path =row["path"].ToString();
                int.TryParse(row["page"].ToString(),out page);
                filename= menu.Text;
                if(Init())article.Clear();
                //page = fr.LoadLastRead(filehistory);
                article = fr.ReadTxt(path);                
                textBox1.Text = article[page];
                toolStripTextBox1.Text = page+1+ "/" + article.Count;
                SetCatalog(fr.GetCatalog(path));                               
                //fr.SaveLastRead(filehistory, page);               
            }
        }

        //添加目录
        void SetCatalog(List<string> chapter)
        {
            treeView1.Nodes.Clear();
            foreach (string s in chapter)
            {
                string s1 = s.Replace(" ", "");
                treeView1.Nodes.Add(s1);
                treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
            }
        }
        //目录节点双击事件
        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView view = (TreeView)sender;
            int page = fr.CatalogJump(article, view);
            textBox1.Text = article[page];
            toolStripTextBox1.Text = page + 1 + "/" + article.Count;
        }

        private void MaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized; 
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal; 
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (toolStripTextBox1.Text.Contains("/")&&!string.IsNullOrEmpty(toolStripTextBox1.Text))
                {
                    string currentpage = toolStripTextBox1.Text.Substring(0, toolStripTextBox1.Text.IndexOf("/", StringComparison.Ordinal));
                    string totalpage = toolStripTextBox1.Text.Substring(toolStripTextBox1.Text.IndexOf("/", StringComparison.Ordinal)+1);
                    if (totalpage != article.Count.ToString() || article.Count == 0)
                    {
                        toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                    }
                    else
                    {
                        int a;
                        if (int.TryParse(currentpage, out a))
                        {
                            page = a - 1;
                            textBox1.Text = article[page];
                        }
                        else
                        {
                            toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                        }

                        
                    }
                }
                else if(Init())
                {
                    toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                }
            }           
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (MysqlHelper.IsConnected())
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string result="";
            list=MysqlHelper.DataReaderTest();            
            foreach (string item in list)
            {
                result+=item+"\n";
            }
            MessageBox.Show(result);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataSet myData=MysqlHelper.DataSetTest();
            foreach (DataTable table in myData.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (object field in row.ItemArray)
                    {
                        MessageBox.Show(field.ToString());
                    }                  
                }
            }
        }
                               
      
    }


}
