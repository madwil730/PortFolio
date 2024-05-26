using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singletone : MonoBehaviour
{
	private static Singletone instance = null;
	// Start is called before the first frame update


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

	public void onOj(GameObject on)
	{
		on.SetActive(true);
	}

	public IEnumerator onoff(GameObject[] on, GameObject[] off)
	{
		yield return null;

		for (int i = 0; i < off.Length; i++)
		{
			//Debug.Log("off");
			off[i].SetActive(false);
		}

		ani.Play("Fade", -1, 0);

		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < on.Length; i++)
		{
			on[i].SetActive(true);
		}
	}

}
