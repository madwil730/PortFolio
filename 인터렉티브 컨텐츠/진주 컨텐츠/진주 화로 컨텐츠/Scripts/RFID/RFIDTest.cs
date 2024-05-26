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

	private void OnEnable()
	{
		Rice_Check.Result = false; // 밥짓기 결말 초기화
		Rice_Check.Stage1 = false; // 밥짓기 1단계 초기화
		Rice_Check.Stage2 = false; // 밥짓기 2단계 초기화
		Timer.timeResult = false;
	}

	private void Update()
	{
		
		// 아두이노 rfid
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

		//아두이노 펌프
		if (ReadSerial.SerialPump.IsOpen)
		{
			
			if (ReadSerial.PumpCount > 6)
				GlowText.GetComponent<Animator>().Play("RFID_text");
		}
	}
	
}
