using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
	public GameObject[] ob;
    
	public void onoff()
	{
		for (int i = 0; i < ob.Length; i++)
			ob[i].SetActive(!ob[i].activeSelf);

	}
}
