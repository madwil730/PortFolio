using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	float time;


    void Start()
    {
		StartCoroutine(aaa());
	
    }


	IEnumerator aaa()
	{
		while(true)
		{
			Debug.Log(1);

			time += Time.deltaTime;
			if(time > 2)
				

			yield return null;
		}
		yield return null;
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
