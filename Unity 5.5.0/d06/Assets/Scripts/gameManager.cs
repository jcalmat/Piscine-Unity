using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

	public player player;
	public static gameManager gm;
	public Text message;

	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this;
		message.GetComponent<Animator> ().SetTrigger ("showText");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void cameraSpotPlayer() {
		player.playerSpotted (20);
	}

	public void setMessage(string msg) {
		message.text = msg;
		message.GetComponent<Animator> ().SetTrigger ("showText");
	}
}
