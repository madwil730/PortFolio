using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
	public Slider slider;
	public GameObject[] bubble;
	[Tooltip("프로그래스 바 사라지면 안되게 만듬")]
	//public GameObject progressbar;

	public static bool check; // 특정 위치 이상이면 거품 나오게 하는 거 체크
	bool[] bubbleCheck;
	int num= 0;
	Vector3 vec;

	AudioSource audio;
	public AudioClip[] clip;


	// Start is called before the first frame update
	void Start()
    {
		audio = this.gameObject.GetComponent<AudioSource>();

		bubbleCheck = new bool[3];
		vec = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {

		if (slider.value > 6.5f)
			check = true;

		// 펌프질 할때마다 버블 생성
		if (Input.GetKeyDown(KeyCode.Alpha1) && check)
		{
			if(!Rice_Check.Result) //check
			{
				audio.clip = clip[Random.Range(0, 2)];
				audio.Play();

				bubble[num].transform.localPosition = new Vector3(Random.Range(-7, 16), 2.4f, -8);
				
				int a = Random.Range(0, 2);

				if (a == 0)
					bubble[num].GetComponent<Animator>().Play("Play_1~2_SteamBubble", -1, 0);
				else if (a == 1)
					bubble[num].GetComponent<Animator>().Play("Play_1~2_SteamBubble_Reverse", -1, 0);

				bubbleCheck[num] = true;

				num += 1;

				if (num == 3)
				{
					num = 0;
				}
			}
		}
	}
}
