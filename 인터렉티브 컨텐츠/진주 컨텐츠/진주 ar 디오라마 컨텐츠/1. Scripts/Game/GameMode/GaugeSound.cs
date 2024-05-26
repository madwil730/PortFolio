using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeSound : MonoBehaviour
{
	public Image image;
	AudioSource audio;
	public AudioClip clip;

	bool check;
	// Start is called before the first frame update
	private void OnEnable()
	{
		StartCoroutine(sound());
	}

	void Start()
    {
		audio = this.gameObject.GetComponent<AudioSource>();
		
    }

    IEnumerator sound()
	{
		while(true)
		{
			if (image.fillAmount == 1 && check)
			{
				//Debug.Log(111);
				audio.PlayOneShot(clip);
				check = false;
			}
			else if (image.fillAmount < 0.8)
			{
				check = true;
				//Debug.Log(222);
			}
			yield return null;
		}
		
	}
}
