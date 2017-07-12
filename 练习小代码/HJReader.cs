using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HJReader
{
    public partial class HJReader : Form
    {
        HJBook CurrentBook;
        int index = 1;
        public HJReader()
        {
            InitializeComponent();
            CurrentBook = new HJBook("���Ӣ�۴�","��ӹ",12,1,".");
            LabelBookName.Text = CurrentBook.Author + ":" + CurrentBook.BookName;
 
            ReaderWeb.Url = new Uri("file:///D:/c-sharp/HJReader/HJReader/bin/Debug/1.html");
           // ReaderWeb.Url = new Uri("http://www.sohu.com");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�����˳�����");
            this.Close();
        }

        private void NextPage_Click(object sender, EventArgs e)
        {
           
            index = CurrentBook.GotoNextPage();
            
            PageLabel.Text = "��ǰҳ�ǣ���" +
                CurrentBook.BookMark.ToString() + "ҳ";
            this.Text = LabelBookName.Text + "----" + PageLabel.Text;
            ReaderWeb.Url = CurrentBook.GetCurrentUri();
        }

        private void PrevPage_Click(object sender, EventArgs e)
        {
            
            index = CurrentBook.GotoPrevPage();
           
            PageLabel.Text = "��ǰҳ�ǣ���" +
                CurrentBook.BookMark.ToString() + "ҳ";
            this.Text = LabelBookName.Text + "----" + PageLabel.Text;
            ReaderWeb.Url = CurrentBook.GetCurrentUri();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            
            index = CurrentBook.GotoAnypage(int.Parse(SelectPage.Text));
            
            PageLabel.Text = "��ǰҳ�ǣ���" +
               index.ToString() + "ҳ";
            this.Text = LabelBookName.Text + "----" + PageLabel.Text;
            ReaderWeb.Url = CurrentBook.GetCurrentUri();
        }

       

       
    }
}