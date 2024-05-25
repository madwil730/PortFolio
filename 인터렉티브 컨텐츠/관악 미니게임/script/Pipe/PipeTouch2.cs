using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PipeTouch2 : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public Camera camera;
	public Canvas canvas;
	public GameObject[] ChildObject;
	public GameObject Prefab;
	public int x;
	public Image image;
	public Sprite open;
	public Sprite close;

	[HideInInspector]
	public GameObject ob; // 부모 오브젝트로 위치 전환 하기 위해 넣은 temp
	[HideInInspector]
	public GameObject Parent; // 맞붙은 콜라이더의 부모 오브젝트

	bool trigerCheck; // 콜리더 트리거 체크 
	bool makeCheck; // 생성 제한 체크
	bool lastPipeCheck; // 마지막 파이프 뺀거 체크 

	[HideInInspector]
	public int youType;// 상대 콜리전 타입 체크
	[HideInInspector]
	public int meType ;// 내 콜리전 타입 체크
	[HideInInspector]
	public int reverseType; // 삭제시 listpipe 조건 체크

	public enum Type { Pipe1, Pipe2, Pipe3, Pipe4, Pipe5, Pipe6 } // 0,1,2,3
	public Type type;

	bool up, down, left, right;
	bool onPoint;

	public void OnDrag(PointerEventData eventData)
	{
		if (transform.localPosition.x > 400 && transform.localPosition.x < 600)
		{
			if (transform.localPosition.y > 460 && transform.localPosition.y < 650)
			{
				image.sprite = open;
			}
		}
		else
			image.sprite = close;



		if (!lastPipeCheck)
		{
			this.transform.localScale = new Vector3(0.7f, 0.7f, 0);
			transform.position = Input.mousePosition;
			this.GetComponent<RectTransform>().SetAsLastSibling();
		}

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Singletone.Instance.rotateObject = this.gameObject; //회전 지정
		this.GetComponent<BoxCollider2D>().enabled = true;
		onPoint = true;

		//Debug.Log(Singletone.Instance.listPipe.Count + "  listPipe");
		//Debug.Log(Singletone.Instance.list.Count + "  List");

		//마지막 파이프 땔수 있게 만드는 코드
		if (Singletone.Instance.listPipe.Count > 0)
		{
			if (this.gameObject == Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1])
			{
				Debug.Log(33);
				ob = new GameObject();
				ob.name = "Temp " + this.gameObject.name;

				this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x+1, this.gameObject.transform.localPosition.y, 0);
				Singletone.Instance.rotateObject = null;

				if (Singletone.Instance.listPipe.Count > 1)
				{
					for (int i = 0; i < Parent.transform.parent.GetComponent<PipeTouch2>().ChildObject.Length; i++)
					{
						//파이프 붙여지 부분만 오브젝트 비할성화
						if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().ChildObject[i].GetComponent<CollisionType>().type == youType)
							Parent.transform.parent.GetComponent<PipeTouch2>().ChildObject[i].SetActive(true);
					}
				}

				Singletone.Instance.listPipe.RemoveAt(Singletone.Instance.listPipe.Count - 1);
				lastPipeCheck = false;
				//trigerCheck = true; // 이유는 모르겠지만 잘 안되서 넣음
			}
		}

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		// 산 아래까지 파이프 안이어지게
		if (Singletone.Instance.listPipe.Count > 0)
		{
			if (Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1].transform.localPosition.y < -39)
				trigerCheck = false;
		}

		// 특정 상황시 오브젝트 생성
		if (this.transform.localPosition.x > -600 && this.transform.localPosition.x < 600)
		{
			if (!makeCheck && this.transform.localPosition.y > -770)
			{
				//Debug.Log(makeCheck);
				Singletone.Instance.list.Add(this.gameObject);
				//Debug.Log("list  " +  Singletone.Instance.list.Count);

				var Parent = Instantiate(Prefab, transform.position, new Quaternion(0, 0, 0, 0));
				Parent.transform.SetParent(canvas.transform);
				Parent.name = "Copy";
				Parent.GetComponent<BoxCollider2D>().enabled = false;
				Parent.GetComponent<PipeTouch2>().enabled = true;

				for (int i = 0; i < Parent.GetComponent<PipeTouch2>().ChildObject.Length; i++)
					Parent.GetComponent<PipeTouch2>().ChildObject[i].SetActive(false);

				Parent.transform.localPosition = new Vector3(x, -836, 0);
				Parent.transform.localScale = new Vector3(0.3f, 0.3f, 0);
				makeCheck = true; // 한번만 생성되게 제어
			}
		}

		trash();

		//파이프 이을시 실행되는 함수
		if (trigerCheck)
		{
			//Debug.Log(this.gameObject.transform.localPosition);

			Singletone.Instance.listPipe.Add(this.gameObject);
			
			for (int i = 0; i < ChildObject.Length; i++)
			{
				if ((int)ChildObject[i].GetComponent<CollisionType>().type == meType)
					ChildObject[i].SetActive(false);
				else
					ChildObject[i].SetActive(true);

				//2번 파이프 위 콜라이더 없애기 있으면 연결 할때 다른 파이프 위에 겹쳐서서 이상해짐
				if ((int)type == 1)
				{
					ChildObject[1].SetActive(false);
				}

				else if ((int)type == 0)
				{
					ChildObject[1].SetActive(false);
				}
			}

			if (StartPosition.start)
			{
				//Debug.Log(33);
				etc();
			}

			else if (Singletone.Instance.listPipe.Count > 1)
			{
				for (int i = 0; i < Parent.transform.parent.GetComponent<PipeTouch2>().ChildObject.Length; i++)
				{
					Parent.transform.parent.GetComponent<PipeTouch2>().ChildObject[i].SetActive(false);
				}

				if ((int)type == 0)
					Pipe0();
				else if ((int)type == 1)
					Pipe1();
				else if ((int)type == 2)
					Pipe2();
				else if ((int)type == 3)
					Pipe3();
				else if ((int)type == 4)
					Pipe4();
				else if ((int)type == 5)
					Pipe5();
			}

			Singletone.Instance.rotateObject = null;
			Destroy(ob);
		
		}

		this.GetComponent<BoxCollider2D>().enabled = false ;
		onPoint = false;
	}


	// 0 up 1 down 2 left 3 right , youType는 상대방 의 콜리전의 타입이다
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(onPoint )
		{
			//Debug.Log(333);
			ob.transform.position = collision.GetComponent<RectTransform>().position;
			ob.transform.parent = canvas.transform;
			Parent = collision.gameObject;

			if (up)
			{
				//up
				if ((int)collision.GetComponent<CollisionType>().type == 1)
				{
				//	Debug.Log("up");
					trigerCheck = true;
					youType = 1;
					meType = 0;
				}
			}
			if (down)
			{
				//down
				if ((int)collision.GetComponent<CollisionType>().type == 0)
				{
				//	Debug.Log("down");
					trigerCheck = true;
					youType = 0;
					meType = 1;
				}
			}
			if (left)
			{
			
				if ((int)collision.GetComponent<CollisionType>().type == 3)
				{
				//	Debug.Log("left");
					trigerCheck = true;
					youType = 3;
					meType = 2;
				}
			}

			if (right)
			{
				if ((int)collision.GetComponent<CollisionType>().type == 2)
				{
					//Debug.Log("right");
					trigerCheck = true;
					youType = 2;
					meType = 3;
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		trigerCheck = false;
	}





	//---------------------------------------------------------------------------



	// Start is called before the first frame update
	void Start()
    {
		ob = new GameObject();
		ob.name = "Temp " + this.gameObject.name;
	}

    // Update is called once per frame
    void Update()
    {

		//Debug.Log("triggerCheck  " + trigerCheck);
		// 해당 오브젝트가 마지막 파이프면 true
		if(Singletone.Instance.listPipe.Count>0)
		{
			if (this.gameObject == Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1])
				lastPipeCheck = true;
		}


		//Pipe1
		if ((int)type == 0)
		{
			//if (this.transform.rotation.eulerAngles.z == 0)
			{
				up = false;
				down = true;
				left = true;
				right = false;
			}
		}

		//Pipe2
		else if ((int)type == 1)
		{
			//if (this.transform.rotation.eulerAngles.z == 0)
			{
				up = true;
				down = false;
				left = false;
				right = true;
			}
		}

		//Pipe3
		else if ((int)type == 2)
		{
			//Debug.Log(this.transform.rotation.eulerAngles);
			//if (this.transform.rotation.eulerAngles.z == 0)
			{
				up = false;
				down = true;
				left = false;
				right = true;
			}
		}

		//Pipe4
		else if ((int)type == 3)
		{
			//if (this.transform.rotation.eulerAngles.z == 0)
			{
				//Debug.Log(888);
				up = true;
				down = false;
				left = true;
				right = false;
			}
		}

		else if ((int)type == 4)
		{
			//if (this.transform.rotation.eulerAngles.z == 0)
			{
				//Debug.Log(888);
				up = true;
				down = true;
				left = false;
				right = false;
			}
		}

		else if ((int)type == 5)
		{
			//if (this.transform.rotation.eulerAngles.z == 0)
			{
				//Debug.Log(888);
				up = false ;
				down = false;
				left = true;
				right = true;
			}
		}
	}

	// 0 up 1 down 2 left 3 right , youType는 상대방 의 콜리전의 타입이다

	void Pipe0()
	{
		Vector3 vec = Parent.transform.parent.localPosition;
		Debug.Log(youType);
		if (youType == 3)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 5)
			{
				this.transform.localPosition = new Vector3(vec.x +280, vec.y-90 , 0);
			}

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 1)
			{
				this.transform.localPosition = new Vector3(vec.x + 280, vec.y - 180, 0);
			}

		}

		else if (youType == 0)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 3)
			{
				this.transform.localPosition = new Vector3(vec.x , vec.y +280, 0);
			}

		}
	}


	void Pipe1()
	{
		//Debug.Log(youType);
		Vector3 vec = Parent.transform.parent.localPosition;
		if (youType == 2)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 3)
				this.transform.localPosition = new Vector3(vec.x-280, vec.y, 0);

		}

		else if (youType == 1)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 0)
				this.transform.localPosition = new Vector3(vec.x + 180, vec.y - 280, 0);
			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 4)
				this.transform.localPosition = new Vector3(vec.x +90, vec.y-280 , 0);
			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 2)
				this.transform.localPosition = new Vector3(vec.x , vec.y-280 , 0);

		}
	}

	void Pipe2()
	{
		//Debug.Log(youType);
		Vector3 vec = Parent.transform.parent.localPosition;
		//Debug.Log(vec);
		if (youType == 2)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 3)
				this.transform.localPosition = new Vector3(vec.x - 280, vec.y-180 , 0);

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 5)
				this.transform.localPosition = new Vector3(vec.x-280 , vec.y-90 , 0);
		}

		else if (youType == 0)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 3)
				this.transform.localPosition = new Vector3(vec.x+180 , vec.y+280 , 0);

		}
	}

	void Pipe3()
	{
		Vector3 vec = Parent.transform.parent.localPosition;
		if (youType == 1)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 0)
				this.transform.localPosition = new Vector3(vec.x , vec.y - 280, 0);

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 4)
				this.transform.localPosition = new Vector3(vec.x-90, vec.y-280 , 0);
		}

		else if (youType == 3)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 5)
			{
				this.transform.localPosition = new Vector3(vec.x + 280, vec.y+90 , 0);
			}

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 1)
			{
				this.transform.localPosition = new Vector3(vec.x +280, vec.y , 0);
			}

		}
	}

	void Pipe4()
	{
		Vector3 vec = Parent.transform.parent.localPosition;
		if (youType == 1)
		{
			if((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 0)
				this.transform.localPosition = new Vector3(vec.x+90 , vec.y - 280, 0);

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 4)
				this.transform.localPosition = new Vector3(vec.x, vec.y - 280, 0);

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 2)
				this.transform.localPosition = new Vector3(vec.x-90 , vec.y -280, 0);
		}
		else if (youType == 0)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 3)
				this.transform.localPosition = new Vector3(vec.x+90 , vec.y +280, 0);
			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 4)
				this.transform.localPosition = new Vector3(vec.x , vec.y + 280, 0);

		}
	}

	void Pipe5()
	{
		Vector3 vec = Parent.transform.parent.localPosition;
		Debug.Log(youType);
	
		if (youType == 2)
		{
			
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 3)
			{
				this.transform.localPosition = new Vector3(vec.x -280, vec.y-90, 0);
			}
		}

		else if (youType == 3)
		{
			if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 5)
			{
				this.transform.localPosition = new Vector3(vec.x + 280, vec.y, 0);
			}

			else if ((int)Parent.transform.parent.GetComponent<PipeTouch2>().type == 1)
			{
				this.transform.localPosition = new Vector3(vec.x+280 , vec.y-90, 0);
			}
		}
	}

	void etc()
	{
		if ((int)type == 0)
			this.transform.localPosition = new Vector3(-202, 700, 0);

		else if ((int)type == 3)
		{
			Debug.Log(1213);
			this.transform.localPosition = new Vector3(-202, 882, 0);
		}

		else if ((int)type == 5)
			this.transform.localPosition = new Vector3(-202, 789, 0);
	}

	void trash()
	{
		if(transform.localPosition.x > 400 && transform.localPosition.x < 600)
		{
			if (transform.localPosition.y > 460 && transform.localPosition.y < 650)
			{

				transform.localPosition = new Vector3(1000, 1000, 0);
				image.sprite = close;

				//Destroy(ob);
				//Destroy(this.gameObject);

			}
		}
	}
}
