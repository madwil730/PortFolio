using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;

public class VCRController : MonoBehaviour
{
	public VCR vcr;
	public GameObject[] onimage;
	public GameObject[] offimage;

	[SerializeField] Animator _Animator;
	// Start is called before the first frame update


	private void OnEnable()
	{
		StartCoroutine(Play());
	}
	
	IEnumerator Play()
	{
		yield return new WaitForSeconds(0.5f);
		//Debug.Log(123);
		vcr.OnPlayButton();
		//Debug.Log(2222);

		while(true)
		{
			

			if (vcr._videoSeekSlider.value == 1)
			{
				_Animator.Play("Transition");

				yield return new WaitForSeconds(0.3f);

				for (int i = 0; i < onimage.Length; i++)
					onimage[i].SetActive(true);

				for (int i = 0; i < offimage.Length; i++)
					offimage[i].SetActive(false);

				//vcr._videoSeekSlider.value = 0;
				vcr.OnRewindButton();
				vcr.OnPauseButton();


				break;
			}
			yield return null;
		}
	}

}
