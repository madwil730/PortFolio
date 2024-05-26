using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Result : MonoBehaviour
{
	public GameObject[] light;
	[SerializeField] GameObject[] scaleUPOnObject;
	[SerializeField] GameObject[] onObj;
	[SerializeField] GameObject[] offObj;
	[SerializeField] GameObject[] activeIcon;
	[SerializeField] GameObject[] taeto;
	[SerializeField] GameObject[] successIcon;
	[SerializeField] Image[] taetoFillamount;
	[SerializeField] Image[] gauge;
	[SerializeField] Animator _Animator;
	//[SerializeField] GameObject[] timeLightCount;

	public Text text;
	public static bool check;

	float a = 15;
	float b;

	private void OnEnable()
	{
		StartCoroutine(time());
		text.text = "15";
		a = 15;

		
	}


	IEnumerator time()
	{
		check = true;
		yield return new WaitForSeconds(1);

		check = false;
		for (int i = 0; i < scaleUPOnObject.Length; i++)
			scaleUPOnObject[i].SetActive(true);

		yield return new WaitForSeconds(1);

		while (true)
		{
			a -= Time.deltaTime;
			if (a >= 0)
			{
				text.text = Math.Truncate(a).ToString();
				light[(int)(a)].SetActive(false);
			}
			else
			{
				_Animator.Play("fadeinout");

				yield return new WaitForSeconds(0.4f);

				for (int i =0; i < onObj.Length; i++)
					onObj[i].SetActive(true);

				for (int i = 0; i < offObj.Length; i++)
					offObj[i].SetActive(false);

				for (int i = 0; i < successIcon.Length; i++)
					successIcon[i].SetActive(false);

				for (int i = 0; i < activeIcon.Length; i++)
					activeIcon[i].SetActive(false);

				for (int i = 0; i < taeto.Length; i++)
					taeto[i].SetActive(false);

				for (int i = 0; i < light.Length; i++)
					light[i].SetActive(true);

				for (int i = 0; i < taetoFillamount.Length; i++)
					taetoFillamount[i].fillAmount = 0;

				for (int i = 0; i < gauge.Length; i++)
					gauge[i].fillAmount = 0;

				a = 15;
				Singtone.Instance.count = 0;
				Timer.time = 0;
				//Debug.Log(Singtone.Instance.count);

				StoveController.PotPrimaryKey = 5;
				StoveController1.PotPrimaryKey1 = 5;

				TouchController.taetoPot0 = false;
				TouchController.taetoPot1 = false;
				TouchController.taetoPot2 = false;
				//FlowManager.check = true;
				break;
			}

			yield return null;
		}
	}
}
