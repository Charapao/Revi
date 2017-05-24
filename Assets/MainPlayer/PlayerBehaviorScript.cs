using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;

public class PlayerBehaviorScript : MonoBehaviour {

	public Thread serialThread;
    public GameObject player;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float speed = 10.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Quaternion localRotation;
    public string[] accelXYZ;
    public string[] joyXY;
    public string[] buttonZC; 
    public int wiiUsing;
    public int check_nodata;
    public Vector3 dir= Vector3.zero;
    public Vector3 v3To;
    public int count=0;
    public float x,y;
    public float lookVectorx,lookVectory,lookVectorz,lookupdown;
    public bool pressZ_Black=false;
    public bool pressC_Black=false;
    public PlayerAttackScript playerAttackScript;
    public PlayerHealth playerHealth;
    public bool isEnabled = true;
    bool isMoving = false;
    Animator anim;
    public Rigidbody rb;
    int floorMask;
    
    SerialPort stream;
	// Use this for initialization
	void Start () {

        playerAttackScript = GetComponent<PlayerAttackScript>();
        playerHealth = GetComponent<PlayerHealth>();
		// playerAttackScript.enableCol();
        rb = GetComponent<Rigidbody>();
        stream = new SerialPort("COM3", 9600);
        if (stream.IsOpen) {
            stream.Close ();

        }
        lookVectorx = 0;
        lookVectory = 0;
        lookVectorz = 0;
        // for(int i=0;i<10;i++){
            
        // }
        
	}
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown ("x")) {
  connect ();
        }
        
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
                    print(data);
                    avalues = data;
                    parseValues(avalues);
                    data="";
                }
            }
        }
    }
    public bool getZbutton_Attack(){
        if(wiiUsing == 1){
            return pressZ_Black;
        }

        return false; 
    }
    void Awake()
    {

        floorMask = LayerMask.GetMask("Floor") ;
        anim = GetComponent<Animator>();
    }
	void FixedUpdate(){
        if(isEnabled==false){
            return;
        }
        if(wiiUsing == 1 ){
            // dir = new Vector3(float.Parse(accelXYZ[0])/1023*360,(float.Parse(accelXYZ[1])/1023*360),float.Parse(accelXYZ[2])/1023*360);

            // dir = Vector3.Lerp(dir, v3To, Time.deltaTime * speed);
        // pitch += 180*Mathf.Atan(float.Parse(accelXYZ[0])/(Mathf.Sqrt(Mathf.Pow(float.Parse(accelXYZ[1]),2.0f)+Mathf.Pow(float.Parse(accelXYZ[2]),2.0f))))/3.14f;
        // yaw -= 180*Mathf.Atan(float.Parse(accelXYZ[2])/(Mathf.Sqrt(Mathf.Pow(float.Parse(accelXYZ[1]),2.0f)+Mathf.Pow(float.Parse(accelXYZ[2]),2.0f))))/3.14f;
        // print(yaw+"FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"+pitch);
        // print(yaw+"FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
        // yaw += speedH * Input.GetAxis("Mouse X")*2;
        // pitch -= speedV * Input.GetAxis("Mouse Y")*2;
            // print(dir+"ssssssssssss");

        // transform.eulerAngles=dir;
        // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            x = float.Parse(joyXY[0]);
            y = float.Parse(joyXY[1]);
            if(int.Parse(buttonZC[0])==1){
                pressZ_Black = true;
            }
            else{
                pressZ_Black = false;
            }
            if(int.Parse(buttonZC[1])==1){
                pressC_Black = true;
            }
            else{
                pressC_Black = false;
            }
            if(pressZ_Black){
                // lookupdown = 45;
                // playerAttackScript.col.enabled = true;
                // playerHealth.AnimateAttack();
                // print(lookupdown);
            }else if(pressC_Black){
                lookupdown = 315;
            }else{
                lookupdown = 0;
                // playerAttackScript.col.enabled = false;
            }

            if((x == 131 || x == 132) && y == 255){
            	isMoving = true;
            	
            rb.AddForce(Vector3.forward*speed,ForceMode.VelocityChange);

            // player.transform.Translate (Vector3.forward);
            lookVectorx = 0;
            lookVectory = 0;
            lookVectorz = 0;
            }
            else if(x >= 133 && x <= 253 && y == 255){
            	isMoving = true;
            	
            rb.AddForce(Vector3.forward/1.414f*speed,ForceMode.VelocityChange);
            rb.AddForce(Vector3.right/1.414f*speed,ForceMode.VelocityChange);
            // player.transform.Translate (Vector3.forward/1.414f);
            // player.transform.Translate (Vector3.right/1.414f);

            lookVectorx = 0;
            lookVectory = 45;
            lookVectorz = 0;
            }                        
            else if (x == 254 && (y == 132 || y == 131)){ 
            	isMoving = true;
            	
            rb.AddForce(Vector3.right*speed,ForceMode.VelocityChange);
            // player.transform.Translate(Vector3.right);
            lookVectorx = 0;
            lookVectory = 90;
            lookVectorz = 0;
            }
            else if(x >=220 && x <= 254 && y >=10 &&y <= 90){
            	isMoving = true;
            	
            rb.AddForce(Vector3.right/1.414f*speed,ForceMode.VelocityChange);
            rb.AddForce(Vector3.back/1.414f*speed,ForceMode.VelocityChange);
            // player.transform.Translate (Vector3.right/1.414f);
            // player.transform.Translate (Vector3.back/1.414f);
            lookVectorx = 0;
            lookVectory = 135;
            lookVectorz = 0;
            }
            else if(x == 131 &&  y == 0){
            	isMoving = true;
            	
            rb.AddForce(Vector3.back*speed,ForceMode.VelocityChange);
            // player.transform.Translate (Vector3.back);
            lookVectorx = 0;
            lookVectory = 180;
            lookVectorz = 0;
            }
            else if( x == 1  && y >= 0 && y <= 131){
            	isMoving = true;
            	
            rb.AddForce(Vector3.back/1.414f*speed,ForceMode.VelocityChange);
            rb.AddForce(Vector3.left/1.414f*speed,ForceMode.VelocityChange);
            // player.transform.Translate (Vector3.back/1.414f);
            // player.transform.Translate (Vector3.left/1.414f);
            lookVectorx = 0;
            lookVectory = 225;
            lookVectorz = 0;
            }
            else if (x == 1 && y == 132){
            	isMoving = true;
            	
            rb.AddForce(Vector3.left*speed,ForceMode.VelocityChange);
            // player.transform.Translate (Vector3.left);
            lookVectorx = 0;
            lookVectory = 270;
            lookVectorz = 0;
            }
            else if( x == 1 && y >= 133 && y <= 255){
            	isMoving = true;
            	
            rb.AddForce(Vector3.left/1.414f*speed,ForceMode.VelocityChange);
            rb.AddForce(Vector3.forward/1.414f*speed,ForceMode.VelocityChange);
            // player.transform.Translate (Vector3.left/1.414f);
            // player.transform.Translate (Vector3.forward/1.414f);
            lookVectorx = 0;
            lookVectory = 315;
            lookVectorz = 0;
            }
            else{
            	isMoving = false;
            }
            Animating();
            transform.eulerAngles = new Vector3(lookVectorx+lookupdown,lookVectory,lookVectorz);
            // if (x == 1 && y == 255)
                // transform.eulerAngles = new Vector3(0,225,0);
            
            
            
        }
    }
    void Animating(){
    	//if the character is moving , then play the walking animation
    	if(isMoving){
    		// we play the walking anim
    		anim.SetFloat("speed",1);

    	}else{
    		anim.SetFloat("speed",0);
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

        buttonZC = button.Split ('/');
        joyXY = joy.Split ('/');
        accelXYZ = accel.Split ('/');
        if(count==0){
            v3To = new Vector3(float.Parse(accelXYZ[0])/1023*360,(float.Parse(accelXYZ[1])/1023*360),float.Parse(accelXYZ[2])/1023*360);
        }
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
    public void DisableMovement(){
        isEnabled = false;
    }
}
