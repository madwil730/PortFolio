using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		this.transform.localPosition = new Vector3(ReadXML.alpha_PostionX, ReadXML.alpha_PostionY,-10);
		this.transform.localScale = new Vector3(ReadXML.alpha_ScaleX, ReadXML.alpha_ScaleY, 1);
    }

 
}
