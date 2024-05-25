using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEngine.UI;

public class videoController : MonoBehaviour
{
	public VCR vcr;
	public Slider slider;
	[Header("------나무 영상-----")]
	public GameObject[] on_0;
	public GameObject[] off_0;

	[Header("------나뭇잎 인터렉티브-----")]
	public GameObject[] on_1;
	public GameObject[] off_1;

	[Header("------사과 영상------")]
	public GameObject[] on_2;
	public GameObject[] off_2;

	[Header("------사과 인터렉티브------")]
	public GameObject[] on_3;
	public GameObject[] off_3;

	[Header("------밤 영상------")]
	public GameObject[] on_4;
	public GameObject[] off_4;

	[Header("------밤 인터렉티브------")]
	public GameObject[] on_5;
	public GameObject[] off_5;

	[Header("------홍보 영상------")]
	public GameObject[] on_6;
	public GameObject[] off_6;

	[Header("------포토 존------")]
	public GameObject[] on_7;
	public GameObject[] off_7;

	[Header("------------")]
	public GameObject photoZone;
	public GameObject Title;
	public GameObject panel;

	public static float time;
	bool photoCheck;
	bool check;
	
	enum flowCheck
	{
		treeMovie,
		treeInteractive,
		appleMovie,
		appleInteractive,
		nightMovie,
		nightInteractive,
		promotion,
		phot
	}

	flowCheck myflow = flowCheck.treeMovie;
	
	//--------------------------------------------------------

	void Start()
    {
		StartCoroutine(scene_movie(on_0,off_0));
		Screen.SetResolution(3840, 2160, true);
    }

	IEnumerator scene_movie(GameObject[] onOb, GameObject[] offOb)
	{
		yield return new WaitForSeconds(0.3f);

		foreach (var ob in offOb)
			ob.SetActive(false);
	
		//vcr.OnRewindButton();
		vcr.OnPlayButton();

		Debug.Log(myflow);

		foreach (var ob in onOb)
			ob.SetActive(true);
			
		yield return new WaitForSeconds(0.1f);

		while (true)
		{
			if (Input.GetKeyDown(KeyCode.Space) && !photoZone.activeSelf)
			{
				foreach (var ob in offOb)
					ob.SetActive(false);
				
				vcr.OnPauseButton();
				break;
			}

			if (slider.value >= 1  )
				break;
			
			if (check)
				break;
	
			yield return null;
		}

		vcr._mediaPlayerB.Stop();
		vcr._mediaPlayer.Stop();

		if (check)
			checkflow();

		else if (myflow == flowCheck.treeMovie)
		{
			
			myflow = flowCheck.treeInteractive;
			vcr.OnOpenVideoFile();
			vcr._mediaPlayer.Control.SetLooping(true);
			vcr._mediaPlayerB.Control.SetLooping(true);
			StartCoroutine(scene_interactive(on_1,off_1));
		}

		else if (myflow == flowCheck.appleMovie)
		{
			
			myflow = flowCheck.appleInteractive;
			vcr._mediaPlayer.Control.SetLooping(false);
			vcr._mediaPlayerB.Control.SetLooping(false);
			StartCoroutine(scene_interactive(on_3, off_3));
		}

		else if (myflow == flowCheck.nightMovie)
		{
			
			myflow = flowCheck.nightInteractive;
			vcr.OnOpenVideoFile();
			vcr._mediaPlayer.Control.SetLooping(true);
			vcr._mediaPlayerB.Control.SetLooping(true);

			StartCoroutine(scene_interactive(on_5, off_5));
		}

		else if (myflow == flowCheck.promotion)
		{
			vcr._VideoIndex = 0;
			//vcr._mediaPlayerB.Stop();
			//vcr._mediaPlayer.Stop();
			myflow = flowCheck.treeMovie;
			vcr.OnOpenVideoFile();
		
			
			StartCoroutine(scene_movie(on_0, off_0));
		}
	}

	//---------------------------------------------------------

