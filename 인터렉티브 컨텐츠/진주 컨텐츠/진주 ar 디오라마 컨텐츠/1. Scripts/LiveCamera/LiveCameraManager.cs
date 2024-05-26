using RenderHeads.Media.AVProLiveCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveCameraManager : MonoBehaviour
{
    private static LiveCameraManager _instance;
    public static LiveCameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (LiveCameraManager)GameObject.FindObjectOfType(typeof(LiveCameraManager));
                if (_instance == null)
                {
                    Debug.LogError("[AVProLiveCamera] AVProLiveCameraManager component required");
                    return null;
                }
                else
                {
                    if (!_instance._isInitialised)
                        _instance.Init();
                }
            }

            return _instance;
        }
    }
    private List<AVProLiveCameraDevice> _devices;

    public bool _isInitialised;
    public bool _supportInternalFormatConversion = true;

    protected bool Init()
    {
        try
        {
            // 내부 형식변환 체크
            if (AVProLiveCameraPlugin.Init(_supportInternalFormatConversion))
            {
                Debug.Log("[AVProLiveCamera] version " + AVProLiveCameraPlugin.GetPluginVersionString() + " initialised. " + SystemInfo.graphicsDeviceName);
            }
            else
            {
                Debug.LogError("[AVProLiveCamera] failed to initialise.");
                this.enabled = false;
                Deinit();
                return false;
            }
        }
        catch (System.DllNotFoundException e)
        {
            // DLL 파일 체크
            Debug.Log("[AVProLiveCamera] Unity couldn't find the DLL, did you move the 'Plugins' folder to the root of your project?");
            throw e;
        }

        GetConversionMethod();
        EnumDevices();

        // 초기화 완료
        _isInitialised = true;

        return _isInitialised;
    }
    public void Deinit()
    {
        ClearDevices();
        _instance = null;
        _isInitialised = false;

        AVProLiveCameraPlugin.Deinit();
    }
    private void ClearDevices()
    {
        if (_devices != null)
        {
            for (int i = 0; i < _devices.Count; i++)
            {
                _devices[i].Close();
                _devices[i].Dispose();
            }
            _devices.Clear();
            _devices = null;
        }
    }



    private void GetConversionMethod()
    {
        bool swapRedBlue = false;
        // Direct3D 11 지원 확인
        if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11"))
        {
            swapRedBlue = true;
        }

        // 스왑 레드블루
        Shader.DisableKeyword("SWAP_RED_BLUE_OFF");
        Shader.EnableKeyword("SWAP_RED_BLUE_ON");
        //if (swapRedBlue)
        //{
        //    Debug.Log("Swap");
        //    Shader.DisableKeyword("SWAP_RED_BLUE_OFF");
        //    Shader.EnableKeyword("SWAP_RED_BLUE_ON");
        //}
        //else
        //{
        //    Debug.Log("NotSwap");
        //    Shader.DisableKeyword("SWAP_RED_BLUE_ON");
        //    Shader.EnableKeyword("SWAP_RED_BLUE_OFF");
        //}

        Shader.DisableKeyword("AVPRO_GAMMACORRECTION");
        Shader.EnableKeyword("AVPRO_GAMMACORRECTION_OFF");
        if (QualitySettings.activeColorSpace == ColorSpace.Linear)
        {
            Shader.DisableKeyword("AVPRO_GAMMACORRECTION_OFF");
            Shader.EnableKeyword("AVPRO_GAMMACORRECTION");
        }
    }

    
    private void EnumDevices()
    {
        ClearDevices();
        _devices = new List<AVProLiveCameraDevice>(8);
        int numDevices = AVProLiveCameraPlugin.GetNumDevices();
        Debug.Log("DeviceNum" + numDevices);
        // 카메라 디바이스 추가
        for (int i = 0; i < numDevices; i++)
        {
            string deviceName;
            if (!AVProLiveCameraPlugin.GetDeviceName(i, out deviceName))
                continue;

            string deviceGUID;
            if (!AVProLiveCameraPlugin.GetDeviceGUID(i, out deviceGUID))
                continue;

            int numModes = AVProLiveCameraPlugin.GetNumModes(i);
            if (numModes > 0)
            {
                AVProLiveCameraDevice device = new AVProLiveCameraDevice(deviceName.ToString(), deviceGUID.ToString(), i);
                Debug.Log("Device Name : " + deviceName.ToString() + " , GUID : " + deviceGUID.ToString());
                _devices.Add(device);
            }
        }
    }
    void Start()
    {
        if (!_isInitialised)
        {
            _instance = this;
            Init();
        }
        //AddNewDevices(); 지금은 필요없음
    }

    // 새로운 디바이스 추가
    private void AddNewDevices()
    {
        bool isDeviceAdded = false;

        int numDevices = AVProLiveCameraPlugin.GetNumDevices();
        for (int i = 0; i < numDevices; i++)
        {
            string deviceGUID;
            if (!AVProLiveCameraPlugin.GetDeviceGUID(i, out deviceGUID))
                continue;

            // 디바이스 GUID가 없으면 새로 추가
            AVProLiveCameraDevice device = FindDeviceWithGUID(deviceGUID);
            if (device == null)
            {
                string deviceName;
                if (!AVProLiveCameraPlugin.GetDeviceName(i, out deviceName))
                    continue;

                int numModes = AVProLiveCameraPlugin.GetNumModes(i);
                if (numModes > 0)
                {
                    device = new AVProLiveCameraDevice(deviceName.ToString(), deviceGUID.ToString(), i);
                    _devices.Add(device);
                    isDeviceAdded = true;
                }
            }
        }

        if (isDeviceAdded)
        {
            this.SendMessage("NewDeviceAdded", null, SendMessageOptions.DontRequireReceiver);
        }
    }
    // GUID로 해당 디바이스 찾기
    private AVProLiveCameraDevice FindDeviceWithGUID(string guid)
    {
        AVProLiveCameraDevice result = null;

        foreach (AVProLiveCameraDevice device in _devices)
        {
            if (device.GUID == guid)
            {
                result = device;
                break;
            }
        }

        return result;
    }

    // 인덱스로 카메라 디바이스 가져오기
    public AVProLiveCameraDevice GetDevice(int index)
    {
        AVProLiveCameraDevice result = null;

        if (index >= 0 && index < _devices.Count)
            result = _devices[index];

        return result;
    }

    // 이름으로 카메라 디바이스 가져오기
    public AVProLiveCameraDevice GetDevice(string name)
    {
        AVProLiveCameraDevice result = null;
        int numDevices = NumDevices;
        for (int i = 0; i < numDevices; i++)
        {
            AVProLiveCameraDevice device = GetDevice(i);
            if (device.Name == name)
            {
                result = device;
                break;
            }
        }
        return result;
    }
    
    public int NumDevices
    {
        get { if (_devices != null) return _devices.Count; return 0; }
    }

    void OnDestroy()
    {
        Deinit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
