using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RenderHeads.Media.AVProVideo.Demos;

public class CountTimer : MonoBehaviour
{
	double time;

	//public Image image;
	[HideInInspector]
	public Sprite[] countImage;

	public GameObject[] off;
	public GameObject[] on;
	public VCR vcr;
	AudioSource audio;

	private void Start()
	{
		audio = this.GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	private void OnEnable()
	{
		StartCoroutine(Count5());
		StartCoroutine(timer());
		
	}

	IEnumerator timer()
	{
		yield return new WaitForSeconds(1);

		while (true)
		{
			
			audio.Play();

			yield return new WaitForSeconds(1);

		}
		
	}

    IEnumerator Count5()
	{
		yield return new WaitForSeconds(0.1f);
		vcr._mediaPlayerB.m_PlaybackRate = 1;
		vcr._mediaPlayerB.Control.SetPlaybackRate(vcr._mediaPlayerB.m_PlaybackRate);
		yield return new WaitForSeconds(0.4f);

		vcr.OnPlayButton();
		time = 5;

		while(true)
		{
			time -= Time.deltaTime;

			//image.sprite = countImage[(int)time];

			if (time <= 0)
				break;

			yield return null;
		}

		time = 0;

		for (int i = 0; i < off.Length; i++)
			off[i].SetActive(false);

		for (int i = 0; i < on.Length; i++)
			on[i].SetActive(true);

		//image.sprite = countImage[4];

	}


	
}
