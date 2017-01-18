using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public float speed = 5f;

	private float rotationY;
	private float rotationX;

	public GameObject Tower;
	public GameObject Wheel;

	private float sensitivityY = 10f;

	private float boost = 100;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		float rotationX = Tower.transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityY;

		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY * 10;
		//	rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		Tower.transform.localEulerAngles = new Vector3 (0, rotationX, 0);
		//		transform.Rotate (0, 20 * Time.deltaTime, 0);


		if (Input.GetKey (KeyCode.W))
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.S))
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (0, 10, 0);
		if (Input.GetKey (KeyCode.A))
			transform.Rotate (0, -10, 0);
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (boost > 0) {
				boost -= 10;
				speed = 7;
			}
		} else {
			speed = 5;
		}
		if (boost < 100)
			boost += 5;
	}
}
