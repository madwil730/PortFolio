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
	string fullPath = Application.streamingAssetsPath + "/read.xml";

	XmlDocument xml;
	XmlNodeList nodes;

	public static string str;

	//QR 영역
	public int ColorUP_x1;
	public int ColorUP_y1;
	public int ColorUP_x2;
	public int ColorUP_y2;
	
	public int ColorDown_x1;
	public int ColorDown_y1;
	public int ColorDown_x2;
	public int ColorDown_y2;

	//페인팅 영역
	public int a_x1;
	public int a_x2;
	public int a_y1;
	public int a_y2;
	 
	public int b_x1;
	public int b_x2;
	public int b_y1;
	public int b_y2;

	public Slider slider;




	// Start is called before the first frame update
	private void Awake()
	{
		xml = new XmlDocument();
		xml.Load(fullPath);

		str = xml.SelectSingleNode("Address/printAddress").InnerText;
		ColorUP_x1 = int.Parse(xml.SelectSingleNode("Address/ColorUP_x1").InnerText);
		ColorUP_y1 = int.Parse(xml.SelectSingleNode("Address/ColorUP_y1").InnerText);
		ColorUP_x2 = int.Parse(xml.SelectSingleNode("Address/ColorUP_x2").InnerText);
		ColorUP_y2 = int.Parse(xml.SelectSingleNode("Address/ColorUP_y2").InnerText);
		ColorDown_x1 = int.Parse(xml.SelectSingleNode("Address/ColorDown_x1").InnerText);
		ColorDown_y1 = int.Parse(xml.SelectSingleNode("Address/ColorDown_y1").InnerText);
		ColorDown_x2 = int.Parse(xml.SelectSingleNode("Address/ColorDown_x2").InnerText);
		ColorDown_y2 = int.Parse(xml.SelectSingleNode("Address/ColorDown_y2").InnerText);
		a_x1 = int.Parse(xml.SelectSingleNode("Address/a_x1").InnerText);
		a_x2 = int.Parse(xml.SelectSingleNode("Address/a_x2").InnerText);
		a_y1 = int.Parse(xml.SelectSingleNode("Address/a_y1").InnerText);
		a_y2 = int.Parse(xml.SelectSingleNode("Address/a_y2").InnerText);
		b_x1 = int.Parse(xml.SelectSingleNode("Address/b_x1").InnerText);
		b_x2 = int.Parse(xml.SelectSingleNode("Address/b_x2").InnerText);
		b_y1 = int.Parse(xml.SelectSingleNode("Address/b_y1").InnerText);
		b_y2 = int.Parse(xml.SelectSingleNode("Address/b_y2").InnerText);


	}



	//IEnumerator check()
	//{
	//	Debug.Log(333);
	//	while(true)
	//	{
	//		if (slider.value > 0.4)
	//			break;

	//		yield return null;
	//	}

	//	Screen.SetResolution(1920, 1200, true);
	//	StartCoroutine(check());

	//}

}
