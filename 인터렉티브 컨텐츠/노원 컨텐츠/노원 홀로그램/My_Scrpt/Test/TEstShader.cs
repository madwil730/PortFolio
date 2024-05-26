using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEstShader : MonoBehaviour
{

	public Material mat;
	public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//ani.colo
		mat.color = Color.clear;
    }
}
