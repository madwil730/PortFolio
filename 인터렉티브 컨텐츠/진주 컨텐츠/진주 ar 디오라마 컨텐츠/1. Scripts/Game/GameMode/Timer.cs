using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Image image;
	public Sprite[] sprite;
	public GameObject result;
	[SerializeField] GameObject[] offObject;

	public static double time =0;
	bool check;
	AudioSource audio;
	public AudioClip[] clip;

	// Start is called before the first frame update
	private void OnEnable()
	{
		audio = this.gameObject.GetComponent<AudioSource>();
		check = false;
		StartCoroutine(timer());
		StartCoroutine(timeSound());
		this.gameObject.GetComponent<Image>().sprite = sprite[0];

	}

	IEnumerator timer()
	{
		yield return new WaitForSeconds(1);
		while (true)
		{
			

			time += Time.deltaTime;

			if (time < 41)
				this.gameObject.GetComponent<Image>().sprite = sprite[(int)time];

			else if (time >41)
			{
				for (int i = 0; i < offObject.Length; i++)
					offObject[i].SetActive(false);

				
				audio.PlayOneShot(clip[1]);
				Singtone.Instance.count = 10;
				result.SetActive(true);
				//Debug.Log(Singtone.Instance.count + " timer");
				break;
			}

			if (Singtone.Instance.count >= 6)
				break;
			
				

			yield return null;
		}
	}

	IEnumerator timeSound()
	{
		while (true)
		{
			if (time > 41)	
				break;
			
			if ((int)time == 35)
				check = true;

			if(check && Singtone.Instance.count< 6)
			{
				audio.PlayOneShot(clip[0]);
				yield return new WaitForSeconds(1);
			}
			yield return null;
		}
	}

}
