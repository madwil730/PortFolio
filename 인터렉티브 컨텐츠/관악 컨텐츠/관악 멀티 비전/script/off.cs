using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;

public class off : MonoBehaviour
{
	public VCR vcr;

	private void Start()
	{
		StartCoroutine(aaa());
	}

	IEnumerator aaa()
	{
		yield return new WaitForSeconds(1);
		vcr._mediaPlayerB.Control.Stop();
	}
}
