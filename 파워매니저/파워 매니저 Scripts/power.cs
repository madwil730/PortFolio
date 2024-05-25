using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Net;
using System.Net.Sockets;


public class power : MonoBehaviour
{

	UdpClient Client;
	public InputField ip;
	public InputField macs;
	public Text debug;

	byte[] mac; //mac주소  쓰면됨.
	float time;

	Ping ping;

	private void Start()
	{
		ping = new Ping(ip.text);
	}

	public void WOL()
	{
		
		if (!ping.isDone)
		{
			try
			{
				mac = StringToByteArray(macs.text);
				Debug.Log(macs.text);
				Client = new UdpClient();
				Client.Connect(IPAddress.Broadcast, 40000);
				WakeUp(mac);
				debug.text = "wol start";
			}

			catch(Exception e)
			{
				debug.text = e.ToString();
			}
		
		}
		else
		{
			debug.text = "(on) ip is wrong, Set the appropriate ip";
		}
	}


	private void WakeUp(byte[] mac)
	{
		//client.Connect(192.168.0.137, 40000);

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


	private void Update()
	{
		time += Time.deltaTime;

		if(time > 60)
		{
			if (!ping.isDone)
			{
				try
				{
					mac = StringToByteArray(macs.text);
					Debug.Log(macs.text);
					Client = new UdpClient();
					Client.Connect(IPAddress.Broadcast, 40000);
					WakeUp(mac);
					debug.text = "wol start";
				}

				catch (Exception e)
				{
					debug.text = e.ToString();
				}

			}
			else
			{
				debug.text = "(on) ip is wrong, Set the appropriate ip";
			}

			time = 0;
		}

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






