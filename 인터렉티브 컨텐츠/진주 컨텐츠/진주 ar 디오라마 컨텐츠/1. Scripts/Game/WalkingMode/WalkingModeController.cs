using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkingModeController : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] FlowManager _FlowManager;
    [SerializeField] CameraSerialManager _CameraSerialManager;

    [Header("Intro")]
    [SerializeField] GameObject ForeGroundIntro;

    [Header("Interface")]
    [SerializeField] GameObject WalkingModeInterface;
    [SerializeField] GameObject TargetJoystick;

    [Header("Experience")]
    [SerializeField] Image[] TargetImage;
    [SerializeField] Sprite[] NonActiveSprite;
    [SerializeField] Sprite[] ActiveSprite;
    [SerializeField] GameObject[] CompliteObject;
    
    [Header("Communication")]
    [SerializeField] GameObject[] Conversation;

    [Header("InfoGraphic")]
    [SerializeField] GameObject WalkingModeInfoGraphic;
    [SerializeField] GameObject[] InfoGraphicConversation;
    [SerializeField] GameObject[] RenderObject;
    Coroutine InfoShowCoroutine;
    
    [Header("InfoGraphicTimer")]
    [SerializeField] Text TimeCountText;
    [SerializeField] Slider TimerSlider;
    public float InfoPanelShowTime = 15;

    [Header("CameraJoystick")]
    public float[] StoryStepAngles;      // XML 받아오기
    public float CameraAngle = 0;       // 카메라 현재 각도
    public int CameraMotorAngle = 0;       // 카메라 모터 각도
    public bool IsMovable = false;     // 카메라 움직임 제어
    public int StoryCameraIndex = 1;     // 카메라 움직임 제어
    public float ErrorRange = 3;       // 카메라 인식 각도 오차 범위
    
    [Header("Shared")]
    public int StoryIndex = 0;      // 산책모드 스토리 진행률
    
    private void OnEnable()
    {
        WalkingModeInitialize();
    }

    // 초기화
    public void WalkingModeInitialize()
    {
        ForeGroundIntro.SetActive(true);
        WalkingModeInterface.SetActive(false);
        WalkingModeInfoGraphic.SetActive(false);
        TargetJoystick.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            Conversation[i].SetActive(false);
            InfoGraphicConversation[i].SetActive(false);
            RenderObject[i].SetActive(false);
            TargetImage[i].sprite = NonActiveSprite[i];
            CompliteObject[i].SetActive(false);
        }
        TargetImage[0].sprite = ActiveSprite[0];

        CameraAngle = 0;
        CameraMotorAngle = 0;
        StoryIndex = 0;
        StoryCameraIndex = 1;
        _CameraSerialManager.MoveCameraPositionToCenter();
    }

    // 인트로 넘김 버튼
    public void IntroNextButtonClick()
    {
        ForeGroundIntro.SetActive(false);
        WalkingModeInterface.SetActive(true);
        Conversation[StoryIndex].SetActive(true);
    }
    // 대화 끝
    public void CompliteConversation()
    {
        TargetJoystick.SetActive(true);
        IsMovable = true;
    }

    // 카메라 이동
    public void MoveCamera(string direction)
    {
        if(!IsMovable)
        {
            return;
        }
        if (direction.Equals("L"))
        {
            CameraAngle -= 0.05f;
            if (CameraAngle < -60)
            {
                CameraAngle = -60;
            }

        }
        else if (direction.Equals("R"))
        {
            CameraAngle += 0.05f;
            if (CameraAngle > 60)
            {
                CameraAngle = 60;
            }
        }
        if (CameraMotorAngle != (int)CameraAngle)
        {
            CameraMotorAngle = (int)CameraAngle;
            _CameraSerialManager.MoveCameraPositionToAngle(CameraMotorAngle);
        }

        if (StoryStepAngles[StoryIndex] + ErrorRange >= CameraAngle && CameraAngle >= StoryStepAngles[StoryIndex] - ErrorRange)
        {
            CameraAngle = StoryStepAngles[StoryIndex];
            CameraMotorAngle = (int)CameraAngle;
            _CameraSerialManager.MoveCameraPositionToAngle(CameraMotorAngle);
            //_CameraSerialManager.MoveCameraPositionToAngle(0);
            InfoGraphicPanelOpen();
        }
    }

    public void LeftMoveCamera()
    {
        StoryCameraIndex--;
        if(StoryCameraIndex < 0)
        {
            StoryCameraIndex = 2;
        }
        CameraAngle = StoryStepAngles[StoryCameraIndex];
        CameraMotorAngle = (int)CameraAngle;
        _CameraSerialManager.MoveCameraPositionToAngle(CameraMotorAngle);
        if(StoryIndex == StoryCameraIndex)
        {
            InfoGraphicPanelOpen();
        }
    }

    public void RightMoveCamera()
    {
        StoryCameraIndex++;
        if (StoryCameraIndex > 2)
        {
            StoryCameraIndex = 0;
        }
        CameraAngle = StoryStepAngles[StoryCameraIndex];
        CameraMotorAngle = (int)CameraAngle;
        _CameraSerialManager.MoveCameraPositionToAngle(CameraMotorAngle);
        if (StoryIndex == StoryCameraIndex)
        {
            InfoGraphicPanelOpen();
        }
    }

    // 인포 그래픽
    private void InfoGraphicPanelOpen()
    {
        IsMovable = false;
        WalkingModeInterface.SetActive(false);
        TargetJoystick.SetActive(false);
        WalkingModeInfoGraphic.SetActive(true);
        RenderObject[StoryIndex].SetActive(true);
        InfoGraphicConversation[StoryIndex].SetActive(true);
        InfoShowCoroutine = StartCoroutine(InfoShowTime());
    }

    // 인포그래픽 Show Timer
    IEnumerator InfoShowTime()
    {
        float t = InfoPanelShowTime;
        while(t > 0)
        {
            TimeCountText.text = t.ToString();
            TimerSlider.value = t;
            yield return new WaitForSeconds(1f);
            t--;
        }
        TimeCountText.text = t.ToString();
        TimerSlider.value = t;
        InfoGraphicPanelClosed();
    }

    // 인포그래픽창 꺼짐
    public void InfoGraphicPanelClosed()
    {
        if (WalkingModeInfoGraphic.activeSelf)
        {
            if(StoryIndex < 2)
            {
                if (InfoShowCoroutine != null) StopCoroutine(InfoShowCoroutine);
                WalkingModeInfoGraphic.SetActive(false);
                RenderObject[StoryIndex].SetActive(false);
                InfoGraphicConversation[StoryIndex].SetActive(false);
                TargetImage[StoryIndex].sprite = NonActiveSprite[StoryIndex];
                CompliteObject[StoryIndex].SetActive(true);
                StoryIndex++;
                WalkingModeInterface.SetActive(true);
                Conversation[StoryIndex].SetActive(true);
                TargetImage[StoryIndex].sprite = ActiveSprite[StoryIndex];
            }
            else
            {
                _FlowManager.PlayMissionMode();
            }
        }
    }
	
}
