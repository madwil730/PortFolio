using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
	[SerializeField] RectTransform rect;
	public float x = 1.008f;
	public float y = 1.008f;
    // Start is called before the first frame update
    void Start()
    {
		rect.localScale = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
