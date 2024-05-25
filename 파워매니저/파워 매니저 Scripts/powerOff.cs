using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
//using System.Diagnostics;
using UnityEngine.UI;


public class powerOff : MonoBehaviour
{

	// 192.168.0.162

	public InputField ip;
	public Text debug;
	float time;

	public void Off()
	{
		StartCoroutine(off());
		
	}

	IEnumerator off()
	{

		Ping ping = new Ping(ip.text);
		yield return new WaitForSeconds(0.2f);

		Debug.Log(ip.text);
		if (ping.isDone)
		{
			try
			{
				Debug.Log("off");
				string strCmdText;
				strCmdText = @"/k net use \\" + ip.text + @" && shutdown /s /m \\" + ip.text + "&& taskkill /im cmd.exe";   //This command to open a new notepad
				System.Diagnostics.Process.Start("CMD.exe", strCmdText); //Start cmd process
				debug.text = "power off start";
			}
			catch (Exception e)
			{
				debug.text = e.ToString();
			}
		}
		else
		{
			debug.text = ip.text + "  (off) is wrong ";
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T)) 
		{
			debug.gameObject.SetActive(!debug.gameObject.activeSelf);
			//Debug.Log(123);
		}
	}
}
