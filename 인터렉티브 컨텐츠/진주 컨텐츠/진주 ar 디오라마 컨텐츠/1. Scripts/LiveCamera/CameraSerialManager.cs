using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class CameraSerialManager : MonoBehaviour
{
	SerialPort _SerialPort;
	public string comPort = "COM3";
	public int Baudrate = 115200;

	//[SerializeField] GameObject DebugObject;

	//private void Start()
	//{
	//    SerialPortInitialize(comPort, baudrate);
	//}

	public void SerialPortInitialize(string com, int baudrate)
	{
		comPort = com;
		Baudrate = baudrate;
		_SerialPort = new SerialPort(comPort, Baudrate, Parity.None, 8);
		_SerialPort.ReadTimeout = 100;
		try
		{
			_SerialPort.Open();
			//SerialWrite("!");   // 모터의 중심으로 이동
			Debug.Log("Camera RFID Scan SerialPort " + comPort + " Is Started");
		}
		catch
		{
			Debug.Log("Camera RFID Scan SerialPort + " + comPort + " Is Not Started, Check USB or ComPort");
		}
	}
	private void Update()
	{
		//if (DebugObject.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				SerialWrite("!");
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				SerialWrite("C");
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				SerialWrite("S");
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				SerialWrite("L");
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				SerialWrite("R");
			}
			if (Input.GetKeyDown(KeyCode.T))
			{
				SerialWrite("-60");
			}
		}
	}

	// 설정된 중심으로 이동
	public void MoveCameraPositionToCenter()
	{
		SerialWrite("C");
	}
	// 현재 위치를 중심으로 설정
	public void SaveCameraCenterPosition()
	{
		SerialWrite("S");
	}

	// -60 ~ 60 각도로 이동
	public void MoveCameraPositionToAngle(float angle)
	{
		// 33.3
		//Debug.Log("Camera Angle : " + angle);
		SerialWrite(angle.ToString());
		//Debug.Log(angle.ToString());
	}

	// 왼쪽으로 이동
	public void MoveCameraPositionToLeft()
	{
		SerialWrite("L");
	}

	// 오른쪽으로 이동
	public void MoveCameraPositionToRight()
	{
		SerialWrite("R");
	}
	//public Text DebugText;
	public void SerialWrite(string str)
	{
		//Debug.Log(str);
		//DebugText.text = "DataSerial : " + str;
		if (_SerialPort != null)
		{
			if (_SerialPort.IsOpen)
			{
				_SerialPort.WriteLine(str);
				Debug.Log("DataSerial : " + str);
			}
		}
	}
}
