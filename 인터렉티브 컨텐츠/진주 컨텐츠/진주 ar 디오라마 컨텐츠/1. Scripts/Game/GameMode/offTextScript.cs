using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offTextScript : MonoBehaviour
{

	public GameObject fail;
	AudioSource audio;
	public AudioClip clip;

	private void Start()
	{
		audio = this.gameObject.GetComponent<AudioSource>();
	}

	void toTrue(int numcheck)
	{
		if(Singtone.Instance.count < 6)
		{
			textAnimation.check[numcheck] = true;
			fail.SetActive(true);
			audio.PlayOneShot(clip);
		}
		
	}
}
