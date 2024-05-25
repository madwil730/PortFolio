using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.Text;

public class sendPacket : MonoBehaviour
{
	SerialPort Serial;
	string Port;
	int BaudRate;

	
	Thread _SerialThread;
	string str;


	// Start is called before the first frame update
	void Start()
    {
		
		Port = "COM6";
		BaudRate = 230400;

		Serial = new SerialPort(Port, BaudRate);
		Serial.Open();
		
		ThreadStart ts = new ThreadStart(SerialReceiverThread1);
		_SerialThread = new Thread(ts);
		_SerialThread.Start();

		str = Convert.ToString(42336, 16);
		//str = "A560";
		byte[] strByte = Encoding.UTF8.GetBytes(str);




		Serial.Write(strByte, 0, strByte.Length);

		//Debug.Log(Serial.ReadByte());
		Debug.Log(Serial.IsOpen);


	}

	private void SerialReceiverThread1()
	{

		//str = Convert.ToString(42336, 16);
		//byte[] strByte = Encoding.UTF8.GetBytes(str);

		//Serial.Write(strByte, 0, strByte.Length);

		//Debug.Log(Serial.ReadLine() + "123");
		//Debug.Log("1231");
		
	}



	private void OnApplicationQuit()
	{
		_SerialThread.Interrupt();
		_SerialThread.Abort();
		
	}
}
