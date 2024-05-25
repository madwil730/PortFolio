using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ydlidar_G2 : MonoBehaviour
{
	// Start is called before the first frame update
	bool ret;
	public Text[] text;
	public GameObject ob;

	public float range_min;
	public float range_max;
	public float angle_min;
	public float angle_max;


	void Start()
    {
		StartCoroutine(turn());

	}


	IEnumerator turn()
	{
		ydlidar.os_init();
		CYdLidar lidarClass = new CYdLidar();
		string port = "COM6";
		int optname = (int)LidarProperty.LidarPropSerialPort;
		
		lidarClass.setlidaropt(optname, port);
		optname = (int)LidarProperty.LidarPropSerialBaudrate;
		lidarClass.setlidaropt(optname, 230400);

		optname = (int)LidarProperty.LidarPropFixedResolution; //Set and Get LiDAR Fixed angluar resolution
		lidarClass.setlidaropt(optname, false);

		//Set and Get LiDAR Reversion.
		//true: LiDAR data rotated 180 degrees.
		//false: Keep raw Data.
		//default: false
		optname = (int)LidarProperty.LidarPropReversion;
		lidarClass.setlidaropt(optname, false);

		//Set and Get LiDAR inverted.
		//true: Data is counterclockwise
		//false: Data is clockwise
		//Default: clockwise
		optname = (int)LidarProperty.LidarPropInverted;
		lidarClass.setlidaropt(optname, false);

		//Set and Get LiDAR Automatically reconnect flag.
		//Whether to support hot plug
		optname = (int)LidarProperty.LidarPropAutoReconnect;
		lidarClass.setlidaropt(optname, true);

		//Set and Get LiDAR single channel. Whether LiDAR communication channel is a single-channel
		optname = (int)LidarProperty.LidarPropSingleChannel;
		lidarClass.setlidaropt(optname, false);

		optname = (int)LidarProperty.LidarPropMaxAngle;
		lidarClass.setlidaropt(optname, 180.0f);

		optname = (int)LidarProperty.LidarPropMinAngle;
		lidarClass.setlidaropt(optname, -180.0f);

		optname = (int)LidarProperty.LidarPropSampleRate;
		lidarClass.setlidaropt(optname, 5);

		optname = (int)LidarProperty.LidarPropScanFrequency; // 속도 조절
		lidarClass.setlidaropt(optname, 10);

		optname = (int)LidarProperty.LidarPropSupportMotorDtrCtrl;
		lidarClass.setlidaropt(optname, false);

		optname = (int)LidarProperty.LidarPropSupportHeartBeat;
		lidarClass.setlidaropt(optname, false);

		optname = (int)LidarProperty.LidarPropMaxRange;
		lidarClass.setlidaropt(optname, 16.0f);

		optname = (int)LidarProperty.LidarPropMinRange;
		lidarClass.setlidaropt(optname, 0.28f);

		optname = (int)LidarProperty.LidarPropAbnormalCheckCount;
		lidarClass.setlidaropt(optname, 2);

		optname = (int)LidarProperty.LidarPropIntenstiy;
		lidarClass.setlidaropt(optname, true);

		optname = (int)LidarProperty.LidarPropLidarType;

		int lidarType = (int)LidarTypeID.TYPE_TRIANGLE;
		lidarClass.setlidaropt(optname, lidarType);

		
		
		ret = lidarClass.initialize();
		if (ret)
		{
			ret = lidarClass.turnOn();
		
		}
		else
		{
			Debug.Log("error:" + lidarClass.DescribeError());
		}
		LaserScan scan = new LaserScan();
		LaserFan fan = new LaserFan();
		LidarVersion version = new LidarVersion();


		//lidarClass.doProcessSimple(scan);

		while (ret && ydlidar.os_isOk())
		{
			//Debug.Log(scan.points.Capacity);
			if (lidarClass.doProcessSimple(scan))
			{
				//--------LaserPointVector----------

				text[0].text = "scan capacity : " + scan.points.Capacity;
				text[1].text = "scan size : " + scan.points.Count;

				//--------LaserConfig----------

				text[2].text = "scan max_angle : " + scan.config.max_angle;
				text[3].text = "scan min_angle : " + scan.config.min_angle;
				text[4].text = "scan max_range : " + scan.config.max_range;
				text[5].text = "scan min_range : " + scan.config.min_range;
				text[6].text = "scan scan_time : " + 1 / scan.config.scan_time;  //hz?
				text[7].text = "scan angle_increment : " + scan.config.angle_increment;
				text[8].text = "scan time_increment : " + scan.config.time_increment;

				//if (1 / scan.config.scan_time < 8)
					//Debug.Log("check ");

					//--------------LaserFan-----------------

					text[9].text = "scan fan_stamp : " + scan.fan_stamp;
				text[10].text = "scan npoints : " + (uint)scan.npoints;


				//--------------LaserPoint-----------------

				text[11].text = "scan angle : " + scan.points[355].angle;
				text[12].text = "scan range : " + scan.points[355].range;
				text[13].text = "scan intensity : " + scan.points[355].intensity;
				text[14].text = "scan stamp : " + scan.stamp;

				for (int i = 0; i < scan.points.Count; i++)
				{


					if (scan.points[i].angle > angle_min && scan.points[i].angle < angle_max)
					{
						if (scan.points[i].range > range_min && scan.points[i].range < range_max)
						{
							float x = scan.points[i].range * (float)Math.Cos(scan.points[i].angle);
							float y = scan.points[i].range * (float)Math.Sin(scan.points[i].angle);

							//-----------------Object Pool--------------

							//GameObject obs = ObjectPool.GetObject();
							//obs.transform.localPosition = new Vector3(x, y, 0);

							//obs.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
							//obs.GetComponent<LineRenderer>().SetPosition(1, new Vector3(x, y, 0));

							//-----------------no ObjectPool------------------

							Instantiate(ob, new Vector3(x, y, 0), Quaternion.identity);

							ob.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
							ob.GetComponent<LineRenderer>().SetPosition(1, new Vector3(x, y, 0));
						}
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.A))
				break;

			yield return new WaitForSeconds(0.14f);
		}
		lidarClass.turnOff();
		lidarClass.disconnecting();
		lidarClass.Dispose();

		//yield return null;
	}
}


// 라디안 * 180 / 파이 = 호도법
