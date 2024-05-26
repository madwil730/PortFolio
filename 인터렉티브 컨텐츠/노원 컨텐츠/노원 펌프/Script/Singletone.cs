using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singletone : MonoBehaviour
{
	private static Singletone instance = null;
	// Start is called before the first frame update

	[HideInInspector]
	public int count;

	public Animator ani;


	private void Awake()
	{
		if (null == instance)
		{
			instance = this;
		}
	}

	public static Singletone Instance
	{
		get
		{
			return instance;
		}
	}


	public void onoff(GameObject[] on, GameObject[] off)
	{

		for (int i = 0; i < off.Length; i++)
		{
			//Debug.Log("off");
			off[i].SetActive(false);
		}

		ani.Play("Fadein",-1,0);

		for (int i = 0; i < on.Length; i++)
		{
			on[i].SetActive(true);
		}

		
	}

}
