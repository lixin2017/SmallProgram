using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJReader
{
    class HJBook
    {
        /*
        �������
     �������
     ��ļ��
     ����½� ������Ӧ����½ڣ�
     ��ǩ��Ҳ���ǵ�ǰ������λ�ã�
     ������ڵ�Ŀ¼
         * */
        public String BookName; // �������
        public String Author; // �������
        public int PagesOfBook; // �Ȿ���ж���ҳ
        public int BookMark; //��ǩ���Ȿ�����������
        String DirectoryOfBook; //�Ȿ�����ĸ�Ŀ¼��
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
        // �����������һҳ������һ������������һ��������
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