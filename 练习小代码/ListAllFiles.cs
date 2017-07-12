using System;
using System.IO;
using System.Collections.Generic;
class ListAllFiles
{
	public static void Main(string [] args)
	{
		ListFiles(new DirectoryInfo(@"D:\工作文档\项目"));
	}

	public static void ListFiles(FileSystemInfo info)
	{
		if(! info.Exists) return;
		//DirectoryInfo dir=info as DirectoryInfo;
		DirectoryInfo dir=(DirectoryInfo) info;
		if(dir==null) return;

		FileSystemInfo [] files =dir.GetFileSystemInfos();
		for(int i=0;i<files.Length;i++)
		{
			FileInfo file=files[i] as FileInfo;
			if(file!=null)
			{
				Console.WriteLine(file.Name+"\t"+file.Length+"\t"+file.LastWriteTime);
			}
			else
			{
				ListFiles(files[i]);
			}
		}
	}
}