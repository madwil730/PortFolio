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
	string fullPath = Application.streamingAssetsPath + "/SettingInfo.xml";

	XmlDocument xml;
	XmlNodeList nodes;

	

	public static int minDetectPixel;
	public static float minDistance;
	public static float maxDistance;
	public static float Distance;
	public static bool OffsetCheck;
	public static int ParticleOffset_X;
	public static int ParticleOffset_Y;
	public static float alpha_PostionX;
	public static float alpha_PostionY;
	public static float alpha_ScaleX;
	public static float alpha_ScaleY;
	public static float InnerDistance;






	// Start is called before the first frame update
	private void Awake()
	{

		//Screen.SetResolution(1200, 1200, true);

		xml = new XmlDocument();
		xml.Load(fullPath);


		minDetectPixel = int.Parse(xml.SelectSingleNode("Setting/minDetectPixel").InnerText);
		minDistance = float.Parse(xml.SelectSingleNode("Setting/minDistance").InnerText);
		maxDistance = float.Parse(xml.SelectSingleNode("Setting/maxDistance").InnerText);
		Distance = float.Parse(xml.SelectSingleNode("Setting/Distance").InnerText);
		OffsetCheck = bool.Parse(xml.SelectSingleNode("Setting/OffsetCheck").InnerText);
		ParticleOffset_X = int.Parse(xml.SelectSingleNode("Setting/ParticleOffset_X").InnerText);
		ParticleOffset_Y = int.Parse(xml.SelectSingleNode("Setting/ParticleOffset_Y").InnerText);
		alpha_PostionX = float.Parse(xml.SelectSingleNode("Setting/alpha_PostionX").InnerText);
		alpha_PostionY = float.Parse(xml.SelectSingleNode("Setting/alpha_PostionY").InnerText);
		alpha_ScaleX = float.Parse(xml.SelectSingleNode("Setting/alpha_ScaleX").InnerText);
		alpha_ScaleY = float.Parse(xml.SelectSingleNode("Setting/alpha_ScaleY").InnerText);
		InnerDistance = float.Parse(xml.SelectSingleNode("Setting/InnerDistance").InnerText);



	}


}

//높이 설정  min 1, max 2.7 임  
// innerDistance 650임 , 최소 찾는 범위 70임