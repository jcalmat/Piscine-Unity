using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour {

	public GameObject exitTeleport;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		other.transform.position = new Vector3(exitTeleport.transform.position.x, exitTeleport.transform.position.y, exitTeleport.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
	}
}
