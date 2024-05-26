using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XDPaint.Demo;

public class PaintButton : MonoBehaviour
{
	public GameObject[] on;
	public GameObject[] off;
	public Demo demo;

	public void onPaint()
	{
		

		on[0].transform.position = Vector3.zero;
		on[1].transform.localPosition = Vector3.zero;

		off[0].SetActive(false);
	}

	public void offPaint()
	{
		off[0].transform.position = new Vector3(3000, 0, 0);
		off[1].transform.localPosition = new Vector3(3000, 0, 0);

		on[0].SetActive(true);
	}
}
