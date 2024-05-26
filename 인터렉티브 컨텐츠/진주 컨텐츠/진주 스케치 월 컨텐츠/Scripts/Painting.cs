using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Jacovone;
public class Painting : MonoBehaviour
{
	[SerializeField] GameObject[] obj;
	[SerializeField] GameObject[] magicPath;
	[SerializeField] GameObject[] magicPath_Star;
	[SerializeField] CodeReader reader;
	[SerializeField] PreviewController preview;
	[SerializeField] ReadXML xml;

	//[SerializeField]Texture2D texture;

	string fullpath = Application.streamingAssetsPath;

	Texture2D tex = null;
	Texture2D QRtexUP = null;
	Texture2D QRtexDown = null;
	Texture2D texShadow = null;
	Queue<string> q; // 큐로 파일 체크  
	DirectoryInfo dirPath; //= new DirectoryInfo(@"C:\Users\almaloco\Desktop\툴 모음집\예전 개발\glow\Assets\StreamingAssets");
	
	byte[] fileData;

	int random = 10; // 10은 임의로 정한 초기화 값
	int Length = 0;
	string dataStr = "";
	string dataStr2 = "";

	bool zeroCheck;

	void Start()
	{
		Screen.SetResolution(1920, 1200, true);
		dirPath = new DirectoryInfo(ReadXML.str);
		q = new Queue<string>();

		StartCoroutine(delete());
		//StartCoroutine(Queue());
		
	}

	#region [delete]

	Queue<string> deleteQ;

	public IEnumerator delete()
	{
		deleteQ = new Queue<string>();

		try
		{
			if (dirPath.GetFiles("*.png").Length > 0)
			{
				if (deleteQ.Count == 0)
				{
					foreach (System.IO.FileInfo file in dirPath.GetFiles())
					{
						if (file.Extension.ToLower().CompareTo(".png") == 0)
						{
							deleteQ.Enqueue(file.Name);
						}
					}
				}

			}
		}
		catch (IOException e)
		{
			Debug.Log(e);
		}
		yield return null;

		if (deleteQ.Count > 0)
		{
			foreach (System.IO.FileInfo file in dirPath.GetFiles())
			{


				if (file.Extension.ToLower().CompareTo(".png") == 0)
				{
					string stri = deleteQ.Dequeue();
					File.Delete(fullpath + "/" + stri);
					File.Delete(fullpath + "/" + stri + ".meta");
				}
			}
		}

		StartCoroutine(Queue());
	}

	#endregion


