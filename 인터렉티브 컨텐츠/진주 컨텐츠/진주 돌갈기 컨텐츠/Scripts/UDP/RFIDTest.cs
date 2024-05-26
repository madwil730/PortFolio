using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;


public class RFIDTest : MonoBehaviour
{

	public GameObject offimage;
	public GameObject[] onimage;
	public GameObject GlowText;
	public Sprite sprite;

	int a, b;

	private void Update()
	{
		if (UDPReceiveManager.check || Input.GetKeyDown(KeyCode.P))
		{
			GlowText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -309, 0);
			GlowText.GetComponent<RectTransform>().sizeDelta = new Vector2(1124, 220);
			GlowText.GetComponent<Image>().sprite = sprite;
			GlowText.GetComponent<Image>().color = new Color(1, 1, 1, 1);


			for (int i = 0; i < onimage.Length; i++)
			{
				onimage[i].SetActive(true);
			}

			offimage.SetActive(false);


			TutorialScript.First = true;
			TutorialScript.Second = true;
		}

		//if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//	GlowText.GetComponent<Animator>().Play("RFID_text");
		//}

		// 아두이노 버전 버튼

		if(ReadSerial.SerialRail.IsOpen)
		{
			if (ReadSerial.ButtonString == "OK")
			{
				GlowText.GetComponent<Animator>().Play("RFID_text");
			}
		}
		


		// 아두이노 버전 레일

		//if (ReadSerial.SerialRail.IsOpen)
		//{
		//	if (ReadSerial.RailCount > a || ReadSerial.RailCount < a)
		//	{
		//		GlowText.GetComponent<Animator>().Play("RFID_text");
		//	}

		//	a = ReadSerial.RailCount;
		//}
	}
	
}
