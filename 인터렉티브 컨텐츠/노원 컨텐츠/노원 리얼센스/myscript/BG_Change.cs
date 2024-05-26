using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;

public class BG_Change : MonoBehaviour
{
	public VCR vcr;
	public GameObject[] Ob;
	float time;

	bool check;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Change());
    }

   
	//1200
	IEnumerator Change()
	{
		while(true)
		{
			time += Time.deltaTime;

			if (time > 120)
			{
				if (!check)
				{
					Ob[1].SetActive(true);
					check = true;

					//yield return new WaitForSeconds(2.4f);
				}

				else if (check)
				{
					Ob[0].SetActive(true);
					check = false;

					//yield return new WaitForSeconds(2f);
				}

				break;
			}

			yield return null;
		}

		

		//vcr.OnOpenVideoFile();
		time = 0;
		StartCoroutine(Change());
	}
}
