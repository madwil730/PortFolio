using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class arduino2 : MonoBehaviour
	{

		SerialPort serial;  //아두이노 레이저 임구
		SerialPort serial2; // 아두이노 레이저 출구
		SerialPort serial3 ; // 아두이노 T5

		public GameObject obj;

		public VCR vcr;
		public Slider slider;
		public Text[] text;
		[SerializeField] RectTransform rect;

		Thread _SerialThreadCOM9;
		Thread _SerialThreadCOM1;

		enum Status { Default, IN, OUT }
		Status _Status = Status.Default;
		
		bool First = false;
		bool Second = false;
		bool StartCheck = false;

		float time;

		bool[] sliderCheck = { false, false, false, false, false };
		bool check, check2, movie;

		int a, b;
		// Start is called before the first frame update
		void Start()
		{
			serial = new SerialPort(ReadXML.port1, ReadXML.baudRate1);
			serial.Open();

			serial2 = new SerialPort(ReadXML.port2, ReadXML.baudRate2);
			serial2.Open();

			serial3 = new SerialPort(ReadOnOff.Port, ReadOnOff.BaudRate);
			serial3.Open();

			ThreadStart tsSerial = new ThreadStart(ReadCOM9);
			_SerialThreadCOM9 = new Thread(tsSerial);
			_SerialThreadCOM9.Start();

			ThreadStart tsSerial2 = new ThreadStart(ReadCOM1);
			_SerialThreadCOM1 = new Thread(tsSerial2);
			_SerialThreadCOM1.Start();
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
				obj.SetActive(!obj.activeSelf);

			try
			{
				if(serial.IsOpen && serial2.IsOpen)
				{
					//Debug.Log("A : " + a); // com9
					//Debug.Log("B : " + b); // com1

					
					checks(a, b);
				}
						
			}
			catch
			{
				Debug.Log("TimeOut");
			}

			//Debug.Log(_Status);
			text[0].text = _Status.ToString();
			text[1].text = "First : "+First.ToString();
			text[2].text = "Second : "+Second.ToString();
		
			//if(slider.value > 0.1f)
			//	_Status = Status.Default;

			if (slider.value == 0)
			{
				sliderCheck[0] = false;
				sliderCheck[1] = false;
				sliderCheck[2] = false;
				sliderCheck[3] = false;
				sliderCheck[4] = false;
			}
			
	
			if (slider.value == 1)
			{
				check2 = false;
				movie = false;
				StartCheck = false;
			}
				

			//Debug.Log("Check : " + check);
		}

		// 검사는 업데이트에서 하기
		public void checks(int serial1 , int serial2 )
		{
			if (serial1 < ReadXML.distance && serial1 != 0  && !movie)
			{
				
				First = true;

				if (_Status.Equals(Status.Default) )
				{
					_Status = Status.IN;
				}
			}
			else
			{
				First = false; //  false 안하면 체크 꼬임 
			}

			if (serial2 < ReadXML.distance && serial2 != 0 && !movie)
			{
				Second = true;

				if (_Status.Equals(Status.Default) )
				{
					_Status = Status.OUT;
				}
			}
			else
			{
				Second = false;
			}

			if (First && Second)
			{
				if (_Status.Equals(Status.IN))
				{
					// 입장
					//vcr.OnPlayButton();
					if (!StartCheck)
					{
						StartCoroutine(timer());
						Screen.SetResolution(ReadXML.width, ReadXML.height, true, 60);
						rect.transform.localScale = new Vector2(ReadXML.rectScaleX, ReadXML.rectScaleY);

						StartCheck = true;
					}
					

					if(!check2)
					{
						check2 = true;
						serial3.Write("OFF\n");
			
						//Debug.Log(check2);
					}

					movie = true;
					//_Status = Status.Default;
					//text.text = "Serial Start , serial.Write(OFF) 한번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;
				}
				else if (_Status.Equals(Status.OUT))
				{
					// 퇴장
				}
			}

			// 3 초 뒤에 defalut
			if (!First && !Second)
			{
				time += Time.deltaTime;
				//Debug.Log(ReadXML.timer);
				if(time > ReadXML.timer)
				{
					time = 0;
					_Status = Status.Default;
				}
	
			}

			//노란색 조명 1
			if (vcr._videoSeekSlider.value > ReadXML.slider1 && !sliderCheck[0])
			{
				serial3.Write("1\n");
				sliderCheck[0] = true;
			}
			//노란색 조명 2
			if (vcr._videoSeekSlider.value > ReadXML.slider2 && !sliderCheck[1])
			{
				serial3.Write("2\n");
				sliderCheck[1] = true;
			}
			//노란색 조명 3
			if (vcr._videoSeekSlider.value > ReadXML.slider3 && !sliderCheck[2])
			{
				serial3.Write("3\n");
				sliderCheck[2] = true;
			}
			//노란색 조명 4
			if (vcr._videoSeekSlider.value > ReadXML.slider4 && !sliderCheck[3])
			{
				serial3.Write("4\n");
				sliderCheck[3] = true;
			}
			//하양색 조명
			if (vcr._videoSeekSlider.value > ReadXML.slider5 && !sliderCheck[4])
			{
					serial3.Write("ON\n");
					sliderCheck[4] = true;
			}
			
		} 


		void ReadCOM9()
		{
			while(true)
			{
				try
				{
					if (serial.IsOpen)
					{
						int.TryParse(serial.ReadLine(), out a);

						//if (int.TryParse(serial.ReadLine(), out a))
						//{
						//	Debug.Log("A :" + a);
						//}
					}
				}
				catch
				{
					Debug.Log("TimeOut");
				}
			}
		}

		void ReadCOM1()
		{
			while(true)
			{
				try
				{
					if (serial2.IsOpen)
					{
						int.TryParse(serial2.ReadLine(), out b);

						//if (int.TryParse(serial2.ReadLine(), out b))
						//{
						//	Debug.Log("B : " + b);
						//}
					}
				}
				catch
				{
					Debug.Log("TimeOut");
				} 
			}
		}


		IEnumerator timer()
		{
			yield return new WaitForSeconds(ReadXML.startTime);
			vcr.OnPlayButton();
		}

		private void OnApplicationQuit()
		{
			if (_SerialThreadCOM1 != null)
			{
				_SerialThreadCOM1.Interrupt();
				_SerialThreadCOM1.Abort();
			}

			if (_SerialThreadCOM9 != null)
			{
				_SerialThreadCOM9.Interrupt();
				_SerialThreadCOM9.Abort();
			}
		}
	}
}

	
