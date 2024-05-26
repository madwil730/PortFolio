using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionTest : MonoBehaviour
{
	delegate void MyDelegate<T1, T2>(T1 a, T2 b);
	MyDelegate<int, int> myDelegate;
	//위와 같은 기능
	Action<int, int> myDelegateA;

	delegate string MyDelegate2<T1, T2>(T1 a, T2 b);
	MyDelegate2<int, int> myDelegate2;
	//위와 같은 기능
	Func<int, int, string> myDelegateF;

	// Start is called before the first frame update
	void Start()
	{
		myDelegateA = (int a, int b) => print(a + b);

		myDelegateA(1, 2);

		myDelegateF = (int a, int b) => { int sum = a + b; return sum + "이 리턴"; };

		print(myDelegateF(3, 5));
	}
}
