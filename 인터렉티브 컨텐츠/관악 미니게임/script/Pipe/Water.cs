using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
	public GameObject ob;
	GameObject reverse;
	public Animator ani;
	public Animator mountain;
	int type;

	private void OnEnable()
	{
		ob.SetActive(false);
	}
	// 0 up 1 down 2 left 3 right
	public void water()
	{
		

		if (Singletone.Instance.listPipe.Count > 0)
		{
			GameObject temp = Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count-1];

			Debug.Log(temp.transform.localPosition);

			if(temp.GetComponent<PipeTouch2>().ChildObject[0].activeSelf)
				type = (int)temp.GetComponent<PipeTouch2>().ChildObject[0].GetComponent<CollisionType>().type;
			else if (temp.GetComponent<PipeTouch2>().ChildObject[1].activeSelf)
				type = (int)temp.GetComponent<PipeTouch2>().ChildObject[1].GetComponent<CollisionType>().type;

			//Debug.Log(type);
			if(type ==1)
			{
				if(temp.GetComponent<PipeTouch2>().type ==0)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x +80, temp.transform.localPosition.y - 400, 0);
				else if ((int)temp.GetComponent<PipeTouch2>().type == 4)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x , temp.transform.localPosition.y - 400, 0);
				else if ((int)temp.GetComponent<PipeTouch2>().type == 2)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x-80, temp.transform.localPosition.y - 340, 0);

				ob.SetActive(!ob.activeSelf);
			}
			else if (type == 2)
			{
				if ((int)temp.GetComponent<PipeTouch2>().type == 1)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x -80, temp.transform.localPosition.y -280, 0);
				else if ((int)temp.GetComponent<PipeTouch2>().type == 5)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x -80, temp.transform.localPosition.y -180, 0);
				else if ((int)temp.GetComponent<PipeTouch2>().type == 3)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x -180 , temp.transform.localPosition.y -280, 0);

				ob.SetActive(!ob.activeSelf);
			}
			else if (type == 3)
			{
				if ((int)temp.GetComponent<PipeTouch2>().type == 1)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x+190 , temp.transform.localPosition.y -280,0);
				else if ((int)temp.GetComponent<PipeTouch2>().type == 5)
					ob.transform.localPosition = new Vector3(temp.transform.localPosition.x+190, temp.transform.localPosition.y-180, 0);
				ob.SetActive(!ob.activeSelf);
			}

			ani.Play("water");

			Debug.Log(ob.transform.localPosition);
			if(ob.transform.localPosition.x >22 && ob.transform.localPosition.x < 463)
			{
				Debug.Log(11);
				if (ob.transform.localPosition.y > -610 && ob.transform.localPosition.y < -410)
				{
					Debug.Log(22);
					mountain.SetBool("fireOff", true);
				}
			}

			Debug.Log("victory");
		}
		
	
	}

	// 안 쓰이는 코드 무시해주세요 역순처리 코드입니다
	public void delete()
	{
		//그냥 파이프 리스트문
		if (Singletone.Instance.list.Count > 0 )
		{
			GameObject list = Singletone.Instance.list[Singletone.Instance.list.Count - 1];

			//파이프 이어진거 저장된 리스트 비교문
			if (Singletone.Instance.listPipe.Count > 0)
			{
			
				GameObject listPipe = Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1];
				Debug.Log(listPipe.transform.localPosition);

				if (listPipe == list)
				{
					int temp = Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1].GetComponent<PipeTouch2>().youType;
					//Debug.Log(temp);

					Debug.Log(Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1].transform.localPosition);
					Singletone.Instance.listPipe.RemoveAt(Singletone.Instance.listPipe.Count - 1);

					//리스트파이프 원소하나 삭제 시켰으니까 다시 체크
					
					if (Singletone.Instance.listPipe.Count > 0)
					{
						GameObject temp2 = Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1];
						Debug.Log((int)temp2.GetComponent<PipeTouch2>().ChildObject[0].GetComponent<CollisionType>().type);

						//if ((int)temp2.GetComponent<PipeTouch2>().ChildObject[0].GetComponent<CollisionType>().type == temp)
						//{
						//	temp2.GetComponent<PipeTouch2>().ChildObject[0].SetActive(true);
						//}
						//else if ((int)temp2.GetComponent<PipeTouch2>().ChildObject[0].GetComponent<CollisionType>().type != temp)
						//{
						//	temp2.GetComponent<PipeTouch2>().ChildObject[1].SetActive(true);
						//}
					}
					
					//Singletone.Instance.listPipe[Singletone.Instance.listPipe.Count - 1].GetComponent<PipeTouch2>().ChildObject[i].SetActive(true);
				}
			}

			Destroy(list.GetComponent<PipeTouch2>().ob);
			Destroy(list);

			Singletone.Instance.list.RemoveAt(Singletone.Instance.list.Count - 1);
		
			//Debug.Log("destroy list  " + Singletone.Instance.list.Count);
		}

	}
}






//if ((int)temp.GetComponent<PipeTouch2>().ChildObject[i].GetComponent<CollisionType>().type == temp.GetComponent<PipeTouch2>().typeCheck)
//	temp.GetComponent<PipeTouch2>().ChildObject[i].SetActive(false);
//else
//	temp.GetComponent<PipeTouch2>().ChildObject[i].SetActive(true);