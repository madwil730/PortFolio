using RenderHeads.Media.AVProLiveCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVProLiveCameraController : MonoBehaviour
{
	public AVProLiveCamera _AVProLiveCamera;
	AVProLiveCameraDevice _AVProLiveCameraDevice;
	//AVProLiveCameraSettingBase _AVProLiveCameraSettingBase;
	bool _isInitialized = false;

	private void Awake()
	{
	}
	// Start is called before the first frame update
	void Start()
	{
		//SetCameraSetting("Brightness", 150);
		Init();
	}

	public void Init()
	{
		// -------------------------수정------------------------------
		_AVProLiveCamera.Begin();
		_AVProLiveCameraDevice = _AVProLiveCamera.Device;
		//Debug.Log(_AVProLiveCameraDevice);
		_isInitialized = true;
	}
	// Update is called once per frame
	void Update()
	{

	}

	public void SetCameraSetting(string name, float value)
	{
		if (_AVProLiveCameraDevice == null)
		{
			_AVProLiveCamera.Begin();
			_AVProLiveCameraDevice = _AVProLiveCamera.Device;
		}
		if (_AVProLiveCameraDevice == null)
		{
			return;
		}

		//Debug.Log(_AVProLiveCameraDevice);
		int settingNum = _AVProLiveCameraDevice.NumSettings;
		//Debug.Log("셋팅 가능 개수 " + settingNum);
		for (int i = 0; i < settingNum; i++)
		{
			AVProLiveCameraSettingBase settingBase = null;
			settingBase = _AVProLiveCameraDevice.GetVideoSettingByIndex(i);
			if (settingBase == null)
			{
				//Debug.Log("EndSettingNum");
				return;
			}
			if (settingBase.Name.Equals(name))
			{
				if (settingBase.DataTypeValue == AVProLiveCameraSettingBase.DataType.Float)
				{
					AVProLiveCameraSettingFloat settingFloat = (AVProLiveCameraSettingFloat)settingBase;
					settingFloat.CurrentValue = value;
					settingFloat.IsAutomatic = false;
					//Debug.Log("PreValue : " + name + " - " + settingFloat.CurrentValue);
					//Debug.Log(string.Format("  setting {0}: {1}({2}) value:{3} default:{4} range:{5}-{6} canAuto:{7} isAuto:{8}", i, settingBase.Name, settingBase.PropertyIndex, settingFloat.CurrentValue, settingFloat.DefaultValue, settingFloat.MinValue, settingFloat.MaxValue, settingBase.CanAutomatic, settingBase.IsAutomatic));
				}
			}
			settingBase.Update();
		}
	}
}
