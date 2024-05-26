using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	[SerializeField] HologramZoomCameraController _HologramZoomCameraController;

	[SerializeField] Transform solar;
	[SerializeField] Transform ourGalaxy;
	[SerializeField] Transform universe;
	[SerializeField] Transform BigUniverse;
	[SerializeField] Transform BigSpace;

	[Header("--------------------------")]
	//[SerializeField] Transform nomeshController;


	Transform TargetTransform;

	public touch touchSystem;

	private void OnEnable()
	{
		if (touchSystem.pos == touch.Pos.moreLeft)
			TargetTransform = solar;

		else if (touchSystem.pos == touch.Pos.left)
		{
			TargetTransform = ourGalaxy;
			//Debug.Log(0);
		}
			

		else if (touchSystem.pos == touch.Pos.center)
		{
			TargetTransform = universe;
			//Debug.Log(1);
		}
			

		else if (touchSystem.pos == touch.Pos.right)
			TargetTransform = BigUniverse;

		else if (touchSystem.pos == touch.Pos.moreRight)
			TargetTransform = BigSpace;

		// 카메라 초기화
		//if (touchSystem.pos == touch.Pos.right)
		//	_HologramZoomCameraController.CameraInit_right();
		//else
			_HologramZoomCameraController.CameraInit();

	}

	//void Update()
	//{
	//	float scroll = Input.GetAxis("Mouse ScrollWheel");


	//	if (scroll > 0)
	//	{


	//		// 확대
	//		TargetTransform.localPosition = new Vector3(0, -0.54f, TargetTransform.localPosition.z - scroll);
	//		if (TargetTransform.localPosition.z <= 6)
	//		{
	//			TargetTransform.localPosition = new Vector3(0, -0.54f, 6);
	//		}


	//		_HologramZoomCameraController.SetZoom(TargetTransform.localPosition.z);
	//	}

	//	else if (scroll < 0)
	//	{

	//		// 축소
	//		TargetTransform.localPosition = new Vector3(0, -0.54f, TargetTransform.localPosition.z - scroll);
	//		if (TargetTransform.localPosition.z >= 12)
	//		{
	//			TargetTransform.localPosition = new Vector3(0, -0.54f, 12);
	//		}


	//		_HologramZoomCameraController.SetZoom(TargetTransform.localPosition.z);
	//	}

	//}

	public void OnDrag(PointerEventData eventData)
	{
		if (Input.touchCount >= 2)
		{

			Vector2 cur = Input.GetTouch(0).position - Input.GetTouch(1).position;
			Vector2 Prev = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition)
					- (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition));//여기까지는 지금 포스와 전 포스의 거리 차를 구하는 걸로 이해된다.
			float TouchData = (cur.magnitude - Prev.magnitude) / 100; //magnityude는 제곱근을 계산해주는 걸로 알고있다.

			//Debug.Log(TouchData);

			//확대
			if (TouchData > 0)
			{

					// 확대
					TargetTransform.localPosition = new Vector3(0, -0.54f, TargetTransform.localPosition.z - 0.3f);
					if (TargetTransform.localPosition.z <= 6)
					{
						TargetTransform.localPosition = new Vector3(0, -0.54f, 6);
					}
				

			}
			//축소
			else if (TouchData < 0)
			{
				TargetTransform.localPosition = new Vector3(0, -0.54f, TargetTransform.localPosition.z + 0.3f);
				if (TargetTransform.localPosition.z >= 12)
				{
					TargetTransform.localPosition = new Vector3(0, -0.54f, 12);
				}
			}

			_HologramZoomCameraController.SetZoom(TargetTransform.localPosition.z);

		}
		else if (Input.touchCount == 1 || Input.GetMouseButton(0))
		{
			//Debug.Log(eventData.delta);
			Vector2 pos = eventData.delta;

			//Debug.Log(pos);
			pos.Normalize();

			if(eventData.delta.x > 50 || eventData.delta.y > 50 ||
				eventData.delta.x < -50 || eventData.delta.y <-50)
			vec = pos;

			TargetTransform.Rotate(pos.y*2, -pos.x*2, 0, Space.World);

		}
	}

	Vector2 vec;
	float time;

    public void OnPointerDown(PointerEventData eventData)
	{
        
    }

	public void OnPointerUp(PointerEventData eventData)
	{
		if (Input.touchCount == 1 || Input.GetMouseButton(0))
		{
			
			StartCoroutine(pointUp());

		}
	}


	IEnumerator pointUp()
	{
		while(true)
		{
			time += Time.deltaTime;

			if (time > 0.3f)
				break;

			TargetTransform.Rotate(vec.y * 2, -vec.x * 2, 0, Space.World);
			yield return null;
		}

		time = 0;
		vec = Vector3.zero;
	}

}
