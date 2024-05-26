using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameModeObject : MonoBehaviour
{
	public GameObject[] guide;

	public GameObject[] solar;
	public GameObject[] ourGalaxy;
	public GameObject[] universe;
	public GameObject[] BigUniverse;
	public GameObject[] BigSpace;

	public GameObject[] Title;

	public touch touchSystem;

	

	private void OnEnable()
	{
		StartCoroutine(onOb());
	}

	IEnumerator onOb()
	{
		yield return new WaitForSeconds(0.15f);
		//ob.SetActive(true);

		//Debug.Log(touchSystem.pos);

		guide[0].SetActive(true);
		guide[1].SetActive(true);

		if (touchSystem.pos == touch.Pos.moreLeft)
		{
			solar[0].SetActive(true);
			solar[1].SetActive(true);

			solar[0].GetComponent<Transform>().eulerAngles = new Vector3(-15.45f, 0, 0);
			solar[1].GetComponent<Transform>().eulerAngles = new Vector3(-15.45f, 0, 0);
			solar[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

			Title[0].SetActive(true);

			ourGalaxy[0].SetActive(false);
			ourGalaxy[1].SetActive(false);
			universe[0].SetActive(false);
			universe[1].SetActive(false);
			BigUniverse[0].SetActive(false);
			BigUniverse[1].SetActive(false);
			BigSpace[0].SetActive(false);
			BigSpace[1].SetActive(false);

			//Debug.Log(0);
		}
			

		else if (touchSystem.pos == touch.Pos.left)
		{
			ourGalaxy[0].SetActive(true);
			ourGalaxy[1].SetActive(true);

			ourGalaxy[0].GetComponent<Transform>().eulerAngles = new Vector3(51.595f, 0, -48.582f);
			ourGalaxy[1].GetComponent<Transform>().eulerAngles = new Vector3(51.595f, 0, -48.582f);
			ourGalaxy[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

			Title[1].SetActive(true);

			solar[0].SetActive(false);
			solar[1].SetActive(false);
			universe[0].SetActive(false);
			universe[1].SetActive(false);
			BigUniverse[0].SetActive(false);
			BigUniverse[1].SetActive(false);
			BigSpace[0].SetActive(false);
			BigSpace[1].SetActive(false);
			//Debug.Log(1);
		}
			

		else if (touchSystem.pos == touch.Pos.center)
		{
			universe[0].SetActive(true);
			universe[1].SetActive(true);

			universe[0].GetComponent<Transform>().eulerAngles = new Vector3(-32.835f, 50.818f, -33.634f);
			universe[1].GetComponent<Transform>().eulerAngles = new Vector3(-32.835f, 50.818f, -33.634f);
			universe[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

			Title[2].SetActive(true);

			solar[0].SetActive(false);
			solar[1].SetActive(false);
			ourGalaxy[0].SetActive(false);
			ourGalaxy[1].SetActive(false);
			BigUniverse[0].SetActive(false);
			BigUniverse[1].SetActive(false);
			BigSpace[0].SetActive(false);
			BigSpace[1].SetActive(false);
			//Debug.Log(2);
		}
			

		else if (touchSystem.pos == touch.Pos.right)
		{
			BigUniverse[0].SetActive(true);
			BigUniverse[1].SetActive(true);

			BigUniverse[0].GetComponent<Transform>().eulerAngles = new Vector3(-24.214f, 57.425f, -29.578f);
			BigUniverse[1].GetComponent<Transform>().eulerAngles = new Vector3(-24.214f, 57.425f, -29.578f);
			BigUniverse[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

			Title[3].SetActive(true);

			solar[0].SetActive(false);
			solar[1].SetActive(false);
			ourGalaxy[0].SetActive(false);
			ourGalaxy[1].SetActive(false);
			universe[0].SetActive(false);
			universe[1].SetActive(false);
			BigSpace[0].SetActive(false);
			BigSpace[1].SetActive(false);
		}
			

		else if (touchSystem.pos == touch.Pos.moreRight)
		{
			BigSpace[0].SetActive(true);
			BigSpace[1].SetActive(true);

			BigSpace[0].GetComponent<Transform>().eulerAngles = new Vector3(-31.181f, 51.097f, -32.684f);
			BigSpace[1].GetComponent<Transform>().eulerAngles = new Vector3(-31.181f, 51.097f, -32.684f);
			BigSpace[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

			Title[4].SetActive(true);

			solar[0].SetActive(false);
			solar[1].SetActive(false);
			ourGalaxy[0].SetActive(false);
			ourGalaxy[1].SetActive(false);
			universe[0].SetActive(false);
			universe[1].SetActive(false);
			BigUniverse[0].SetActive(false);
			BigUniverse[1].SetActive(false);
		}

	}

	
}
