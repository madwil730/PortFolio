using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InButton : MonoBehaviour
{
	[SerializeField] GameObject[] on;
	[SerializeField] GameObject[] off;
	[SerializeField] GameObject Fade;
	[SerializeField] GameObject homeButton;
	//[SerializeField] GuideText guide;


	//터치 조건식 있어야 함
	public void startRotation()
	{
		if(!touch.isDrag)
		StartCoroutine(onoff(on,off));
	}

	public IEnumerator onoff(GameObject[] on, GameObject[] off)
	{
		yield return new WaitForSeconds(0.01f);

		Fade.SetActive(true);
		
		
		yield return new WaitForSeconds(0.3f);


		homeButton.transform.localPosition = new Vector3(-849, 420, 0);
		for (int i = 0; i < off.Length; i++)
		{
			//Debug.Log("off");
			off[i].SetActive(false);
		}

		for (int i = 0; i < on.Length; i++)
		{
			//Debug.Log(444);
			on[i].SetActive(true);
		}

		
	}
}
