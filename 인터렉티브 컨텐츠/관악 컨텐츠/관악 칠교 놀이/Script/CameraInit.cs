using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProLiveCamera;
using System.IO;
using UnityEngine.UI;
using System.Xml;


public class CameraInit : MonoBehaviour
{
	public AVProLiveCamera _liveCamera;
	public AVProLiveCameraManager _liveCameraManager;
	AVProLiveCameraSettingBase settingBase;
	AVProLiveCameraSettingFloat settingFloat;

	public GameObject panel; 
	public GameObject camera;
	public Slider[] slider;
	public Text[] text;

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

	public static int x1;
	public static int x2;
	public static int y1;
	public static int y2;
	public static float breakTime;
	public static float RGB_r;
	public static float RGB_g;
	public static float RGB_b;
	
	

	private void Awake()
	{
		LoadXML();
	}

	// Start is called before the first frame update
	void Start()
    {
		_liveCamera._deviceSelection = AVProLiveCamera.SelectDeviceBy.Index;
		_liveCamera._desiredDeviceIndex = 0;
		_liveCamera.Begin();

		StartCoroutine(unload());
	}


	IEnumerator unload()
	{
		while(true)
		{
			Resources.UnloadUnusedAssets();
			System.GC.Collect();

			yield return new WaitForSeconds(10);
			//Debug.Log(3);
		}
	}

	private void Update()
	{


		if (Input.GetKeyDown(KeyCode.A))
		{
			panel.SetActive(!panel.activeSelf);
		}

		if (Input.GetKeyDown(KeyCode.P))
			camera.SetActive(!camera.activeSelf);

		//if(panel.activeSelf == true)
		//{

		//	//Debug.Log(_liveCamera.Device.NumSettings);
		//	for(int i =0; i < 12; i++)
		//	{

		//		//Debug.Log(settingFloat.MaxValue);

		//		if(i <5)
		//		{
		//			settingBase = _liveCamera.Device.GetVideoSettingByIndex(i);
		//			settingFloat = (AVProLiveCameraSettingFloat)settingBase;
		//			text[i].text = slider[i].value.ToString();
		//			settingFloat.CurrentValue = slider[i].value;
		//			settingFloat.IsAutomatic = false;
		//			settingBase.Update();
		//		}
		//		else if(i >5)
		//		{
		//			settingBase = _liveCamera.Device.GetVideoSettingByIndex(i);
		//			settingFloat = (AVProLiveCameraSettingFloat)settingBase;
		//			settingFloat = (AVProLiveCameraSettingFloat)settingBase;
		//			//Debug.Log(settingFloat.MaxValue);

		//			text[i - 1].text = slider[i - 1].value.ToString();
		//			settingFloat.CurrentValue = slider[i - 1].value;
		//			settingFloat.IsAutomatic = false;
		//			settingBase.Update();
		//		}

		//	}
		//}
	}

	private void LoadXML()
	{
		string path = Application.streamingAssetsPath + "/CameraSetting.xml";
		//Debug.Log("XMLDataPath : " + path);
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(path);

		// 하나씩 가져오기
		XmlNode rootNode = xmlDoc.SelectSingleNode("Data");
		XmlNode settingNode = rootNode.SelectSingleNode("Setting");

		//Brightness = float.Parse(settingNode.SelectSingleNode("Brightness").InnerText);
		//Contrast = float.Parse(settingNode.SelectSingleNode("Contrast").InnerText);
		//Saturation = float.Parse(settingNode.SelectSingleNode("Saturation").InnerText);
		//Sharpness = float.Parse(settingNode.SelectSingleNode("Sharpness").InnerText);
		//WhiteBalance = float.Parse(settingNode.SelectSingleNode("WhiteBalance").InnerText);
		//Gain = float.Parse(settingNode.SelectSingleNode("Gain").InnerText);
		//Pan = float.Parse(settingNode.SelectSingleNode("Pan").InnerText);
		//Tilt = float.Parse(settingNode.SelectSingleNode("Tilt").InnerText);
		//Zoom = float.Parse(settingNode.SelectSingleNode("Zoom").InnerText);
		//Exposure = float.Parse(settingNode.SelectSingleNode("Exposure").InnerText);
		//Focus = float.Parse(settingNode.SelectSingleNode("Focus").InnerText);

		//slider[0].value = Brightness;
		//slider[1].value = Contrast;
		//slider[2].value = Saturation;
		//slider[3].value = Sharpness;
		//slider[4].value = WhiteBalance;
		//slider[5].value = Gain;
		//slider[6].value = Pan;
		//slider[7].value = Tilt;
		//slider[8].value = Zoom;
		//slider[9].value = Exposure;
		//slider[10].value = Focus;

		path = Application.streamingAssetsPath + "/imageSetting.xml";
		xmlDoc.Load(path);

		rootNode = xmlDoc.SelectSingleNode("Data");

		x1 = int.Parse(rootNode.SelectSingleNode("x1").InnerText);
		x2 = int.Parse(rootNode.SelectSingleNode("x2").InnerText);
		y1 = int.Parse(rootNode.SelectSingleNode("y1").InnerText);
		y2 = int.Parse(rootNode.SelectSingleNode("y2").InnerText);
		breakTime = float.Parse(rootNode.SelectSingleNode("breakTime").InnerText);
		RGB_r = float.Parse(rootNode.SelectSingleNode("r").InnerText);
		RGB_g = float.Parse(rootNode.SelectSingleNode("g").InnerText);
		RGB_b = float.Parse(rootNode.SelectSingleNode("b").InnerText);

	}



}
