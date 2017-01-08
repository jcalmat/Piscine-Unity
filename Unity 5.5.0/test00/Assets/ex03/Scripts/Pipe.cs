using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	static float speed = 1;
	public Bird bird;

	//checkIncrement is used to count the score and increase the speed only once by pipe
	private bool checkIncrement = false;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("bird");
		bird = go.GetComponent<Bird>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!bird.isDead) {
			// Move the pipes
			transform.Translate(Vector3.left * speed * Time.deltaTime);
			// If the pipe is out of the screen, put it at the beginning
			if (transform.position.x < -4.7)
				transform.position = new Vector3(4.7f, transform.position.y, transform.position.z);
			// If the bird is in the pipe's zone, set the bool at true 
			if (transform.position.x <= 1.37 && transform.position.x >= -0.87)
				bird.checkIfBirdIsDead();
			if (transform.position.x > -0.87)
				checkIncrement = true;
			if (transform.position.x <= -0.87 && !bird.isDead && checkIncrement) {
				checkIncrement = false;
				speed += 0.2f;
				bird.score += 5;
			}
		}
	}
}
