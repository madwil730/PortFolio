using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Text;
using System.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
public class addObject : MonoBehaviour
{

	public GameObject[] ob;
	UdpClient Client;
	byte[] mac; //mac주소  쓰면됨.
	public string str;
	string fullPath;
	public Text text;



	XmlDocument xml;
	string[] active = new string[15];
	int x = 0;

	private void Start()
	{
		fullPath = Application.streamingAssetsPath + str;

		xml = new XmlDocument();
		xml.Load(fullPath);

		for (int i = 0; i < active.Length; i++)
		{
			active[i] = xml.SelectSingleNode("powerManager/pc" + (i + 1) + "/active").InnerText;
			if (active[i] == "true")
			{
				ob[i].SetActive(true);
				x++;
			}
		}
	}

	public void add()
	{
		Debug.Log(str + "  " + x);
		if(x < ob.Length)
		{
			ob[x].SetActive(true);
			xml.SelectSingleNode("powerManager/pc"+(x+1)+"/active").InnerText = "true";
			xml.Save(fullPath);
			x++;
			
		}
	}

	public void delete()
	{
		if(x > 0)
		{
			//Debug.Log(str + "  " + x);
			ob[x-1].SetActive(false);
			xml.SelectSingleNode("powerManager/pc" + (x) + "/active").InnerText = "false";
			xml.Save(fullPath);
			x--;
		//Debug.Log("delete " + i);
		}

		if (x <3)
		{
			xml.SelectSingleNode("powerManager/pc5/active").InnerText = "false";
			xml.Save(fullPath);
		}
	}

	//-----------------------------------------------------------------------------

	private void WakeUp(byte[] mac)
	{

		byte[] packet = new byte[17 * 6];

		for (int i = 0; i < 6; i++)
		{
			packet[i] = 0xFF;
		}

		for (int i = 1; i <= 16; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				packet[i * 6 + j] = mac[j];
			}
		}
		Client.Send(packet, packet.Length);
		Debug.Log("wol start");
	}

	

	// 방법 1
	public static byte[] ConvertHexStringToByte(string convertString)
	{
		byte[] convertArr = new byte[convertString.Length / 2];

		for (int i = 0; i < convertArr.Length; i++)
		{
			convertArr[i] = Convert.ToByte(convertString.Substring(i * 2, 2), 16);
		}
		return convertArr;
	}

	// 방법 2
	public static byte[] StringToByteArray(string hex)
	{
		return Enumerable.Range(0, hex.Length)
						 .Where(x => x % 2 == 0)
						 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
						 .ToArray();
	}



	public void ALLWol()
	{
		//for (int i = 0; i < ob.Length; i++)
		//{
		//	if (ob[i].activeSelf)
		//	{
		//		Ping ping = new Ping(ob[i].GetComponent<ReadSerial>().Tip.text);

		//		if (!ping.isDone)
		//		{
		//			mac = StringToByteArray(ob[i].GetComponent<ReadSerial>().Tip.text);
		//			Client = new UdpClient();
		//			Client.Connect(IPAddress.Broadcast, 40000);
		//			WakeUp(mac);
		//			//text.text = "ALlWol";
		//		}
		//	}
		//}
	}

	public void Alloff()
	{
		for (int i = 0; i < ob.Length; i++)
		{
			off(i);
		}
	}

	IEnumerator off(int i)
	{
		Ping ping = new Ping(ob[i].GetComponent<ReadSerial>().Tip.text);

		yield return new WaitForSeconds(0.2f);
		if (ping.isDone)
		{
			string strCmdText;
			strCmdText = @"/k net use \\" + ob[i].GetComponent<ReadSerial>().Tip.text +
				@" && shutdown /s /m \\" + ob[i].GetComponent<ReadSerial>().Tip.text + "&& taskkill /im cmd.exe";   //This command to open a new notepad
			System.Diagnostics.Process.Start("CMD.exe", strCmdText); //Start cmd process
																	 //text.text = "All off";
		}
	}

}
