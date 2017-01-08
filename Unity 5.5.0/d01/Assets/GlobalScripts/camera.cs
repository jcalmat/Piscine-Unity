using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void centerCamera(float x, float y) {
		transform.position = new Vector3(x, y, transform.position.z);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
