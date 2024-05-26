using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class Disturbance : MonoBehaviour
{
	public GameObject[] disturbanceUI;
	public GameObject[] IconAnimation;
	public GameObject[] IconUI;
	public GameObject water;
	public GameObject LoopingUI;
	public Slider slider;

	Vector3 vec = new Vector3(6, 0, 0);
	Vector3 vecOrigin = new Vector3(-348, -258.2f, -2);
	AudioSource audio;
	AudioSource audio2;
	public AudioClip[] clip;

	Color color;

	string str;
	int num = 0 ;
	float time;
	bool timeCheck;

	int[] RandomWater;

	// Start is called before the first frame update
	void Start()
	{
		
		audio = gameObject.GetComponent<AudioSource>();
		audio2 = gameObject.GetComponent<AudioSource>();
		color = new Color(1, 1, 1, 1);

		setint();
		if (num == 0)
			RandomWater = new int[3] { 0, 1, 2 };
		if (num == 1)
			RandomWater = new int[3] { 1, 0, 2 };
		if (num == 2)
			RandomWater = new int[3] { 2, 1, 0 };

	}

	int a;
	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		//Debug.Log(ReadSerial.ButtonString);

		if (time > 1 )
		{
			if (a > 2)
			{
				setint();
				if (num == 0)
					RandomWater = new int[3] { 0, 1, 2 };
				if (num == 1)
					RandomWater = new int[3] { 1, 0, 2 };
				if (num == 2)
					RandomWater = new int[3] { 2, 1, 0 };
				a = 0;
			}

			if (disturbanceUI[RandomWater[a]].transform.localPosition.x < -334 )
			{
				disturbanceUI[RandomWater[a]].transform.localPosition += vec * Time.deltaTime;
				// 아이콘 히트 못하면 사라짐
				if (disturbanceUI[RandomWater[a]].transform.localPosition.x > -336)
				{
					if (color.a >= 0)
					{
						color.a -= 2 * Time.deltaTime;
						disturbanceUI[RandomWater[a]].GetComponent<SpriteRenderer>().color = color;
					}
				}
			}
			else
			{
				disturbanceUI[RandomWater[a]].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); color.a = 1;
				disturbanceUI[RandomWater[a]].transform.localPosition = vecOrigin;
				

				a += 1;
			}

			if (a > 2)
			{
				setint();
				if (num == 0)
					RandomWater = new int[3] { 0, 1, 2 };
				if (num == 1)
					RandomWater = new int[3] { 1, 0, 2 };
				if (num == 2)
					RandomWater = new int[3] { 2, 1, 0 };
				a = 0;
			}

			if (disturbanceUI[RandomWater[a]].transform.localPosition.x < -335 && disturbanceUI[RandomWater[a]].transform.localPosition.x > -340)
			{
				//버튼 아두이노
				if (ReadSerial.SerialRail.IsOpen)
				{

					if (ReadSerial.ButtonString == "OK")
						str = "OK";

					//Debug.Log(str);
					if (str == "OK")
					{


						disturbanceUI[RandomWater[a]].transform.localPosition = vecOrigin;

						if (RandomWater[a] == 0)
						{
							StartCoroutine(water_iconCheck());
							slider.value += 1;


							agricultural_Rail.count += 1;

						}

						else if (RandomWater[a] == 1)
						{
							audio.PlayOneShot(clip[3]);
							StartCoroutine(disturbance_Fire());
							slider.value -= 1;

							if (agricultural_Rail.count > 0)
								agricultural_Rail.count -= 1;

						}

						else if (RandomWater[a] == 2)
						{
							audio.PlayOneShot(clip[3]);
							StartCoroutine(disturbance_Sand());
							slider.value -= 1;
							if (agricultural_Rail.count > 0)
								agricultural_Rail.count -= 1;

						}

						str = null;

						a += 1;
					}
				}

				//키보드
				//if (Input.GetKeyDown(KeyCode.Z)) // 이곳에 시리얼 통신으로 값 받아옴
				//{
				//	disturbanceUI[RandomWater[a]].transform.localPosition = vecOrigin;
				//	if (RandomWater[a] == 0)
				//	{
				//		StartCoroutine(water_iconCheck());
				//		slider.value += 1;
				//		agricultural_Rail.count += 1;

				//	}

				//	else if (RandomWater[a] == 1)
				//	{
				//		audio.PlayOneShot(clip[3]);
				//		StartCoroutine(disturbance_Fire());
				//		slider.value -= 1;
				//		agricultural_Rail.count -= 1;

				//	}

				//	else if (RandomWater[a] == 2)
				//	{
				//		audio.PlayOneShot(clip[3]);
				//		StartCoroutine(disturbance_Sand());
				//		slider.value -= 1;
				//		agricultural_Rail.count -= 1;

				//	}

				//	a += 1;
				//}
			}
		}
	}

	// 0 물, 1 불, 2 흙
	void setint()
	{
		num = Random.Range(0, 3);
	}



	// 물 아이콘 껐다 키기
	IEnumerator water_iconCheck()
	{

		//LoopingUI.SetActive(false);
		IconUI[0].SetActive(true);
		IconAnimation[0].GetComponent<Animator>().Play("Play_Water");
		audio.clip = clip[0];
		audio.Play();

		yield return new WaitForSeconds(1);

		IconUI[0].SetActive(false);
		LoopingUI.SetActive(true);

		
	}



	// 불 아이콘 껐다 키기
	IEnumerator disturbance_Fire()
	{
		//LoopingUI.SetActive(false);
		IconUI[1].SetActive(true);
		IconAnimation[1].GetComponent<Animator>().Play("Play_Fire");
		audio.clip = clip[1];
		audio.Play();
	
		yield return new WaitForSeconds(1);

		IconUI[1].SetActive(false);
		LoopingUI.SetActive(true);
		
	}


	// 모래 아이콘 껐다 키기
	IEnumerator disturbance_Sand()
	{
		//LoopingUI.SetActive(false);
		IconUI[2].SetActive(true);
		IconAnimation[2].GetComponent<Animator>().Play("Play_Sand");
		audio.clip = clip[2];
		audio.Play();


		yield return new WaitForSeconds(1);
		IconUI[2].SetActive(false);
		LoopingUI.SetActive(true);
		
	}
}
