using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCut : MonoBehaviour
{
	[Header("화면 전환")]
	public GameObject[] offImage;
	public GameObject[] onImage;
	public Image image;
	Color color;

	private void Start()
	{
		color = new Color(0, 0, 0, 0);
	}

	public void Fade()
	{
		//Debug.Log(234);

		StartCoroutine(Singletone.Instance.Fade(color, image, offImage, onImage));
		this.gameObject.GetComponent<Button>().transform.position += new Vector3(2000, 0, 0);

	}

	public void onoff_Image()
	{
		for (int i = 0; i < offImage.Length; i++)
		{
			offImage[i].SetActive(false);
		}


		for (int i = 0; i < onImage.Length; i++)
		{
			onImage[i].SetActive(true);
		}

		this.gameObject.GetComponent<Button>().transform.position += new Vector3(2000, 0, 0);
		
	}
	
}
