using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class OscListener : MonoBehaviour {

	private string UDPHost = "127.0.0.1";
	private int listenerPort = 3333;
	private int broadcastPort = 8001;
	private Osc oscHandler;
	private int count = 0;
	private int signal = 0;
	private bool turnedAround = false;
	public GameObject character;
	Animator animator;

	private int testInt;

	public void Start (){
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(UDPHost, broadcastPort, listenerPort);
		oscHandler = GetComponent<Osc>();
		oscHandler.init(udp);
		oscHandler.SetAddressHandler("/test", counterTest);
		animator = character.GetComponent<Animator> ();
	}
	
	public void Update (){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			count++;
			turnedAround = false;
			signal = 0;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			iTween.RotateBy(character, new Vector3(0, (float) -0.25, 0), 1);
		}
		
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			iTween.RotateBy(character, new Vector3(0, (float) 0.25, 0), 1);
		}
		
		animator.SetInteger ("animState", count);
		
		if (count == 2 || count == 5 || count == 8) {
			if (!turnedAround){
				signal = -1;
				turnedAround = true;
			}
		}
		
		if (count == 3 || count == 9) {
			if (!turnedAround){
				signal = 1;
				turnedAround = true;
			}
		}

		sendData ("/Rumbo " + signal);
	}
	

	public void counterTest ( OscMessage oscMessage  ){	
		Debug.Log ("Receiving sth");
		testInt = (int) oscMessage.Values[0];
	}

	public void sendData ( string data  ){
		oscHandler.Send(Osc.StringToOscMessage(data));
	}
	
	
	void OnApplicationQuit (){
		Debug.Log ("Goodbye");
		oscHandler.Cancel();
	}
	
	void LogMsg (string msg){
		Debug.Log(msg);
	}

}