using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < Display.displays.Length; i++)
		{
			//Debug.Log(i);
			Display.displays[i].Activate();
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
