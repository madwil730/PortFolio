using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour
{
	delegate void Del();
	delegate void DelString(string s);


	private void Start()
	{
		Del dela = new Del(aaa1);
		Del delb = aaa2;

		DelString delc = bbb1;
		DelString deld= bbb2;

		//delc += bbb1;
		//delc += bbb2;

		dela();
		delc("수박은");
		delc("사과는");

	}


	void aaa1()
	{
		Debug.Log(1);
	}

	void aaa2()
	{
		Debug.Log(2);
	}


	void bbb1(string s)
	{
		Debug.Log(s + "  맞다");
	}

	void bbb2(string s)
	{
		Debug.Log(s + "  아니다");
	}

}
