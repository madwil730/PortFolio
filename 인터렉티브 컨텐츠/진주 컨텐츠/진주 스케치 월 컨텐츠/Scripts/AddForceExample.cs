using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceExample : MonoBehaviour
{
	Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
		rigid = this.gameObject.GetComponent<Rigidbody2D>();
		rigid.gravityScale = Random.Range(0.1f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
		//if(Input.GetKeyDown(KeyCode.Space))
		rigid.AddForce(new Vector3(-1, 0, 0));

		if (this.gameObject.transform.position.y < -300)
			Destroy(this.gameObject);
    }
}
