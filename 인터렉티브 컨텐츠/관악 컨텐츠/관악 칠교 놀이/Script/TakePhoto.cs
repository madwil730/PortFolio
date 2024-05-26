using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProLiveCamera;
using System.IO;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo.Demos;

public class TakePhoto : MonoBehaviour
{
	public AVProLiveCamera _liveCamera;
	public AVProLiveCameraManager _liveCameraManager;
	public RawImage raw;
	public ControllAnimal controllAnimal;
	public CodeReader reader;

	public GameObject[] onOb;
	public GameObject[] offOb;
	public GameObject Fade;
	public Text debugText;

	[Header("-----------------------------")]
	public GameObject Display;
	public VCR vcr;
	bool idleCheck;

	Texture2D tex;


	private void Start()
	{
		Debug.Log(33);
		StartCoroutine(captureCountDown());
	
	}


	//IEnumerator Check()
	//{
	//	//Debug.Log(3);
	//	yield return new WaitForSeconds(1f);
	//	while (true)
	//	{
	//		tex = GetRTPixels(_liveCamera.Device.OutputRenderTexture);

	//		if (int.TryParse(reader.ReadCode(tex), out int intCheck))
	//			break;

	//		yield return new WaitForSeconds(2); // 부하 줄이기
	//	}

	//	foreach (var ob in onOb)
	//		ob.SetActive(true);
	//	foreach (var ob in offOb)
	//		ob.SetActive(false);


	//	yield return new WaitForSeconds(1f);
	//	StartCoroutine(captureCountDown());
	//}


	IEnumerator captureCountDown()
	{
		yield return new WaitForSeconds(0.1f);

		Debug.Log("inPhoto");
		while(true)
		{
			Debug.Log(reader.ReadCode(tex));
			tex = GetRTPixels(_liveCamera.Device.OutputRenderTexture);
			if (int.TryParse(reader.ReadCode(tex), out int b))
			{
				//debugText.text = "is out  " + reader.ReadCode(tex);
				break;
			}

			//if (reader.ReadCode(tex) == null || reader.ReadCode(tex) == "")
			//	debugText.text = "is null";

			//else
			//	debugText.text = reader.ReadCode(tex) + "  in while";


			yield return new WaitForSeconds(1f); // 부하 줄이기
		}

		foreach (var ob in offOb)
			ob.SetActive(false);

		if (reader.ReadCode(tex) == "1")
		{
			ControllAnimal.choice = 0;
			Fade.SetActive(true);
			vcr._VideoIndex = 0;
			vcr.OnOpenVideoFile();
			yield return new WaitForSeconds(0.5f);
			Display.gameObject.transform.localPosition = Vector3.zero;
		}
		else if (reader.ReadCode(tex) == "2")
		{
			ControllAnimal.choice = 1;
			Fade.SetActive(true);
			vcr._VideoIndex = 1;
			vcr.OnOpenVideoFile();
			yield return new WaitForSeconds(0.5f);
			Display.gameObject.transform.localPosition = Vector3.zero;
		}
		else if (reader.ReadCode(tex) == "3")
		{
			ControllAnimal.choice = 2;
			Fade.SetActive(true);
			vcr._VideoIndex = 2;
			vcr.OnOpenVideoFile();
			yield return new WaitForSeconds(0.5f);
			Display.gameObject.transform.localPosition = Vector3.zero;
		}
		else if (reader.ReadCode(tex) == "4")
		{
			Debug.Log(333);
			ControllAnimal.choice = 3;
			Fade.SetActive(true);
			vcr._VideoIndex = 3;
			vcr.OnOpenVideoFile();
			yield return new WaitForSeconds(0.5f);
			Display.gameObject.transform.localPosition = Vector3.zero;
		}
		else if (reader.ReadCode(tex) == "5")
		{
			ControllAnimal.choice = 4;
			Fade.SetActive(true);
			vcr._VideoIndex = 4;
			vcr.OnOpenVideoFile();
			yield return new WaitForSeconds(0.5f);
			Display.gameObject.transform.localPosition = Vector3.zero;
		}
		
		yield return new WaitForSeconds(13);

		Fade.SetActive(true);
		vcr.OnPauseButton();
		yield return new WaitForSeconds(0.5f);
		Display.gameObject.transform.localPosition = new Vector3(5000, 0, 0);
		

		yield return new WaitForSeconds(CameraInit.breakTime);

		//제대로 세팅 되었다고 치면
		tex = GetRTPixels(_liveCamera.Device.OutputRenderTexture);

		for (int x = CameraInit.x1; x < CameraInit.x2; x++)
		{
			for (int y = CameraInit.y1; y < CameraInit.y2; y++)
			{
				if (tex.GetPixel(x, y).r > CameraInit.RGB_r && tex.GetPixel(x, y).g > CameraInit.RGB_g && tex.GetPixel(x, y).b > CameraInit.RGB_b)
					tex.SetPixel(x, y, Color.clear);
			}
		}

		//tex.Apply();

		Color[] colors = tex.GetPixels(CameraInit.x1, CameraInit.y1, CameraInit.x2-CameraInit.x1, CameraInit.y2 -CameraInit.y1);
		Texture2D texShadow = new Texture2D(CameraInit.x2 - CameraInit.x1, CameraInit.y2 - CameraInit.y1);
		texShadow.SetPixels(colors);
		texShadow.Apply();


		raw.texture = texShadow;
		raw.gameObject.SetActive(true);

		yield return new WaitForSeconds(4f);
		
		controllAnimal.set();

		while (true)
		{
			tex = GetRTPixels(_liveCamera.Device.OutputRenderTexture);
			if (reader.ReadCode(tex) == "" )
			break;

			yield return new WaitForSeconds(2f);
		
		}

		Debug.Log("break PhotoCheck");
		StartCoroutine(captureCountDown());
	}


	public void photo()
	{
		if (!idleCheck)
		{
			foreach (var ob in onOb)
				ob.SetActive(true);
			foreach (var ob in offOb)
				ob.SetActive(false);
			idleCheck = true;
		}
		StartCoroutine(captureCountDown());
	}



	Texture2D LoadPNG(string filePath)
	{

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}


	Texture2D GetRTPixels(RenderTexture rt)
	{
		// Remember currently active render texture
		RenderTexture currentActiveRT = RenderTexture.active;

		// Set the supplied RenderTexture as the active one
		RenderTexture.active = rt;

		// Create a new Texture2D and read the RenderTexture image into it
		Texture2D tex2D = new Texture2D(rt.width, rt.height);
		tex2D.ReadPixels(new Rect(0, 0, tex2D.width, tex2D.height), 0, 0);


		// Restorie previously active render texture
		RenderTexture.active = currentActiveRT;
		return tex2D;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
			debugText.gameObject.SetActive(!debugText.gameObject.activeSelf);
	}
}




//byte[] Picturebytes = GetRTPixels(_liveCamera.Device.OutputRenderTexture).EncodeToPNG();
//File.WriteAllBytes(string.Concat(Application.streamingAssetsPath, "/photo.png"), Picturebytes);

//tex = LoadPNG(string.Concat(Application.streamingAssetsPath, "/photo.png"));