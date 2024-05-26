using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goHome : MonoBehaviour
{
	[SerializeField] GameObject[] on;
	[SerializeField] GameObject[] off;
	[SerializeField] GameObject Fade;
	[SerializeField] GuideText guide;


	public void home()
	{
		
		StartCoroutine(onoff(on, off));
	}

	public IEnumerator onoff(GameObject[] on, GameObject[] off)
	{
		yield return new WaitForSeconds(0.01f);

		Fade.SetActive(true);

		

		for (int i = 12; i < off.Length; i++)
		{
			Debug.Log("off");
			off[i].SetActive(false);
		}
		

		yield return new WaitForSeconds(0.3f);

		this.transform.localPosition = new Vector3(1000, 1000, 0);

		for (int i = 0; i < off.Length; i++)
		{
			//Debug.Log("off");
			off[i].SetActive(false);
		}

	
		
			guide.Guidefalse();

		

		for (int i = 0; i < on.Length; i++)
		{
			//Debug.Log(444);
			on[i].SetActive(true);
		}


	}
}
