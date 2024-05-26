using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


/// <summary>
/// 아두이노 T5
/// </summary>
public class ReadOnOff : MonoBehaviour
{
	string fullPath = Application.streamingAssetsPath + "/OnOff.xml";

	XmlDocument xml;
	
	public static string Port;
	public static int BaudRate;

	private void Awake()
	{
		xml = new XmlDocument();
		xml.Load(fullPath);

		XmlNode node = xml.SelectSingleNode("arduino");

		Port = node.SelectSingleNode("port").InnerText;
		BaudRate = int.Parse(node.SelectSingleNode("baudRate").InnerText);
		


	
	}
}
