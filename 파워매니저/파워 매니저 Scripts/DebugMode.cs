using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{

	public GameObject[] ob;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.K))
		{
			Debug.Log(33);
			for(int i =0; i < ob.Length; i ++)
			ob[i].SetActive(!ob[i].activeSelf);
		}
    }
}
