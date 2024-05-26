using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class XMLDataManager : MonoBehaviour
{
    public string xmlFileName = "CameraSetting.xml";

    [Header("Controller")]
    [SerializeField] AVProLiveCameraController _AVProLiveCameraController;

    [Header("SettingPanel")]
    [SerializeField] GameObject AVProSettingPanel;
    [SerializeField] Slider slider_Brightness;
    [SerializeField] Slider slider_Contrast;
    [SerializeField] Slider slider_Saturation;
    [SerializeField] Slider slider_Sharpness;
    [SerializeField] Slider slider_WhiteBalance;
    [SerializeField] Slider slider_Gain;
    [SerializeField] Slider slider_Pan;
    [SerializeField] Slider slider_Tilt;
    [SerializeField] Slider slider_Zoom;
    [SerializeField] Slider slider_Exposure;
    [SerializeField] Slider slider_Focus;
    private float Brightness;
    private float Contrast;
    private float Saturation;
    private float Sharpness;
    private float WhiteBalance;
    private float Gain;
    private float Pan;
    private float Tilt;
    private float Zoom;
    private float Exposure;
    private float Focus;


    // Start is called before the first frame update
    void Start()
    {
        LoadXML(xmlFileName);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            AVProSettingPanel.SetActive(!AVProSettingPanel.activeSelf);
            if (AVProSettingPanel.activeSelf)
            {
                SetSliderValue();
            }
        }
    }

    private void SetSliderValue()
    {
        slider_Brightness.value = Brightness;
        slider_Contrast.value = Contrast;
        slider_Saturation.value = Saturation;
        slider_Sharpness.value = Sharpness;
        slider_WhiteBalance.value = WhiteBalance;
        slider_Gain.value = Gain;
        slider_Pan.value = Pan;
        slider_Tilt.value = Tilt;
        slider_Zoom.value = Zoom;
        slider_Exposure.value = Exposure;
        slider_Focus.value = Focus;
    }

    public void SetDefaultValue()
    {
        slider_Brightness.value = 128;
        slider_Contrast.value = 128;
        slider_Saturation.value = 128;
        slider_Sharpness.value = 128;
        slider_WhiteBalance.value = 4000;
        slider_Gain.value = 0;
        slider_Pan.value = 0;
        slider_Tilt.value = 0;
        slider_Zoom.value = 100;
        slider_Exposure.value = -5;
        slider_Focus.value = 50;
    }

    private void LoadXML(string _fileName)
    {
        string path = Application.streamingAssetsPath + "/" + xmlFileName;
        //Debug.Log("XMLDataPath : " + path);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);

        // 하나씩 가져오기
        XmlNode rootNode = xmlDoc.SelectSingleNode("Data");


        XmlNode settingNode = rootNode.SelectSingleNode("Setting");
        //Debug.Log(settingNode.SelectSingleNode("Brightness").Name + " : " + settingNode.SelectSingleNode("Brightness").InnerText);
        Brightness = float.Parse(settingNode.SelectSingleNode("Brightness").InnerText);
        Contrast = float.Parse(settingNode.SelectSingleNode("Contrast").InnerText);
        Saturation = float.Parse(settingNode.SelectSingleNode("Saturation").InnerText);
        Sharpness = float.Parse(settingNode.SelectSingleNode("Sharpness").InnerText);
        WhiteBalance = float.Parse(settingNode.SelectSingleNode("WhiteBalance").InnerText);
        Gain = float.Parse(settingNode.SelectSingleNode("Gain").InnerText);
        Pan = float.Parse(settingNode.SelectSingleNode("Pan").InnerText);
        Tilt = float.Parse(settingNode.SelectSingleNode("Tilt").InnerText);
        Zoom = float.Parse(settingNode.SelectSingleNode("Zoom").InnerText);
        Exposure = float.Parse(settingNode.SelectSingleNode("Exposure").InnerText);
        Focus = float.Parse(settingNode.SelectSingleNode("Focus").InnerText);

        _AVProLiveCameraController.SetCameraSetting("Brightness", Brightness);
        _AVProLiveCameraController.SetCameraSetting("Contrast", Contrast);
        _AVProLiveCameraController.SetCameraSetting("Saturation", Saturation);
        _AVProLiveCameraController.SetCameraSetting("Sharpness", Sharpness);
        _AVProLiveCameraController.SetCameraSetting("WhiteBalance", WhiteBalance);
        _AVProLiveCameraController.SetCameraSetting("Gain", Gain);
        _AVProLiveCameraController.SetCameraSetting("Pan", Pan);
        _AVProLiveCameraController.SetCameraSetting("Tilt", Tilt);
        _AVProLiveCameraController.SetCameraSetting("Zoom", Zoom);
        _AVProLiveCameraController.SetCameraSetting("Exposure", Exposure);
        _AVProLiveCameraController.SetCameraSetting("Focus", Focus);

        if (AVProSettingPanel.activeSelf)
        {
            slider_Brightness.value = Brightness;
            slider_Contrast.value = Contrast;
            slider_Saturation.value = Saturation;
            slider_Sharpness.value = Sharpness;
            slider_WhiteBalance.value = WhiteBalance;
            slider_Gain.value = Gain;
            slider_Pan.value = Pan;
            slider_Tilt.value = Tilt;
            slider_Zoom.value = Zoom;
            slider_Exposure.value = Exposure;
            slider_Focus.value = Focus;
        }
        Debug.Log("DataLoad");
    }

    public void SetBrightness()
    {
        Brightness = slider_Brightness.value;
        _AVProLiveCameraController.SetCameraSetting("Brightness", Brightness);
    }
    public void SetContrast()
    {
        Contrast = slider_Contrast.value;
        _AVProLiveCameraController.SetCameraSetting("Contrast", Contrast);
    }
    public void SetSaturation()
    {
        Saturation = slider_Saturation.value;
        _AVProLiveCameraController.SetCameraSetting("Saturation", Saturation);
    }
    public void SetSharpness()
    {
        Sharpness = slider_Sharpness.value;
        _AVProLiveCameraController.SetCameraSetting("Sharpness", Sharpness);
    }
    public void SetWhiteBalance()
    {
        WhiteBalance = slider_WhiteBalance.value;
        _AVProLiveCameraController.SetCameraSetting("White Balance", WhiteBalance);
    }
    public void SetGain()
    {
        Gain = slider_Gain.value;
        _AVProLiveCameraController.SetCameraSetting("Gain", Gain);
    }
    public void SetPan()
    {
        Pan = slider_Pan.value;
        _AVProLiveCameraController.SetCameraSetting("Pan", Pan);
    }
    public void SetTilt()
    {
        Tilt = slider_Tilt.value;
        _AVProLiveCameraController.SetCameraSetting("Tilt", Tilt);
    }
    public void SetZoom()
    {
        Zoom = slider_Zoom.value;
        _AVProLiveCameraController.SetCameraSetting("Zoom", Zoom);
    }
    public void SetExposure()
    {
        Exposure = slider_Exposure.value;
        _AVProLiveCameraController.SetCameraSetting("Exposure", Exposure);
    }
    public void SetFocus()
    {
        Focus = slider_Focus.value;
        _AVProLiveCameraController.SetCameraSetting("Focus", Focus);
    }

    public void SaveCameraSetting()
    {
        if (AVProSettingPanel.activeSelf)
        {
            string path = Application.streamingAssetsPath + "/" + xmlFileName;
            Debug.Log("XMLDataPath : " + path);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            // 하나씩 가져오기
            XmlNode rootNode = xmlDoc.SelectSingleNode("Data");


            XmlNode settingNode = rootNode.SelectSingleNode("Setting");
            //Debug.Log(settingNode.SelectSingleNode("Brightness").Name + " : " + settingNode.SelectSingleNode("Brightness").InnerText);
            settingNode.SelectSingleNode("Brightness").InnerText = slider_Brightness.value.ToString();
            //Debug.Log(settingNode.SelectSingleNode("Brightness").Name + " : " + settingNode.SelectSingleNode("Brightness").InnerText);
            settingNode.SelectSingleNode("Contrast").InnerText = slider_Contrast.value.ToString();
            settingNode.SelectSingleNode("Saturation").InnerText = slider_Saturation.value.ToString();
            settingNode.SelectSingleNode("Sharpness").InnerText = slider_Sharpness.value.ToString();
            settingNode.SelectSingleNode("WhiteBalance").InnerText = slider_WhiteBalance.value.ToString();
            settingNode.SelectSingleNode("Gain").InnerText = slider_Gain.value.ToString();
            settingNode.SelectSingleNode("Pan").InnerText = slider_Pan.value.ToString();
            settingNode.SelectSingleNode("Tilt").InnerText = slider_Tilt.value.ToString();
            settingNode.SelectSingleNode("Zoom").InnerText = slider_Zoom.value.ToString();
            settingNode.SelectSingleNode("Exposure").InnerText = slider_Exposure.value.ToString();
            settingNode.SelectSingleNode("Focus").InnerText = slider_Focus.value.ToString();

            xmlDoc.Save(path);
            Debug.Log("Data Save");
        }
    }
}
