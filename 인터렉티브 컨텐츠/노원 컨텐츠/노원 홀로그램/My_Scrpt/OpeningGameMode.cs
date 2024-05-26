using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningGameMode : MonoBehaviour
{
	[SerializeField] HologramZoomCameraController _HologramZoomCameraController;

	public GameObject[] solar;
	public GameObject[] ourGalaxy;
	public GameObject[] universe;
	public GameObject[] BigUniverse;
	public GameObject[] BigSpace;


	public touch touchSystem;



	private void OnEnable()
	{
		StartCoroutine(onOb());
	}

	IEnumerator onOb()
	{
		yield return null;

		//if (touchSystem.pos == touch.Pos.right)
		//	_HologramZoomCameraController.CameraInit_right();
		//else
			_HologramZoomCameraController.CameraInit();


		if (touchSystem.pos == touch.Pos.moreLeft)
		{
		

			solar[0].GetComponent<Transform>().eulerAngles = new Vector3(-15.45f, 0, 0);
			solar[1].GetComponent<Transform>().eulerAngles = new Vector3(-15.45f, 0, 0);
			solar[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

			ourGalaxy[0].SetActive(false);
			ourGalaxy[1].SetActive(false);
			universe[0].SetActive(false);
			universe[1].SetActive(false);
			BigUniverse[0].SetActive(false);
			BigUniverse[1].SetActive(false);
			BigSpace[0].SetActive(false);
			BigSpace[1].SetActive(false);


		}


		else if (touchSystem.pos == touch.Pos.left)
		{
		
			ourGalaxy[0].GetComponent<Transform>().eulerAngles = new Vector3(51.595f, 0, -48.582f);
			ourGalaxy[1].GetComponent<Transform>().eulerAngles = new Vector3(51.595f, 0, -48.582f);
			ourGalaxy[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

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


			universe[0].GetComponent<Transform>().eulerAngles = new Vector3(-32.835f, 50.818f, -33.634f);
			universe[1].GetComponent<Transform>().eulerAngles = new Vector3(-32.835f, 50.818f, -33.634f);
			universe[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

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
			

			BigUniverse[0].GetComponent<Transform>().eulerAngles = new Vector3(-24.214f, 57.425f, -29.578f);
			BigUniverse[1].GetComponent<Transform>().eulerAngles = new Vector3(-24.214f, 57.425f, -29.578f);
			BigUniverse[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

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
			

			BigSpace[0].GetComponent<Transform>().eulerAngles = new Vector3(-31.181f, 51.097f, -32.684f);
			BigSpace[1].GetComponent<Transform>().eulerAngles = new Vector3(-31.181f, 51.097f, -32.684f);
			BigSpace[0].GetComponent<Transform>().localPosition = new Vector3(0, -0.54f, 12);

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

	float a = 0;
	float b = 57.425f;
	float c = 51.097f;

	private void Update()
	{
		a += Time.deltaTime * 6;
		b += Time.deltaTime * 6;
		c += Time.deltaTime * 6;
		ourGalaxy[1].transform.eulerAngles = new Vector3(51.595f, a, -48.582f);
		universe[1].transform.eulerAngles = new Vector3(-32.835f, a, -33.634f);
		BigUniverse[1].transform.eulerAngles = new Vector3(-24.214f, b, -29.578f);
		BigSpace[1].transform.eulerAngles = new Vector3(-31.181f, c, -32.684f);
	}
}
