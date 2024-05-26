using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result : MonoBehaviour
{
	public GameObject on;
	public GameObject[] off;
	public GameObject black;

	private void OnEnable()
	{
		black.SetActive(false);
	}

	public void goScene()
	{
		black.SetActive(true);

		StartCoroutine(aaa());
		
		
		


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

		black.SetActive(true);

		StartCoroutine(aaa());
	}


	IEnumerator aaa()
	{
		yield return new WaitForSeconds(0.5f);

		on.SetActive(true);

		for (int i = 0; i < off.Length; i++)
		{
			off[i].SetActive(false);
		}
	}
}
