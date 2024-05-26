using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalSoundB : MonoBehaviour
{
	AudioSource audio;
	public GameObject goal;
	// Start is called before the first frame update
	void Start()
	{
		audio = this.GetComponent<AudioSource>();
	}

	public void sound()
	{
		if (ShotB.goalB)
		{
			audio.Play();
			ShotB.goalB = false;
			goal.SetActive(true);
		}

	}

}
