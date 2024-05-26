using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeResult : MonoBehaviour
{
	[Header("화면 전환")]
	public GameObject[] offImage;
	public GameObject[] onImage;
	public GameObject[] rock;

	public GameObject dandi;
	public Sprite sprite;

	public Image image;
	public VCR vcr;

	public AudioClip[] clip;
	Color color;

	bool check2;

	// Start is called before the first frame update

	private void OnEnable()
	{ 
		StartCoroutine(timeCheck());
		check2 = false;
	}

	IEnumerator timeCheck()
	{
		while(true)
		{
			if (agricultural_Rail.countCheck)
			{
				//Debug.Log(agricultural_Rail.countCheck);
				//check = true;
				break;
			}
				

			else if (Timer.time > 40)
			{
				//Debug.Log(Timer.time);
				//check = true;
				break;
			}

			yield return null;
		}

		
		if (agricultural_Rail.count >= 30 )
		{
			check2 = true;
			yield return new WaitForSeconds(1.0f);

			StartCoroutine(Singletone.Instance.Result(color, image, offImage, onImage,rock, sprite,vcr, 0));
			this.gameObject.GetComponent<AudioSource>().clip = clip[0];
			this.gameObject.GetComponent<AudioSource>().Play();
			BacktoOpening.OpenigCheck = true;
			//Debug.Log("성공 ,0");
			//check = false;
		}
		else if(!check2)
		{
			StartCoroutine(Singletone.Instance.Result(color, image, offImage, onImage, rock, sprite, vcr, 1));
			this.gameObject.GetComponent<AudioSource>().clip = clip[1];
			this.gameObject.GetComponent<AudioSource>().Play();
			BacktoOpening.OpenigCheck = true;
			yield return new WaitForSeconds(1);
			dandi.SetActive(true);
			//Debug.Log("실패 ,1");
			//check = false;
		}

		yield return null;
	}

}
