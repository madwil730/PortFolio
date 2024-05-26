using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest2 : MonoBehaviour
{
	void Start()
	{
		EventTest.Instance.PrintAllEvent += PrintMyName;
		EventTest.Instance.ttt = text;

	
	}

	void PrintMyName(string s)
	{
		Debug.Log(name + " is " + s);
	}

	void text(string s)
	{
		Debug.Log("외부에서 호출  " + s);
	}

	void OnDestroy()
	{
		EventTest.Instance.PrintAllEvent -= PrintMyName;
	}



}
