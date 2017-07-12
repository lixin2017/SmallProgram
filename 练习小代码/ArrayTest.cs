using System;
using System.Collections.Generic;
public class Test
{
	public static void Main(string [] args)
	{
		string [] ary={"Apple","pineapple","banana","cherry"};
		Show(ary);
		Array.Sort(ary);
		Show(ary);
		Array.Sort(ary,(a,b)=>a.Length-b.Length);
		Show(ary);

		int i=Array.BinarySearch(ary,"Pearl");
		Console.WriteLine(i);
		Array.Reverse(ary);
		Show(ary);

		int [] ary1={1,2,34,2,56,4,5,8,10,9};
		Show(ary1);
		Array.Sort(ary1);
		Show(ary1);
		
		List<int> number=new List<int>();
		number.Add(1);
		number.Add(8);
		number.Add(2);
		Show(number);
	}

	public static void Show(object [] ary)
	{
		foreach(object obj in ary)
			Console.Write(obj+" ");
		Console.WriteLine();
	}
	public static void Show(int [] ary)
	{
		foreach(int item in ary)
			Console.Write(item+" ");
		Console.WriteLine();
	}
	public static void Show(List<int> ary)
	{
		foreach(int item in ary)
			Console.Write(item+" ");
		Console.WriteLine();
	}
}