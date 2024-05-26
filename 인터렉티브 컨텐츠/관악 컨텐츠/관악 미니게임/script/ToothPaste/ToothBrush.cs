using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : MonoBehaviour
{

	private void OnEnable()
	{
		transform.localPosition = new Vector3(-1000, 1000, 0);
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(ToothSequence.ToothCheck);
        if(ToothSequence.ToothCheck)
		{
			if (Input.mousePosition.x > 250 )
			{
				if(Input.mousePosition.y < 1550)
					transform.position = Input.mousePosition;
			}
		}
    }
}
