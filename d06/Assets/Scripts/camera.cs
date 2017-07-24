using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	public float speed;


	public float mouseSensitivity = 100.0f;
	public float clampAngle = 0;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis


	// Use this for initialization
	void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}
	
	// Update is called once per frame
	void Update () {

//		transform.LookAt (Camera.main.ScreenToWorldPoint(Input.mousePosition));

//		Debug.Log (Input.mousePosition);

		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.rotation = localRotation;

//		if (Input.GetKey (KeyCode.LeftShift))
//			speed = 10;
//		else
//			speed = 5;
//		
//		if (Input.GetKey (KeyCode.W))
//			transform.Translate (Vector3.forward * speed * Time.deltaTime);
//		if (Input.GetKey(KeyCode.S))
//			transform.Translate (Vector3.back * speed * Time.deltaTime);
//		if (Input.GetKey(KeyCode.A))
//			transform.Translate (Vector3.left * speed * Time.deltaTime);
//		if (Input.GetKey(KeyCode.D))
//			transform.Translate (Vector3.right * speed * Time.deltaTime);


	}
}
