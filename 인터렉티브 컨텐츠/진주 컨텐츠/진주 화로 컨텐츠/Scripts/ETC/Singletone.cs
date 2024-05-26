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

	// 화면 페이드 인,아웃
	public void Next(Color color, Image image, GameObject[] offImage, GameObject[] onImage)
	{

		StartCoroutine(Fade(color, image, offImage, onImage));
	}

	/// <summary>
	///  페이드 전환 
	/// </summary>
	public IEnumerator Fade(Color color, Image image, GameObject[] offImage, GameObject[] onImage)
	{
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
	/// 


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
	///  결과창 재생 쓸모 없으면 지울것
	/// </summary>
	public IEnumerator Result(Color color, Image image, GameObject[] offImage, GameObject[] onImage, VCR vcr, int num)
	{
		image.transform.position = image.transform.position - new Vector3(2000, 0, 0);
		color = new Color(0, 0, 0, 0);

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

		vcr.TEST(num);

		while (color.a >= 0)
		{
			color.a -= 1.2f * Time.deltaTime;
			image.color = color;

			yield return null;
		}

		image.transform.position = image.transform.position + new Vector3(2000, 0, 0);

		yield return null;
	}





	public IEnumerator timeIllumination2(GameObject obj)
	{
		float time = 0;

		while (true)
		{
			time += Time.deltaTime;

			if (time > 0 && time < 0.5 || time > 1 && time < 1.5) // 0.5
			{
				obj.SetActive(true);
			}

			else if (time > 0.5 && time < 1 || time > 1.5 && time < 2) // 0.5
			{
				obj.SetActive(false);
			}

			else if (time > 2 && time < 2.4 || time > 2.8 && time < 3.2) //0.4
			{
				obj.SetActive(true);
			}

			else if (time > 2.4 && time < 2.8 || time > 3.2 && time < 3.6) // 0.4
			{
				obj.SetActive(false);
			}

			else if (time > 3.6 && time < 3.9) // 0.3
			{
				obj.SetActive(true);
			}

			else if (time > 3.9 && time < 4.2) // 0.3
			{
				obj.SetActive(false);
			}

			else if (time > 4.2 && time < 4.4) // 0.2
			{
				obj.SetActive(true);
			}

			else if (time > 4.4 && time < 4.6) // 0.2
			{
				obj.SetActive(false);
			}

			else if (time > 4.6 && time < 4.7 || time > 4.8 && time < 4.9) // 0.1
			{
				obj.SetActive(true);
			}

			else if (time > 4.7 && time < 4.8 || time > 4.9 && time < 5) // 0.1
			{
				obj.SetActive(false);
			}

			else
			{
				Rice_Check.activeTime = true;
				break;
			}
				

			yield return null;
		}
	}
}
