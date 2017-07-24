using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorlessSwitchScript : MonoBehaviour {

	private GameObject[] blueDoors;
	private GameObject[] yellowDoors;
	private GameObject[] redDoors;

	private float[] yellowDoorsOriginY;
	private float[] blueDoorsOriginY;
	private float[] redDoorsOriginY;

	private bool isTriggered = false;

	// Use this for initialization
	void Start () {
		blueDoors = GameObject.FindGameObjectsWithTag ("blueDoor");
		yellowDoors = GameObject.FindGameObjectsWithTag ("yellowDoor");
		redDoors = GameObject.FindGameObjectsWithTag ("redDoor");
	
		yellowDoorsOriginY = new float[yellowDoors.Length];
		for(int i = 0; i < yellowDoors.Length; i++) {
			yellowDoorsOriginY [i] = yellowDoors[i].transform.position.y;
		}

		blueDoorsOriginY = new float[blueDoors.Length];
		for(int i = 0; i < blueDoors.Length; i++) {
			blueDoorsOriginY [i] = blueDoors[i].transform.position.y;
		}

		redDoorsOriginY = new float[redDoors.Length];
		for(int i = 0; i < redDoors.Length; i++) {
			redDoorsOriginY [i] = redDoors[i].transform.position.y;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "john" || other.name == "thomas" || other.name == "claire")
			isTriggered = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.name == "john") {
			for (int i = 0; i < yellowDoors.Length; i++) {
				Rigidbody2D test = yellowDoors [i].GetComponent<Rigidbody2D> ();
				test.simulated = false;
				if (yellowDoors [i].transform.position.y < yellowDoorsOriginY [i] + 2.5)
					yellowDoors [i].transform.Translate (Vector3.up * Time.deltaTime * 5);
			}
		} else if (other.name == "claire") {
			for (int i = 0; i < blueDoors.Length; i++) {
				Rigidbody2D test = blueDoors [i].GetComponent<Rigidbody2D> ();
				test.simulated = false;
				if (blueDoors [i].transform.position.y < blueDoorsOriginY [i] + 2.5)
					blueDoors [i].transform.Translate (Vector3.up * Time.deltaTime * 5);
			}
		} else if (other.name == "thomas") {
			for (int i = 0; i < redDoors.Length; i++) {
				Rigidbody2D test = redDoors [i].GetComponent<Rigidbody2D> ();
				test.simulated = false;
				if (redDoors [i].transform.position.y < redDoorsOriginY [i] + 2.5)
					redDoors [i].transform.Translate (Vector3.up * Time.deltaTime * 5);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "john") {
			for (int i = 0; i < yellowDoors.Length; i++) {
				Rigidbody2D test = yellowDoors [i].GetComponent<Rigidbody2D> ();
				test.simulated = true;
			}
		} else if (other.name == "claire") {
			for (int i = 0; i < blueDoors.Length; i++) {
				Rigidbody2D test = blueDoors [i].GetComponent<Rigidbody2D> ();
				test.simulated = true;
			}
		} else if (other.name == "thomas") {
			for (int i = 0; i < redDoors.Length; i++) {
				Rigidbody2D test = redDoors [i].GetComponent<Rigidbody2D> ();
				test.simulated = true;
			}
		}
		if (isTriggered)
			isTriggered = false;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
