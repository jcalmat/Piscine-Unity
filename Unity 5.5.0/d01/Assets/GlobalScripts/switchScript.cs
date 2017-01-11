using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour {

	public GameObject[] gameobjects;
	private float[] doorOriginY;
	private bool isTriggered = false;

	// Use this for initialization
	void Start () {
		doorOriginY = new float[gameobjects.Length];
		for (int i = 0; i < gameobjects.Length; i++) {
			doorOriginY[i] = gameobjects[i].transform.position.y;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (transform.tag == "yellowSwitch" && other.name == "john" || transform.tag == "redSwitch" && other.name == "thomas" || transform.tag == "blueSwitch" && other.name == "claire")
			isTriggered = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (isTriggered) {
			for (int i = 0; i < gameobjects.Length; i++) {
				Rigidbody2D test = gameobjects[i].GetComponent<Rigidbody2D> ();
				test.simulated = false;
				if (gameobjects [i].transform.position.y < doorOriginY [i] + 2.5)
					gameobjects [i].transform.Translate (Vector3.up * Time.deltaTime * 5);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		for (int i = 0; i < gameobjects.Length; i++) {
			Rigidbody2D test = gameobjects [i].GetComponent<Rigidbody2D> ();
			test.simulated = true;
		}
		if (isTriggered)
			isTriggered = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
