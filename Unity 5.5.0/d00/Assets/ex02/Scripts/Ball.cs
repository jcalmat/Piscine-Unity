using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed;
	public float deceleration;
	public Vector3 direction;
	public bool gameIsEnded = false;
	public bool ballIsMoving = false;
	public int score = -15;

	void Start() {
		direction = Vector3.down;
	}

	void Update () {
		if (speed > 0) {
			ballIsMoving = true;
			speed -= deceleration;
		}
		else {
			if (ballIsMoving && !gameIsEnded) {
				score += 5;
				Debug.Log("Your actual score is " + score);
			}
			ballIsMoving = false;
			speed = 0;
		}
		// Ball in movement
		transform.Translate(direction * speed * Time.deltaTime);
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.2f, 3.2f), Mathf.Clamp(transform.position.y, -4.65f, 4.65f), transform.position.z);
		// The ball is in the hole
		if (speed <= 2 && transform.position.y <= -2.8F && transform.position.y >= -3.15F && transform.position.x <= 0.17f && transform.position.x >= -0.17f) {
			speed = 0;
			if (!gameIsEnded) {
				if (score > 0)
					Debug.Log("Your score is " + score + ". You loose :(");
				else
					Debug.Log("Your score is " + score + ". You win !");

			}
			gameIsEnded = true;
			if (transform.localScale.x > 0) {
				Vector3 temp = new Vector3(transform.localScale.x - 0.005f, transform.localScale.y - 0.005f, transform.localScale.z);
				transform.localScale = temp;
			}
		}
		//Bounces on the wall
		if (transform.position.y >= 4.65 || transform.position.y <= -4.65)
			direction = new Vector3(direction.x, -direction.y, direction.z);
		else if (transform.position.x >= 3.2 || transform.position.x <= -3.2)
			direction = new Vector3(-direction.x, direction.y, direction.z);

	}
}
