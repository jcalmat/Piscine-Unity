using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	public player player;
	public static gameManager gm;

	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void cameraSpotPlayer() {
		player.playerSpotted (20);
	}
}
