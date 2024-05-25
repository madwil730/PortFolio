using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{

	public GameObject ob;
	public static bool start;

	// Update is called once per frame
	void Update()
    {

		if (Singletone.Instance.listPipe.Count == 0)
		{
			ob.SetActive(true);
			start = true;
			//Debug.Log("listpipe is 0");
		}

		else if (Singletone.Instance.listPipe.Count > 0)
		{
			ob.SetActive(false);
			start = false;
			//Debug.Log("listpipe over 0");
		}

	}
}
