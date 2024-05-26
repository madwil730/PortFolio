using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionModeController : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] FlowManager _FlowManager;

    [Header("Objects")]
    [SerializeField] GameObject Intro;
    [SerializeField] GameObject Mission;

    private void OnEnable()
    {
        InitMIssionMode();
    }

    private void InitMIssionMode()
    {
        Intro.SetActive(true);
    }

    // 인트로 대사 끝날시
    public void StartMissionMode()
    {
        Intro.SetActive(false);
        Mission.SetActive(true);
    }

    public void InitButtonClick()
    {
        _FlowManager.GameInitialize();
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
