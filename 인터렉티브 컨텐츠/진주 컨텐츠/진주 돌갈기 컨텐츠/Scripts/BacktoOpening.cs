using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacktoOpening : MonoBehaviour
{
	float time;
	public GameObject[] offImage;
	public GameObject[] onImage;
	public GameObject[] disturbance;
	public GameObject[] disturbanceAnimation;
	public GameObject[] IconUI;
	public GameObject LoodingUI;
	public Sprite alpha;
	public Slider slider;
	public GameObject rock;
	public GameObject rock2;
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
		if(OpenigCheck)
		{
			time += Time.deltaTime;

			if (time > 9)
			{
				time = 0; // 시간 초기화
				Timer.time = 0; // 타이머 초기화
				OpenigCheck = false; // 오프닝 돌아가기 초기화
				TutorialScript.First = false; //튜토리얼 불값 초기화
				TutorialScript.Second = false; //튜토리얼 불값 초기화
				slider.value = 0; // 실린더 초기화
				UDPReceiveManager.check = false; // rfid 초기화
				agricultural_Rail.count = 0; // 농기구 레일 카운트 초기화
				agricultural_Rail.countCheck = false; // TimeResult 클래스에서 사용하는 값 초기화

				disturbance[0].transform.localPosition = new Vector3(-348, -258.2f, -2);
				disturbance[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
				disturbance[1].transform.localPosition = new Vector3(-348, -258.2f, -2);
				disturbance[1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
				disturbance[2].transform.localPosition = new Vector3(-348, -258.2f, -2);
				disturbance[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
				disturbanceAnimation[0].GetComponent<SpriteRenderer>().sprite = alpha;
				disturbanceAnimation[1].GetComponent<SpriteRenderer>().sprite = alpha;
				disturbanceAnimation[2].GetComponent<SpriteRenderer>().sprite = alpha;
				IconUI[0].SetActive(false);
				IconUI[1].SetActive(false);
				IconUI[2].SetActive(false);
				LoodingUI.SetActive(true);
				StartCoroutine(Singletone.Instance.Fade(color, image, offImage, onImage)); // Fdae InOut
				rock.transform.localPosition = new Vector3(-338, -260, -1);
				rock2.transform.localPosition = new Vector3(-338, -260, -1);

			}
		}
				
    }
}
