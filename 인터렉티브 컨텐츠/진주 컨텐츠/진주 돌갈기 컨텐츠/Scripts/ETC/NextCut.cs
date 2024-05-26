using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCut : MonoBehaviour
{
	public GameObject[] offImgae;
	public GameObject[] onImage;
	public Image image;
	[Header("해당 버튼 위치 이동 ")]
	public GameObject button;
	Color color;

	// Start is called before the first frame update
	void Start()
	{
		color = new Color(0, 0, 0, 0);
	}

	public void NextAnother()
	{
		for (int i = 0; i < offImgae.Length; i++)
		{
			offImgae[i].SetActive(false);
		}

		for (int i = 0; i < onImage.Length; i++)
		{
			onImage[i].SetActive(true);
		}

		button.transform.position += new Vector3(2000, 0, 0);

	}
		

	// 화면 페이드 인,아웃
	public void Next()
	{
		//StartCoroutine(Fadein());
	}

}
