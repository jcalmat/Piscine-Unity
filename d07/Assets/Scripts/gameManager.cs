using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {
		
	public static gameManager gm;
		
	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playDead() {
		GetComponent<AudioSource> ().Play ();
	}
}