	IEnumerator scene_interactive(GameObject[] onOb, GameObject[] offOb)
	{

		time = 0;
		Debug.Log(myflow);

		foreach (var ob in offOb)
			ob.SetActive(false);
		
		if (myflow == flowCheck.treeInteractive)
			yield return new WaitForSeconds(1);

		else if (myflow == flowCheck.nightInteractive)
			yield return new WaitForSeconds(1);


		foreach (var ob in onOb)
			ob.SetActive(true);
			

		while (true)
		{
			if(!photoZone.activeSelf)
			time += Time.deltaTime;

			//Debug.Log(time);

			 if (Input.GetKeyDown(KeyCode.Space) && !photoZone.activeSelf)
				break;
			//120
			if (time > 120)
				break;

			if (check)
				break;

			yield return null;
		}

		//Debug.Log(222);

		vcr._mediaPlayerB.Stop();
		vcr._mediaPlayer.Stop();

		if (check)
			checkflow();

		 else if (myflow == flowCheck.treeInteractive)
		{
			vcr.OnOpenVideoFile();
			vcr._mediaPlayer.Control.SetLooping(false);
			vcr._mediaPlayerB.Control.SetLooping(false);
			myflow = flowCheck.appleMovie;
			StartCoroutine(scene_movie(on_2, off_2));
		}

		else if (myflow == flowCheck.appleInteractive)
		{
			vcr.OnOpenVideoFile();
			myflow = flowCheck.nightMovie;
			StartCoroutine(scene_movie(on_4, off_4));
		}

		else if (myflow == flowCheck.nightInteractive)
		{
			//Debug.Log(333);
			vcr.OnOpenVideoFile();
			vcr._mediaPlayer.Control.SetLooping(false);
			vcr._mediaPlayerB.Control.SetLooping(false);
			myflow = flowCheck.promotion;
			StartCoroutine(scene_movie(on_6, off_6));
		}

		yield return null;
	}




	void checkflow()
	{
			if (myflow == flowCheck.treeMovie)
			{
				check = false;
				vcr._VideoIndex = 0;
				vcr.OnOpenVideoFile();
				vcr._mediaPlayer.Control.SetLooping(true);
				vcr._mediaPlayerB.Control.SetLooping(true);
				StartCoroutine(scene_movie(on_0, off_0));
			}

			else if (myflow == flowCheck.appleMovie)
			{
				check = false;
				vcr._VideoIndex = 2;
				vcr.OnOpenVideoFile();
				vcr._mediaPlayer.Control.SetLooping(false);
				vcr._mediaPlayerB.Control.SetLooping(false);
				StartCoroutine(scene_movie(on_2, off_2));
			}

			else if (myflow == flowCheck.nightMovie)
			{
				check = false;
				vcr._VideoIndex = 3;
				vcr.OnOpenVideoFile();
				vcr._mediaPlayer.Control.SetLooping(true);
				vcr._mediaPlayerB.Control.SetLooping(true);
				StartCoroutine(scene_movie(on_4, off_4));
			}

			else if (myflow == flowCheck.promotion)
			{
				check = false;
				vcr._VideoIndex = 5;
				vcr.OnOpenVideoFile();
				vcr._mediaPlayer.Control.SetLooping(false);
				vcr._mediaPlayerB.Control.SetLooping(false);
				StartCoroutine(scene_movie(on_6, off_6));
			}
		
	}

	//--------------------------------------------------

