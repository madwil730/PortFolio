using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
	public RectTransform rect;
	public Image image;
	public Sprite[] SolarSprite;
	public Sprite[] OurGalaxySprite;
	public Sprite[] UniverseSprite;
	public Sprite[] BigSpaceSprite;




	//float pos;

	enum Pos {leftStart, RightStart}
	Pos pos = Pos.leftStart;


	private void Start()
	{
		
	}

	private void Update()
	{
		// 태양계 ~ 우리 은하
		if (rect.transform.localPosition.x > -550 &&
		rect.transform.localPosition.x <= -277)
		{
			var x = 110+ (int)( rect.transform.localPosition.x/5);

			//Debug.Log(x);

			if (x  < SolarSprite.Length )
				image.sprite = SolarSprite[x];
		}


		// 우리 은하 ~ 은하단
		if (rect.transform.localPosition.x > -277 &&
			rect.transform.localPosition.x <= 0)
		{
			var x = 55+ (int)(rect.transform.localPosition.x / 5);

			//Debug.Log(x);

			if (x < OurGalaxySprite.Length)
				image.sprite = OurGalaxySprite[x];
		}

		//은하단 ~ 초은하단
		if (rect.transform.localPosition.x > 0 &&
			rect.transform.localPosition.x <= 275)
		{
			var x = (int)(rect.transform.localPosition.x / 6);

			//Debug.Log(x);

			if (x < UniverseSprite.Length)
				image.sprite = UniverseSprite[x];
		}


		// 초은하단 ~ 우주 거대 구조
		if (rect.transform.localPosition.x > 275 &&
			rect.transform.localPosition.x <= 549)
		{
			var x = (int)(rect.transform.localPosition.x / 6)- 45;

			//Debug.Log(x);

			if (x < BigSpaceSprite.Length)
				image.sprite = BigSpaceSprite[x];
		}

	}


}




















//if (rect.transform.localPosition.x > 0 && rect.transform.localPosition.x< 270)
//		{
//			if(pos == Pos.leftStart)
//			{
//				if (touch.isRight)
//				{
//					if (rect.transform.localPosition.x > rightCount &&
//						rect.transform.localPosition.x<rightCount + 10)
//					{
//						if (num<SolarSprite.Length)
//							image.sprite = SolarSprite[num];
//						rightCount += 5;
//						num++;
//						//Debug.Log(rightCount);
//					}
//				}

//				else if (!touch.isRight)
//				{


//					if (rect.transform.localPosition.x<rightCount &&
//						rect.transform.localPosition.x> rightCount - 10)
//					{
//						num = SolarSprite.Length - minusCount;

//						if (num >= 0)
//						{
//							image.sprite = SolarSprite[num];
//							rightCount -= 5;
//							minusCount++;
//						}
//						Debug.Log(num);
//					}
//				}
//			}

//			else if (pos == Pos.RightStart)
//			{
//				if (!touch.isRight)
//				{
//					if (rect.transform.localPosition.x > rightCount &&
//						rect.transform.localPosition.x<rightCount + 10)
//					{
//						if (num<SolarSprite.Length)
//							image.sprite = SolarSprite[num];
//						rightCount += 5;
//						num++;
//						//Debug.Log(rightCount);
//					}
//				}

//				else if (touch.isRight)
//				{


//					if (rect.transform.localPosition.x<rightCount &&
//						rect.transform.localPosition.x> rightCount - 10)
//					{
//						num = SolarSprite.Length - minusCount;

//						if (num >= 0)
//						{
//							image.sprite = SolarSprite[num];
//							rightCount -= 5;
//							minusCount++;
//						}
//						Debug.Log(num);
//					}
//				}
//			}
//		}

//		else
//		{
//			rightCount = 0;
//			leftCount = 0;
//			num = 0;
//			minusCount = 1;

//		}





