using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
	public GameObject[] offImgae;
	public GameObject[] onImage;
	public VCR vcr;
	[Header("----------------------------------")]
	public GameObject[] offImgae2;
	public GameObject[] onImage2;


	public Image image;
	Color color;

	public static bool First ; // 시작화면 5초 체크용
	public static bool Second ; // 체험방법화면 9초
	float time;
	// Start is called before the first frame update
	void Start()
    {
		//color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
		if(First)
		{
			time += Time.deltaTime;

			if (time > 7)
			{
				time = 0;
				First = false;
				StartCoroutine(Singletone.Instance.FadeVCR(color, image, offImgae, onImage, vcr));
				
			}
		}

		else if(Second)
		{
			time += Time.deltaTime;

			if (time > 18)
			{
				time = 0;
				Second = false;
				StartCoroutine(Singletone.Instance.Fade(color, image, offImgae2, onImage2));
			}
		}
    }
}
