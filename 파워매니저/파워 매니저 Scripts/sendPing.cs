using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendPing : MonoBehaviour
{
	public InputField ip;
	public Image image;

	float time =0 ;

	public void Start()
	{
		
		StartCoroutine(pings());
	}


	IEnumerator pings()
	{
		//Debug.Log(ip.text);

		yield return new WaitForSeconds(0.5f);

		Ping ping = new Ping(ip.text);

		while(true)
		{
			time += Time.deltaTime;

			if(time> 60)
			{
				if (ping.isDone)
				{
					image.color = Color.white;
				}

				else
				{
					image.color = Color.black;
				}

				time = 0;
			}

			yield return null;
		}
	}
}
