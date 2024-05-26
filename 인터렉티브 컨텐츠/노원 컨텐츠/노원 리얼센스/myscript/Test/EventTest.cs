using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
	#region Singleton
	private static EventTest instance;
	public static EventTest Instance { get { return instance; } }
	#endregion

	void Awake()
	{
		instance = this;
	}

	public delegate void PrintDelegate(string s);
	public event PrintDelegate PrintAllEvent;

	public delegate void Test(string s);
	public Test ttt;


	private void Start()
	{
		ttt = text;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) //스페이스바를 누르면 이벤트 발생
		{
			if (PrintAllEvent != null)
				PrintAllEvent("print out!"); //이벤트 발생

			ttt(" 나는 델리게이트");

			//혹은 이렇게 써도 동일함
			//PrintAllEvent?.Invoke("print out!");
		}

		
	}

	void text(string s)
	{
		Debug.Log(s);
	}

	
}
