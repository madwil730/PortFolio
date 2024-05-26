using extOSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] RFID_SerialManager _RFID_SerialManager;
    [SerializeField] UDPReceiveManager _UDPReceiveManager;
    [SerializeField] UDPSendManager _UDPSendManager;
    [SerializeField] CameraSerialManager _CameraSerialManager;
    [SerializeField] WalkingModeController _WalkingModeController;
    [SerializeField] OSCTransmitter _OSCTransmitter;

    [Header("XML")]
    [SerializeField] string XmlFileName;
    
    private void Awake()
    {
        DataInitializing();
    }

    private void DataInitializing()
    {
        string path = Application.streamingAssetsPath + "/" + XmlFileName;
        //Debug.Log("XMLDataPath : " + path);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);

        // 하나씩 가져오기
        XmlNode rootNode = xmlDoc.SelectSingleNode("Data");


        SerialSetting(rootNode.SelectSingleNode("SerialPort"));
        CameraSerialSetting(rootNode.SelectSingleNode("CameraSerial"));
        UDPSenderSetting(rootNode.SelectSingleNode("UDPSender"));
        UDPReceiverSetting(rootNode.SelectSingleNode("UDPReceiver"));
        WalkingModeStoryAngle(rootNode.SelectSingleNode("StoryAngle"));
        OSCTransmitterSetting(rootNode.SelectSingleNode("MainOSC"));
    }

    private void OSCTransmitterSetting(XmlNode targetNode)
    {
        _OSCTransmitter.RemoteHost = targetNode.SelectSingleNode("OSCIP").InnerText;
        //Debug.Log(targetNode.SelectSingleNode("Baudrate").InnerText);
        _OSCTransmitter.RemotePort = int.Parse(targetNode.SelectSingleNode("OSCPort").InnerText);
    }

    // Serial Data Setting
    public void SerialSetting(XmlNode targetNode)
    {
        string comPort = targetNode.SelectSingleNode("COMPort").InnerText;
        //Debug.Log(targetNode.SelectSingleNode("Baudrate").InnerText);
        int baudrate = int.Parse(targetNode.SelectSingleNode("Baudrate").InnerText);
        _RFID_SerialManager.SerialPortInitialize(comPort, baudrate);
    }

    public void CameraSerialSetting(XmlNode targetNode)
    {
        //Debug.Log(targetNode.Attributes.Count);
        string comPort = targetNode.SelectSingleNode("CameraCOMPort").InnerText;
        int baudrate = int.Parse(targetNode.SelectSingleNode("CameraBaudrate").InnerText);
        _CameraSerialManager.SerialPortInitialize(comPort, baudrate);
    }
    
    public void UDPSenderSetting(XmlNode targetNode)
    {
        string targetIP = targetNode.SelectSingleNode("TargetIP").InnerText;
        int targetPort = int.Parse(targetNode.SelectSingleNode("TargetPort").InnerText);
        _UDPSendManager.SenderSetting(targetIP, targetPort);
    }

    public void UDPReceiverSetting(XmlNode targetNode)
    {
        int targetPort = int.Parse(targetNode.SelectSingleNode("ReceivePort").InnerText);
        _UDPReceiveManager.ReceiverInitializing(targetPort);
    }

    public void WalkingModeStoryAngle(XmlNode targetNode)
    {
        int story1 = int.Parse(targetNode.SelectSingleNode("Story1").InnerText);
        int story2 = int.Parse(targetNode.SelectSingleNode("Story2").InnerText);
        int story3 = int.Parse(targetNode.SelectSingleNode("Story3").InnerText);
        float limit = float.Parse(targetNode.SelectSingleNode("Range").InnerText);
        _WalkingModeController.StoryStepAngles[0] = story1;
        _WalkingModeController.StoryStepAngles[1] = story2;
        _WalkingModeController.StoryStepAngles[2] = story3;
        _WalkingModeController.ErrorRange = limit;
    }

}
