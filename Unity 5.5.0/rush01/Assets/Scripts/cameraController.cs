using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	//
	private Transform target;
	private float xSpeed = 125;
	private bool rightClicked = false;
	public  float x = 0;
	public float z;
	public Quaternion rotation;
	//

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;

		//
		z = transform.eulerAngles.x;
		x = transform.eulerAngles.y;
	//	target = transform.parent.gameObject.GetComponent<Transform>();
		//
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Mouse1))
			rightClicked = true;
		else if (Input.GetKeyUp(KeyCode.Mouse1))
			rightClicked = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = offset + player.transform.position;

		//
		if (rightClicked) {
			x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
			rotation = Quaternion.Euler (z, x, 0);
		//	Vector3 position = rotation * new Vector3 (transform.rotation.x, 0,  z) + transform.position;
			transform.rotation = rotation;
		//	transform.position = position;
		}
		//
	}
}
