using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appleCollider : MonoBehaviour
{
	float time;
	int count;

	public Animator ani;

	public string startState;
	public string endState;

	public Sprite sprite;

	private void OnTriggerEnter(Collider other)
	{
		//count++;

		if (!ani.GetCurrentAnimatorStateInfo(0).IsName(endState))
			ani.Play(startState);
	}

	private void OnTriggerStay(Collider other)
	{
		Debug.Log("Shaking");


		if (time > 2)
		{
			ani.Play(endState);
			time = 0;
			//this.gameObject.GetComponent<Image>().sprite = sprite;
			Debug.Log("Wake up");
			Debug.Log(time);

		}


		if (!ani.GetCurrentAnimatorStateInfo(0).IsName(endState))
		{
			time += Time.deltaTime;
			
			//Debug.Log(time);
		}

	}


	

	private void OnTriggerExit(Collider other)
	{

		//if (count > 0)
		//	count--;

	
		//if (count == 0)
		//{
		//	Debug.Log("Exit");
		//	//time = 0;
		//}
	}

}
