using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singletone : MonoBehaviour
{
	private static Singletone instance = null;
	// Start is called before the first frame update

	private void Awake()
	{
		if (null == instance)
		{
			instance = this;
		}
	}

	public static Singletone Instance
	{
		get
		{
			return instance;
		}
	}


	/// <summary>
	///  페이드 전환 
	/// </summary>
	public IEnumerator Fade(Color color, Image image, GameObject[] offImage, GameObject[] onImage)
	{
		color = new Color(0, 0, 0, 0);
		image.transform.position = image.transform.position - new Vector3(2000, 0, 0);

		while (color.a <= 1)
		{
			color.a += 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}


		for (int i = 0; i < onImage.Length; i++)
		{
			onImage[i].SetActive(true);
		}

		for (int i = 0; i < offImage.Length; i++)
		{
			offImage[i].SetActive(false);
		}

		while (color.a >= 0)
		{
			color.a -= 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}

		image.transform.position = image.transform.position + new Vector3(2000, 0, 0);

		yield return null;
	}

	/// <summary>
	///  페이드  동영상 전환 
	/// </summary>

	public IEnumerator FadeVCR(Color color, Image image, GameObject[] offImage, GameObject[] onImage, VCR vcr)
	{
		color = new Color(0, 0, 0, 0);
		image.transform.position = image.transform.position - new Vector3(2000, 0, 0);

		vcr.OnRewindButton();

		while (color.a <= 1)
		{
			color.a += 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}

		for (int i = 0; i < offImage.Length; i++)
		{
			offImage[i].SetActive(false);
		}


		for (int i = 0; i < onImage.Length; i++)
		{
			onImage[i].SetActive(true);
		}

		//vcr.OnPlayButton();
		vcr.TEST(0);

		while (color.a >= 0)
		{
			color.a -= 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}
	
		image.transform.position = image.transform.position + new Vector3(2000, 0, 0);

		yield return null;
	}


	/// <summary>
	///  결과창 재생 
	/// </summary>
	public IEnumerator Result(Color color, Image image, GameObject[] offImage, GameObject[] onImage,GameObject[] rock, Sprite sprite, VCR vcr, int num)
	{
		image.transform.position = image.transform.position - new Vector3(2000, 0, 0);
		color = new Color(0, 0, 0, 0);
		vcr.OnRewindButton();

		while (color.a <= 1)
		{
			color.a += 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}

		for (int i = 0; i < rock.Length; i++)
		{
			rock[i].GetComponent<SpriteRenderer>().sprite = sprite;
		}

		for (int i = 0; i < offImage.Length; i++)
		{
			offImage[i].SetActive(false);
		}


		for (int i = 0; i < onImage.Length; i++)
		{
			onImage[i].SetActive(true);
		}

		vcr.TEST(num);

		while (color.a >= 0)
		{
			color.a -= 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}

		
		//vcr.OnPlayButton();
		//vcr.OnOpenVideoFile();
	
		image.transform.position = image.transform.position + new Vector3(2000, 0, 0);

		yield return null;
	}

}
