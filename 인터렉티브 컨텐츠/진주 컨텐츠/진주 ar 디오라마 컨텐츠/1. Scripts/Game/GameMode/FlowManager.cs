using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using RenderHeads.Media.AVProVideo.Demos;

public class FlowManager : MonoBehaviour
{
	[SerializeField] CameraSerialManager _CameraSerialManager;
	[SerializeField] XMLManager _XMLManager;

	[Header("Objects")]
	[SerializeField] GameObject IdleObject;
	[SerializeField] GameObject WalkingMode;
	[SerializeField] GameObject MissionMode;

	public int maxInitTime = 300;
	public int initTime;
	public static bool check; 



	private void Awake()
	{
		Screen.SetResolution(1920, 1080, true);
		Input.multiTouchEnabled = false;
	}

	void Start()
	{
		GameInitialize();
		StartCoroutine(InitControl());
	}

	IEnumerator InitControl()
	{
		initTime = maxInitTime;
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (!IdleObject.activeSelf) initTime--;
			else initTime = maxInitTime;

			if (initTime < 0)
			{
				initTime = maxInitTime;
				GameInitialize();
			}
		}
	}

	// 초기화면, 한번만 실행되는거 아님?
	public void GameInitialize()
	{
		TransitionFadeInOut.Instance.StartFadeInOut(IdleObject, WalkingMode, MissionMode);
		_CameraSerialManager.SerialWrite("C");
		_XMLManager.CameraSettingValueApply();
		check = true;
		Debug.Log("GameInitialize");
		//IdleObject.SetActive(true);
		//WalkingMode.SetActive(false);
		//MissionMode.SetActive(false);
	}

	// 산책모드 시작
	public void PlayWalkingMode()
	{
		//IdleObject.SetActive(false);
		//WalkingMode.SetActive(true);
		//check = false;
		if (IdleObject.activeSelf)
		TransitionFadeInOut.Instance.StartFadeInOut(WalkingMode, IdleObject);
	}

	// 미션모드 시작
	public void PlayMissionMode()
	{
		//WalkingMode.SetActive(false);
		//MissionMode.SetActive(true);
		TransitionFadeInOut.Instance.StartFadeInOut(MissionMode, WalkingMode);
		_CameraSerialManager.SerialWrite("C");
	}



	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			initTime = maxInitTime;
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			if (IdleObject.activeSelf)
			{
				PlayWalkingMode();
				//Debug.Log("??");
			}
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			if (IdleObject.activeSelf)
			{
				IdleObject.SetActive(false);
				PlayMissionMode();
				//Debug.Log("??");
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameInitialize();
		}
	}
}
