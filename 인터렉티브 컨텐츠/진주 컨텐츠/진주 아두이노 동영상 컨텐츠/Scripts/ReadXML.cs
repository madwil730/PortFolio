using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;


/// <summary>
/// 아두이노 레이저
/// </summary>
public class ReadXML : MonoBehaviour
{
	string fullPath = Application.streamingAssetsPath + "/arduinoXML.xml";

	XmlDocument xml;
	XmlNodeList nodes;

	public static string port1;
	public static string port2;
	public static int baudRate1;
	public static int baudRate2;
	public static float timer;
	public static int distance;
	public static float slider1;
	public static float slider2;
	public static float slider3;
	public static float slider4;
	public static float slider5;
	public static float startTime;
	public static float rectScaleX;
	public static float rectScaleY;
	public static int width;
	public static int height;




	// Start is called before the first frame update
	private void Awake()
	{
		

		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("arduino/Config");

		foreach (XmlNode node in nodes)
		{

			port1 = node.SelectSingleNode("port1").InnerText;
			port2 = node.SelectSingleNode("port2").InnerText;
			baudRate1 = int.Parse(node.SelectSingleNode("baudRate1").InnerText);
			baudRate2 = int.Parse(node.SelectSingleNode("baudRate2").InnerText);
			timer = float.Parse(node.SelectSingleNode("timer").InnerText);
			distance = int.Parse(node.SelectSingleNode("distance").InnerText);
			slider1 = float.Parse(node.SelectSingleNode("slider1").InnerText);
			slider2 = float.Parse(node.SelectSingleNode("slider2").InnerText);
			slider3 = float.Parse(node.SelectSingleNode("slider3").InnerText);
			slider4 = float.Parse(node.SelectSingleNode("slider4").InnerText);
			slider5 = float.Parse(node.SelectSingleNode("slider5").InnerText);
			startTime = float.Parse(node.SelectSingleNode("startTime").InnerText);
			width = int.Parse(node.SelectSingleNode("width").InnerText);
			height = int.Parse(node.SelectSingleNode("height").InnerText);
			rectScaleX = float.Parse(node.SelectSingleNode("rectScaleX").InnerText);
			rectScaleY = float.Parse(node.SelectSingleNode("rectScaleY").InnerText);
			
		}
		//Debug.Log(ip);

		Screen.SetResolution(width, height, true, 60);
		Screen.fullScreen = true;
	}

}
