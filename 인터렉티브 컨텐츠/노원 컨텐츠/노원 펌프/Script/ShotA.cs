using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jacovone;
using RenderHeads.Media.AVProVideo.Demos;


public class ShotA : MonoBehaviour
{
	public GameObject[] pathOfpath;
	public GameObject[] Imagepath;

	public GameObject shotPath;
	public GameObject shotPath2;
	public GameObject sphere;
	public GameObject red;

	public VCR vcr;

	public GameObject Aim;
	public Image whiteArrow;
	public Image redArrow;

	public GameObject[] countOb;
	public AudioClip[] clip;

	public Animator ani;
	public Image[] Gauge;

	//public Text beadText;

	public static int count= 0;

	public float min_0;
	public float max_0;
	public float min_1;
	public float max_1;

	AudioSource audio;


	bool pathCheck;
	public static bool goalA;

	// Start is called before the first frame update

	private void Start()
	{
		audio = this.GetComponent<AudioSource>();
		
	}

	private void OnEnable()
	{
		ani.speed = 1;
		count = 0;
		pathCheck = false;
	

		StartCoroutine(Slider());
	}


	IEnumerator Slider()
	{
		redArrow.gameObject.SetActive(false);

		while (true)
		{
			if (Input.GetKeyDown(KeyCode.A) || ReadSerial.Count1 >= 5)
			{

				Gauge[0].fillAmount += 0.2f;
				yield return new WaitForSeconds(0.33f);
			}

			if (Gauge[0].fillAmount >= 1 )
				break;

			yield return null;
		}

		red.SetActive(true);
		yield return new WaitForSeconds(0.2f);

		StartCoroutine(shotBall());
			yield return null;
	}


	//노란 구슬 담당
	IEnumerator shotBall()
	{
		yield return new WaitForSeconds(0.2f);

		Imagepath[0].SetActive(true);
		pathOfpath[0].SetActive(true);

		Imagepath[0].GetComponent<PathMagic>().Play();
		pathOfpath[0].GetComponent<PathMagic>().Play();


		Aim.SetActive(true);

		sphere.SetActive(true);
		sphere.GetComponent<Image>().color = Color.white;
		sphere.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
		



		while (true)
		{
			if (Imagepath[1].GetComponent<PathMagic>().CurrentPos > min_1 && Imagepath[1].GetComponent<PathMagic>().CurrentPos < max_1 ||
			Imagepath[0].GetComponent<PathMagic>().CurrentPos > min_0 && Imagepath[0].GetComponent<PathMagic>().CurrentPos < max_0)
			{

				redArrow.gameObject.SetActive(true);
			}
			else
			{

				redArrow.gameObject.SetActive(false);
			}

			if (Imagepath[0].GetComponent<PathMagic>().CurrentPos == 1)
			{
				Imagepath[0].SetActive(false);
				Imagepath[0].GetComponent<PathMagic>().Rewind();
				Imagepath[1].SetActive(true);
				Imagepath[1].GetComponent<PathMagic>().Play();
				
			}
			else if (Imagepath[1].GetComponent<PathMagic>().CurrentPos == 1)
			{
				Imagepath[1].SetActive(false);
				Imagepath[1].GetComponent<PathMagic>().Rewind();
				Imagepath[0].SetActive(true);
				Imagepath[0].GetComponent<PathMagic>().Play();
				
			}


			if (pathOfpath[0].GetComponent<PathMagic>().CurrentPos == 1)
			{
				pathOfpath[0].SetActive(false);
				pathOfpath[0].GetComponent<PathMagic>().Rewind();
				pathOfpath[1].SetActive(true);
				pathOfpath[1].GetComponent<PathMagic>().Play();
				pathCheck = true;
			}
			else if (pathOfpath[1].GetComponent<PathMagic>().CurrentPos == 1)
			{
				pathOfpath[1].SetActive(false);
				pathOfpath[1].GetComponent<PathMagic>().Rewind();
				pathOfpath[0].SetActive(true);
				pathOfpath[0].GetComponent<PathMagic>().Play();
				pathCheck = false;
			}

			if (Input.GetKeyDown(KeyCode.A) || ReadSerial.Count1 >= 5)
			{
				audio.PlayOneShot(clip[0]);
				red.SetActive(false);

				//Debug.Log(goalA);

				if (Imagepath[1].GetComponent<PathMagic>().CurrentPos > min_1 && Imagepath[1].GetComponent<PathMagic>().CurrentPos < max_1 ||
				Imagepath[0].GetComponent<PathMagic>().CurrentPos > min_0 && Imagepath[0].GetComponent<PathMagic>().CurrentPos < max_0)
				{
					Singletone.Instance.count++;

					//if(ani.speed < 2)
					//ani.speed += 0.1f;

					goalA = true;


					if (vcr._mediaPlayerB.m_PlaybackRate <= 1.5)
					{
						vcr._mediaPlayerB.m_PlaybackRate += 0.008f;
						vcr._mediaPlayerB.Control.SetPlaybackRate(vcr._mediaPlayerB.m_PlaybackRate);
						//Debug.Log(vcr._mediaPlayerB.m_PlaybackRate);

					}


					if (count < 10)
					{
						countOb[count].SetActive(true);
						count++;
					};
				}
				
				Imagepath[0].SetActive(false);
				Imagepath[0].GetComponent<PathMagic>().Rewind();
				Imagepath[1].SetActive(false);
				Imagepath[1].GetComponent<PathMagic>().Rewind();

				pathOfpath[0].SetActive(false);
				pathOfpath[0].GetComponent<PathMagic>().Rewind();
				pathOfpath[1].SetActive(false);
				pathOfpath[1].GetComponent<PathMagic>().Rewind();

				if(!pathCheck)
				{
					if (!shotPath.activeSelf)
						audio.PlayOneShot(clip[1], 1);

					shotPath.SetActive(true);
					shotPath.GetComponent<PathMagic>().Play();
				}
				else if (pathCheck)
				{
					if (!shotPath2.activeSelf)
						audio.PlayOneShot(clip[1], 1);

					shotPath2.SetActive(true);
					shotPath2.GetComponent<PathMagic>().Play();
				}

				break;
			}
			yield return null;
		}

		Gauge[0].fillAmount = 0;
		Aim.SetActive(false);

		sphere.GetComponent<Animator>().Play("shot");
	

		yield return new WaitForSeconds(3);

		// 노란 구슬
		//sphere.GetComponent<RectTransform>().localPosition = new Vector3(-420, -313, 0);
		sphere.SetActive(false);

		
		
		shotPath.SetActive(false);
		shotPath.GetComponent<PathMagic>().Rewind();
		shotPath2.SetActive(false);
		shotPath2.GetComponent<PathMagic>().Rewind();

		pathCheck = false;

		StartCoroutine(Slider());
	}


}
