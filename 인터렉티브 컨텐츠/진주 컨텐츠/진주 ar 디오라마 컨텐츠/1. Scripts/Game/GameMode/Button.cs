using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo.Demos;

public class Button : MonoBehaviour
{
	[SerializeField] VCR vcr;
	public GameObject[] light;
	[SerializeField] GameObject[] onObj;
	[SerializeField] GameObject[] offObj;
	[SerializeField] GameObject[] activeIcon;
	[SerializeField] GameObject[] successIcon;
	[SerializeField] GameObject[] taeto;
	[SerializeField] Image[] taetoFillamount;
	[SerializeField] Image[] gauge;
	[SerializeField] Animator _Animator;
	[SerializeField] Animator _AnimatorResult;
	
	public void Play()
	{
		StartCoroutine(Playing());
	}


	public void Result()
	{
		StartCoroutine(Initialization());
	}

	IEnumerator Playing()
	{

		_Animator.Play("Transition");

		yield return new WaitForSeconds(0.3f);

		for (int i = 0; i < onObj.Length; i++)
			onObj[i].SetActive(true);

		for (int i = 0; i < offObj.Length; i++)
			offObj[i].SetActive(false);

		//vcr._videoSeekSlider.value = 0;
		vcr.OnRewindButton();
		vcr.OnPauseButton();

		yield return null;
	}


	IEnumerator Initialization()
	{
		_AnimatorResult.Play("fadeinout");

		yield return new WaitForSeconds(0.4f);

		for (int i = 0; i < onObj.Length; i++)
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

		Singtone.Instance.count = 0;
		Timer.time = 0;

		StoveController.PotPrimaryKey = 5;
		StoveController1.PotPrimaryKey1 = 5;

		TouchController.taetoPot0 = false;
		TouchController.taetoPot1 = false;
		TouchController.taetoPot2 = false;

		FlowManager.check = true;
		//Debug.Log(Singtone.Instance.count);
		yield return null;
	}



}
