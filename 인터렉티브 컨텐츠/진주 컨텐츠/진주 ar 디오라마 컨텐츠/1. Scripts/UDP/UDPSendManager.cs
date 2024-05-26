using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPSendManager : MonoBehaviour
{
    public enum PCNumber { PC1, PC2, PC3, PC4 }
    public PCNumber _PCNumber;

    UdpClient ClientSender;
    //string serverIP = "192.168.0.100";
    public string serverIP = "";        // IP 변동 가능성 있음 (StreamingAssets에서 변경할수 있게 해주시면 감사합니다.)
    public int port = 7000;             // 포트 고정
    int contentsIndex;

    // Start is called before the first frame update
    void Start()
    {
        //ClientSender = new UdpClient(serverIP, port);
        
    }

    public void SenderSetting(string targetIP, int targetPort)
    {
        serverIP = targetIP;
        port = targetPort;
        ClientSender = new UdpClient(serverIP, port);
    }


    #region [Contents Index Number]
    // 10 Server
    // 20 써클비전
    // 30~33 디오라마
    // 40~43 옥VR
    // 50~51 화로
    // 60~61 농기구
    // 70~71 XR
    // 80~81 기념사진
    #endregion
    
    public void DioramaA_ContentsRFIDRecognition(string rfidCode)
    {
        switch (_PCNumber)
        {
            case PCNumber.PC1:
                Send("30:" + rfidCode);
                break;
            case PCNumber.PC2:
                Send("31:" + rfidCode);
                break;
            case PCNumber.PC3:
                Send("32:" + rfidCode);
                break;
            case PCNumber.PC4:
                Send("33:" + rfidCode);
                break;
            default:
                Send("34:" + rfidCode);
                break;
        }
    }

    // 서버로 전송
    public void Send(string str)
    {
        if(ClientSender != null)
        {
            if (str != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(str);
                ClientSender.Send(data, data.Length);
            }
        }
    }
}
