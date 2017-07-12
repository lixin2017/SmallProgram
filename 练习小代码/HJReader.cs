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
            CurrentBook = new HJBook("射雕英雄传","金庸",12,1,".");
            LabelBookName.Text = CurrentBook.Author + ":" + CurrentBook.BookName;
 
            ReaderWeb.Url = new Uri("file:///D:/c-sharp/HJReader/HJReader/bin/Debug/1.html");
           // ReaderWeb.Url = new Uri("http://www.sohu.com");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("即将退出程序");
            this.Close();
        }

        private void NextPage_Click(object sender, EventArgs e)
        {
           
            index = CurrentBook.GotoNextPage();
            
            PageLabel.Text = "当前页是：第" +
                CurrentBook.BookMark.ToString() + "页";
            this.Text = LabelBookName.Text + "----" + PageLabel.Text;
            ReaderWeb.Url = CurrentBook.GetCurrentUri();
        }

        private void PrevPage_Click(object sender, EventArgs e)
        {
            
            index = CurrentBook.GotoPrevPage();
           
            PageLabel.Text = "当前页是：第" +
                CurrentBook.BookMark.ToString() + "页";
            this.Text = LabelBookName.Text + "----" + PageLabel.Text;
            ReaderWeb.Url = CurrentBook.GetCurrentUri();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            
            index = CurrentBook.GotoAnypage(int.Parse(SelectPage.Text));
            
            PageLabel.Text = "当前页是：第" +
               index.ToString() + "页";
            this.Text = LabelBookName.Text + "----" + PageLabel.Text;
            ReaderWeb.Url = CurrentBook.GetCurrentUri();
        }

       

       
    }
}