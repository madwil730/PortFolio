
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlobTracking.Examples;
using BlobTracking.Core;

public class Particle_Collider : MonoBehaviour
{
	public RectTransform ColliderTrans; // collider 좌표

	public GameObject CenterImage; // 초기화를 체크하기 위해 넣은 오브젝트
	public GameObject starBorn;
	public GameObject supernova;
	public ParticleSystem ps;


	public int i; // 블룸 에셋 지정용
	[HideInInspector]
	public bool colliderCheck;
	float speed; // 파티클 속도


	
	float Femission = 10; // 파티클 이미션 적용 할려고 만든 float형 
	float time = 0; //시간 측정용 
	int count = 0; // 100m 이동 측정


	private void Start()
	{
		

		StartCoroutine(move());

	}

	IEnumerator move()
	{
		yield return new WaitForSeconds(0.1f);

		Vector3 one = ColliderTrans.position;
		starBorn.SetActive(false);
		supernova.SetActive(false);
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
				//Debug.Log(i + "  " + BlobInformationUI.followCheck[i]);
				break;
				
			}

			// 콜라이더 렉트끼리 충돌했을때 
			if (ColliderTrans.transform.localPosition.x == 2000)
				break;

			//Debug.Log(BlobInformationUI.followCheck[i]);

			//if (Vector3.Distance(Vector3.zero, CenterImage.transform.localPosition) > 650)
			//	break;

			//스크린 콜라이더 포지션을 월드뷰로 
			var vec = GetWorldPositionOnPlane(ColliderTrans.position,10);

			//파티클 따라붙는 속도
			speed = Vector3.Distance(ps.transform.position, vec)* 2;

			if(vec.y <0)
			{
				ps.transform.position = Vector3.MoveTowards(ps.transform.position, new Vector3(vec.x,vec.y+0.3f,vec.z), Time.deltaTime * speed);
			}
			else
				ps.transform.position = Vector3.MoveTowards(ps.transform.position, vec, Time.deltaTime * speed);


			//파티클 기울기 조절
			//var vo = ps.velocityOverLifetime;
			//vo.x = speed;


			// 걸을 때마다 파티클 증가
			if (Vector3.Distance(one, ColliderTrans.position) > ReadXML.Distance)
			{
				one = ColliderTrans.position;

				var em = ps.GetComponent<ParticleSystem>().emission;


				if (Femission <= 40)
					Femission += 10;

				em.rateOverTime = Femission;

			}

			
			else if (Femission >= 40 && Vector3.Distance(ps.transform.position, vec) < 0.2f)
			{
				

				// 걍 혹시 몰라 넣어봄 초기화를 위해
				//BlobInformationUI.followCheck[i] = false;

				Debug.Log(i + "  overWalk");
				break;
			}

			time += Time.deltaTime;

			if (time > 20)
				break;

			yield return null;
		}

		ps.gameObject.SetActive(false);
		var em2 = ps.emission;
		em2.rateOverTime = 10;
		Femission = 10;

		// 파티클 콜라이더 영역에 아예 맞게 초기화
		ps.transform.position = GetWorldPositionOnPlane(ColliderTrans.position, 10);

		if (ColliderTrans.localPosition.y > 0)
		{
			starBorn.transform.localPosition = new Vector3(ColliderTrans.localPosition.x - ReadXML.ParticleOffset_X, ColliderTrans.localPosition.y-ReadXML.ParticleOffset_Y, 0);
			supernova.transform.localPosition = new Vector3(ColliderTrans.localPosition.x - ReadXML.ParticleOffset_X, ColliderTrans.localPosition.y - ReadXML.ParticleOffset_Y, 0);
		}
		else
		{
			starBorn.transform.localPosition = new Vector3(ColliderTrans.localPosition.x - ReadXML.ParticleOffset_X, ColliderTrans.localPosition.y, 0);
			supernova.transform.localPosition = new Vector3(ColliderTrans.localPosition.x - ReadXML.ParticleOffset_X, ColliderTrans.localPosition.y, 0);
		}
			


		// 초기화
		ColliderTrans.position = new Vector3(10000, 10000, 0);
		

		//모이는 거 보기 위해 넣은거 ?
		yield return new WaitForSeconds(2);


	
		
		//제어문
		colliderCheck = true;




		if (time > 5)
		{
			switch (Random.Range(0, 3))
			{
				case 0:
					starBorn.transform.localScale = new Vector3(0.7f, 0.7f, 1);
					break;

				case 1:
					starBorn.transform.localScale = new Vector3(0.5f, 0.5f, 1);
					break;

				case 2:
					starBorn.transform.localScale = new Vector3(0.6f, 0.6f, 1);
					break;
			}

			starBorn.SetActive(true);
			//yield return new WaitForSeconds(18);
			yield return new WaitForSeconds(3);
		}

		else
		{
			switch (Random.Range(0, 3))
			{
				case 0:
					supernova.transform.localScale = new Vector3(0.7f, 0.7f, 1);
					break;

				case 1:
					supernova.transform.localScale = new Vector3(0.5f, 0.5f, 1);
					break;

				case 2:
					supernova.transform.localScale = new Vector3(0.6f, 0.6f, 1);
					break;
			}

			supernova.SetActive(true);
			//yield return new WaitForSeconds(10);
			yield return new WaitForSeconds(3);
		}

		time = 0;
		StartCoroutine(move());

		yield return null;
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
