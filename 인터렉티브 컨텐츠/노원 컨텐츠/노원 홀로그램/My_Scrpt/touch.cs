using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class touch : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	public Image image;
	public Sprite[] sprite;
	public GameObject[] activeImage;
	public GameObject[] targetObject;
	public AudioSource audio;

	RectTransform rect;
	Color color;

	[HideInInspector]
	public enum Pos { moreLeft, left, center, right, moreRight }
	[HideInInspector]
	public Pos pos = Pos.center;

	

	public static bool isDrag;
	bool audiocheck;

	private void Start()
	{
		rect = GetComponent<RectTransform>();
		color = Color.white;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if(rect.localPosition.x >-570 && rect.localPosition.x <550)
		rect.anchoredPosition = new Vector2(rect.anchoredPosition.x+eventData.delta.x , rect.anchoredPosition.y) ;

		if(rect.localPosition.x < -570)
		rect.anchoredPosition = new Vector2(-569,-383);

		else if (rect.localPosition.x > 550)
		rect.anchoredPosition = new Vector2(549, -383);


		isDrag = true;

		if (rect.localPosition.x > -20 && rect.localPosition.x < 20 )
		{
			if(!audiocheck)
			{
				audio.Play();
				audiocheck = true;
			}

			targetObject[0].SetActive(false);
			targetObject[1].SetActive(false);
			targetObject[2].SetActive(true);
			targetObject[3].SetActive(false);
			targetObject[4].SetActive(false);
			
		}

		else if (rect.localPosition.x > -279 && rect.localPosition.x < -266)
		{
			if (!audiocheck)
			{
				audio.Play();
				audiocheck = true;
			}

			targetObject[0].SetActive(false);
			targetObject[1].SetActive(true);
			targetObject[2].SetActive(false);
			targetObject[3].SetActive(false);
			targetObject[4].SetActive(false);

		}

		else if (rect.localPosition.x > -577 && rect.localPosition.x < -567)
		{
			if (!audiocheck)
			{
				audio.Play();
				audiocheck = true;
			}

			targetObject[0].SetActive(true);
			targetObject[1].SetActive(false);
			targetObject[2].SetActive(false);
			targetObject[3].SetActive(false);
			targetObject[4].SetActive(false);

		}

		else if (rect.localPosition.x > 255 && rect.localPosition.x < 295)
		{
			if (!audiocheck)
			{
				audio.Play();
				audiocheck = true;
			}

			targetObject[0].SetActive(false);
			targetObject[1].SetActive(false);
			targetObject[2].SetActive(false);
			targetObject[3].SetActive(true);
			targetObject[4].SetActive(false);

		}

		else if (rect.localPosition.x > 535 && rect.localPosition.x < 554)
		{
			if (!audiocheck)
			{
				audio.Play();
				audiocheck = true;
			}

			targetObject[0].SetActive(false);
			targetObject[1].SetActive(false);
			targetObject[2].SetActive(false);
			targetObject[3].SetActive(false);
			targetObject[4].SetActive(true);
		}

		else
			audiocheck = false;

		///---------------------------------------------
		if(rect.localPosition.x <= -428)
			pos = Pos.moreLeft;

		else if (rect.localPosition.x > -428 && rect.localPosition.x <= -141)
			pos = Pos.left;

		else if (rect.localPosition.x > -141 && rect.localPosition.x <= 136)
			pos = Pos.center;

		else if (rect.localPosition.x > 136 && rect.localPosition.x <= 416)
			pos = Pos.right;

		else if (rect.localPosition.x > 416)
			pos = Pos.moreRight;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isDrag = false;

		//moreLeft
		if (rect.localPosition.x <= -428)
		{
			this.transform.localPosition = new Vector3(-557, -383, 0);
			pos = Pos.moreLeft;
			image.sprite = sprite[0];
			activeCheck(0);
			setActive(0);
		}


		//Left
		else if (rect.localPosition.x > -428 && rect.localPosition.x <= -141)
		{
			this.transform.localPosition = new Vector3(-277, -383, 0);
			pos = Pos.left;
			image.sprite = sprite[1];
			activeCheck(1);
			setActive(1);
		}

		//Center
		else if (rect.localPosition.x > -141 && rect.localPosition.x <= 136)
		{
			this.transform.localPosition = new Vector3(0, -383, 0);
			pos = Pos.center;
			image.sprite = sprite[2];
			activeCheck(2);
			setActive(2);

		}

		//right
		else if (rect.localPosition.x > 136 && rect.localPosition.x <= 416)
		{
			this.transform.localPosition = new Vector3(275, -383, 0);
			pos = Pos.right;
			image.sprite = sprite[3];
			activeCheck(3);
			setActive(3);
		}

		//moreRight
		else if (rect.localPosition.x > 416)
		{
			this.transform.localPosition = new Vector3(554, -383, 0);
			pos = Pos.moreRight;
			image.sprite = sprite[4];
			activeCheck(4);
			setActive(4);

		}

	}


	void activeCheck(int a)
	{
		for(int i= 0;  i < activeImage.Length; i++)
		{
			if (i == a)
				activeImage[i].SetActive(true);
			else
				activeImage[i].SetActive(false);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{

	}


	void setActive(int x )
	{
		for(int i = 0; i < targetObject.Length; i ++)
		{
			if(i == x)
			{
				targetObject[i].SetActive(true);
			}
			else
				targetObject[i].SetActive(false);

		}
	}

}

