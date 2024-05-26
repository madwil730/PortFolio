using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
	public RectTransform obj;
	public RectTransform[] Pot;
	public GameObject[] StovePot;
	public GameObject[] failPot;
	[SerializeField] GameObject[] activeIcon;
	[SerializeField] GameObject[] taetoBuild;
	public Image[] Potimage;
	public Image[] gaugeImage;

	AudioSource audio;
	public AudioClip clip;

	Vector3 pos;
	public static bool taetoPot0, taetoPot1, taetoPot2, taeto;
	bool PPotcheck0, PPotcheck1, PPotcheck2;
	bool PPPotcheck0, PPPotcheck1, PPPotcheck2;

	enum Pottery { num0, num1, num2, not }
	Pottery pottery;

	// Start is called before the first frame update
	void Start()
	{
		pottery = Pottery.not;
		audio = this.gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (pos.x > -1.86 && pos.x < 0.05 &&
			pos.y > -2.94 && pos.y < -1.13 && Input.GetMouseButtonDown(0))
			Singtone.Instance.mouseCheck = true;

		if (Input.GetMouseButton(0) && Singtone.Instance.mouseCheck && Singtone.Instance.count < 6)
		{
			//Debug.Log(Singtone.Instance.count);
			obj.position = Input.mousePosition;
		}

		else if (Input.GetMouseButtonUp(0))
		{
			if (obj.transform.localPosition.x > 56 && obj.transform.localPosition.x < 184 &&
			   obj.transform.localPosition.y > -333 && obj.transform.localPosition.y < -217)
			{
				taetoPot0 = true;
				activeIcon[0].SetActive(true);
			}
			else if (obj.transform.localPosition.x > 272 && obj.transform.localPosition.x < 396 &&
			   obj.transform.localPosition.y > -333 && obj.transform.localPosition.y < -217)
			{
				taetoPot1 = true;
				activeIcon[1].SetActive(true);
			}
			else if (obj.transform.localPosition.x > 493 && obj.transform.localPosition.x < 613 &&
			   obj.transform.localPosition.y > -333 && obj.transform.localPosition.y < -217)
			{
				taetoPot2 = true;
				activeIcon[2].SetActive(true);
			}

			//taeto = false;
			Singtone.Instance.mouseCheck = false;
			obj.transform.localPosition = new Vector3(-100, -327, -2);
		}
		else
			taeto = false;


		// 항아리 fillamount 채우기
		fillAmount(taetoPot0, 0);
		fillAmount(taetoPot1, 1);
		fillAmount(taetoPot2, 2);

		Pots();

		if (StoveController.offSound||StoveController1.offSound)
			audio.Stop();
	}

	void Pots()
	{
		if (Input.GetMouseButtonDown(0) && Singtone.Instance.count < 6)
		{
			if (pos.x > 0.59f && pos.x < 1.65f &&
			  pos.y > -2.06f && pos.y < -0.77f && gaugeImage[0].fillAmount == 1)
			{
				PPotcheck0 = true;
				pottery = Pottery.num0;
			}

			if (pos.x > 2.59f && pos.x < 3.6f &&
			  pos.y > -2.06f && pos.y < -0.77f && gaugeImage[1].fillAmount == 1)
			{

				PPotcheck1 = true;
				pottery = Pottery.num1;
			}

			if (pos.x > 4.55f && pos.x < 5.58f &&
			  pos.y > -2.06f && pos.y < -0.77f && gaugeImage[2].fillAmount == 1)
			{
				PPotcheck2 = true;
				pottery = Pottery.num2;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			// 마우스를 화로 위에 올린다면
			if (pos.x > -3.5 && pos.x < -0.95 &&
			  pos.y > -0.47 && pos.y < 1.7 && StoveController.PotPrimaryKey == 5)
			{
	
				if (PPotcheck0)
				{
					audio.clip = clip;
					audio.Play();
					activeIcon[0].SetActive(false);
					StovePot[0].SetActive(true);
					//failPot[1].SetActive(false);
					//failPot[2].SetActive(false);
					Potimage[0].fillAmount = 0;
					taetoBuild[0].SetActive(false);
					gaugeImage[0].fillAmount = 0;
					taetoPot0 = false;
				}

				else if (PPotcheck1)
				{
					audio.clip = clip;
					audio.Play();
					activeIcon[1].SetActive(false);
					StovePot[1].SetActive(true);
					Potimage[1].fillAmount = 0;
					taetoBuild[1].SetActive(false);
					gaugeImage[1].fillAmount = 0;
					taetoPot1 = false;
				}
				else if (PPotcheck2)
				{
					audio.clip = clip;
					audio.Play();
					activeIcon[2].SetActive(false);
					StovePot[2].SetActive(true);
					Potimage[2].fillAmount = 0;
					taetoBuild[2].SetActive(false);
					gaugeImage[2].fillAmount = 0;
					taetoPot2 = false;
				}
			}

			else if (pos.x > -0.33 && pos.x < 1.47f &&
					pos.y > -0.17f && pos.y < 1.32f && StoveController1.PotPrimaryKey1 == 5)
			{
				if (PPotcheck0)
				{
					audio.clip = clip;
					audio.Play();
					activeIcon[0].SetActive(false);
					StovePot[3].SetActive(true);
					Potimage[0].fillAmount = 0;
					taetoBuild[0].SetActive(false);
					gaugeImage[0].fillAmount = 0;
					taetoPot0 = false;
				}
				else if (PPotcheck1)
				{
					audio.clip = clip;
					audio.Play();
					activeIcon[1].SetActive(false);
					StovePot[4].SetActive(true);
					Potimage[1].fillAmount = 0;
					taetoBuild[1].SetActive(false);
					gaugeImage[1].fillAmount = 0;
					taetoPot1 = false;
				}
				else if (PPotcheck2)
				{
					audio.clip = clip;
					audio.Play();
					activeIcon[2].SetActive(false);
					StovePot[5].SetActive(true);
					Potimage[2].fillAmount = 0;
					taetoBuild[2].SetActive(false);
					gaugeImage[2].fillAmount = 0;
					taetoPot2 = false;
				}
			}

			PPotcheck0 = false;
			PPotcheck1 = false;
			PPotcheck2 = false;
			pottery = Pottery.not;
			Pot[0].localPosition = new Vector3(119, -250, 0);
			Pot[1].localPosition = new Vector3(339, -245, 0);
			Pot[2].localPosition = new Vector3(546, -249, 0);
		}

		if (Result.check)
		{
			PPotcheck0 = false;
			PPotcheck1 = false;
			PPotcheck2 = false;
			pottery = Pottery.not;
			Pot[0].localPosition = new Vector3(119, -250, 0);
			Pot[1].localPosition = new Vector3(339, -245, 0);
			Pot[2].localPosition = new Vector3(546, -249, 0);

			Singtone.Instance.mouseCheck = false;
			obj.transform.localPosition = new Vector3(-100, -327, -2);
		}


		if (PPotcheck0)
		{
			// 해당 항아리를 클랙했다면
			if (pottery == Pottery.num0 && Input.GetMouseButton(0))
				Pot[0].position = Input.mousePosition;
		}

		if (PPotcheck1)
		{
			// 해당 항아리를 클랙했다면
			if (pottery == Pottery.num1 && Input.GetMouseButton(0))
				Pot[1].position = Input.mousePosition;
		}

		if (PPotcheck2)
		{
			// 해당 항아리를 클랙했다면
			if (pottery == Pottery.num2 && Input.GetMouseButton(0))
				Pot[2].position = Input.mousePosition;
		}
	}

	void fillAmount(bool check, int num)
	{
		if (check)
		{
			Potimage[num].fillAmount += Time.deltaTime / 2;
			gaugeImage[num].fillAmount += Time.deltaTime / 2;

			if (gaugeImage[num].fillAmount > 0.8f)
				gaugeImage[num].color = new Color(1, 0.47f, 0.082f);
			else
				gaugeImage[num].color = new Color(0.611f, 0.95f, 0.90f);

			if (gaugeImage[num].fillAmount == 1)
				taetoBuild[num].SetActive(true);
			

		}
	}
}
