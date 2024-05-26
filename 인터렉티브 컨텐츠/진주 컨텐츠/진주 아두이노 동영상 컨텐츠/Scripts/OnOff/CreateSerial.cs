using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class CreateSerial : MonoBehaviour
{
	public static SerialPort serial;
	// Start is called before the first frame update
	void Start()
    {
		serial = new SerialPort(ReadOnOff.Port, ReadOnOff.BaudRate);
		serial.Open();
    }

}
