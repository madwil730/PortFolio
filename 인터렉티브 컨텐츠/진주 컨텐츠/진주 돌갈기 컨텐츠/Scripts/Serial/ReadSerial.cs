using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class ReadSerial : MonoBehaviour
{
	public static SerialPort SerialRail;

	string fullPath = Application.streamingAssetsPath + "/Serial.xml";

	XmlDocument xml;
	XmlNodeList nodes;

	string port;
	int Baudrate;
	public static int TopCheck;
	public static int BottomCheck;
	public static float Multiple;
	public static float Multiple2;
	public static float Multiple3;
	public static float Multiple4;
	public static float RockY;
	string check;

	public static float RailCount;
	public static string ButtonString;

	

	Thread _SerialThread;
	// Start is called before the first frame update

	private void Awake()
	{
		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("arduino/Config");

		foreach (XmlNode node in nodes)
		{
			port = node.SelectSingleNode("port").InnerText;
			Baudrate = int.Parse(node.SelectSingleNode("baudrate").InnerText);
			TopCheck = int.Parse(node.SelectSingleNode("TopCheck").InnerText);
			BottomCheck = int.Parse(node.SelectSingleNode("BottomCheck").InnerText);
			Multiple = float.Parse(node.SelectSingleNode("Multiplication").InnerText);
			Multiple2 = float.Parse(node.SelectSingleNode("Multiplication2").InnerText);
			Multiple3 = float.Parse(node.SelectSingleNode("Multiplication3").InnerText);
			Multiple4 = float.Parse(node.SelectSingleNode("Multiplication4").InnerText);
			RockY = float.Parse(node.SelectSingleNode("RockY").InnerText);
			check = node.SelectSingleNode("Check").InnerText;
		}

		SerialRail = new SerialPort(port, Baudrate);
		if(check == "true")
		SerialRail.Open();

		ThreadStart ts = new ThreadStart(SerialReceiverThread);
		_SerialThread = new Thread(ts);
		_SerialThread.Start();
	}

	private void SerialReceiverThread()
	{
		while(true)
		{
			if (SerialRail.IsOpen)
			{
				ButtonString = SerialRail.ReadLine();

				float.TryParse(ButtonString, out RailCount);
			
			}
		}
	}

	private void OnApplicationQuit()
	{
		_SerialThread.Interrupt();
		_SerialThread.Abort();
	}
}
