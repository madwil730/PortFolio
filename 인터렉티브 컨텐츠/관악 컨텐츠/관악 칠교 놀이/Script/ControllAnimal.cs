using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllAnimal : MonoBehaviour
{
	public GameObject[] rabbit;
	public GameObject[] butterfly;
	public GameObject[] turtle;
	public GameObject[] fish;
	public GameObject[] bird;

	public AudioSource audio;

	public GameObject smokeUp;
	public GameObject smokeDown;

	public static Queue<GameObject> queue = new Queue<GameObject>();
	public static int choice;



	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			choice = 0;
		else if (Input.GetKeyDown(KeyCode.S))
			choice = 1;
		else if (Input.GetKeyDown(KeyCode.D))
			choice = 2;
		else if (Input.GetKeyDown(KeyCode.F))
			choice = 3;
		else if (Input.GetKeyDown(KeyCode.G))
			choice = 4;


		//Debug.Log(queue.Count);
	}

	public void set()
	{
		switch(choice)
		{
			case 1:

				smokeDown.SetActive(true);
				StartCoroutine(setting(rabbit));
				break;

			case 3:
				smokeUp.SetActive(true);
				StartCoroutine(setting(butterfly));
				break;

			case 2:
				smokeDown.SetActive(true);
				StartCoroutine(setting(turtle));
				break;

			case 0:
				smokeDown.SetActive(true);
				StartCoroutine(setting(fish));
				break;

			case 4:
				smokeUp.SetActive(true);
				StartCoroutine(setting(bird));
				break;
		}	
	}

	IEnumerator setting(GameObject[] Ob)
	{
		audio.Play();

		if (queue.Count < 5)
		{

			for (int i = 0; i < 5; i++)
			{
				if (!Ob[i].activeSelf)
				{
					//Debug.Log(i);
					Ob[i].SetActive(true);
					//Ob[i].transform.localPosition = new Vector3(0, 0, 0);

					queue.Enqueue(Ob[i]);
					break;
				}
			}
		}
		else if (queue.Count >= 5) // 총 5개 에서 스캔이 더 들어온다면
		{


			GameObject ob = queue.Dequeue();
			ob.SetActive(false);
			ob.GetComponent<LifeTime>().time = 0;

			for (int i = 0; i < 5; i++)
			{
				if (!Ob[i].activeSelf)
				{
					//Debug.Log(i);
					Ob[i].SetActive(true);
					//Ob[i].transform.localPosition = new Vector3(0, 0, 0);

					queue.Enqueue(Ob[i]);
					break;
				}
			}

			//Debug.Log(111);
		}

		yield return null;
	}
}
