using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jacovone;
using RenderHeads.Media.AVProVideo.Demos;


public class ShotB : MonoBehaviour
{
	//파란 구슬 담당

	public GameObject[] pathOfpath;
	public GameObject[] Imagepath;

	public GameObject shotPath;
	public GameObject sphere;
	public GameObject red;
	public VCR vcr;

	public GameObject Aim;
	public Image whiteArrow;
	public Image redArrow;

	public GameObject[] countOb;
	public AudioClip[] clip;


	//public Animator ani;
	public Image[] Gauge;

	//public Text beadText;

	public static int count = 0;

	public float min_0;
	public float max_0;
	public float min_1;
	public float max_1;

	AudioSource audio;

	public static bool goalB;

	// Start is called before the first frame update

	private void Start()
	{
		audio = this.GetComponent<AudioSource>();

	}

	private void OnEnable()
	{
		count = 0;

		StartCoroutine(Slider());
	}

	IEnumerator Slider()
	{
		redArrow.gameObject.SetActive(false);

		while (true)
		{
			if (Input.GetKeyDown(KeyCode.D) || ReadSerial.Count2 >= 5)
			{

				Gauge[1].fillAmount += 0.2f;
				yield return new WaitForSeconds(0.33f); 
			}

			if ( Gauge[1].fillAmount >= 1)
				break;

			yield return null;
		}

		Imagepath[0].GetComponent<PathMagic>().Play();
		pathOfpath[0].GetComponent<PathMagic>().Play();

		yield return new WaitForSeconds(0.2f);
		
		Imagepath[0].GetComponent<PathMagic>().Pause();
		pathOfpath[0].GetComponent<PathMagic>().Pause();

		StartCoroutine(shotBall());
		yield return null;
	}

	IEnumerator shotBall()
	{

		yield return new WaitForSeconds(0.2f);

		red.SetActive(true);
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
			}
			else if (pathOfpath[1].GetComponent<PathMagic>().CurrentPos == 1)
			{
				pathOfpath[1].SetActive(false);
				pathOfpath[1].GetComponent<PathMagic>().Rewind();
				pathOfpath[0].SetActive(true);
				pathOfpath[0].GetComponent<PathMagic>().Play();
			}

			if (Input.GetKeyDown(KeyCode.D) || ReadSerial.Count2>=5)
			{
				audio.PlayOneShot(clip[0]);
				red.SetActive(false);

				if (Imagepath[1].GetComponent<PathMagic>().CurrentPos > min_1 && Imagepath[1].GetComponent<PathMagic>().CurrentPos < max_1 ||
				Imagepath[0].GetComponent<PathMagic>().CurrentPos > min_0 && Imagepath[0].GetComponent<PathMagic>().CurrentPos < max_0)
				{
					Singletone.Instance.count++;

					goalB = true;

					
					if (vcr._mediaPlayerB.m_PlaybackRate <= 1.5)
					{
						vcr._mediaPlayerB.m_PlaybackRate += 0.008f;
						vcr._mediaPlayerB.Control.SetPlaybackRate(vcr._mediaPlayerB.m_PlaybackRate);
						//vcr._mediaPlayerB.Control.SetLooping
						//vcr._mediaPlayerB.
						
						//Debug.Log(vcr._mediaPlayerB.m_PlaybackRate);
						
					}
				

					if (count < 10)
					{
						countOb[count].SetActive(true);
						count++;
					}
					

				}
				


				Imagepath[0].SetActive(false);
				Imagepath[0].GetComponent<PathMagic>().Rewind();
				Imagepath[1].SetActive(false);
				Imagepath[1].GetComponent<PathMagic>().Rewind();

				pathOfpath[0].SetActive(false);
				pathOfpath[0].GetComponent<PathMagic>().Rewind();
				pathOfpath[1].SetActive(false);
				pathOfpath[1].GetComponent<PathMagic>().Rewind();

				if (!shotPath.activeSelf)
					audio.PlayOneShot(clip[1],1);

				shotPath.SetActive(true);
				shotPath.GetComponent<PathMagic>().Play();
				break;
			}
			yield return null;
		}


		Gauge[1].fillAmount = 0;
		Aim.SetActive(false);
		
		sphere.GetComponent<Animator>().Play("shot");


		yield return new WaitForSeconds(3);


		//파란 구슬
		sphere.SetActive(false);
		//sphere.GetComponent<RectTransform>().localPosition = new Vector3(117, -228, 0);
		shotPath.SetActive(false);
		shotPath.GetComponent<PathMagic>().Rewind();

		
		StartCoroutine(Slider());
	}


}
