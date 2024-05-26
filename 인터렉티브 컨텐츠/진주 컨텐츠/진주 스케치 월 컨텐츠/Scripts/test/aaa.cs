using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Diagnostics;

public class aaa : MonoBehaviour
{

	public RawImage raw;
	public Texture2D mTexture;
	
	Texture2D QRtex;

	DirectoryInfo dirPath = new DirectoryInfo(@"C:\Users\almaloco\Desktop\툴 모음집\예전 개발\glow\Assets\StreamingAssets");


	//[SerializeField] CodeReader reader;
	//[SerializeField] PreviewController preview;


	string fullpath = Application.streamingAssetsPath;

	Texture2D tex = null;
	Texture2D QRtexUP = null;
	Texture2D QRtexDown = null;
	Queue<string> q; // 큐로 파일 체크  
	

	byte[] fileData;


	string dataStr = "";
	string dataStr2 = "";


	private void Start()
	{
		QRtex = new Texture2D(300, 300, TextureFormat.RGBA32, false);
		
		q = new Queue<string>();

		string path = @"C:\Users\almaloco\Desktop\진주 청동기 프로젝트";
		var fol = new ProcessStartInfo(path);
		fol.WindowStyle = ProcessWindowStyle.Maximized;
		Process.Start(fol);
	 

	}

	public void PixelChange()
	{
		// 아래
		//Color[] c = mTexture.GetPixels(100, 0, 200, 250);
		//Texture2D m2Texture = new Texture2D(200, 250);
		//m2Texture.SetPixels(c);
		//m2Texture.Apply();

		//위 
		//Color[] c = mTexture.GetPixels(1450, 1000, 200, 200);
		//Texture2D m2Texture = new Texture2D(200, 200);
		//m2Texture.SetPixels(c);
		//m2Texture.Apply();

		//중앙 정위치
		//Color[] c = mTexture.GetPixels(150, 150, 1450, 800);
		//Texture2D m2Texture = new Texture2D(1450, 800);
		//m2Texture.SetPixels(c);
		//m2Texture.Apply();

		//중앙 역위치
		Color[] c = mTexture.GetPixels(150, 260 , 1450, 820);
		Texture2D m2Texture = new Texture2D(1450, 820);
		m2Texture.SetPixels(c);
		m2Texture.Apply();

		raw.texture = m2Texture;
	}

	public void log()
	{
		//debug
	}

	public void Paints()
	{
		//imageCrop----------------------------------------------

		for (int y = 0; y < mTexture.height; y++)
		{
			for (int x = 0; x < mTexture.width; x++)
			{
				{
					if (x > 150 && x < 1590 && y > 150 && y < 950)
					{
						if (mTexture.GetPixel(x, y).r > 0.8 && mTexture.GetPixel(x, y).g > 0.8 && mTexture.GetPixel(x, y).b > 0.8)
							mTexture.SetPixel(x, y, Color.clear);
					}
					else
						mTexture.SetPixel(x, y, Color.clear);
				}
			}
		}

		mTexture.Apply();

		raw.texture = mTexture;
	}


	// 큐 삽입 
	public void Queue()  // Start this Coroutine on some button click
	{									
		while (true)
		{
			try
			{
				if (dirPath.GetFiles("*.png").Length > 0)
				{
					if (q.Count == 0)
					{
						foreach (System.IO.FileInfo file in dirPath.GetFiles())
						{
							if (file.Extension.ToLower().CompareTo(".png") == 0 )
							{
								q.Enqueue(file.Name);
								//Debug.Log("Enqueue : " + file.Name);
							}
						}
					}
					//Debug.Log(dirPath.GetFiles("*.png").Length);
					break;
				}
			}
			catch (IOException e)
			{
				//Debug.Log(e);
			}
		
		}

		StartCoroutine(Paint());
	}
	public IEnumerator Paint()
	{
		//fileCheck--------------------------------------------

		string str = q.Dequeue();

		while (true)
		{
			try
			{
				if (File.Exists(fullpath + "/" + str))
				{
					fileData = File.ReadAllBytes(fullpath + "/" + str);
					tex = new Texture2D(640, 480, TextureFormat.RGBA32, false);
					tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.

					break;
				}
			}
			catch (IOException e)
			{
				//Debug.Log(e);
			}
			yield return null;
		}


		//QR decode----------------------------------------------------

		// up
		Color[] colorUP = tex.GetPixels(1450, 1000, 200, 200);
		QRtexUP = new Texture2D(200, 200);
		QRtexUP.SetPixels(colorUP);
		QRtexUP.Apply();
		raw.texture = QRtexUP;

		//down;
		//Color[] colorDown = tex.GetPixels(100, 0, 200, 300);
		//QRtexDown = new Texture2D(200, 300);
		//QRtexDown.SetPixels(colorDown);
		//QRtexDown.Apply();
		//raw.texture = QRtexDown;

	}

}
