using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display2_Rotation : MonoBehaviour
{
	[SerializeField] Transform TargetTransform;

	// Update is called once per frame
	void Update()
	{
		if (TargetTransform.gameObject.activeSelf)
			transform.rotation = TargetTransform.rotation;
	}
}