	// 큐 삽입 
	public IEnumerator Queue()  // Start this Coroutine on some button click
	{
		while (true)
		{
			if (!obj[0].activeSelf)
				break;
			else if ((!obj[1].activeSelf))
				break;
			else if ((!obj[2].activeSelf))
				break;
			else if ((!obj[3].activeSelf))
				break;
			else if ((!obj[4].activeSelf))
				break;
			else if ((!obj[5].activeSelf))
				break;

			//else
			//Debug.Log("keep going");

			yield return null;
		}

		if (Length == 0)
		{
			for (int i = 0; i < obj.Length; i++) // obj.Length -> 5
			{
				if (!obj[i].activeSelf)
				{
					Singletone.Instance.list.Add(i);
					Length++;
					//Debug.Log("Length : " +Length);
				}
			}
		}

		//fileCheck--------------------------------------------
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
							if (file.Extension.ToLower().CompareTo(".png") == 0 && q.Count< Length )  
							{
								q.Enqueue(file.Name);
								//Debug.Log(Length);
							}
						}
					}
					//Debug.Log(dirPath.GetFiles("*.png").Length);
					break;
				}
			}
			catch (IOException e)
			{
				Debug.Log(e);
			}
			yield return null;
		}

		StartCoroutine(Paint());
	}


	public IEnumerator Paint()
	{
		//fileCheck--------------------------------------------

		string str = q.Dequeue();

		Debug.Log(str);
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
				Debug.Log(e);
			}
			yield return null;
		}


		//list random start--------------------------------------------
		random = Random.Range(0, Length);
		
		int num = Singletone.Instance.list[random];

		//QR decode----------------------------------------------------

		// up
		Color[] colorUP = tex.GetPixels(xml.ColorUP_x1, xml.ColorUP_y1, xml.ColorUP_x2, xml.ColorUP_y2);
		QRtexUP = new Texture2D(xml.ColorUP_x2, xml.ColorUP_y2);
		QRtexUP.SetPixels(colorUP);
		QRtexUP.Apply();
		dataStr = reader.ReadCode(QRtexUP);


		yield return new WaitForSeconds(0.1f);  // 이거 원래는 2초임

		// down
		Color[] colorDown = tex.GetPixels(xml.ColorDown_x1, xml.ColorDown_y1, xml.ColorDown_x2, xml.ColorDown_y2);
		QRtexDown = new Texture2D(xml.ColorDown_x2, xml.ColorDown_y2);
		QRtexDown.SetPixels(colorDown);
		QRtexDown.Apply();
		dataStr2 = reader.ReadCode(QRtexDown);

		int a, b;
		int.TryParse(dataStr, out a); // up 이면 true
		int.TryParse(dataStr2, out b); // down 이면 false
		Debug.Log(str);
		if (a > 0)
		{
			Singletone.Instance.QR_WareHouse[num] = a;

		}
		else if (b >0)
		{
			Singletone.Instance.QR_WareHouse[num] = b;
		}
		else
			zeroCheck = true;

		Debug.Log(zeroCheck);

		Debug.Log("active Object :" + num);

		//imageCrop----------------------------------------------

		
		if (!zeroCheck)
		{
			if (a > 0)
			{
				for (int y = xml.a_y1; y < xml.a_y2; y++)
				{
					for (int x = xml.a_x1; x < xml.a_x2; x++)
					{

						if (tex.GetPixel(x, y).r > 0.8 && tex.GetPixel(x, y).g > 0.8 && tex.GetPixel(x, y).b > 0.8)
							tex.SetPixel(x, y, Color.clear);
					}

					yield return null;
				}

				Color[] aaa = tex.GetPixels(xml.a_x1, xml.a_y1, xml.a_x2-xml.a_x1, xml.a_y2-xml.a_y1);
				texShadow = new Texture2D(xml.a_x2 - xml.a_x1, xml.a_y2 - xml.a_y1);
				texShadow.SetPixels(aaa);
				texShadow.Apply();
			}

			else if (b > 0)
			{
				for (int y = xml.b_y1; y < xml.b_y2; y++)
				{
					for (int x = xml.b_x1; x < xml.b_x2; x++)
					{
						if (tex.GetPixel(x, y).r > 0.8 && tex.GetPixel(x, y).g > 0.8 && tex.GetPixel(x, y).b > 0.8)
							tex.SetPixel(x, y, Color.clear);

					}

					yield return null;
				}

				Color[] bbb = tex.GetPixels(xml.b_x1, xml.b_y1, xml.b_x2 - xml.b_x1, xml.b_y2 - xml.b_y1);
				texShadow = new Texture2D(xml.b_x2 - xml.b_x1, xml.b_y2 - xml.b_y1);
				texShadow.SetPixels(bbb);
				texShadow.Apply();
			}


			//for (int y = 0; y < tex.height; y++)
			//{
			//	for (int x = 0; x < tex.width; x++)
			//	{
			//		if (a > 0)
			//		{
			//			if (x > 200 && x < 1490 && y > 260 && y < 890)
			//			{
			//				if (tex.GetPixel(x, y).r > 0.8 && tex.GetPixel(x, y).g > 0.8 && tex.GetPixel(x, y).b > 0.8)
			//					tex.SetPixel(x, y, Color.clear);
			//			}
			//			else
			//				tex.SetPixel(x, y, Color.clear);
			//		}

			//		else if (b > 0)
			//		{
			//			if (x > 200 && x < 1490 && y > 390 && y < 990)
			//			{
			//				if (tex.GetPixel(x, y).r > 0.8 && tex.GetPixel(x, y).g > 0.8 && tex.GetPixel(x, y).b > 0.8)
			//					tex.SetPixel(x, y, Color.clear);
			//			}
			//			else
			//				tex.SetPixel(x, y, Color.clear);
			//		}
			//	}
			//	yield return null;
			//}

			tex.Apply();

			//update Texture -------------------------------------------

			Length--;

			//qr 못 읽을 경우 예외 처리

			{
				obj[num].GetComponent<RawImage>().texture = texShadow;

				if (b > 0)
				{

					magicPath[num].GetComponent<PathMagic>().waypoints[0].rotation = new Vector3(0, 0, 180);
					magicPath[num].GetComponent<PathMagic>().waypoints[1].rotation = new Vector3(0, 0, 180);
				}


				obj[num].SetActive(true);
				magicPath[num].SetActive(true);
				magicPath_Star[num].SetActive(true);

			}
			Singletone.Instance.list.RemoveAt(random);
		}
		else
			Debug.Log("Exception");

		
		File.Delete(fullpath + "/" + str);
		File.Delete(fullpath + "/" + str + ".meta");
		a = 0;
		b = 0;
		
		yield return null;

		if (q.Count > 0)
		{
			StartCoroutine(Paint());
			zeroCheck = false;
		}
			
		else if (q.Count == 0)
		{
			StartCoroutine(Queue());
			zeroCheck = false;
		}

	
	}
}
