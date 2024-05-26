using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Xml;
using System.Threading;
using XDPaint.Demo;
using UnityEngine.UI;
using System;

public class Serial : MonoBehaviour
{

	public static SerialPort serialPort_1;


	string fullPath = Application.streamingAssetsPath + "/Serial.xml";
	XmlDocument xml;
	XmlNodeList nodes;

	string Port1;
	int BaudRate1;
	public static float hardness;
	public static float alpha;

	public Text text ;


	public static string str;
	public static string threadCheck;
	public static bool check;

	Thread _SerialThread1;


	private void Awake()
	{
		str = "none";
		threadCheck = "not On";

		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("Data/RfidConfig");

		foreach (XmlNode node in nodes)
		{
			Port1 = node.SelectSingleNode("COMPort1").InnerText;
			BaudRate1 = int.Parse(node.SelectSingleNode("Baudrate1").InnerText);
			hardness = float.Parse(node.SelectSingleNode("hardness").InnerText);
			alpha = float.Parse(node.SelectSingleNode("alpha").InnerText);
		}

		//-----------------------------------------

		//Debug.Log(hardness);

		//StartCoroutine(sleep());
		//serialPort_1 = new SerialPort(Port1, BaudRate1, Parity.None, 8);
		//serialPort_1.ReadTimeout = 100;
		//serialPort_1.DtrEnable = true;
		//serialPort_1.Open();

		//check = serialPort_1.IsOpen;

		//ThreadStart ts1 = new ThreadStart(SerialReceiverThread_1);
		//_SerialThread1 = new Thread(ts1);
		//_SerialThread1.Start();

	}

	private void SerialReceiverThread_1()
	{
		while (true)
		{
			//Thread.Sleep(1000);

			try
			{
				str = serialPort_1.ReadLine();
				
			}
			catch(Exception e)
			{
				//str = e.ToString();
			}

			threadCheck = "is on";
		}
	}




	//private void OnApplicationQuit()
	//{
	//	_SerialThread1.Interrupt();
	

	//	_SerialThread1.Abort();
	
	//}
}
