using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacktoOpening : MonoBehaviour
{
	float time;
	public GameObject[] offImage;
	public GameObject[] onImage;
	public GameObject[] water;
	public GameObject[] sect;
	public GameObject[] bubble;
	public Slider slider;
	public Image image;
	Color color;


	public static bool OpenigCheck;

	// Start is called before the first frame update
	private void OnEnable()
	{
		color = new Color(0, 0, 0, 0);
		time = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (OpenigCheck)
		{
			time += Time.deltaTime;

			if (time > 7)
			{
				
				slider.value = 0; // 실린더 초기화
				time = 0; // 시간 초기화
				Timer.time = 0; // 타이머 초기화
				Bubble.check = false; // Bubble  체크 초기화
				OpenigCheck = false; // 오프닝 돌아가기 초기화
				TutorialScript.First = false; //튜토리얼 불값 초기화
				TutorialScript.Second = false; //튜토리얼 불값 초기화
				Rice_Check.timer[0] = false; // 시간 다됐을 경우 결말 초기화
				Rice_Check.timer[1] = false; // 시간 다됐을 경우 결말 초기화
				Rice_Check.timer[2] = false; // 시간 다됐을 경우 결말 초기화
				UDPReceiveManager.check = false; // rfid 초기화
				water[0].transform.localPosition = new Vector3(-906.85f, -540.05f, -3); // 앞물 위치 초기화
				sect[0].transform.localPosition = new Vector3(-816, 197, 0); // 활성화 위치 초기화
				sect[1].transform.localPosition = new Vector3(-816, 197, 0); // 비활성화 위치 초기화
				bubble[0].transform.position = new Vector3(-816, 197, 0); // 비활성화 위치 초기화
				bubble[1].transform.position = new Vector3(-816, 197, 0); // 비활성화 위치 초기화
				bubble[2].transform.position = new Vector3(-816, 197, 0); // 비활성화 위치 초기화
		
				StartCoroutine(Singletone.Instance.Fade(color, image, offImage, onImage)); // Fdae InOut
				
			}
		}

	}
}
