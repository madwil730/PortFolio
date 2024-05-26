using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO.Ports;
using System.Threading;

public class Read : MonoBehaviour
{
	string fullPath = Application.streamingAssetsPath + "/Rfid.xml";

	XmlDocument xml;
	XmlNodeList nodes;

	public static string COMPort;
	public static int Baudrate;
	public static string TargetIP;
	public static int TargetPort;
	public static int ReceivePort;
	public static string index;
	public static string str;



	// Start is called before the first frame update
	private void Awake()
	{
		Screen.SetResolution(1920, 1080, true);

		xml = new XmlDocument();
		xml.Load(fullPath);
		nodes = xml.SelectNodes("Data/RfidConfig");

		foreach (XmlNode node in nodes)
		{

			COMPort = node.SelectSingleNode("COMPort").InnerText;
			Baudrate = int.Parse(node.SelectSingleNode("Baudrate").InnerText);
			TargetIP = node.SelectSingleNode("TargetIP").InnerText;
			TargetPort = int.Parse(node.SelectSingleNode("TargetPort").InnerText);
			ReceivePort = int.Parse(node.SelectSingleNode("ReceivePort").InnerText);
			index = node.SelectSingleNode("Index").InnerText;


		}

	}

	private void Start()
	{
		
	}
}
