using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XDPaint;

public class paintInit : MonoBehaviour
{

    public PaintManager manager;


	private void OnEnable()
	{
		manager.Init();
	}
}
