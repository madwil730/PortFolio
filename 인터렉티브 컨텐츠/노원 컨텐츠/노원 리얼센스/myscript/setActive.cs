using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;

public class setActive : MonoBehaviour
{
	public VCR vcr;

    void setFalse()
	{
		StartCoroutine(toFalse());
	}

	IEnumerator toFalse()
	{
		vcr.OnOpenVideoFile();
		yield return new WaitForSeconds(0.2f);
		this.gameObject.SetActive(false);
	}
}
