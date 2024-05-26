using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Xml;
using UnityEngine;

public class RFID_SerialManager : MonoBehaviour
{

	public UDPSendManager _UDPSendManager;

	SerialPort _SerialPort;
	public string comPort; // 변동 가능성 있음 (StreamingAssets에서 변경할수 있게 해주시면 감사합니다.)
	public int baudrate ;
	// Thread
	Thread _SerialThread;

	private void Start()
	{
		SerialPortInitialize();
		SerialThreadOpen();
	}

	public void SerialPortInitialize()
	{
		_SerialPort = new SerialPort(Read.COMPort, Read.Baudrate, Parity.None, 8);
		_SerialPort.ReadTimeout = 100;
		try
		{
			_SerialPort.Open();

			Debug.Log("RFID Scan SerialPort Is Started");
		}
		catch
		{
			Debug.Log("RFID Scan SerialPort Is Not Started, Check USB or ComPort");
		}

	}

	private void SerialThreadOpen()
	{
		ThreadStart ts = new ThreadStart(SerialReceiverThread);
		_SerialThread = new Thread(ts);
		_SerialThread.Start();
	}

	private void SerialReceiverThread()
	{
		//Debug.Log("ThreadStarted");
		while (true)
		{
			//yield return null;
			if (!_SerialPort.IsOpen) continue;

			string readData = "";
			try
			{
				readData = _SerialPort.ReadLine();
	
				//Debug.Log(str);
			}
			catch
			{
				//Debug.Log("TimeOut");
				continue;
			}

			if (readData != null)
			{
				//Debug.Log(str);
				if (readData.Length == 8)
				{
					// 초기화 및 가입
					_UDPSendManager.test(readData);
					//Debug.Log(readData);
					Debug.Log("RFID Check");
				}
				else
				{
					Debug.Log("RFID Failed, is Not Support RFID");
				}
			}
		}
	}
	private void OnDisable()
	{
		if (_SerialPort != null)
		{
			_SerialPort.Close();
		}
		if (_SerialThread != null)
		{
			_SerialThread.Interrupt();
			_SerialThread.Abort();
		}
	}

	private void OnApplicationQuit()
	{
		if (_SerialPort != null)
		{
			_SerialPort.Close();
		}
		if (_SerialThread != null)
		{
			_SerialThread.Interrupt();
			_SerialThread.Abort();
		}
	}
}
