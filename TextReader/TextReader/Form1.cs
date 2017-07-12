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
        string filehistory;
        int page;
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
        }

        //打开文件按钮
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFile();

        }

        public bool bls()
        {
            if (page > 0 && textBox1.Text != "") return true;
            else return false;
        }
        //上一页
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (bls())
            {
                page--;
                textBox1.Text = article[page];
                toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                fr.SaveLastRead(filehistory, page);
            }
        }
        //下一页
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (bls())
            {
                page++;
                textBox1.Text = article[page];
                toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                fr.SaveLastRead(filehistory, page);
            }
        }
        
        //清除按钮
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fr.DeleteReadHistory();
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
            if (bls()) 
            { 
                fr.SaveLastRead(filehistory, page); 
            }
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
                article = fr.ReadTxt(ofd.FileName);
                filehistory = ofd.FileName.Substring(ofd.FileName.LastIndexOf(@"\")+1);
                if (Init())
                {
                    textBox1.Text = article[0];
                    toolStripTextBox1.Text = page + 1 + "/" + article.Count;
                    SetCatalog(fr.GetCatalog(ofd.FileName));
                    fr.SaveReadHistory(ofd.FileName);
                    fr.SaveLastRead(filehistory, page);
                    LoadReadHistory();
                }
                else MessageBox.Show("此文档为空");
                
            }
        }       
        //载入文件菜单
        public void LoadReadHistory()
        {
            FileStream fs = new FileStream(fr.readhistory, FileMode.OpenOrCreate);
            fs.Close();
            StreamReader sr = new StreamReader(fr.readhistory, true);
            toolStripMenuItem1.DropDownItems.Clear();
            toolStripMenuItem1.DropDownItems.Insert(0, 打开ToolStripMenuItem);
            toolStripMenuItem1.DropDownItems.Insert(1, toolStripSeparator1);
            toolStripMenuItem1.DropDownItems.Insert(2, 退出ToolStripMenuItem);
            toolStripMenuItem1.DropDownItems.Insert(3, 清除ToolStripMenuItem);
            int i;
            for (i = 1; i <= 5 && sr.Peek() > 0; i++)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(sr.ReadLine());
                toolStripMenuItem1.DropDownItems.Insert(i, menu);
                menu.Click += menu_Click;
            }
            sr.Close();
        }

        //文件菜单按钮事件
        void menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            //fr.SaveLastRead(filehistory, page);
            if (File.Exists(menu.Text))
            {                                                        
                filehistory = menu.Text.Substring(menu.Text.LastIndexOf(@"\") + 1);
                if(Init())article.Clear();
                page = fr.LoadLastRead(filehistory);
                article = fr.ReadTxt(menu.Text);                
                textBox1.Text = article[page];
                toolStripTextBox1.Text = page+1+ "/" + article.Count;
                SetCatalog(fr.GetCatalog(menu.Text));                               
                fr.SaveLastRead(filehistory, page);               
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
                if (toolStripTextBox1.Text.Contains("/")&&toolStripTextBox1.Text != "" && toolStripTextBox1.Text != null)
                {
                    string currentpage = toolStripTextBox1.Text.Substring(0, toolStripTextBox1.Text.IndexOf("/"));
                    string totalpage = toolStripTextBox1.Text.Substring(toolStripTextBox1.Text.IndexOf("/")+1);
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
                               
      
    }


}
