using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTimer : MonoBehaviour
{
	float time;
	public GameObject[] on;
	public GameObject[] off;
    // Start is called before the first frame update
  


	private void OnEnable()
	{
		StartCoroutine(times());
	}

	IEnumerator times()
	{
		while(true)
		{
			time += Time.deltaTime;

			if (ReadSerial.Count1 > 3 || ReadSerial.Count2 > 3)
				time = 0;

			if (time > 30)
				break;

			yield return null;
		}

		Singletone.Instance.onoff(on, off);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
