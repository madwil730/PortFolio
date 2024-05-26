using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCSendTimingControl : MonoBehaviour
{
    [SerializeField] OSCManager _OSCManager;

    public enum OSCTarget { Init, BG, WalkingBG, MissionBG, Line1, Line2, Line3, LineOff, Point1, Point2, Point3, Point4, PointOff, Success, Failed, BGOff,EffectOff }
    public OSCTarget _OSCTarget;

    private void OnEnable()
    {
        CheckOSC();
    }

    private void CheckOSC()
    {
        switch (_OSCTarget)
        {
            case OSCTarget.Init:
                _OSCManager.FloorBackgroundLoop();
                _OSCManager.WallBackgroundLoop();
                _OSCManager.ModeBackground(9);
                _OSCManager.Line(9);
                _OSCManager.Pointing(9);
                break;
            case OSCTarget.BG:
                _OSCManager.FloorBackgroundLoop();
                _OSCManager.WallBackgroundLoop();
                break;
            case OSCTarget.WalkingBG:
                _OSCManager.ModeBackground(0);
                break;
            case OSCTarget.MissionBG:
                _OSCManager.ModeBackground(1);
                break;
            case OSCTarget.Line1:
                _OSCManager.Line(0);
                break;
            case OSCTarget.Line2:
                _OSCManager.Line(1);
                break;
            case OSCTarget.Line3:
                _OSCManager.Line(2);
                break;
            case OSCTarget.LineOff:
                _OSCManager.Line(9);
                break;
            case OSCTarget.Point1:
                _OSCManager.Pointing(0);
                break;
            case OSCTarget.Point2:
                _OSCManager.Pointing(1);
                break;
            case OSCTarget.Point3:
                _OSCManager.Pointing(2);
                break;
            case OSCTarget.Point4:
                _OSCManager.Pointing(3);
                break;
            case OSCTarget.PointOff:
                _OSCManager.Pointing(9);
                break;
            case OSCTarget.Success:
                _OSCManager.Judgment(true);
                break;
            case OSCTarget.Failed:
                _OSCManager.Judgment(false);
                break;

			case OSCTarget.BGOff:
				_OSCManager.ModeBackground(9);
				break;

			case OSCTarget.EffectOff:
				_OSCManager.JudgmentOff();
				break;
		}
    }
}
