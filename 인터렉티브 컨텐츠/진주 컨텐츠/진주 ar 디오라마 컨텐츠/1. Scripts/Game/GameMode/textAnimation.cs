using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textAnimation : MonoBehaviour
{
	public GameObject[] text; // 말풍선 오브젝트
	public GameObject[] fail;
	public GameObject[] good;
	[SerializeField] SpriteRenderer[] textSprite;
	[SerializeField] Animator[] textAnimator;
	[SerializeField] SpriteRenderer[] failSprite;
	[SerializeField] SpriteRenderer[] goodSprite;
	[SerializeField] Sprite sprite;
	public GameObject result; // 성공 나오는 곳
	[SerializeField] GameObject BG; // 배경음악 오브젝트
	public string[] str;
	public int[] WaitTime;
	AudioSource audio;
	public AudioClip[] clip;

	public  static bool[] check = { false,false,false};
	// Start is called before the first frame update

	Color[] color = new Color[3];
	float[] time = new float[3];

	private void OnEnable()
	{
		audio = this.gameObject.GetComponent<AudioSource>();
		StartCoroutine(Animation(0, 0, 0, 0, WaitTime[0], 0));
		StartCoroutine(Animation(1, 1, 1, 1, WaitTime[1], 1));
		StartCoroutine(Animation(2, 2, 2, 2, WaitTime[2], 2));
		//Debug.Log(Singtone.Instance.count);
		check[0] = false;
		check[1] = false;
		check[2] = false;
		color[0] = Color.white;
		color[1] = Color.white;
		color[2] = Color.white;

		textSprite[0].color = Color.white;
		textSprite[1].color = Color.white;
		textSprite[2].color = Color.white;
	}

	IEnumerator Animation(int numtext, int numfail, int numgood, int numstr, int numWaitTime, int numcheck)
	{
		textAnimator[numtext].enabled = true;
		yield return new WaitForSeconds(numWaitTime);
		textAnimator[numtext].Play(str[numstr]);
		textSprite[numtext].enabled = true;
		audio.PlayOneShot(clip[0]);

		while (true)
		{
			if (Singtone.Instance.count > 6)
			{
				text[numtext].GetComponent<SpriteRenderer>().enabled = false;

				break;
			}

			if (Singtone.Instance.count == 6)
			{
				//Debug.Log(Singtone.Instance.count + "  in");
				result.SetActive(true);
				
				audio.PlayOneShot(clip[1]);
				
				BG.SetActive(false);
				Singtone.Instance.count += 1;
				
			}

			if(check[numcheck])
			{
				time[numtext] += Time.deltaTime;
				textSprite[numtext].sprite = sprite;
				textAnimator[numtext].enabled = false;

				if (color[numtext].a >= 0 && time[numtext] > 1)
				{
					color[numtext].a -= 1.4f * Time.deltaTime;
					textSprite[numtext].color = color[numtext];
					goodSprite[numgood].color = color[numtext];
					failSprite[numfail].color = color[numtext];
				}

				if (textSprite[numtext].color.a <= 0)
				{
					fail[numfail].SetActive(false);
					good[numgood].SetActive(false);

					yield return new WaitForSeconds(1);
					textSprite[numtext].color = Color.white;
					goodSprite[numgood].color = Color.white;
					failSprite[numfail].color = Color.white;
					color[numtext] = Color.white;
					textAnimator[numtext].enabled = true;
					textAnimator[numtext].Play(str[numstr], -1, 0);
					
					audio.PlayOneShot(clip[0]);

					time[numtext] = 0;
					check[numcheck] = false;
				}

				//----------------------------------------------------------------
				//yield return new WaitForSeconds(1);

				//fail[numfail].SetActive(false);
				//good[numgood].SetActive(false);
				//text[numtext].GetComponent<SpriteRenderer>().enabled = false;


				//yield return new WaitForSeconds(1);
				//text[numtext].GetComponent<Animator>().Play(str[numstr],-1,0);
				//text[numtext].GetComponent<SpriteRenderer>().enabled = true;
				//audio.PlayOneShot(clip[0]);

				//check[numcheck] = false;
			}

			yield return null;
		}
	}
}
