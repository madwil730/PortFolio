using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject,1);
		//Invoke("DestroyOb", 1);
    }


	public void DestroyOb()
	{

		
		ObjectPool.ReturnObject(gameObject);

		
	}
}
