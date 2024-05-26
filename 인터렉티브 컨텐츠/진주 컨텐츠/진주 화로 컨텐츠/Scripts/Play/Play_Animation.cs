using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Animation : MonoBehaviour
{
	public GameObject[] water;
	public GameObject[] bubble;
	public GameObject[] bubbleOnRice;
	public GameObject steam;
	public GameObject level1;
	public GameObject level2;

	public GameObject resultSmoke;
	
	Vector3 down;

	private void OnEnable()
	{
		down = new Vector3(0, 2, 0);

		StartCoroutine(stage1());
	}

	IEnumerator stage1 ()
	{
		while(true)
		{
			if (Rice_Check.Stage1)
				break;
		
			yield return null;
		}


		while(true)
		{
			for (int i = 0; i < water.Length; i++)
			{
				
				water[i].transform.localPosition -= down * Time.deltaTime;

			}

			if (water[0].transform.localPosition.y < -548 )
				break;

			yield return null;
		}

		bubbleOnRice[0].SetActive(true);

		yield return new WaitForSeconds(1f);

		bubbleOnRice[1].SetActive(true);

		yield return new WaitForSeconds(1f);

		bubbleOnRice[2].SetActive(true);

		yield return new WaitForSeconds(1f);

		bubbleOnRice[3].SetActive(true);


		StartCoroutine(stage2());

	}



	IEnumerator stage2()
	{

		while (true)
		{
			
			if (Rice_Check.Stage2)
				break;

			yield return null;
		}

		for(int i = 0; i < bubbleOnRice.Length; i++)
		{
			bubbleOnRice[i].SetActive(false);

		}

		level2.SetActive(true);

		for (int i = 0; i < bubble.Length; i++)
		{
			bubble[i].SetActive(true);
		}

		yield return new WaitForSeconds(0.5f);

		steam.SetActive(true);

		yield return new WaitForSeconds(0.1f);


		while (true)
		{
			
			if (Rice_Check.Result)
				break;

			yield return null;
		}

		resultSmoke.SetActive(true);
		



		yield return null;
	}
}
