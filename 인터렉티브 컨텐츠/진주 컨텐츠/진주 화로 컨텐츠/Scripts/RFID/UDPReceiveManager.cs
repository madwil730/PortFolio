using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPReceiveManager : MonoBehaviour
{
    UdpClient _UDPReciver;
    IPEndPoint ReceiveIPEndPoint;
    // 받는 포트 6000번 / StreamingAssets 변경 가능하도록 부탁드립니다.
    public int ReceivePort ;
	public static bool check;
    // Start is called before the first frame update
    void Start()
    {
		ReceivePort = ReadSerial.ReceivePort;
		// 오는거 다 받음
		ReceiveIPEndPoint = new IPEndPoint(IPAddress.Any, ReceivePort);
        _UDPReciver = new UdpClient(ReceiveIPEndPoint);

        StartCoroutine(UDPReceiver());
        Debug.Log("ReceiverStart");
    }

    IEnumerator UDPReceiver()
    {
        while (true)
        {
            yield return null;
            byte[] ReceiveData = null;

            if(_UDPReciver.Available > 0)
            {
                ReceiveData = _UDPReciver.Receive(ref ReceiveIPEndPoint);

                if (ReceiveData != null && ReceiveData.Length > 0)
                {
                    string data = Encoding.UTF8.GetString(ReceiveData);
	
                    ReceiveDataParsing(data);
                }
            }
        }
    }

    private void ReceiveDataParsing(string data)
    {
        bool ExperienceContents;
        if(bool.TryParse(data, out ExperienceContents))
        {
            // True or False
            // 정상 인식 >> Play
            Debug.Log("RFID 인식 여부 : " + ExperienceContents);
			check = ExperienceContents;
        }
        else
        {
            //Debug.Log("포토프린터 출력용 리스트");
            //string[] ExperienceList = data.Split(',');
            //int cnt = ExperienceList.Length;
            //for(int i = 0; i < cnt; i++)
            //{
            //    Debug.Log("ContentsName : " + ExperienceList[i].Split(':')[0] + ", ContentsExperienceCheck : " + ExperienceList[i].Split(':')[1]);
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
