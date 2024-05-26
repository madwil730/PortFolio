using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionText : MonoBehaviour
{
	[SerializeField] Image[] checkImage;
	[SerializeField] GameObject Obj;

	int check;
	// Start is called before the first frame update

	private void OnEnable()
	{
		StartCoroutine(text());
	}
	
	IEnumerator text()
	{
		while (true)
		{
			for (int i = 0; i < checkImage.Length; i++)
			{
				if (checkImage[i].fillAmount == 1)
				{
					//Debug.Log(111);
					check = 0;
				}

				else if(checkImage[i].fillAmount == 0)
				{
					//Debug.Log(2222);
					check++;
				}
			}


			if (check > 3)
				Obj.SetActive(true);

			if (Obj.activeSelf)
			{
				yield return new WaitForSeconds(4);
				Obj.SetActive(false);
			}
				

			yield return null;
		}
	}
}
