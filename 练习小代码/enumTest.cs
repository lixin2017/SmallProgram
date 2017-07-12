using System;
enum LightColor
{
	Red,
	Yellow,
	Green
}

class TrafficLight
{
	public static void WhatInfo(LightColor color){
		switch(color){
			case LightColor.Red:
				Console.WriteLine("Stop!");
				break;
			case LightColor.Green:
				Console.WriteLine("Go!");
				break;
			case LightColor.Yellow:
				Console.WriteLine("Warning");
				break;
			default:
				break;
		}
	}
}

class Test
{
	static void Main(){
		LightColor c=LightColor.Red;
		Console.WriteLine(c.ToString());
		TrafficLight.WhatInfo(c);
		TrafficLight.WhatInfo(LightColor.Red);
	}
}