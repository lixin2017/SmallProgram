using System;
using System.IO;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
class DownloadImages
{
    public static void  Main(string [] args)
    {
        string pattern = @"(<)(.[^<]*)((src|href)=)('|""| )?(?<fileUrl>(.[^'|\s|""]*)(\.)(jpg|gif|png|bmp|jpeg|swf))('|""|\s|>)(.[^>]*)(>)";
 
        string pageUrl = @"http://www.pku.edu.cn/";
        if (args !=null && args.Length>1)
        {
            pageUrl =args[0];
        }
 
        pageUrl = pageUrl.Replace('\\','/' );
        int p = pageUrl.LastIndexOf('/');
        string urlPath = pageUrl.Substring(0, p+1);
        int ph = pageUrl.IndexOf( '/', pageUrl.IndexOf( '/')+2 );
        string urlHost = pageUrl.Substring(0, ph);
 
        string pageContent = DownOnePage( pageUrl );
        Console.WriteLine( pageContent );
        
        Regex  rx = new Regex( pattern, RegexOptions.IgnoreCase);
         
        MatchCollection mc = rx.Matches(pageContent);
        Console.WriteLine("��{0}��ƥ��", mc.Count);
        foreach ( Match mt in mc )
        {
            string fileUrl = mt.Result("${fileUrl}");
             
            fileUrl = fileUrl.Replace('\\', '/' );
            if( fileUrl.IndexOf(':') < 0 ) //������Э����
            {
                if( fileUrl[0]== '/' ) //���Ǿ���·��
                    fileUrl = urlHost + fileUrl;
                else    //�������·��
                    fileUrl = urlPath + fileUrl;
            }
            Console.WriteLine( fileUrl );
             
            int p2 = fileUrl.LastIndexOf('/');
            string fileName = fileUrl.Substring(p2+1);
 
            DownOneFile( fileUrl, fileName);
 
        }   
 
    }
 
    static void DownOneFile(string url, string fileName)
    {
        WebClient client = new WebClient();
        client.DownloadFile(url, fileName);
    }
 
    static string DownOnePage(string url)
    {
        WebClient client = new WebClient();
        //string pageHtml = client.DownloadString(url);
         
        byte[] pageData = client.DownloadData(url);
        //string pageHtml = Encoding.Default.GetString(pageData);
        string pageHtml = Encoding.UTF8.GetString(pageData);
        return pageHtml;
    }
     
}
