using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;

public class vcrController : MonoBehaviour
{
	public VCR vcr;

	private void OnEnable()
	{
		StartCoroutine(setup());
	}



	IEnumerator setup()
	{
		yield return new WaitForSeconds(0.1f);

		Debug.Log(vcr._VideoIndex);

		if(vcr._videoSeekSlider.value>0.1f)
		{
			vcr._VideoIndex = 0;
			vcr.OnOpenVideoFile();
		}

		else if (vcr._VideoIndex == 0)
		{
			vcr._VideoIndex = 0;
			vcr.OnOpenVideoFile();

			//vcr._mediaPlayer.Control.SetLooping(false);
			//vcr._mediaPlayerB.Control.SetLooping(false);
		}

		//else if(vcr._mediaPlayer.Control.IsPlaying)
	}
}
