using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	public float strength;
	private Ball ballScript;
	private bool stopPressSpace = true;

	private Vector3 prevPosition;

	// Use this for initialization
	void Start () {
		GameObject ball = GameObject.Find("ball");
		ballScript = ball.GetComponent<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!ballScript.gameIsEnded) {
			// Replace the club when the ball stopped

			if (ballScript.speed == 0 && stopPressSpace) {
				transform.position = new Vector3(ballScript.transform.position.x - 0.05f, ballScript.transform.position.y + 1.1f, ballScript.transform.position.z);
			}

			if (Input.GetKey("space") && ballScript.speed == 0) {
				if (stopPressSpace)
					prevPosition = transform.position;
				if (strength < 5f) {
					strength += 0.1f;
					transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
				}
				stopPressSpace = false;
			} 
			else if (strength != 0){
				if(!stopPressSpace) {
					transform.position = prevPosition;
					// if (ballScript.gameBegan) {
					// 	ballScript.score += 5;
					// 	Debug.Log("Your actual score is " + ballScript.score);
					// }
					stopPressSpace = true;
					ballScript.speed = strength;
					ballScript.direction = Vector3.down;
					strength = 0;
				}
			}
		}
	}
}
