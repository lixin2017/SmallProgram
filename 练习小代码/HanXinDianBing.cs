using System;
class HanXin
{
	static void Main()
	{
		for(int n=0;n<106;n++)
		{
			if(n%3==2&&n%4==1&&n%12==5)
				Console.Write(n+" ");
		}
	}
}