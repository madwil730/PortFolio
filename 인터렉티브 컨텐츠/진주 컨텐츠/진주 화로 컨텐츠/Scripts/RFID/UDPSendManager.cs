using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPSendManager : MonoBehaviour
{
    UdpClient ClientSender;
    //string serverIP = "192.168.0.100";
    public string serverIP;        // IP 변동 가능성 있음 (StreamingAssets에서 변경할수 있게 해주시면 감사합니다.)
    public int port;             // 포트 고정

    // Start is called before the first frame update
    void Start()
    {
		serverIP = ReadSerial.TargetIP;
		port = ReadSerial.TargetPort;

		ClientSender = new UdpClient(serverIP, port);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region [Contents Index Number]
    // Diorama A : 0
    // Diorama B : 1
    // Diorama C : 2
    // Diorama D : 3
    // CircleVision : 4
    // JadeVR : 5
    // Oven : 6
    // FarmEquipment : 7
    // XRTelescope : 8
    // PhotoPrint : 9
    #endregion
    public void test(string data)
    {
        //DioramaA_ContentsRFIDRecognition(data);
        //DioramaB_ContentsRFIDRecognition(data);
        //DioramaC_ContentsRFIDRecognition(data);
        //DioramaD_ContentsRFIDRecognition(data);
        //CircleVision_ContentsRFIDRecognition(data);
        //JadeVR_ContentsRFIDRecognition(data);
        // resultIndex (10~17, 20~27, 30~37)
        //SetJadeVR_ContentsResultIndex(data, 17);
        Oven_ContentsRFIDRecognition(data);
        //FarmEquipment_ContentsRFIDRecognition(data);
        //XRTelescope_ContentsRFIDRecognition(data);
        //PhotoPrint_ContentsRFIDRecognition(data);
    }

    public void DioramaA_ContentsRFIDRecognition(string 
		rfidCode)
    {
        Send("0:" + rfidCode);
    }
    public void DioramaB_ContentsRFIDRecognition(string rfidCode)
    {
        Send("1:" + rfidCode);
    }
    public void DioramaC_ContentsRFIDRecognition(string rfidCode)
    {
        Send("2:" + rfidCode);
    }
    public void DioramaD_ContentsRFIDRecognition(string rfidCode)
    {
        Send("3:" + rfidCode);
    }
    public void CircleVision_ContentsRFIDRecognition(string rfidCode)
    {
        Send("4:" + rfidCode);
    }

    // 옥 VR
    // Serial 에서 RFID 코드가 들어오면 이걸 통해서 전송해주시면 됩니다.
    // 인터넷은 사용하지 못하며, 인트라넷(내부망) 에서 데이터 통신이 이루어져야합니다.
    public void JadeVR_ContentsRFIDRecognition(string rfidCode)
    {
        Send("5:" + rfidCode);
    }
    // 옥 VR에서 완성한 모델 이미지 코드가 게임이 끝나면 전송됩니다.
    // 곡옥 10~17 / 관옥 20~27 / 환옥 30~37
    // 잘 만들어진 옥이 낮은 숫자.
    public void SetJadeVR_ContentsResultIndex(string rfidCode, int resultIndex)
    {
        Send("5:" + rfidCode + ":" + resultIndex);
    }


    public void Oven_ContentsRFIDRecognition(string rfidCode)
    {
        Send(ReadSerial.index + rfidCode);
    }
    public void FarmEquipment_ContentsRFIDRecognition(string rfidCode)
    {
        Send("7:" + rfidCode);
    }

    // XR 망원경
    public void XRTelescope_ContentsRFIDRecognition(string rfidCode)
    {
        Send("8:" + rfidCode);
    }
    public void PhotoPrint_ContentsRFIDRecognition(string rfidCode)
    {
        Send("9:" + rfidCode);
    }

    // 서버로 전송
    public void Send(string str)
    {
        if(str != null)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            ClientSender.Send(data, data.Length);
        }
    }

    //// 해당 주소로 전송
    //public void UDPSender(string ipAddress, int ipPort, string str)
    //{
    //    ClientSender = new UdpClient(ipAddress, ipPort);
    //    byte[] byteData = Encoding.UTF8.GetBytes(str);
    //    if (byteData != null)
    //    {
    //        ClientSender.Send(byteData, byteData.Length);
    //    }
    //    else
    //    {
    //        // 오류
    //    }
    //}
}
