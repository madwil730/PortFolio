
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlobTracking.Examples;
using BlobTracking.Core;

public class Particle_New: MonoBehaviour
{
	public RectTransform ColliderTrans; // collider 좌표

	public GameObject CenterImage; // 초기화를 체크하기 위해 넣은 오브젝트
	public GameObject[] starBorn;
	public GameObject[] supernova;
	public ParticleSystem ps;


	public int i; // 블룸 에셋 지정용
	[HideInInspector]
	public bool colliderCheck;


	Vector3[] vec;

	float speed; // 파티클 속도
	float Femission = 60; // 파티클 이미션 적용 할려고 만든 float형 
	float time = 0; //시간 측정용 
	int count = 0; // 숫자 체크용


	private void Start()
	{

		vec = new Vector3[starBorn.Length];
		StartCoroutine(move());

	}

	IEnumerator move()
	{
		yield return new WaitForSeconds(0.1f);

		Vector3 one = ColliderTrans.position;
		colliderCheck = false;

		while (true)
		{
			//제어문
			if (BlobInformationUI.followCheck[i])
			{

				ps.gameObject.SetActive(true);
				ps.transform.position = GetWorldPositionOnPlane(ColliderTrans.position, 10);
				break;
			}
			yield return null;
		}


		while (true)
		{
			//제어문
			if (!BlobInformationUI.followCheck[i])
			{
				ps.gameObject.SetActive(false);
				//Debug.Log(i + "  " + BlobInformationUI.followCheck[i]);
				break;

			}


			//스크린 콜라이더 포지션을 월드뷰로 
			var vec = GetWorldPositionOnPlane(ColliderTrans.position, 10);

			//파티클 따라붙는 속도
			speed = Vector3.Distance(ps.transform.position, vec);

			if (vec.y < 0)
			{
				ps.transform.position = Vector3.MoveTowards(ps.transform.position, new Vector3(vec.x, vec.y + 0.3f, vec.z), Time.deltaTime * speed*3);
				//ps.transform.position = new Vector3(vec.x, vec.y+0.3f, vec.z);
			}
			else
			{
				ps.transform.position = Vector3.MoveTowards(ps.transform.position, vec, Time.deltaTime * speed*3);
				//ps.transform.position = vec;

			}


			//파티클 기울기 조절
			var vo = ps.velocityOverLifetime;
			vo.x = speed;


			// 걸을 때마다 파티클 증가
			if (Vector3.Distance(one, ColliderTrans.position) > ReadXML.Distance)
			{
				one = ColliderTrans.position;

				var em = ps.GetComponent<ParticleSystem>().emission;
				//Debug.Log(33);

				if (Femission <= 100)
					Femission += 10;

				em.rateOverTime = Femission;

			}


			else if (Femission >= 100 && Vector3.Distance(ps.transform.position, vec) < 0.2f)
			{


				// 걍 혹시 몰라 넣어봄 초기화를 위해
				BlobInformationUI.followCheck[i] = false;

				//Debug.Log(i + "  overWalk");
				break;
			}

			time += Time.deltaTime;

			if (time > 40)
				break;

			yield return null;
		}


		vec[count] = ColliderTrans.localPosition;
		//ps.gameObject.SetActive(false);
		var em2 = ps.emission;
		em2.rateOverTime = 70;
		Femission = 70;
		
		

		// 파티클 콜라이더 영역에 아예 맞게 초기화
		ps.transform.position = GetWorldPositionOnPlane(ColliderTrans.position, 10);



		StartCoroutine(ani());
		StartCoroutine(move());

		count++;
		

		if (count >= starBorn.Length)
			count = 0;

		time = 0;

		yield return null;
	}



	IEnumerator ani()
	{
		//Debug.Log(count);
		

		if (ColliderTrans.localPosition.y > 0)
		{
			starBorn[count].transform.localPosition = new Vector3(vec[count].x - ReadXML.ParticleOffset_X, vec[count].y - ReadXML.ParticleOffset_Y, 0);
			supernova[count].transform.localPosition = new Vector3(vec[count].x - ReadXML.ParticleOffset_X, vec[count].y - ReadXML.ParticleOffset_Y, 0);
		}
		else
		{
			starBorn[count].transform.localPosition = new Vector3(vec[count].x - ReadXML.ParticleOffset_X, vec[count].y, 0);
			supernova[count].transform.localPosition = new Vector3(vec[count].x - ReadXML.ParticleOffset_X, vec[count].y, 0);
		}

		// 초기화
		//ColliderTrans.position = new Vector3(10000, 10000, 0);

		if (time > 5)
		{
			switch (Random.Range(0, 3))
			{
				case 0:
					starBorn[count].transform.localScale = new Vector3(0.7f, 0.7f, 1);
					break;

				case 1:
					starBorn[count].transform.localScale = new Vector3(0.5f, 0.5f, 1);
					break;

				case 2:
					starBorn[count].transform.localScale = new Vector3(0.6f, 0.6f, 1);
					break;
			}

			starBorn[count].SetActive(true);
			//yield return new WaitForSeconds(10);
			yield return new WaitForSeconds(1);
		}

		else
		{
			switch (Random.Range(0, 3))
			{
				case 0:
					supernova[count].transform.localScale = new Vector3(0.7f, 0.7f, 1);
					break;

				case 1:
					supernova[count].transform.localScale = new Vector3(0.5f, 0.5f, 1);
					break;

				case 2:
					supernova[count].transform.localScale = new Vector3(0.6f, 0.6f, 1);
					break;
			}

			supernova[count].SetActive(true);
			//yield return new WaitForSeconds(10);
			yield return new WaitForSeconds(1);
		}

		//Debug.Log(count);
		starBorn[count].SetActive(false);
		supernova[count].SetActive(false);
	}



	//perspective 
	public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}
}