	private void Update()
	{

		// if (Input.GetKeyDown(KeyCode.P) && !photoZone.activeSelf)
		//{
		//	Title.SetActive(true);
		//	photoZone.SetActive(false);

		//	if (myflow != flowCheck.appleInteractive)
		//		vcr.OnPauseButton();
		//}
		if (Input.GetKeyDown(KeyCode.Q) && !Title.activeSelf)
		{
			Title.SetActive(false);
			photoZone.SetActive(true);
			panel.SetActive(false);

			if( myflow != flowCheck.appleInteractive)
			vcr.OnPauseButton();
		}

		else if (Input.GetKeyDown(KeyCode.M))
		{
			Title.SetActive(false);
			photoZone.SetActive(false);
			panel.SetActive(true);

			if (myflow != flowCheck.appleInteractive)
				vcr.OnPlayButton();
		}
		
		if (Input.GetKeyDown(KeyCode.L))
		{
			vcr.OnPauseButton();
		}

		else if (Input.GetKeyDown(KeyCode.K))
		{
			vcr.OnPlayButton();
		}

		//else if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//	vcr._VideoIndex = 0;
		//	myflow = flowCheck.treeMovie;
		//	check = true;
		//}

		//else if (Input.GetKeyDown(KeyCode.Alpha2))
		//{
		//	vcr._VideoIndex = 2;
		//	myflow = flowCheck.appleMovie;
		//	check = true;
		//}

		//else if (Input.GetKeyDown(KeyCode.Alpha3))
		//{
		//	vcr._VideoIndex = 3;
		//	myflow = flowCheck.nightMovie;
		//	check = true;
		//}

		//else if (Input.GetKeyDown(KeyCode.Alpha4))
		//{
		//	vcr._VideoIndex = 5;
		//	myflow = flowCheck.promotion;
		//	check = true;
		//}

	}



















	//IEnumerator photo()
	//{
	//	photoCheck = false;

	//	time = 0;
	//	foreach (var ob in on_7)
	//	{
	//		ob.SetActive(true);
	//	}

	//	while (true)
	//	{
	//		time += Time.deltaTime;


	//		if (time > 2)
	//			break;

	//		yield return null;
	//	}

	//	time = 0;
	//	foreach (var ob in off_7)
	//	{
	//		ob.SetActive(false);
	//	}

	//	vcr._VideoIndex = 0;
	//	vcr.OnOpenVideoFile();

	//	myflow = flowCheck.treeMovie;
	//	StartCoroutine(scene_movie(on_0, off_0));
	//	yield return null;
	//}























	// 사과 영상
	//IEnumerator scene_movie_2()
	//{
	//	yield return new WaitForSeconds(0.1f);
	//	while (true)
	//	{

	//		if(slider.value >= 1)
	//		{

	//			foreach (var ob in on_0)
	//			{
	//				ob.SetActive(true);
	//			}

	//			foreach (var ob in off_0)
	//			{
	//				ob.SetActive(false);
	//			}

	//			break;
	//		}
	//		yield return null;
	//	}

	//	time = 0;
	//	StartCoroutine(scene_interactive_3());

	//	yield return null;
	//}

	////사과 인터렉티브
	//IEnumerator scene_interactive_3()
	//{

	//	while (true)
	//	{

	//		time += Time.deltaTime;

	//		if (time > 2)
	//			break;

	//		yield return null;
	//	}

	//	foreach (var ob in on_1)
	//	{
	//		ob.SetActive(true);
	//	}

	//	foreach (var ob in off_1)
	//	{
	//		ob.SetActive(false);
	//	}


	//	vcr.OnOpenVideoFile();
	//	StartCoroutine(scene_movie_4());

	//	yield return null;
	//}

	//// 밤 영상
	//IEnumerator scene_movie_4()
	//{

	//	yield return new WaitForSeconds(0.1f);
	//	while (true)
	//	{

	//		if (slider.value >= 1)
	//		{

	//			foreach (var ob in on_0)
	//			{
	//				ob.SetActive(true);
	//			}

	//			foreach (var ob in off_0)
	//			{
	//				ob.SetActive(false);
	//			}

	//			break;
	//		}
	//		yield return null;
	//	}

	//	time = 0;
	//	StartCoroutine(scene_interactive_5());

	//	yield return null;
	//}

	////밤 인터렉티브
	//IEnumerator scene_interactive_5()
	//{

	//	while (true)
	//	{

	//		time += Time.deltaTime;

	//		if (time > 2)
	//			break;

	//		yield return null;
	//	}

	//	foreach (var ob in on_1)
	//	{
	//		ob.SetActive(true);
	//	}

	//	foreach (var ob in off_1)
	//	{
	//		ob.SetActive(false);
	//	}

	//	vcr.OnOpenVideoFile();
	//	StartCoroutine(scene_movie_0());

	//	yield return null;
	//}



}
