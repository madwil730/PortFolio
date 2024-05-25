using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class nightCollider : MonoBehaviour
{
	public Animator ani;

	public string startState;
	public string endState;

	bool check = true;


	private void OnEnable()
	{
		this.GetComponent<Image>().color = Color.white;
		a = 1;
		
	}

	private void OnTriggerStay(Collider other)
	{

		ani.SetBool("on", true);
	}



	float a = 1;
	private void Update()
	{
		if (ani.GetCurrentAnimatorStateInfo(0).IsName(endState)
			&& ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
		{

			ani.SetBool("on", false);
			check = false;

		}


		if (videoController.time > 115)
		{
			
			if (a >= 0)
			{
				a -= Time.deltaTime;
				this.GetComponent<Image>().color = new Color(1, 1, 1, a);

			}
		}

	}


	void goFirst()
	{
		ani.SetBool("on", false);
		ani.Play(startState);
		check = true;

	}
}
