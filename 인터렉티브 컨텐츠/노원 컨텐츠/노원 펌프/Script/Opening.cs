using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo.Demos;

public class Opening : MonoBehaviour
{
	public GameObject[] on;
	public GameObject[] off;
	public GameObject[] bead;
	public Image image;
	public VCR vcr;

	public Image[] Guage;

	public Animator ani;

	// Start is called before the first frame update

	public void OnEnable()
	{
		ReadSerial.Count2 = 0;
		ReadSerial.Count1 = 0;
		StartCoroutine(open());
		image.color = Color.white;
		ani.speed = 1;
		
	}


	IEnumerator open()
	{
		yield return new WaitForSeconds(0.1f);
		vcr.OnPlayButton();

		yield return new WaitForSeconds(0.4f);

		while (true)
		{
			//vcr.OnPlayButton();
			if (Input.GetKeyDown(KeyCode.S) || ReadSerial.Count2 >= 5 || ReadSerial.Count1 >= 5)
				break;

			yield return null;
		}


		yield return new WaitForSeconds(0.2f);

		
		Guage[0].fillAmount = 0;
		Guage[1].fillAmount = 0;
		

		Singletone.Instance.onoff(on, off);

	}



	
}
