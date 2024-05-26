using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class ColorOut : MonoBehaviour
{
	public GameObject ob;
	public RawImage raw;
	Texture2D tex;


	public void getColor()
	{
		tex = (Texture2D)raw.texture;

		//Debug.Log(tex.width);

		for (int x = 0; x < tex.width; x ++)
		{
			for(int y = 0; y < tex.height; y++)
			{
				if (tex.GetPixel(x, y).r > 0.5 && tex.GetPixel(x, y).g > 0.5 && tex.GetPixel(x, y).b > 0.5)
					tex.SetPixel(x, y, Color.clear);
			}
		}

		tex.Apply();
		raw.texture = tex;
		
	}

	public void on()
	{
		ob.SetActive(true);
	}


}
