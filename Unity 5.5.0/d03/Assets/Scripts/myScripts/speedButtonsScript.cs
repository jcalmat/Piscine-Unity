using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedButtonsScript : MonoBehaviour {

	public gameManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void accelerate0() {
		manager.changeSpeed (1);
	}

	public void accelerate1() {
		manager.changeSpeed (2.5f);
	}

	public void accelerate2() {
		manager.changeSpeed (5);
	}
}
