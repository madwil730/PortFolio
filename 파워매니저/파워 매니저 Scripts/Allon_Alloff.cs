using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Net;
using System.Net.Sockets;


public class Allon_Alloff : MonoBehaviour
{
	public GameObject[] ob;
	UdpClient Client;
	public Text text;

	byte[] mac; //mac주소  쓰면됨.


	public void ALLWol()
	{
		for(int i = 0; i < ob.Length; i ++)
		{
			if(ob[i].activeSelf)
			{
				Ping ping = new Ping(ob[i].GetComponent<ReadSerial>().Tip.text);

				if (!ping.isDone)
				{
					mac = StringToByteArray(ob[i].GetComponent<ReadSerial>().Tmac.text);
					Client = new UdpClient();
					Client.Connect(IPAddress.Broadcast, 40000);
					WakeUp(mac);
					text.text = "All on";
				}
			}
		}
	}

	public void Alloff()
	{
		for (int i = 0; i < ob.Length; i++)
		{
			Ping ping = new Ping(ob[i].GetComponent<ReadSerial>().Tip.text);
			//if (ping.isDone)
			{
				string strCmdText;
				strCmdText = @"/k net use \\" + ob[i].GetComponent<ReadSerial>().Tip.text + 
					@" && shutdown /s /m \\" + ob[i].GetComponent<ReadSerial>().Tip.text + "&& taskkill /im cmd.exe";   //This command to open a new notepad
				System.Diagnostics.Process.Start("CMD.exe", strCmdText); //Start cmd process
				text.text = "All off";
			}
		}
	}

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
}
