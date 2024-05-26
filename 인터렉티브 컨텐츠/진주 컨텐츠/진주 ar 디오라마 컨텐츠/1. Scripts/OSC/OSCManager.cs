using extOSC;
using extOSC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    OSCTransmitter _OSCTransmitter;
    public enum ComputerIndex { PC1, PC2, PC3, PC4 }
    public ComputerIndex _ComputerIndex;

    int ModeBackgroundLayerIndex;   // 산책, 미션 모드 인덱스
    int LineLayerIndex;             // 산책모드 라인 인덱스
    int PointLayerIndex;            // 포인팅 인덱스
    int JudgmentLayerIndex;         // 승패 판정 인덱스
    int SuccessFullLayerIndex;      // 승리 전체화면
    int FailedFullLayerIndex;       // 패배 전체화면

    private void Awake()
    {
        _OSCTransmitter = GetComponent<OSCTransmitter>();
        //Debug.Log("aaaaaaaaaaa");
        ModeBackgroundLayerIndex = 3;   // 산책, 미션 모드 인덱스
        LineLayerIndex = 7;             // 산책모드 라인 인덱스
        PointLayerIndex = 11;            // 포인팅 인덱스
        JudgmentLayerIndex = 15;         // 승패 판정 인덱스
        SuccessFullLayerIndex = 19;      // 승리 전체화면
        FailedFullLayerIndex = 20;       // 패배 전체화면
        switch (_ComputerIndex)
        {
            case ComputerIndex.PC1:
                ModeBackgroundLayerIndex += 0;
                LineLayerIndex += 0;
                PointLayerIndex += 0;
                JudgmentLayerIndex += 0;
                break;
            case ComputerIndex.PC2:
                ModeBackgroundLayerIndex += 1;
                LineLayerIndex += 1;
                PointLayerIndex += 1;
                JudgmentLayerIndex += 1;
                break;
            case ComputerIndex.PC3:
                ModeBackgroundLayerIndex += 2;
                LineLayerIndex += 2;
                PointLayerIndex += 2;
                JudgmentLayerIndex += 2;
                break;
            case ComputerIndex.PC4:
                ModeBackgroundLayerIndex += 3;
                LineLayerIndex += 3;
                PointLayerIndex += 3;
                JudgmentLayerIndex += 3;
                break;
        }
    }

    int lineTest = 0;
    int pointTest = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FloorBackgroundLoop();
            WallBackgroundLoop();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ModeBackground(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            lineTest++;
            if (lineTest > 1)
            {
                lineTest = 0;
            }
            Line(lineTest);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pointTest++;
            if (pointTest > 3)
            {
                pointTest = 0;
            }
            Pointing(pointTest);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ModeBackground(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Judgment(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Judgment(false);
        }
    }

    public void FloorBackgroundLoop()
    {
        MakeOSCAddress(1, 0);
    }

    public void WallBackgroundLoop()
    {
        MakeOSCAddress(2, 0);
    }

    public void ModeBackground(int clip)
    {
        MakeOSCAddress(ModeBackgroundLayerIndex, clip);
        //MakeOSCAddress(ModeBackgroundLayerIndex, 0);   // 산책모드 배경
        //MakeOSCAddress(ModeBackgroundLayerIndex, 1);   // 미션모드 배경
        //MakeOSCAddress(ModeBackgroundLayerIndex, 2);   // 끄기
    }

    public void Line(int storyindex)
    {
        MakeOSCAddress(LineLayerIndex, storyindex);
    }
    public void Pointing(int storyindex)
    {
        MakeOSCAddress(PointLayerIndex, storyindex);
    }
    public void Judgment(bool win)
    {
        if(win)
        {
            MakeOSCAddress(JudgmentLayerIndex, 0);   // 승리
            MakeOSCAddress(SuccessFullLayerIndex, 0);   // 승리
        }
        else
        {
            MakeOSCAddress(JudgmentLayerIndex, 1);   // 패배
            MakeOSCAddress(FailedFullLayerIndex, 0);   // 패배
        }
    }

	public void JudgmentOff()
	{
		MakeOSCAddress(JudgmentLayerIndex, 9);
		MakeOSCAddress(FailedFullLayerIndex, 9);
		MakeOSCAddress(SuccessFullLayerIndex, 9);
	}
    private void MakeOSCAddress(int layer, int clipNum)
    {
        string str = "/composition/layers/" + layer + "/connectspecificclip";
        SendOSC(str, clipNum);
    }

    public void SendOSC(string addr, bool value)
    {
        _OSCTransmitter.SendMessage(addr, value);
    }

    public void SendOSC(string addr, int value)
    {
        //_OSCTransmitter.Send(addr, OSCValue.Int(value));
        //_OSCTransmitter.SendMessage(addr, OSCValue.Int(value));
       // Debug.Log("OSC Control Address : " + addr + ", Value : " + value);
        OSCMessage _OSCMessage = new OSCMessage(addr, OSCValue.Int(value));
        if(_OSCTransmitter == null)
            _OSCTransmitter = GetComponent<OSCTransmitter>();
        _OSCTransmitter.Send(_OSCMessage);
    }
}
