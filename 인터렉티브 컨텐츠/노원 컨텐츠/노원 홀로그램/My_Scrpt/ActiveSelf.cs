using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelf : MonoBehaviour
{
    
	public void offSelf()
	{
		this.gameObject.SetActive(false);
	}
}
