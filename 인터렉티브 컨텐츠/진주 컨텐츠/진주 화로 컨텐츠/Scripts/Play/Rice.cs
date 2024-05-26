using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class Rice : MonoBehaviour
{

	public Slider slider;

	AudioSource audio;
	AudioSource audio2;
	public AudioClip[] clip;
	[SerializeField] Text text;


	bool up;
	int b;
	private void OnEnable()
	{
		up = false;
	}

	// Start is called before the first frame update
	void Start()
    {
		audio = this.gameObject.GetComponent<AudioSource>();
		audio2 = this.gameObject.GetComponent<AudioSource>();
	
	}

	void Update()
    {
		slider.value -= 1 * Time.deltaTime; // 계속 아래로 내려감

		//아두이노 버전
		if (ReadSerial.SerialPump.IsOpen)
		{


			if (Timer.timeResult || Rice_Check.Result)
				return;

			if (!Rice_Check.Stage1 && ReadSerial.PumpCount > ReadSerial.Exception)
			{
				//audio.Stop();
				audio2.Stop();
				audio.PlayOneShot(clip[0]);
				up = true;
				//b = 0;
			}

			else if (!Rice_Check.Stage1 && ReadSerial.PumpCount == 0 && up)
			{

				audio2.clip = clip[2];
				audio2.Play();
				up = false;
				//b = 0;
			}

			else if (Rice_Check.Stage1 && ReadSerial.PumpCount > ReadSerial.Exception)
			{
				audio.clip = clip[1];
				audio.Play();
			}


			slider.value += (ReadSerial.PumpCount) * Time.deltaTime; //  펌프 압력을 더해줌

			text.text = ReadSerial.PumpCount.ToString();

			if (Input.GetKeyDown(KeyCode.T))
				text.enabled = !text.enabled;
		}


		//키보드 버전

		//if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//	if (Timer.timeResult || Rice_Check.Result)
		//		return;

		//	if (!Rice_Check.Stage1)
		//	{
		//		audio.PlayOneShot(clip[0]);
		//		b = 0;
		//	}


		//	else if (Rice_Check.Stage1)
		//	{

		//		audio.clip = clip[1];
		//		audio.Play();

		//	}

		//	slider.value += 0.8f;
		//}
		//else if (Input.GetKeyUp(KeyCode.Alpha1) && !Rice_Check.Stage1)
		//{
		//	audio.clip = clip[2];
		//	audio.Play();
		//	b = 2;
		//}

		//if (b == 2 && slider.value == 0)
		//{
		//	audio.Stop();
		//}
	}
}
