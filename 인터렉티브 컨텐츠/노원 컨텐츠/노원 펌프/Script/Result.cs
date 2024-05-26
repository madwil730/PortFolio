using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Jacovone;
using RenderHeads.Media.AVProVideo.Demos;

public class Result : MonoBehaviour
{

	double time;

	public Text[] text;
	public Text ResultText;
	public Text[] ScreenText;

	public GameObject Next;
	public GameObject[] good;
	public GameObject[] bad;
	[Header("--------초기화--------")]
	public GameObject[] on;
	public GameObject[] off;
	public PathMagic[] path;
	[Tooltip("0 파란 1 노란")]
	public GameObject[] bead;

	float idleTime;
	public VCR vcr;
	public Animator ani;


	private void OnEnable()
	{
		StartCoroutine(Count90());
	}




	IEnumerator Count90()
	{

		yield return new WaitForSeconds(0.5f);

		time = 120;
		idleTime = 0;

		while (true)
		{
			time -= Time.deltaTime;

			//Debug.Log(Singletone.Instance.count);

			text[0].text = Math.Truncate(time).ToString();
			text[1].text = Math.Truncate(time).ToString();


			if (time <= 0 || Singletone.Instance.count == 20)
			{
				ResultText.gameObject.SetActive(true);
				ResultText.text = "거대인력에 보낸 구슬 : 총 " + (ShotB.count+ShotA.count)+ "개";

				if(Singletone.Instance.count >=1)
				{
					good[0].SetActive(true);
					good[1].SetActive(true);

					break;
				}

				else
				{
					bad[0].SetActive(true);
					bad[1].SetActive(true);

					break;
				}

				
			}

			yield return null;
		}

		time = 0;
		Singletone.Instance.count = 0;
		ReadSerial.Count2 = 0;
		ReadSerial.Count1 = 0;

		yield return new WaitForSeconds(0.3f);

		while (true)
		{
			if (Input.GetKeyDown(KeyCode.Q) || ReadSerial.Count2 >= 5 ||  ReadSerial.Count1 >= 5)
			{
				for (int i = 0; i < path.Length; i++)
				{
					path[i].Rewind();

				}
				break;
			}

			idleTime += Time.deltaTime;

			if(idleTime>30)
			{
				

				break;
			}

			yield return null;
				
		}

		yield return new WaitForSeconds(0.5f);
		Next.SetActive(true);
		idleTime = 0;

		while (true)
		{
			if (Input.GetKeyDown(KeyCode.Q) || ReadSerial.Count2 >=5 || ReadSerial.Count1 >=5)
			{
				
				break;
			}

			idleTime += Time.deltaTime;

			if (idleTime > 30)
			{
				break;
			}

			yield return null;

		}

		ani.speed = 0.5f;
		ScreenText[0].text = "";
		ScreenText[1].text = "";
		Singletone.Instance.onoff(on, off);
		vcr.OnRewindButton();
		text[0].text = "120";
		text[1].text = "120";


	}
}
