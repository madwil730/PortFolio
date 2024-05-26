
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlobTracking.Examples;
using BlobTracking.Core;


public class Coliider : MonoBehaviour
{
	public RectTransform rect;
	public GameObject particle;
	[HideInInspector]
	public bool stay;

	public int i;

	//public enum State { start, stay, end};
	//public static State state;

	private void OnEnable()
	{
		//StartCoroutine(follow());
	}


	private void OnTriggerStay(Collider other)
	{	

		if (other.tag == "follow Rect")
		{
			// 두명이 붙었는데 파티클이 겹치면 다른 곳으로 보내줌
			//particle.SetActive(false);
			//Debug.Log(other.name);
			this.transform.localPosition = new Vector3(2000, 2000, 0);
			
		}

		//followCheck 가 true 면 영역 안이라는 뜻 영역안에서만 따라감 
		else if (other.tag == "icon" && BlobInformationUI.followCheck[i])
		{
				//Debug.Log(other.name);
				this.transform.localPosition = other.transform.localPosition;

		}

	}

	private void OnTriggerExit(Collider other)
	{
		//BlobInformationUI.distanceCheck[i] = false;
		//Debug.Log(123);
	}

}
