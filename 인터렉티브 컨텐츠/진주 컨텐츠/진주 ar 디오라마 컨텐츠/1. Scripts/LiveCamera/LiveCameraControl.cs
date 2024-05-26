using RenderHeads.Media.AVProLiveCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveCameraControl : MonoBehaviour
{
    protected AVProLiveCameraDevice _device = null;
    protected AVProLiveCameraDeviceMode _mode = null;
    protected int _videoInput = -1;

    [Header("Device Selection")]
    // Device selection
    public SelectDeviceBy _deviceSelection = SelectDeviceBy.Default;                    // 디바이스 선택
    [Tooltip("SelectDevice is Name")]
    public string _desiredDeviceName;                                                   // 디바이스 이름으로 선택
    [Tooltip("SelectDevice is Index")]
    public int _desiredDeviceIndex = 0;                                                 // 디바이스 순서로 선택

    // Mode selection
    public SelectModeBy _modeSelection = SelectModeBy.Default;                          // 모드 선택
    public bool _desiredAnyResolution = true;
    [Tooltip("640*480, 800*600, 1280*720, 1920*1080, 2560*1440, 3840*2160")]
    public Vector2 _desiredResolutions = new Vector2(1920, 1080);                       // 해상도
    public int _desiredModeIndex = -1;                                                  // 인덱스 선택
    public bool _maintainAspectRatio = false;
    public float _desiredFrameRate = 0f;                                                // 프레임
    public bool _desiredFormatAny = true;                                               // 포맷
    public bool _desiredTransparencyFormat = false;                                     //투명도
    public AVProLiveCameraPlugin.VideoFrameFormat _desiredFormat = AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_HDYC;     // 포맷 형식

    // Video Input selection
    public SelectDeviceBy _videoInputSelection = SelectDeviceBy.Default;
    public AVProLiveCameraPlugin.VideoInput _desiredVideoInputs = AVProLiveCameraPlugin.VideoInput.Video_Serial_Digital;
    public int _desiredVideoInputIndex = 0;

    [Header("Device Start")]
    [SerializeField] bool _preferPreviewPin = false;
    [SerializeField] AVProLiveCameraDevice.ClockMode _clockMode = AVProLiveCameraDevice.ClockMode.None;
    public bool _deinterlace = false;
    public bool _playOnStart = true;

    [Header("Display")]
    public bool _allowTransparency = true;
    public bool _flipX;
    public bool _flipY;
    [SerializeField] YCbCrRange _yCbCrRange = YCbCrRange.Limited;

    [Header("Update")]
    public bool _updateHotSwap = false;
    public bool _updateFrameRates = false;
    public bool _updateSettings = false;

    #if UNITY_5_4_OR_NEWER || (UNITY_5 && !UNITY_5_0 && !UNITY_5_1)
        private System.IntPtr _renderFunc;
    #endif

    public AVProLiveCameraDevice Device
    {
        get { return _device; }
    }

    public Texture OutputTexture
    {
        get { if (_device != null) return _device.OutputTexture; return null; }
    }

    void Reset()
    {
        _videoInput = -1;
        _mode = null;
        _device = null;
        _flipX = _flipY = false;
        _allowTransparency = false;
        _yCbCrRange = YCbCrRange.Limited;

        _deviceSelection = SelectDeviceBy.Default;
        _modeSelection = SelectModeBy.Default;
        _videoInputSelection = SelectDeviceBy.Default;
        _desiredDeviceName = "Logitech BRIO";
        _desiredResolutions = new Vector2(3840, 2160);
        _desiredVideoInputs = AVProLiveCameraPlugin.VideoInput.Video_Serial_Digital;
        _desiredVideoInputIndex = 0;
        _maintainAspectRatio = false;
        _desiredTransparencyFormat = false;
        _desiredAnyResolution = true;
        _desiredFrameRate = 0f;
        _desiredFormatAny = true;
        _desiredFormat = AVProLiveCameraPlugin.VideoFrameFormat.MPEG;
        _desiredModeIndex = -1;
        _desiredDeviceIndex = 0;
    }

    // 디바이스 선택용
    public enum SelectDeviceBy
    {
        Default,
        Name,
        Index,
    }
    // 모드 선택용
    public enum SelectModeBy
    {
        Default,
        Resolution,
        Index,
    }

    private void Start()
    {
        if (_playOnStart)
        {
            Begin();
        }
    }

    public void Begin()
    {
        SelectDeviceAndMode();

        if (_device != null)
        {
            if (_renderRoutine != null)
            {
                StopCoroutine(_renderRoutine);
                _renderRoutine = null;
            }

            _device.Deinterlace = _deinterlace;
            _device.AllowTransparency = _allowTransparency;
            _device.YCbCrRange = _yCbCrRange;
            _device.FlipX = _flipX;
            _device.FlipY = _flipY;
            _device.Clock = _clockMode;
            _device.PreferPreviewPin = _preferPreviewPin;
            if (!_device.Start(_mode, _videoInput))
            {
                Debug.LogWarning("[AVPro Live Camera] Device failed to start.");
                _device.Close();
                _device = null;
                _mode = null;
                _videoInput = -1;
            }
            else
            {
                if (_renderRoutine == null && this.gameObject.activeInHierarchy)
                {
                    _renderRoutine = StartCoroutine(RenderRoutine());
                }
            }
        }
    }


    private YieldInstruction _wait = new WaitForEndOfFrame();
    private Coroutine _renderRoutine;
    private int _lastFrameUpdated;      // 마지막 프레임 업데이트 확인
    private System.Collections.IEnumerator RenderRoutine()
    {
        while (Application.isPlaying)
        {
            yield return null;

            if (this.enabled)
            {
                bool hasUpdatedThisFrame = Render();

                // NOTE: in editor, if the game view isn't visible then WaitForEndOfFrame will never complete
                yield return _wait;

                if (!hasUpdatedThisFrame)
                {
                    // Try again to get the frame
                    if (!Render())
                    {
                        //Debug.Log("frame dropped :(");
                    }
                }
                else
                {
                    // If nothing has updated, send another event
                    int lastFrameUpdated = AVProLiveCameraPlugin.GetLastFrameUploaded(_device.DeviceIndex);
                    if (_lastFrameUpdated == lastFrameUpdated)
                    {
                        int eventId = AVProLiveCameraPlugin.PluginID | (int)AVProLiveCameraPlugin.PluginEvent.UpdateOneTexture | _device.DeviceIndex;
#if UNITY_5_4_OR_NEWER || (UNITY_5 && !UNITY_5_0 && !UNITY_5_1)
                        GL.IssuePluginEvent(_renderFunc, eventId);
#else
							GL.IssuePluginEvent(eventId);
#endif
                    }
                }
            }
        }
        _renderRoutine = null;
    }

    private int _lastFrameCount;
    private bool Render()
    {
        bool result = false;
        if (_device != null)
        {
            // Prevent this function being executed again this frame if the camera frame has been updated this frame already
            if (_lastFrameCount != Time.frameCount)
            {
                if (_device != null)
                {
                    if (_device.Render())
                    {
                        _lastFrameCount = Time.frameCount;
                        result = true;
                    }
                    else
                    {
                        _lastFrameUpdated = AVProLiveCameraPlugin.GetLastFrameUploaded(_device.DeviceIndex);
                    }

                    {
                        int eventId = AVProLiveCameraPlugin.PluginID | (int)AVProLiveCameraPlugin.PluginEvent.UpdateOneTexture | _device.DeviceIndex;
#if UNITY_5_4_OR_NEWER || (UNITY_5 && !UNITY_5_0 && !UNITY_5_1)
                        GL.IssuePluginEvent(_renderFunc, eventId);
#else
							GL.IssuePluginEvent(eventId);
#endif
                    }
                }
            }
        }
        return result;
    }

    public void SelectDeviceAndMode()
    {
        _device = null;
        _mode = null;
        _videoInput = -1;

        _device = SelectDevice();
        if (_device != null)
        {
            _mode = SelectMode();
            _videoInput = SelectVideoInputIndex();
        }
        else
        {
            Debug.LogWarning("[AVProLiveCamera] Could not find the device.");
        }
    }

    // 디바이스 선택
    private AVProLiveCameraDevice SelectDevice()
    {
        AVProLiveCameraDevice result = null;

        switch (_deviceSelection)
        {
            default:
            case SelectDeviceBy.Default:
                result = LiveCameraManager.Instance.GetDevice(0);
                break;
            case SelectDeviceBy.Name:
                result = LiveCameraManager.Instance.GetDevice(_desiredDeviceName);
                break;
            case SelectDeviceBy.Index:
                if (_desiredDeviceIndex >= 0)
                {
                    result = LiveCameraManager.Instance.GetDevice(_desiredDeviceIndex);
                }
                break;
        }

        return result;
    }

    // 모드 선택
    private AVProLiveCameraDeviceMode SelectMode()
    {
        AVProLiveCameraDeviceMode result = null;

        switch (_modeSelection)
        {
            default:
            case SelectModeBy.Default:
                result = null;
                break;
            case SelectModeBy.Resolution:
                result = GetClosestMode(_device, _desiredAnyResolution, _desiredResolutions, _maintainAspectRatio, _desiredFrameRate, _desiredFormatAny, _desiredTransparencyFormat, _desiredFormat);
                if (result == null)
                {
                    Debug.LogWarning("[AVProLiveCamera] Could not find desired mode, using default mode.");
                }
                break;
            case SelectModeBy.Index:
                if (_desiredModeIndex >= 0)
                {
                    result = _device.GetMode(_desiredModeIndex);
                    if (result == null)
                    {
                        Debug.LogWarning("[AVProLiveCamera] Could not find desired mode, using default mode.");
                    }
                }
                break;
        }

        if (result != null)
        {
            if (_desiredFrameRate <= 0)
            {
                result.SelectHighestFrameRate();
            }
            else
            {
                result.SelectClosestFrameRate(_desiredFrameRate);
            }
        }

        return result;
    }

    // 비디오 인풋 인덱스
    private int SelectVideoInputIndex()
    {
        int result = -1;

        switch (_videoInputSelection)
        {
            default:
            case SelectDeviceBy.Default:
                result = -1;
                break;
            case SelectDeviceBy.Name:
                result = -1;
                //if (_desiredVideoInputs.Count > 0 && _device.NumVideoInputs > 0)
                //{
                //    foreach (AVProLiveCameraPlugin.VideoInput videoInput in _desiredVideoInputs)
                //    {
                //        for (int i = 0; i < _device.NumVideoInputs; i++)
                //        {
                //            if (videoInput.ToString().Replace("_", " ") == _device.GetVideoInputName(i))
                //            {
                //                result = i;
                //                break;
                //            }
                //        }
                //        if (result >= 0)
                //            break;
                //    }
                //}
                break;
            case SelectDeviceBy.Index:
                if (_desiredVideoInputIndex >= 0)
                {
                    result = _desiredVideoInputIndex;
                }
                break;
        }

        return result;
    }

    // 디바이스 모드 설정
    private static AVProLiveCameraDeviceMode GetClosestMode(AVProLiveCameraDevice device, bool anyResolution, Vector2 resolutions, bool maintainApectRatio, float frameRate, bool anyPixelFormat, bool transparentPixelFormat, AVProLiveCameraPlugin.VideoFrameFormat pixelFormat)
    {
        AVProLiveCameraDeviceMode result = null;
        if (anyResolution)
        {
            result = device.GetClosestMode(-1, -1, maintainApectRatio, frameRate, anyPixelFormat, transparentPixelFormat, pixelFormat);
        }
        else
        {
            result = device.GetClosestMode(Mathf.FloorToInt(resolutions.x), Mathf.FloorToInt(resolutions.y), maintainApectRatio, frameRate, anyPixelFormat, transparentPixelFormat, pixelFormat);
        }
        return result;
    }
}
