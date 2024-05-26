using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Sprite[] sprite;
	[Tooltip("0.5 이하")]
	public Sprite[] SpriteUrgent;
	[Tooltip("0.5 이상")]
	public Sprite[] SpriteUrgentEmpty;
	public static double time =0;

	public GameObject[] offimage;
	public GameObject resultVCR;
	public GameObject resultVideoDisPlay;
	public GameObject resultSmoke;

	public static bool timeResult;

	AudioSource audio;

	// Start is called before the first frame update
	private void OnEnable()
	{
		audio = this.gameObject.GetComponent<AudioSource>();

		StartCoroutine(timer());
	}

	IEnumerator timer()
	{
		while(true)
		{

			time += Time.deltaTime;

			if (time < 30)
				this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite[(int)time];
			//int a = Math.Truncate(time);

			else if (time > 30 && time < 40)
			{
				if (time - (int)time < 0.5f)
				{
					this.gameObject.GetComponent<SpriteRenderer>().sprite = SpriteUrgent[(int)time - 30];
					//Debug.Log("이하");
				}

				else if (time - (int)time > 0.5f)
				{
					this.gameObject.GetComponent<SpriteRenderer>().sprite = SpriteUrgentEmpty[(int)time - 30];
					//Debug.Log("이상");
				}
			}

			else if (time > 40)
				break;

			yield return null;
		}
		

		if (Rice_Check.timer[0])
			StartCoroutine(Resultcheck(0, 0));

		else if (Rice_Check.timer[1])
			StartCoroutine(Resultcheck(1, 1));

		else if (Rice_Check.timer[2])
			StartCoroutine(Resultcheck(2, 1));

		else
			StartCoroutine(Resultcheck(2, 1));
	}

	IEnumerator Resultcheck(int a, int b)
	{
		Rice_Check.Result = true;
		//timeResult = true;

		resultSmoke.SetActive(true);
		resultVCR.SetActive(true);
		resultVCR.GetComponent<VCR>()._VideoIndex = a;
		yield return new WaitForSeconds(2.5f);

		audio.clip = resultVCR.GetComponent<VCR>().resultClip[b];
		audio.Play();

		resultVideoDisPlay.SetActive(true);
		resultVCR.GetComponent<VCR>().TEST(a);

		for (int i = 0; i < offimage.Length; i++)
			offimage[i].SetActive(false);

		BacktoOpening.OpenigCheck = true;
	}
}
