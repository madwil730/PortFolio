using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLine : MonoBehaviour
{
	public Image[] image;
	public TrailRenderer[] trail;
	public GameObject PathMagic_Star_Reverse;
	public int i;
	// Start is called before the first frame update

	private void OnEnable()
	{

		if (i == 1)
			StartCoroutine(starLine1());
		else if (i == 2)
			StartCoroutine(starLine2());
		else if (i == 3)
			StartCoroutine(starLine3());
	}

	IEnumerator starLine1()
	{
		yield return new WaitForSeconds(3);

		while(true)
		{
			if (image[0].fillAmount < 1)
			{
				image[0].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}

		while (true)
		{
			if (image[1].fillAmount < 1)
			{
				image[1].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}

		while (true)
		{
			if (image[2].fillAmount < 1)
			{
				image[2].fillAmount += Time.deltaTime;
			}
			
			if (image[3].fillAmount < 1)
			{
				image[3].fillAmount += Time.deltaTime;
			}

			if (image[2].fillAmount >= 1 && image[3].fillAmount >= 1)
				break;

			yield return null;
		}


		while(true)
		{	
			if(PathMagic_Star_Reverse.activeSelf)
			{
				for (int i = 0; i < image.Length; i++)
					image[i].fillAmount = 0;
			
				for (int i = 0; i < trail.Length; i++)
					trail[i].enabled = true;

				break;
			}

			yield return null;
			
		}

		yield return new WaitForSeconds(3.5f);

		for (int i = 0; i < trail.Length; i++)
			trail[i].enabled = false;
		
		yield return null;
	}

	IEnumerator starLine2()
	{
		yield return new WaitForSeconds(3);

		while (true)
		{
			if (image[0].fillAmount < 1)
			{
				image[0].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}

		while (true)
		{
			if (image[1].fillAmount < 1)
			{
				image[1].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}


		while (true)
		{
			if (image[2].fillAmount < 1)
			{
				image[2].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}
		yield return null;

		while (true)
		{
			if (PathMagic_Star_Reverse.activeSelf)
			{
				for (int i = 0; i < image.Length; i++)
					image[i].fillAmount = 0;

				for (int i = 0; i < trail.Length; i++)
					trail[i].enabled = true;

				break;
			}

			yield return null;

		}

		yield return new WaitForSeconds(3.5f);

		for (int i = 0; i < trail.Length; i++)
			trail[i].enabled = false;



	}

	IEnumerator starLine3()
	{
		yield return new WaitForSeconds(3);

		while (true)
		{
			if (image[0].fillAmount < 1)
			{
				image[0].fillAmount += Time.deltaTime;
			}

			if (image[1].fillAmount < 1)
			{
				image[1].fillAmount += Time.deltaTime;
			}

			if (image[0].fillAmount >= 1 && image[1].fillAmount >= 1)
				break;

			yield return null;
		}

		while (true)
		{
			if (image[2].fillAmount < 1)
			{
				image[2].fillAmount += Time.deltaTime;
			}

			if (image[3].fillAmount < 1)
			{
				image[3].fillAmount += Time.deltaTime;
			}

			if (image[2].fillAmount >= 1 && image[3].fillAmount >= 1)
				break;

			yield return null;
		}

		while (true)
		{
			if (image[4].fillAmount < 1)
			{
				image[4].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}

		while (true)
		{
			if (image[5].fillAmount < 1)
			{
				image[5].fillAmount += Time.deltaTime;
			}
			else
				break;

			yield return null;
		}

		while (true)
		{
			if (PathMagic_Star_Reverse.activeSelf)
			{
				for (int i = 0; i < image.Length; i++)
					image[i].fillAmount = 0;

				for (int i = 0; i < trail.Length; i++)
					trail[i].enabled = true;

				break;
			}

			yield return null;

		}

		yield return new WaitForSeconds(3.5f);

		for (int i = 0; i < trail.Length; i++)
			trail[i].enabled = false;

	}

	float wiggleDistance = 0.2f;
	float wiggleSpeed = 1;

}
