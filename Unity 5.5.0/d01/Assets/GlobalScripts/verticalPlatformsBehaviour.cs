using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalPlatformsBehaviour : MonoBehaviour {

	public float speed = 1;
	public float origin_y;
	public Vector3 direction;
	public float distance = 2;
	// Use this for initialization
	void Start () {
		origin_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > origin_y + distance)
			direction = Vector3.down;
		else if (transform.position.y <= origin_y)
			direction = Vector3.up;
		transform.Translate(direction * Time.deltaTime * speed);			
	}
}
