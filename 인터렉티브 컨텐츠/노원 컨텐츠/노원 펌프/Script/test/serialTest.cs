using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class serialTest : MonoBehaviour
{
	public Text text1;
	public Text text2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		text1.text = ReadSerial.check1+"  "+ReadSerial.Count1.ToString();
		text2.text = ReadSerial.check2 + "  " + ReadSerial.Count2.ToString();

		if (Input.GetKeyDown(KeyCode.B))
		{
			text1.gameObject.SetActive(!text1.gameObject.activeSelf);
			text2.gameObject.SetActive(!text2.gameObject.activeSelf);
			// Debug.Log(123);
		}

    }
}
