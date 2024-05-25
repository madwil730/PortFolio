using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChoice : MonoBehaviour
{
	public GameObject on;
	public GameObject[] off;

	public void goScene()
	{
	
		{
			on.SetActive(true);

			for (int i = 0; i < off.Length; i++)
			{
				off[i].SetActive(false);
			}
		}

		
	}

	public void goPipe()
	{
	
		while (Singletone.Instance.list.Count > 0)
		{
				GameObject list = Singletone.Instance.list[Singletone.Instance.list.Count - 1];

				Destroy(list.GetComponent<PipeTouch2>().ob);
				Destroy(list);

				Singletone.Instance.list.RemoveAt(Singletone.Instance.list.Count - 1);
		}
		

		if (Singletone.Instance.listPipe.Count > 0)
		{
			Singletone.Instance.listPipe.Clear();
		}

		on.SetActive(true);

		for (int i = 0; i < off.Length; i++)
		{
			off[i].SetActive(false);
		}
	}
}
