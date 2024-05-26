using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionIntroController : MonoBehaviour
{
    [SerializeField] MissionModeController _MissionModeController;

    [SerializeField] MissionConversationController _MissionConversationController;

    [SerializeField] Animator HobotEyesAnimator;

    [Header("Buttons")]
    [SerializeField] GameObject MissionModeButton;
    [SerializeField] GameObject InitButton;

    [Header("Timer")]
    [SerializeField] GameObject CloseTimers;
    [SerializeField] Slider _TimeSlider;
    [SerializeField] Text _TimeText;
    int InitTime = 15;
    

    private void OnEnable()
    {
        InitMissionIntro();
    }

    private void InitMissionIntro()
    {
        MissionModeButton.SetActive(true);
        InitButton.SetActive(true);
        _MissionConversationController.gameObject.SetActive(true);
        CloseTimers.SetActive(true);
        _TimeSlider.value = InitTime;
        _TimeText.text = InitTime.ToString();
        StartCoroutine(InitTimeSliderCoroutine());
    }

    // 타이머
    IEnumerator InitTimeSliderCoroutine()
    {
        int t = InitTime;
        while(t > 0 )
        {
            yield return new WaitForSeconds(1f);
            t--;
            _TimeSlider.value = t;
            _TimeText.text = t.ToString();
        }
        if(CloseTimers.activeSelf) _MissionModeController.InitButtonClick();
    }

    // 게임모드 버튼 클릭
    public void MissionStartButtonClick()
    {
        HobotEyesAnimator.SetTrigger("GameStart");
        CloseTimers.SetActive(false);
        MissionModeButton.SetActive(false);
        InitButton.SetActive(false);
        _MissionConversationController.NextChat();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
