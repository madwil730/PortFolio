using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;

public class ToothSequence : MonoBehaviour
{
	bool check;
	public static bool ToothCheck;
	public GameObject[] ob;
	public VCR vcr;

	public result Result;

	bool resultCheck;
	bool resultDoubleCheck;
	
	private void OnEnable()
	{
		ToothCheck = false;
		for (int i = 0; i < ob.Length; i++)
			ob[i].SetActive(false);

		resultDoubleCheck = false;
		resultCheck = false;
	}

	public void first()
	{
		StartCoroutine(tooth());
	}
	


	IEnumerator tooth()
	{
	
		if(vcr._videoSeekSlider.value > 0.99f)
		{
			ToothCheck = true;
			vcr._VideoIndex = 1;
			vcr.OnOpenVideoFile();
			vcr._mediaPlayer.Control.SetLooping(true);
			vcr._mediaPlayerB.Control.SetLooping(true);

			yield return new WaitForSeconds(0.1f);

			for (int i = 0; i < ob.Length; i++)
				ob[i].SetActive(true);

			resultDoubleCheck = true;
			yield return null;
		}

		
	}

	private void Update()
	{
		if(ToothCheck && resultDoubleCheck)
		{
			if(!ob[0].activeSelf)
			{
				if (!ob[1].activeSelf)
				{
					if (!ob[2].activeSelf)
					{
						if (!ob[3].activeSelf)
						{
							resultCheck = true;
						}
					}
				}
			}

			if (resultCheck)
				Result.goScene();
		}
	}


}
