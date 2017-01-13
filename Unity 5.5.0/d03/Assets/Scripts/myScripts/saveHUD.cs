using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveHUD : MonoBehaviour {

	public Canvas canvas;
	public endMenuScript endScript;
	public pauseMenuScript pauseScript;
	public bool reload = false;

	// Use this for initialization
	void Start () {
		pauseScript = this.GetComponent<pauseMenuScript> ();
		if (!reload) {
			endScript.hideMenu ();
//			pauseScript.hideMenu ();
//			pauseScript.hideExitMenu ();
			DontDestroyOnLoad (canvas);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}

