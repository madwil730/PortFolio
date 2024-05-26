using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

	AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
		audio = this.gameObject.GetComponent<AudioSource>();
    }

   public void Play()
   {
		StartCoroutine(play());
		Debug.Log(123);
   }

	IEnumerator play()
	{
		audio.Play();
		yield return null;
	}
}
