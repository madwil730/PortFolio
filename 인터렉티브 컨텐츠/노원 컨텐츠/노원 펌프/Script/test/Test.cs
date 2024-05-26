using UnityEngine;

using UnityEngine.UI;
using System.Collections;
using RenderHeads.Media.AVProVideo;
using RenderHeads.Media.AVProVideo.Demos;



public class Test : MonoBehaviour
{
	public VCR vcr;
	public MediaPlayer a;
	public MediaPlayer b;


	float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(test());
    }

	


	IEnumerator test()
	{
		while(true)
		{
			yield return null;
			Debug.Log(b.m_PlaybackRate);

			if (Input.GetKeyDown(KeyCode.A))
			{
				b.m_PlaybackRate += 0.2f;
				b.Control.SetPlaybackRate(b.m_PlaybackRate);
				//a.m_PlaybackRate += 1;
				//b.m_PlaybackRate += 1;
				//vcr.OnPlayButton();
			
			

				
			}
			
		}
	}
   
}
