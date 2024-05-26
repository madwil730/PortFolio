using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jacovone;

public class Constellation : MonoBehaviour
{
	RawImage raw;
	//[SerializeField] GameObject me;
	[SerializeField] GameObject[] star;
	[SerializeField] GameObject magicPath;
	[SerializeField] GameObject magicPath_Star;
	[SerializeField] GameObject[] magicPath_Star_Reverse;

	[SerializeField] int index;
	[SerializeField] GameObject explore;
	[SerializeField] GameObject obj;
	//[SerializeField] AudioSource audio;

	float time;
	
	Color minusColor = new Color(0, 0, 0, 0.01f);




	private void OnEnable()
	{
		explore.SetActive(false);

		raw = this.gameObject.GetComponent<RawImage>();
		obj.SetActive(true);
		star[Singletone.Instance.QR_WareHouse[index] - 1].SetActive(true);


		magicPath.GetComponent<PathMagic>().Play();
		magicPath_Star.GetComponent<PathMagic>().Play();

		

		StartCoroutine(TimeConstellation());

	}

	float wiggleDistance = 10;
	float wiggleSpeed = 1f;

	void Update()
	{
		float yPosition = Mathf.Cos(Time.time * wiggleSpeed) * wiggleDistance;
		float xPosition = Mathf.Sin(Time.time * wiggleSpeed) * wiggleDistance;
		this.transform.localPosition = new Vector3(xPosition, yPosition-15, 0);
		//Debug.Log(11);
	}

	IEnumerator TimeConstellation()
	{
		int num = Singletone.Instance.QR_WareHouse[index];
		

		while (true)
		{
			if(num == 0)
			{
				//Debug.Log("while in 0 goto break");
				break;
			}

			time += Time.deltaTime;

			if (time > 90)
				break;

			//Debug.Log(raw.color);
			
			yield return null;
		}

		time = 0;
		explore.SetActive(true);

		magicPath.SetActive(false);
		magicPath.GetComponent<PathMagic>().Rewind();

		magicPath.GetComponent<PathMagic>().waypoints[0].rotation = new Vector3(0, 0, 0);
		magicPath.GetComponent<PathMagic>().waypoints[1].rotation = new Vector3(0, 0, 0);

		magicPath_Star.SetActive(false);
		magicPath_Star.GetComponent<PathMagic>().Rewind();
		obj.GetComponent<Animator>().enabled = false;

		yield return new WaitForSeconds(1); // 오디오 출력 때문에 1로 정함


		if (num - 1 == 0)
		{
			Debug.Log(0);
			magicPath_Star_Reverse[0].transform.GetChild(0).gameObject.SetActive(true);
			magicPath_Star_Reverse[0].transform.GetChild(0).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(1).gameObject.SetActive(true);
			magicPath_Star_Reverse[0].transform.GetChild(1).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(2).gameObject.SetActive(true);
			magicPath_Star_Reverse[0].transform.GetChild(2).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(3).gameObject.SetActive(true);
			magicPath_Star_Reverse[0].transform.GetChild(3).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(4).gameObject.SetActive(true);
			magicPath_Star_Reverse[0].transform.GetChild(4).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);

			magicPath_Star_Reverse[0].transform.GetChild(5).gameObject.SetActive(true);
			magicPath_Star_Reverse[0].transform.GetChild(5).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
		}
		else if(num -1 == 1)
		{
			Debug.Log(1);
			magicPath_Star_Reverse[1].transform.GetChild(0).gameObject.SetActive(true);
			magicPath_Star_Reverse[1].transform.GetChild(0).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(1).gameObject.SetActive(true);
			magicPath_Star_Reverse[1].transform.GetChild(1).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(2).gameObject.SetActive(true);
			magicPath_Star_Reverse[1].transform.GetChild(2).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(3).gameObject.SetActive(true);
			magicPath_Star_Reverse[1].transform.GetChild(3).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);

			magicPath_Star_Reverse[1].transform.GetChild(4).gameObject.SetActive(true);
			magicPath_Star_Reverse[1].transform.GetChild(4).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
		}
		else if (num - 1 == 2)
		{
			Debug.Log(2);
			magicPath_Star_Reverse[2].transform.GetChild(0).gameObject.SetActive(true);
			magicPath_Star_Reverse[2].transform.GetChild(0).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[2].transform.GetChild(1).gameObject.SetActive(true);
			magicPath_Star_Reverse[2].transform.GetChild(1).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[2].transform.GetChild(2).gameObject.SetActive(true);
			magicPath_Star_Reverse[2].transform.GetChild(2).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[2].transform.GetChild(3).gameObject.SetActive(true);
			magicPath_Star_Reverse[2].transform.GetChild(3).GetComponent<PathMagic>().Play();
			yield return new WaitForSeconds(0.2f);
			
		}

		yield return new WaitForSeconds(3.5f);

		star[num-1].SetActive(false);

		if (num - 1 == 0)
		{
			magicPath_Star_Reverse[0].transform.GetChild(0).gameObject.SetActive(false);
			magicPath_Star_Reverse[0].transform.GetChild(0).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(1).gameObject.SetActive(false);
			magicPath_Star_Reverse[0].transform.GetChild(1).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(2).gameObject.SetActive(false);
			magicPath_Star_Reverse[0].transform.GetChild(2).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(3).gameObject.SetActive(false);
			magicPath_Star_Reverse[0].transform.GetChild(3).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(4).gameObject.SetActive(false);
			magicPath_Star_Reverse[0].transform.GetChild(4).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[0].transform.GetChild(5).gameObject.SetActive(false);
			magicPath_Star_Reverse[0].transform.GetChild(5).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
		}
		else if (num - 1 == 1)
		{
			magicPath_Star_Reverse[1].transform.GetChild(0).gameObject.SetActive(false);
			magicPath_Star_Reverse[1].transform.GetChild(0).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(1).gameObject.SetActive(false);
			magicPath_Star_Reverse[1].transform.GetChild(1).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(2).gameObject.SetActive(false);
			magicPath_Star_Reverse[1].transform.GetChild(2).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(3).gameObject.SetActive(false);
			magicPath_Star_Reverse[1].transform.GetChild(3).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[1].transform.GetChild(4).gameObject.SetActive(false);
			magicPath_Star_Reverse[1].transform.GetChild(4).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
		}
		else if (num - 1 == 2)
		{
			magicPath_Star_Reverse[2].transform.GetChild(0).gameObject.SetActive(false);
			magicPath_Star_Reverse[2].transform.GetChild(0).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[2].transform.GetChild(1).gameObject.SetActive(false);
			magicPath_Star_Reverse[2].transform.GetChild(1).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[2].transform.GetChild(2).gameObject.SetActive(false);
			magicPath_Star_Reverse[2].transform.GetChild(2).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);
			magicPath_Star_Reverse[2].transform.GetChild(3).gameObject.SetActive(false);
			magicPath_Star_Reverse[2].transform.GetChild(3).GetComponent<PathMagic>().Rewind();
			yield return new WaitForSeconds(0.2f);

		}


		obj.SetActive(false);
		obj.GetComponent<Animator>().enabled = true;

		this.gameObject.SetActive(false);

		
	}

}
