using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramZoomCameraController : MonoBehaviour
{
	[Header("Cameras")]
	[SerializeField] Transform CenterCamera;
	[SerializeField] Transform LeftCamera;
	[SerializeField] Transform RightCamera;

	public void CameraInit()
	{
		CenterCamera.localPosition = new Vector3(0, 0, -10);
		LeftCamera.localPosition = new Vector3(-10, 0, 0);
		RightCamera.localPosition = new Vector3(10, 0, 0);
	}

	public void CameraInit_right()
	{
		CenterCamera.localPosition = new Vector3(0, 0, -12);
		LeftCamera.localPosition = new Vector3(-12, 0, 0);
		RightCamera.localPosition = new Vector3(12, 0, 0);
	}

	public void SetZoom(float targetPositionZ)
	{
		float zoom = targetPositionZ -2 *1.2f;
		//Debug.Log(zoom);
		CenterCamera.localPosition = new Vector3(0, 0, -zoom);
		LeftCamera.localPosition = new Vector3(-zoom, 0, 0);
		RightCamera.localPosition = new Vector3(zoom, 0, 0);
	}

	public void SetZoom_Center(float targetPositionZ)
	{
		float zoom = targetPositionZ - 1.5f;
		//Debug.Log(zoom);
		CenterCamera.localPosition = new Vector3(0, 0, -zoom);
		LeftCamera.localPosition = new Vector3(-zoom, 0, 0);
		RightCamera.localPosition = new Vector3(zoom, 0, 0);
	}

	public void SetZoom_Right(float targetPositionZ)
	{
		float zoom = targetPositionZ+1;
		//Debug.Log(zoom);
		CenterCamera.localPosition = new Vector3(0, 0, -zoom);
		LeftCamera.localPosition = new Vector3(-zoom, 0, 0);
		RightCamera.localPosition = new Vector3(zoom, 0, 0);
	}
}
