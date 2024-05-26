using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListExample : MonoBehaviour
{
	List<int> list;
	int Length = 8;
	int random = 10; // 10은 임의로 정한 초기화 값
	// Start is called before the first frame update
	void Start()
    {
		list = new List<int>();

		list.Add(0);
		list.Add(1);
		list.Add(2);
		list.Add(3);
		list.Add(4);
		list.Add(5);
		list.Add(6);
		list.Add(7);
    }

    

	public int RandomDispose()
	{
		if(Length>=0)
		{
			random = Random.Range(0, Length);
			Debug.Log("random : " + list[random]);

			list.RemoveAt(random);
			Length--;

			Debug.Log("Length : "+list.Count);

			
		}

		return random;
	}
}
