using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisPlay : MonoBehaviour
{

	public GameObject slider;
	public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < Display.displays.Length; i++)
		{
			//Debug.Log(i);
			Display.displays[i].Activate();
		}
		//Display.displays[1].Activate();
    }

	private void Update()
	{
		//아두이노
		if (ReadSerial.SerialPump.IsOpen)
		{
			//if (ReadSerial.PumpCount > 13)
			//	fire.GetComponent<Animator>().Play("Display_Fire2");

			//else if (ReadSerial.PumpCount > 3)
			//	fire.GetComponent<Animator>().Play("Display_Fire1");

			if (ReadSerial.PumpCount > 3)
				fire.GetComponent<Animator>().Play("Display_Fire1");

		}

		//키보드
		//if (slider.activeSelf)
		//{
		//	if (Input.GetKeyDown(KeyCode.Alpha1))
		//		fire.GetComponent<Animator>().Play("Display_Fire1");

		//	else if (Input.GetKeyDown(KeyCode.Alpha2))
		//		fire.GetComponent<Animator>().Play("Display_Fire2");
		//}

	}
}
