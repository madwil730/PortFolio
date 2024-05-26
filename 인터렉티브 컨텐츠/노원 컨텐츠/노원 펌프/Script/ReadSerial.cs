using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Xml;
using System.Threading;


public class ReadSerial : MonoBehaviour
{
    SerialPort SerialPump1;
    SerialPort SerialPump2;

	string fullPath = Application.streamingAssetsPath + "/Serial.xml";
	XmlDocument xml;
	XmlNodeList nodes;

	string Port1;
	int BaudRate1;
	string Port2;
	int BaudRate2;

	public static int Count1;
	public static int Count2;

	public static bool check1;
	public static bool check2;

	Thread _SerialThread1;
	Thread _SerialThread2;

	private void Awake()
	{
		Count1 = 0;
		Count2 = 0;
		Screen.SetResolution(1920, 1080, true);

		// 화덕 펌프 아두이노
		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("Data/RfidConfig");

		foreach (XmlNode node in nodes)
		{
			Port1 = node.SelectSingleNode("COMPort1").InnerText;
			BaudRate1 = int.Parse(node.SelectSingleNode("Baudrate1").InnerText);
			Port2 = node.SelectSingleNode("COMPort2").InnerText;
			BaudRate2 = int.Parse(node.SelectSingleNode("Baudrate2").InnerText);
		}

		//-----------------------------------------

		SerialPump1 = new SerialPort(Port1, 9600);
		SerialPump2 = new SerialPort(Port2, 9600);

		SerialPump1.Open();
		SerialPump2.Open();

		ThreadStart ts = new ThreadStart(SerialReceiverThread1);
		_SerialThread1 = new Thread(ts);
		_SerialThread1.Start();

		ThreadStart ts2 = new ThreadStart(SerialReceiverThread2);
		_SerialThread2 = new Thread(ts2);
		_SerialThread2.Start();
	}

	private void SerialReceiverThread1()
	{
		while (true)
		{
			if (SerialPump1.IsOpen)
				int.TryParse(SerialPump1.ReadLine(), out Count1);

			check1 = SerialPump1.IsOpen;

		}

	}

	private void SerialReceiverThread2()
	{
		while (true)
		{
			if (SerialPump2.IsOpen)
				int.TryParse(SerialPump2.ReadLine(), out Count2);

			check2 = SerialPump2.IsOpen;
		}

	}

	private void OnApplicationQuit()
	{
		_SerialThread1.Interrupt();
		_SerialThread2.Interrupt();

		_SerialThread1.Abort();
		_SerialThread2.Abort();
	}
}
