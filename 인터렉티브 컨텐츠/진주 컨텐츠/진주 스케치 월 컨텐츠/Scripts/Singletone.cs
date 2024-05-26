using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jacovone;

public class Singletone : MonoBehaviour
{
	private static Singletone instance = null;
	// Start is called before the first frame update

	public enum Position
	{
		up,down,defaults
	}

	//Position[] pos;


	public List<int> list;
	public int[] QR_WareHouse = new int[6];
	

	private void Awake()
	{
		if (null == instance)
		{
			instance = this;
		}

		list = new List<int>();

	


	}

	public static Singletone Instance
	{
		get
		{
			return instance;
		}

	}

	public IEnumerator constellation2(GameObject star , GameObject me, GameObject pathMagic)
	{


			yield return null;


		me.SetActive(false);
		me.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
		me.GetComponent<RawImage>().color = Color.white;
	}

	public Texture2D TextureToTexture2D(Texture texture)
	{
		Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
		Graphics.Blit(texture, renderTexture);

		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		texture2D.Apply();

		RenderTexture.active = currentRT;
		RenderTexture.ReleaseTemporary(renderTexture);
		return texture2D;
	}
}










