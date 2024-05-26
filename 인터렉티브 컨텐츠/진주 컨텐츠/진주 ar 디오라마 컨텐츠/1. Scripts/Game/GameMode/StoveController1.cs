using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//, IBeginDragHandler, IDragHandler, IEndDragHandler
public class StoveController1 : MonoBehaviour
{
	public GameObject[] animator; // 구워지는 항아리 표시(성공, 실패 항아리 포함됨)
	public GameObject[] textanimatior; // 텍스트 말풍선 표시
	public GameObject[] successIcon; // 성공 아이콘 표시
	[SerializeField] GameObject Burnning; // 화덕 구워지는 상태
	public RectTransform[] PotRect; // 옮겨져야할 항아리 표시
	public Image StoveGuage;
	AudioSource audio;
	public AudioClip[] clip;
	Vector3 pos;

	bool[] check = { false, false, false };
	bool moveCheck;

	// 5 는 널값
	public static bool offSound;
	public static int PotPrimaryKey1 = 5;
	bool Burned;
	float burnTime = 0;
	// Start is called before the first frame update
	private void OnEnable()
	{
		StartCoroutine(Pot(0, 0));
		StartCoroutine(Pot(3, 1));
		StartCoroutine(Pot(6, 2));
		textanimatior[0].SetActive(false);
		textanimatior[1].SetActive(false);
		textanimatior[2].SetActive(false);
		textanimatior[3].SetActive(false);
		textanimatior[4].SetActive(false);
		textanimatior[5].SetActive(false);
		PotPrimaryKey1 = 5;

	}

	void Start()
	{
		audio = this.gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		// 마우스를 누르고 있고 게이지바 가 다 차면
		if (Input.GetMouseButtonDown(0) && StoveGuage.fillAmount == 1 && !Burned && Singtone.Instance.count < 6)
		{
			if (pos.x > -0.61 && pos.x <1.7  &&
			  pos.y > -0.49 && pos.y < 1.56)
			{
				moveCheck = true;
				offSound = true;
				Burnning.SetActive(false);
				//Singtone.Instance.mouseCheck = true;
				animator[0].SetActive(false);
				animator[3].SetActive(false);
				animator[6].SetActive(false);
				burnTime = 0;
				StoveGuage.fillAmount = 0;
			}
		}
		// 마우스를 떼면 초기화
		else if (Input.GetMouseButtonUp(0) && moveCheck && Singtone.Instance.count < 6)
		{
			//해당 말풍선 영역 안에 있다면
			if (pos.x > -5.39f && pos.x < -3.12f &&
			  pos.y > 1.15f && pos.y < 3.25f && !textAnimation.check[0])
			{

				if (PotPrimaryKey1 == 0)
				{
					textanimatior[0].SetActive(true);
					successIcon[Singtone.Instance.count].SetActive(true);
					Singtone.Instance.count += 1;
					textAnimation.check[0] = true;
					audio.PlayOneShot(clip[0]);

				}
				else
				{
					textanimatior[1].SetActive(true);
					textAnimation.check[0] = true;
					audio.PlayOneShot(clip[1]);

				}
			}
			else if (pos.x > -2.24f && pos.x < -0.05f &&
			  pos.y > 2.99f && pos.y < 5.13f && !textAnimation.check[1])
			{

				if (PotPrimaryKey1 == 1)
				{
					textanimatior[2].SetActive(true);
					successIcon[Singtone.Instance.count].SetActive(true);
					Singtone.Instance.count += 1;
					textAnimation.check[1] = true;
					audio.PlayOneShot(clip[0]);

				}
				else
				{
					textanimatior[3].SetActive(true);
					textAnimation.check[1] = true;
					audio.PlayOneShot(clip[1]);

				}
			}
			else if (pos.x > 2.01f && pos.x < 4.14f &&
			  pos.y > 3.08f && pos.y < 4.96f && !textAnimation.check[2])
			{

				if (PotPrimaryKey1 == 2)
				{
					textanimatior[4].SetActive(true);
					successIcon[Singtone.Instance.count].SetActive(true);
					Singtone.Instance.count += 1;
					textAnimation.check[2] = true;
					audio.PlayOneShot(clip[0]);
				}
				else
				{
					textanimatior[5].SetActive(true);
					textAnimation.check[2] = true;
					audio.PlayOneShot(clip[1]);
				}
			}

			moveCheck = false;
			offSound = false;
			for (int i = 0; i < animator.Length; i++)
				animator[i].SetActive(false);

			PotPrimaryKey1 = 5;
			PotRect[0].localPosition = new Vector3(59, -33, 0);
			PotRect[1].localPosition = new Vector3(59, -33, 0);
			PotRect[2].localPosition = new Vector3(59, -33, 0);
		}

		if(Result.check )
		{
			PotPrimaryKey1 = 5;
			PotRect[0].localPosition = new Vector3(59, -33, 0);
			PotRect[1].localPosition = new Vector3(59, -33, 0);
			PotRect[2].localPosition = new Vector3(59, -33, 0);

			moveCheck = false;
			offSound = false;
			for (int i = 0; i < animator.Length; i++)
				animator[i].SetActive(false);
		}

		if (moveCheck)
		{
			//Debug.Log(PotPrimaryKey1);
			if (PotPrimaryKey1 == 0)
			{
				PotRect[0].position = Input.mousePosition;
			}

			else if (PotPrimaryKey1 == 1)
			{
				PotRect[1].position = Input.mousePosition;
			}

			else if (PotPrimaryKey1 == 2)
			{
				PotRect[2].position = Input.mousePosition;
			}
		}

	}

	IEnumerator Pot(int num, int num2)
	{
		while (true)
		{
			if (animator[num].activeSelf)
			{
				// 항아리 키값 삽입
				PotPrimaryKey1 = num2;
				Burnning.SetActive(true);

				StoveGuage.fillAmount += Time.deltaTime;
				animator[num + 1].SetActive(true);

				if (StoveGuage.fillAmount > 0.8f)
					StoveGuage.color = new Color(1, 0.47f, 0.082f);
				else
					StoveGuage.color = new Color(0.611f, 0.95f, 0.90f);

				// 게이지가 다 차면 탄 토기 나옴
				if (StoveGuage.fillAmount == 1)
				{
					burnTime += Time.deltaTime;
					//yield return new WaitForSeconds(2);

					// 마우스를 클릭하지 않으면
					if (!moveCheck && burnTime > 3)
					{
						Burned = true;
						animator[num + 2].SetActive(true);

						// 탄 토기 알파가 1이면 false
						if (animator[num + 2].GetComponent<Image>().color.a == 1)
						{
							animator[num].SetActive(false);
							animator[num + 1].SetActive(false);
							check[num2] = true;
							Burnning.SetActive(false);
							audio.PlayOneShot(clip[2]); // 토기 터지는 소리
						}
					}
				}

			}
			// check 쓰는 이유는 다른 오브젝트 와의 혼동을 피하기 위해
			else if (!animator[num].activeSelf && check[num2])
			{
				StoveGuage.fillAmount = 0;
				//Debug.Log(011);
				//	//원래 토기가 사라지므로 disapper 사라짐
				animator[num + 2].GetComponent<Animator>().SetBool("disapper", true);

				if (animator[num + 2].GetComponent<Image>().color.a == 0)
				{
					animator[num + 2].SetActive(false);
					Burned = false;
					check[num2] = false;
					PotPrimaryKey1 = 5;
					burnTime = 0;
				}

			}

			yield return null;
		}

	}

}
