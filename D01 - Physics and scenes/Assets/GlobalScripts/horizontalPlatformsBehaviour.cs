using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalPlatformsBehaviour : MonoBehaviour {

	public float speed = 1;
	public float origin_x;
	public Vector3 direction;
	// Use this for initialization
	void Start () {
		origin_x = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > origin_x + 2)
			direction = Vector3.left;
		else if (transform.position.x <= origin_x)
			direction = Vector3.right;
		transform.Translate(direction * Time.deltaTime * speed);			
	}
}
