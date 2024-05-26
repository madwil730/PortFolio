
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rice_Check : MonoBehaviour
{
	public Slider slider;

	[Tooltip("소리나는 백그라운드 && 타이머 끌것")]
	public GameObject[] offimage;

	[Tooltip("동영상 파일 재생 오브젝트, 패널 지정")]
	public GameObject resultVCR;
	public GameObject resultVideoDisPlay;

	[Tooltip("활성화 구간 on ")]
	public GameObject[] sect;
	[Tooltip("화력 활성화 아이콘")]
	public GameObject[] fireIcon;

	AudioSource audio;
	public AudioClip clip;


	public static bool Stage1; // 애니메이션 재생시키기 위한 체크 1
	public static bool Stage2; // 애니메이션 재생시키기 위한 체크 2
	public static bool Result; // 애니메이션 재생시키기 위한 체크 3

	public static bool[] timer = { false,false,false}; // 타이머 체크를 위한 변수

	public static bool activeTime; // 싱글턴 전용 시간

	// Start is called before the first frame update
	private void OnEnable()
	{
		StartCoroutine(upCheck());
		audio = this.gameObject.GetComponent<AudioSource>();
	}


	IEnumerator upCheck()
	{
		while (true) // 바의 활성화 영역 안에 들어갔는지 체크
		{
			if (slider.value > 6.5f)
			{
				Stage1 = true;
				StartCoroutine(Singletone.Instance.timeIllumination2(sect[0]));
				break;
			}

			yield return null;
		}

		while (true)
		{

			// 적절
			if (slider.value > 6.5f && slider.value < 8.03f)
			{
				fireIcon[0].SetActive(true);


				// 5초 이상이면 클리어
				if (activeTime)
				{
					fireIcon[0].SetActive(false);
					audio.PlayOneShot(clip);

					Debug.Log("middle");
					//PlayParts(-343);
					StartCoroutine(PlayPartanimation(-146));

					StartCoroutine(downCheck(4.11f, 2.56f, false));

					break;
				}
			}

			//위
			else if (slider.value > 8.03f)
			{
				fireIcon[0].SetActive(false);

				// 5초 이상이면 클리어
				if (activeTime)
				{
					Debug.Log("up");
					//PlayParts(-474);
					StartCoroutine(PlayPartanimation(-277));

					StartCoroutine(downCheck(2.57f, 1.1f, false));

					break;
				}
			}
			//아래
			else if (slider.value < 6.5f)
			{
				fireIcon[0].SetActive(false);
				// 5초 이상이면 클리어
				if (activeTime)
				{
					Debug.Log("down");
					//PlayParts(127);
					StartCoroutine(PlayPartanimation(324));

					StartCoroutine(downCheck(9.51f, 7.9f, true));

					break;
				}
			}
			yield return null;
		}

		yield return null;
	}



	//a는 큰수 b는 작은 수 , up middle = flase , down = true
	IEnumerator downCheck(float a, float b, bool another)
	{
		while (true)
		{
			if(!another)
			{
				if (slider.value < a )
				{
					Stage2 = true;
					StartCoroutine(Singletone.Instance.timeIllumination2(sect[0]));

					timer[0] = false;
					timer[1] = false;
					timer[2] = false;
					break;
				}
			}

			else if (another)
			{

				if (slider.value > b)
				{
					Stage2 = true;
					StartCoroutine(Singletone.Instance.timeIllumination2(sect[0]));

					timer[0] = false;
					timer[1] = false;
					timer[2] = false;
					break;
				}
			}

			// 타이머 계산을 위해 만든 식
			if(slider.value > b && slider.value < a)
				timer[0] = true;
			else if (slider.value > a)
				timer[1] = true;
			else if (slider.value < b)
				timer[2] = true;

			yield return null;
		}

		while (true)
		{
			// 적절
			if (slider.value > b && slider.value < a)
			{
				fireIcon[1].SetActive(true);
				timer[0] = true;
				timer[1] = false;
				timer[2] = false;

				// 5초 이상이면 클리어 잘 익은 밥
				if (activeTime)
				{
					fireIcon[1].SetActive(true);
					audio.PlayOneShot(clip);

					Debug.Log("2 middle");
					Result = true;

					yield return new WaitForSeconds(2.4f);
					PlayParts2(0,0);
					
					break;
				}
			}

			//위
			else if (slider.value > a)
			{
				fireIcon[1].SetActive(false);
				timer[0] = false;
				timer[1] = true;
				timer[2] = false;

				//Debug.Log(timer[1]);

				// 5초 이상이면 클리어 탄 밥
				if (activeTime)
				{
					Debug.Log("2 up");
					Result = true;

					yield return new WaitForSeconds(2.4f);
					PlayParts2(1,1);
					break;
				}
			}
			//아래
			else if (slider.value < b)
			{
				fireIcon[1].SetActive(false);
				timer[0] = false;
				timer[1] = false;
				timer[2] = true;

				// 5초 이상이면 클리어 설익은 밥
				if (activeTime)
				{

					Debug.Log("2 down");
					Result = true;

					yield return new WaitForSeconds(2.4f);
					PlayParts2(2,1);
					break;
				}
			}
			yield return null;
		}
	}



	IEnumerator PlayPartanimation(int a)
	{
		activeTime = false;
		if (a < 0)
		{
			while (sect[0].transform.localPosition.y > a)
			{
				
				
				sect[0].transform.localPosition += new Vector3(0, -3, 0);
				sect[1].transform.localPosition += new Vector3(0, -3, 0);
				yield return null;
			}
		}
		else if (a > 0)
		{
			while (sect[0].transform.localPosition.y < a)
			{

				sect[0].transform.localPosition += new Vector3(0, 3, 0);
				sect[1].transform.localPosition += new Vector3(0, 3, 0);
				yield return null;
			}
		}
		yield return null;
	}

	//반복해서 써지는거 한줄로 만들어서 가시성 확보할려고 만듬
	void PlayParts(int a)
	{
		sect[0].transform.localPosition += new Vector3(0, a, 0);
		sect[1].transform.localPosition += new Vector3(0, a, 0);
		activeTime = false;
	}

	void PlayParts2(int a, int b)
	{
		activeTime = false;
		offimage[0].SetActive(false);
		offimage[1].SetActive(false);
		
		resultVCR.SetActive(true);
		resultVCR.GetComponent<VCR>()._VideoIndex = a;


		audio.clip = resultVCR.GetComponent<VCR>().resultClip[b];
		audio.Play();

		resultVideoDisPlay.SetActive(true);
		resultVCR.GetComponent<VCR>().TEST(a);
		BacktoOpening.OpenigCheck = true;
	}

}




