using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class agricultural_Rail : MonoBehaviour
{
	public Slider slider;

	[Header("변화되는 돌 이미지")]
	public GameObject[] rocks;
	public Animator animator;
	public Animator animator2;
	public Animator RockDust;
	public static int count = 0;
	public static bool countCheck; // 30 회 되면 TimeResult 클래스에 리턴해줌

	public AudioClip[] clip;
	AudioSource audio;

	bool up;
	bool check1, check2, check3;


	private void OnEnable()
	{
		check1 = false;
		check2 = false;
		check3 = false;
	}

	void Start()
    {
		audio = this.gameObject.GetComponent<AudioSource>();
    }

	void Update()
    {
		//Debug.Log(count);
		// 아두이노 버전
		if (ReadSerial.SerialRail.IsOpen)
		{
			if (ReadSerial.RailCount > 0)
			{


				if (ReadSerial.RailCount > ReadSerial.TopCheck && !up)
				{
					up = true;
					//rocks[0].transform.localPosition = new Vector3(-338, -258, -1);
				}

				if (ReadSerial.RailCount < ReadSerial.BottomCheck && up)
				{
					slider.value += 1;
					RockDust.Play("Play_RockDust");

					count += 1;
					up = false;
					//rocks[0].transform.localPosition = new Vector3(-338, -262, -1);


				}
				rocks[0].transform.localPosition = new Vector3(-338, (ReadSerial.RailCount / ReadSerial.Multiple) + ReadSerial.RockY, -1);
				rocks[1].transform.localPosition = Vector3.MoveTowards(rocks[1].transform.localPosition, rocks[0].transform.localPosition, ReadSerial.Multiple2);


			}
		}

		// 키보드 버전
		//if (Input.GetKeyDown(KeyCode.Alpha9) && !up)
		//{
		//	up = true;
		//	//for(int i =0; i < 4; i ++)
		//	rocks[0].transform.localPosition += new Vector3(0, 220, 0) * Time.deltaTime;
		//}

		//if (Input.GetKeyDown(KeyCode.Alpha1) && up)
		//{

		//	//for (int i = 0; i < 4; i++)
		//	rocks[0].transform.localPosition -= new Vector3(0, 220, 0) * Time.deltaTime;

		//	slider.value += 1;
		//	RockDust.Play("Play_RockDust");

		//	count += 1;
		//	up = false;
		//}


		// 해당 카운터가 되면 돌 모습이 바뀜
		if (count == 10)
		{
			if (!check1)
			{
				audio.clip = clip[0];
				audio.Play();
			}

			check1 = true;

			animator.SetBool("goto10", true);
			animator2.SetBool("goto10", true);
		}

		else if (count == 20)
		{
			if (!check2)
			{
				audio.clip = clip[1];
				audio.Play();
			}

			check2 = true;


			animator.SetBool("goto20", true);
			animator2.SetBool("goto20", true);

		}

		else if (count == 30)
		{
			if (!check3)
			{
				audio.clip = clip[2];
				audio.Play();
			}

			check3 = true;

	

			animator.SetBool("goto30", true);
			animator2.SetBool("goto30", true);

		}


		if(slider.value >=30)
			countCheck = true;

	}
}





