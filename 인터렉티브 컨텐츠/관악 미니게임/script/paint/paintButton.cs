using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XDPaint;

public class paintButton : MonoBehaviour
{
    public Sprite sprite;
    public SpriteRenderer render;
	public PaintManager manger;

	public string str;

	public bool frog;
	public bool bird;
	public bool monkey;

	public Animator ani;
	// Start is called before the first frame update

	public void change()
	{
		ani.Play(str);
		render.sprite = sprite;

		if(frog)
		{
		render.gameObject.transform.localScale = new Vector3(0.6f, 0.6f,0);
		render.gameObject.transform.localPosition = new Vector3(0.1f, 0.68f,0);
		}

		else if (bird)
		{
			render.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0);
			render.gameObject.transform.localPosition = new Vector3(0.1f, 0.68f, 0);
		}

		else if (monkey)
		{
			render.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0);
			render.gameObject.transform.localPosition = new Vector3(0.53f, 0.68f, 0);
		}
		manger.Init();
	
	}

	public void change2()
	{
		ani.Play(str);
	}
	
}
