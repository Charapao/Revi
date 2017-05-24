using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;

public class FirstPersonCam : MonoBehaviour {
	public Thread serialThread;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float speed = 100000.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Quaternion localRotation;
    public string[] accelXYZ;
    public int wiiUsing;
    public int check_nodata;
    SerialPort stream;

    void Start () {
		stream = new SerialPort("COM3", 9600);
		if (stream.IsOpen) {
			stream.Close ();

		}
		localRotation = transform.rotation;
		//Open the Serial Stream.
	}
	void connect() {
		Debug.Log ("Connection started");
		try {
			stream.Open();
			stream.ReadTimeout = 2000;
			stream.Handshake = Handshake.None;
			serialThread = new Thread(recData);
			serialThread.Start ();
			Debug.Log("Port Opened!");
		}catch (SystemException e)
		{
			Debug.Log ("Error opening = "+e.Message);
		}

	}

    void Update () {
    	if (Input.GetKeyDown ("x")) {
			connect ();
		}

        // yaw += speedH * Input.GetAxis("Mouse X")*2;
        // pitch -= speedV * Input.GetAxis("Mouse Y")*2;
		int oldx=0;
		int oldy=0;
		int oldz=0;
		int toldx=0;
		int toldy=0;
		int toldz=0;
        // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        if(wiiUsing == 1){
	        float curSpeed = Time.deltaTime * speed;
	        if(check_nodata==0){
		        oldx=int.Parse(accelXYZ[0]);
		        oldy=int.Parse(accelXYZ[1]);
		        // oldz=int.Parse(accelXYZ[2]);
		        check_nodata = 1;
	    	}
	    	 // toldx = int.Parse(accelXYZ[0]) * 90;
		     // toldy = -int.Parse(accelXYZ[1]) * 90;
		     // toldz = int.Parse(accelXYZ[2]) * 90;
		     // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion(new Vector3(toldx, toldy, toldz)), Time.deltaTime * 5);
	    	// if(Mathf.Abs(int.Parse(accelXYZ[0]) - oldx) >= 40){
	    		// toldx = int.Parse(accelXYZ[0]);
	        // localRotation.x += (toldx) * curSpeed;
	    	// }
	        // if(Mathf.Abs(int.Parse(accelXYZ[1]) - oldy) >= 40){
	        	// toldy = int.Parse(accelXYZ[1]);
	        // localRotation.y += (toldy) * curSpeed;
	    	// }
	    	// if(Mathf.Abs(int.Parse(accelXYZ[2]) - oldz) >= 40){
	    		// toldz = int.Parse(accelXYZ[2]);
	        // localRotation.z = (toldz-oldz) * curSpeed;
	        // }

	        	oldx=toldx;
		        oldy=toldy;
		        // oldz=toldz;
	        // transform.rotation = localRotation;
    	}
    }
    void recData() {
		if ((stream != null) && (stream.IsOpen)) {
			byte tmp;
			string data = "";
			string avalues="";
			tmp = (byte) stream.ReadByte();
			// print(tmp);
			while(tmp !=255) {
				data+=((char)tmp);
				tmp = (byte) stream.ReadByte();
				if((tmp=='>') && (data.Length > 20)){
					// print(data);
					avalues = data;
					parseValues(avalues);
					data="";
				}
			}
		}
	}
	void parseValues(string av) {

		string[] deleteMorethan = av.Split ('>');
		string[] datafromsteam = deleteMorethan [1].Split(',');
		string button = datafromsteam [0];
		string joy = datafromsteam [1];
		string accel = datafromsteam [2];
		wiiUsing = int.Parse(datafromsteam [3]);
		if (wiiUsing == 0) {
			//print (av);
			//wii 1 สีดำ wii 0 สีขาว
			//ซ้าย 1 ขวา 254
			//ล่าง 0 บน 255 
		}

		string[] buttonZC = button.Split ('/');
		string[] joyXY = joy.Split ('/');
		accelXYZ = accel.Split ('/');
		if (wiiUsing == 0) {
			//print (joyXY[0] + "," + joyXY [1]);

		}
		// x = int.Parse (joyXY [0]);
		// y = int.Parse (joyXY [1]);
		// print(accel.Split('/')[0]);
		// print(y);

		// TranslatePlayer (int.Parse(joyXY[0]),int.Parse(joyXY[1]),wiiUsing);
		//print (split2);
		// c = float.Parse (split2 [0]);
		// d = float.Parse (split2 [1]);
		// print (c);
		// print (d);

	}
	void OnApplicationQuit() 
	{
		stream.Close();
	}
}