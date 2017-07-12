using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace TextReader
{
    public class FileReadHelper
    {
        public string readhistory = "readhistory.txt";      
        
        //保存阅读历史


        public void Init(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            fs.Close();           
        }
        public void SaveReadHistory(string filename)
        {
            Init(readhistory);
            StreamReader sr = new StreamReader(readhistory, true);
            List<string> rh = new List<string>();
            while (sr.Peek() > 0)
            {
                rh.Add(sr.ReadLine());
            }
            sr.Close();
            FileStream fs2 = new FileStream(readhistory, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs2);
            rh.Add(filename);
            List<string> rh2 = rh.Distinct().ToList();
            foreach (string read in rh2)
            {
                sw.WriteLine(read);
            }
            sw.Close();              
        }
        
        //读取文档
        public  List<string> ReadTxt(string filename)
        {           
            if (File.Exists(filename))
            {
                FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file, Encoding.Default);
                if (reader.Peek() > 0)
                {
                    string text = reader.ReadToEnd();
                    reader.Close();
                    List<string> list = TextProcess(text);
                    return list;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        //文档处理
        List<string> TextProcess(string text)
        {
            List<string> article = new List<string>();
            string text2="";
            text = text.Replace(" ", "").Replace("\n", "");
            string[] temp = text.Split('第');
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].IndexOf("章") < 3)
                {
                    temp[i] = "\n" + "\r" + "第" + temp[i].Replace("章", "章" + "\r" + "\n");
                }
                else
                {
                    temp[i] = "第" + temp[i];
                }
            }
            foreach (string str in temp)
            {
                text2 += str;
            }
            int wordnum = 600;
            if (text2.Length > wordnum)
            {
                for (int i = 0; i <= text2.Length - wordnum; i += wordnum)
                {
                    article.Add(text2.Substring(i, wordnum));
                }
            }
            else
            {
                article.Add(text2);
            }
            return article;
        }
        //保存最后阅读
        public void SaveLastRead(string filename,int page)
        {
            Init(filename);
            StreamWriter sw = new StreamWriter(filename, true);
            sw.WriteLine(page);
            sw.Close();
        }

        //载入最后阅读
        public int LoadLastRead(string filename)
        {
            int n;
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename, true);
                string lst = sr.ReadLine();
                sr.Close();
                int.TryParse(lst, out n);
                if (n > 0)    //&& n < textBox1.Text.Length
                {
                    return n;
                }
            }
            return 0;
        }

        //获取目录
        public List<string> GetCatalog(string filename)
        {
            FileStream file2 = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader reader2 = new StreamReader(file2, Encoding.Default);
            List<string> text = new List<string>();
            List<string> chapter = new List<string>();
            while (reader2.Peek() > 0)
            {
                text.Add(reader2.ReadLine());
            }
            file2.Close();
            reader2.Close();
            foreach (string s in text)
            {
                if (s.Contains("第") && s.Contains("章") && s.Length < 20)
                {
                    chapter.Add(s);
                }
            }
            return chapter;
        }

        //目录跳转
        public int CatalogJump(List<string> article,TreeView view)
        {
            int page=0;
                for (int i = 0; i < article.Count; i++)
                {
                    if (article[i].Contains(view.SelectedNode.Text))
                    {                        
                        page = i;
                    }
                }
                return page;
        }

        //清除阅读历史
        public void DeleteReadHistory()
        {            
            StreamReader sr = new StreamReader(readhistory, true);
            List<string> rh = new List<string>();
            while (sr.Peek() > 0)
            {
                rh.Add(sr.ReadLine());
            }
            sr.Close();
            FileStream fs2 = new FileStream(readhistory, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs2);
            rh.Clear();           
            foreach (string read in rh)
            {
                sw.WriteLine(read);
            }
            sw.Close();                 
        }

       
    }
}
