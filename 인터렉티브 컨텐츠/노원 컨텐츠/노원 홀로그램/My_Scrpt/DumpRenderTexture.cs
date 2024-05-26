using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DumpRenderTexture : MonoBehaviour
{
	public RenderTexture rts;
	string path = Application.streamingAssetsPath + "/solar.png";

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		
		{
			RenderTextures(rts, path);
			//File.Create(path + "txswwt.png");

			Debug.Log("create");
		}

	}


	public void RenderTextures(RenderTexture rt, string pngOutPath)
	{
		var oldRT = RenderTexture.active;

		var tex = new Texture2D(rt.width, rt.height);
		RenderTexture.active = rt;
		tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		tex.Apply();

		File.WriteAllBytes(pngOutPath, tex.EncodeToPNG());
		RenderTexture.active = oldRT;
	
	}
}
