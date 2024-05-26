using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
	public Sprite[] sprite;
	[Tooltip("0.5 이하")]
	public Sprite[] SpriteUrgent1;
	[Tooltip("0.5 이상")]
	public Sprite[] SpriteUrgent2;
	public static double time = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;

		if (time < 30)
			this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite[(int)time];
		//int a = Math.Truncate(time);

		else if (time > 30 && time < 40)
		{
			if (time - (int)time < 0.5f)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = SpriteUrgent1[(int)time-30];
				//Debug.Log("이하");
			}

			else if (time - (int)time > 0.5f)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = SpriteUrgent2[(int)time - 30];
				//Debug.Log("이상");
			}
				
			
		}

		else if (time > 40)
		{
			return;
		}
			

	}
}
