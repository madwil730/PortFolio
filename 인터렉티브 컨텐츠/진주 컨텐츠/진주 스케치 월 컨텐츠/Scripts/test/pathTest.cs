using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacovone;

public class pathTest : MonoBehaviour
{
	public PathMagic path;

    // Start is called before the first frame update
    void Start()
    {
		//path.waypoints[0].position = new Vector3(3, 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.A))
			path.waypoints[1].position = new Vector3(100, 100, 3);

		else if (Input.GetKeyDown(KeyCode.R))
			path.Rewind();
	}
}
