using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Xml;
using System.Threading;


public class ReadSerial : MonoBehaviour
{
	public static SerialPort SerialPump;

	string fullPath = Application.streamingAssetsPath + "/Serial.xml";
	XmlDocument xml;

	string Port;
	int BaudRate;
	public static float Exception;
	public static int PumpCount;
	string check;


	string fullPath2 = Application.streamingAssetsPath + "/Rfid.xml";

	XmlDocument xml2;
	XmlNodeList nodes;

	public static string COMPort;
	public static int Baudrate;
	public static string TargetIP;
	public static int TargetPort;
	public static int ReceivePort;
	public static string index;
	public static string str;

	Thread _SerialThread;

	private void Awake()
	{
		Screen.SetResolution(1920, 1080, true);

		// 화덕 펌프 아두이노
		xml = new XmlDocument();
		xml.Load(fullPath);

		XmlNode node1 = xml.SelectSingleNode("arduino");

		Port = node1.SelectSingleNode("port").InnerText;
		BaudRate = int.Parse(node1.SelectSingleNode("baudrate").InnerText);
		Exception = float.Parse(node1.SelectSingleNode("exception").InnerText);
		check = node1.SelectSingleNode("check").InnerText;

		SerialPump = new SerialPort(Port, BaudRate);
		if(check == "true")
			SerialPump.Open();


		//-----------------------------------------

		// udp 통신
		xml2 = new XmlDocument();
		xml2.Load(fullPath2);
		nodes = xml2.SelectNodes("Data/RfidConfig");

		foreach (XmlNode node in nodes)
		{
			COMPort = node.SelectSingleNode("COMPort").InnerText;
			Baudrate = int.Parse(node.SelectSingleNode("Baudrate").InnerText);
			TargetIP = node.SelectSingleNode("TargetIP").InnerText;
			TargetPort = int.Parse(node.SelectSingleNode("TargetPort").InnerText);
			ReceivePort = int.Parse(node.SelectSingleNode("ReceivePort").InnerText);
			index = node.SelectSingleNode("Index").InnerText;
		}


		ThreadStart ts = new ThreadStart(SerialReceiverThread);
		_SerialThread = new Thread(ts);
		_SerialThread.Start();
	}

	private void SerialReceiverThread()
	{
		while(true)
		{
			if (SerialPump.IsOpen)
				int.TryParse(SerialPump.ReadLine(), out PumpCount);
		}
		
	}
}
