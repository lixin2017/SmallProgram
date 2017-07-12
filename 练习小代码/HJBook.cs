using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJReader
{
    class HJBook
    {
        /*
        书的名字
     书的作者
     书的简介
     书的章节 （可适应多个章节）
     书签（也就是当前读到的位置）
     书的所在的目录
         * */
        public String BookName; // 书的名字
        public String Author; // 书的作者
        public int PagesOfBook; // 这本书有多少页
        public int BookMark; //书签，这本书读到哪里了
        String DirectoryOfBook; //这本书在哪个目录下
        public HJBook(String InputName,String InputAuthor,int Pages,int Mark,String dir)
        {
            BookName = InputName;
            Author = InputAuthor;
            PagesOfBook = Pages;
            BookMark = Mark;
            DirectoryOfBook = dir;
        }
        public String SetBookName(String inputBookName)
        {
            if (inputBookName!=null)
                BookName = inputBookName;
            return BookName;
        }
        public String GetBookName()
        {
            return BookName;
        }

        public String SetAuthor(String inputAuthor)
        {
            if (inputAuthor != null)
                Author = inputAuthor;
            return Author;
        }
        public String GetAuthor()
        {
            return Author;
        }
        public int GetPageNumberOfBook()
        {
            return BookMark;
        }
        public int GotoNextPage()
        {
            BookMark++;
            if (BookMark > PagesOfBook)
                BookMark = PagesOfBook;
            return BookMark;	
        }
        public int GotoPrevPage()
        {
            BookMark--;
            if (BookMark <= 0)
                BookMark = 1;
            return BookMark;
        }
        public int GotoFirstPage()
        {
            BookMark = 1;
            return BookMark;
        }
        public int GotoLastPage()
        {
            BookMark = PagesOfBook;
            return BookMark;
        }
        // 翻到书的任意一页，输入一个整数，返回一个整数。
        public int GotoAnypage(int PageNumber)
        {
            if (PageNumber <= 0)
                BookMark = 1;
            else if (PageNumber >= PagesOfBook)
                BookMark = PagesOfBook;
            else
                BookMark = PageNumber;
            return BookMark;
        }

        public Uri GetCurrentUri()
        {
              Uri BookPageUrl = new Uri(
                    "file:///D:/c-sharp/HJReader/HJReader/bin/Debug/"
                      + BookMark.ToString() + ".html");
              return BookPageUrl;
        }
    }
}