using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class badTooth : MonoBehaviour
{
	public Animator ani;
	public GameObject bubble;
	float time;

	private void OnEnable()
	{
		time = 0;
		bubble.SetActive(false);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		
		time += Time.deltaTime;

		if(time > 2)
		{
			ani.SetBool("dead", true);
			bubble.SetActive(false);
		}
		else
			bubble.SetActive(true);
	}


}
