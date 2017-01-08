using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	public float strength;
	private Ball ballScript;
	private bool stopPressSpace = false;
	// Use this for initialization
	void Start () {
		GameObject ball = GameObject.Find("ball");
		ballScript = ball.GetComponent<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!ballScript.gameIsEnded) {
			// Replace the club when the ball stopped
			if (ballScript.speed == 0) {
				transform.position = new Vector3(ballScript.transform.position.x - 0.05f, ballScript.transform.position.y + 1.1f, ballScript.transform.position.z);
			}
			if (Input.GetKey("space") && ballScript.speed == 0) {
				if (strength < 5f)
					strength += 0.1f;
				stopPressSpace = false;
			}
			else {
				if(!stopPressSpace) {
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
