using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

using System.Threading;
using UnityEngine.UI;

public class ReadSerial : MonoBehaviour
{
	// /IPandMAC.xml
	public string fileName;
	public string str;
	string fullPath;

	XmlDocument xml;
	XmlNodeList nodes;

	public InputField Tname;
	public InputField Tmac;
	public InputField Tip;
	public InputField Ttext;


	private void Awake()
	{
		fullPath = Application.streamingAssetsPath + fileName;
		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("powerManager/" +str);

		foreach (XmlNode node in nodes)
		{
			Tname.text = node.SelectSingleNode("name").InnerText;
			Tmac.text = node.SelectSingleNode("mac").InnerText;
			Tip.text = node.SelectSingleNode("ip").InnerText;
			Ttext.text = node.SelectSingleNode("text").InnerText;
		}
	}


	//private void Update()
	//{
	//	if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I))
	//	{
	//		saveButton.SetActive(!saveButton.activeSelf);
	//	}
	//}

	public void save()
	{
		xml = new XmlDocument();
		xml.Load(fullPath);

		nodes = xml.SelectNodes("powerManager/" + str);


		foreach (XmlNode node in nodes)
		{
			node.SelectSingleNode("name").InnerText = Tname.text;
			node.SelectSingleNode("mac").InnerText = Tmac.text;
			node.SelectSingleNode("ip").InnerText = Tip.text;
			node.SelectSingleNode("text").InnerText = Ttext.text;
		}

		xml.Save(fullPath);
		Debug.Log("Data Save");
	}
}




















//// 루트노드
//XmlNode root = xdoc.CreateElement("Employees");
//xdoc.AppendChild(root);

//// Employee#1001
//XmlNode emp1 = xdoc.CreateElement("Employee");
//XmlAttribute attr = xdoc.CreateAttribute("Id");
//attr.Value = "1001";
//emp1.Attributes.Append(attr);

//XmlNode name1 = xdoc.CreateElement("Name");
//name1.InnerText = "Tim";
//emp1.AppendChild(name1);

//XmlNode dept1 = xdoc.CreateElement("Dept");
//dept1.InnerText = "Sales";
//emp1.AppendChild(dept1);

//root.AppendChild(emp1);