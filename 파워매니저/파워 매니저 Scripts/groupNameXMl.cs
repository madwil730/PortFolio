using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

using System.Threading;
using UnityEngine.UI;

public class groupNameXMl : MonoBehaviour
{


	string fullPath;

	XmlDocument xml;
	XmlNodeList nodes;
	public Text[] text;


	private void Awake()
	{
		fullPath = Application.streamingAssetsPath + "/GroupName.xml";
		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("powerManager");

		foreach (XmlNode node in nodes)
		{
			text[0].text = node.SelectSingleNode("GroupName1").InnerText;
			text[1].text = node.SelectSingleNode("GroupName2").InnerText;
			text[2].text = node.SelectSingleNode("GroupName3").InnerText;
			text[3].text = node.SelectSingleNode("GroupName4").InnerText;
		}
	}
}
