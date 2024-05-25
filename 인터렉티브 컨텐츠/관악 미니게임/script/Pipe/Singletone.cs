using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone : MonoBehaviour
{
  
    private static Singletone instance = null;

    public List<GameObject> list;
    public List<GameObject> listPipe;

	public int Count;
	public GameObject rotateObject;

    void Awake()
    {
        if (null == instance)
        {
        
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
      
            Destroy(this.gameObject);
        }
    }

	private void Start()
	{
		list = new List<GameObject>();
		listPipe = new List<GameObject>();

		//rotateObject = new GameObject();
		//rotateObject.name = "rotateObject";
		
	}

	public static Singletone Instance
    {
        get
        {

            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

}
