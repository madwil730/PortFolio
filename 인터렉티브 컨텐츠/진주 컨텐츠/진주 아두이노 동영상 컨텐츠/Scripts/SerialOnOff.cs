using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class SerialOnOff : MonoBehaviour
{
	public Text text;



	public void Serial1()
	{

		CreateSerial.serial.Write("1\n");
		
		text.text = "Serial Start , serial.Write(1) 한번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;

	}

	public void Serial2()
	{

		CreateSerial.serial.Write("2\n");

		text.text = "Serial Start , serial.Write(2) 한번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;

	}


	public void Serial3()
	{

		CreateSerial.serial.Write("3\n");

		text.text = "Serial Start , serial.Write(3) 한번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;

	}


	public void Serial4()
	{

		CreateSerial.serial.Write("4\n");

		text.text = "Serial Start , serial.Write(4) 한번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;

	}



	public void SerialOn()
	{

		CreateSerial.serial.Write("ON\n");
		CreateSerial.serial.Write("ON\n");
		
		text.text = "Serial Start , serial.Write(ON) 두번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;

	}


	public void SerialOff()
	{
		CreateSerial.serial.Write("OFF\n");
		text.text = "Serial Start , serial.Write(OFF) 한번 전송  \nPort : " + ReadOnOff.Port + " \nBaudRate :" + ReadOnOff.BaudRate;

	}

}
