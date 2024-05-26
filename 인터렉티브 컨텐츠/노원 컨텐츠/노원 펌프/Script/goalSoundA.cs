using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalSoundA: MonoBehaviour
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
		if(ShotA.goalA)
		{
			audio.Play();
			ShotA.goalA = false;
			goal.SetActive(true);
			
		}
		
	}

}
