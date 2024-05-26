using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideText : MonoBehaviour
{
	[SerializeField] Sprite[] sprtie;
	[SerializeField] GameObject[] guide;
	public touch touchSystem;

	bool GuideCheck =true;
   
	public void Guide()
	{

		if(GuideCheck)
		{
			this.transform.localPosition = new Vector3(413, 0, 0);
			this.GetComponent<Image>().sprite = sprtie[0];
			GuideCheck = false;

			if (touchSystem.pos == touch.Pos.moreLeft)
				guide[0].SetActive(true);
			else if (touchSystem.pos == touch.Pos.left)
				guide[1].SetActive(true);
			else if (touchSystem.pos == touch.Pos.center)
				guide[2].SetActive(true);
			else if (touchSystem.pos == touch.Pos.right)
				guide[3].SetActive(true);
			else if (touchSystem.pos == touch.Pos.moreRight)
				guide[4].SetActive(true);

		}
		else
		{
			this.transform.localPosition = new Vector3(935, 0, 0);
			this.GetComponent<Image>().sprite = sprtie[1];

			guide[0].SetActive(false);
			guide[1].SetActive(false);
			guide[2].SetActive(false);
			guide[3].SetActive(false);
			guide[4].SetActive(false);
			GuideCheck = true;
		}
	}

	public void Guidefalse()
	{
		this.transform.localPosition = new Vector3(935, 0, 0);
		this.GetComponent<Image>().sprite = sprtie[1];

		guide[0].SetActive(false);
		guide[1].SetActive(false);
		guide[2].SetActive(false);
		guide[3].SetActive(false);
		guide[4].SetActive(false);
		GuideCheck = true;
	}
}
