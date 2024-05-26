using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singtone : MonoBehaviour
{
	private static Singtone instance = null;
	public bool mouseCheck;
	public int count ;
	// Start is called before the first frame update

	private void Awake()
	{
		Input.multiTouchEnabled = false;
		count = 0;
		if (null == instance)
		{
			instance = this;
		}
	}

	public static Singtone Instance
	{
		get
		{
			return instance;
		}
	}
}
