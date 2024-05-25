using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEngine.UI;

public class photo : MonoBehaviour
{
	public VCR vcr;
	public Slider slider;
	public GameObject ob;

	//private void OnEnable()
	//{
	//	if (vcr._VideoIndex == 0)
	//		vcr.OnOpenVideoFile();


	//}

	void Update()
    {
		if(ob.activeSelf)
		{
			if (vcr._mediaPlayer.Control.IsPaused() || vcr._mediaPlayerB.Control.IsPaused())
				vcr.OnPlayButton();

			if (vcr._VideoIndex == 1)
			{
				if (slider.value >= 0.99f)
				{
					vcr.OnOpenVideoFile();
					vcr.OnPlayButton();
				}
			}

			if (Input.GetKeyDown(KeyCode.X))
				vcr.OnOpenVideoFile();
		}
		

		if (vcr._VideoIndex == 0 && !ob.activeSelf)
			vcr.OnOpenVideoFile();
    }
}
