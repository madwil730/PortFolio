using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Animator ani;

   public void rotate()
	{
		GameObject ob = Singletone.Instance.rotateObject;
		ani.Play("Rotate");

		if(ob != null)
		{
			ob.transform.rotation = ob.transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90));

			for (int i = 0; i < ob.GetComponent<PipeTouch2>().ChildObject.Length; i++)
			{
				ob.GetComponent<PipeTouch2>().ChildObject[i].GetComponent<CollisionType>().type =
				ob.GetComponent<PipeTouch2>().ChildObject[i].GetComponent<CollisionType>().type + 1;

				if ((int)ob.GetComponent<PipeTouch2>().ChildObject[i].GetComponent<CollisionType>().type > 3)
					ob.GetComponent<PipeTouch2>().ChildObject[i].GetComponent<CollisionType>().type = 0;
			}
		}
	}
}
