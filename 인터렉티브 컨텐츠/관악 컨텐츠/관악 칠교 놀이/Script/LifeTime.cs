using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
	Rigidbody rigidbody;

	[HideInInspector]
	public float time = 0;


	void Start()
	{
		rigidbody = this.GetComponent<Rigidbody>();

		// 토끼
		if (ControllAnimal.choice == 1)
			StartCoroutine(Move(-60, -1050, -600));

		// 나비
		else if (ControllAnimal.choice == 3)
			//StartCoroutine(Postion(150));
			StartCoroutine(Move(1050, 60, 600));

		// 거북이
		else if (ControllAnimal.choice == 2)
			//StartCoroutine(Postion(-150));
			StartCoroutine(Move(-60, -1050, -600));

		//물고기
		else if (ControllAnimal.choice == 0)
			//StartCoroutine(Postion(-250));
			StartCoroutine(Move(-60, -1050, -600));

		// 새
		else if (ControllAnimal.choice == 4)
			//StartCoroutine(Postion(250));
			StartCoroutine(Move(1050, 60, 600));


	}

	IEnumerator Postion(int y)
	{
		yield return new WaitForSeconds(0.5f);

		while (true)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, y, 0), 3);
			if (transform.localPosition.y == y)
				break;

			yield return null;
		}

		if (ControllAnimal.choice == 1)
			StartCoroutine(Move(200, 100, 150));

		else if (ControllAnimal.choice == 2)
			StartCoroutine(Move(-100, -200, -150));

		else if (ControllAnimal.choice == 3)
			StartCoroutine(Move(-200, -300, -250));

		else if (ControllAnimal.choice == 4)
			StartCoroutine(Move(300, 200, 250));

		yield return null;
	}

	// y1 큰 수 , y2 작은 수
	IEnumerator Move(int y1, int y2, int height)
	{
		transform.localPosition = new Vector3(0, height, 0);

		while (true)
		{
			float x = Random.Range(-3f, 3f);

			float y = Random.Range(-3f, 3f);

			yield return new WaitForSeconds(3);


			if (transform.localPosition.x < -1900)
				x = 3;
			else if (transform.localPosition.x > 1900)
				x = -3;

			if (transform.localPosition.y < y2)
				y = 3;
			else if (transform.localPosition.y > y1)
				y = -3;


			rigidbody.velocity = new Vector3(x*2, y*2);

			yield return null;
		}
	}

	void Update()
    {
		if(this.gameObject.activeSelf)
		{
			time += Time.deltaTime;
			//Debug.Log(time);

			if(time > 200)
			{
				ControllAnimal.queue.Dequeue().SetActive(false);

				time = 0;
			}
		}
    }
}
