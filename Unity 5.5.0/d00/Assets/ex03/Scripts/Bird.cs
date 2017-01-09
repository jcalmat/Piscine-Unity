using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	public float gravity = 2;
	public float jump = 50;
	public bool isDead = false;
	public int score = 0;

	// Use this for initialization
	void Start () {
	}
	
	public void checkIfBirdIsDead() {
		if (transform.position.y < -0.02 || transform.position.y > 2.85)
			isDead = true;
		// if ((transform.position.y <= -0.87 || transform.position.y >= -0.89) && !isDead)
		// 	score += 5;
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			transform.Translate(gravity * Vector3.down * Time.deltaTime);
		// if (transform.eulerAngles.z < 315f)
		// 	transform.Rotate(0f, 0f, -20f);

			if (Input.GetKeyDown("space")) {
				transform.Translate(jump * Vector3.up * Time.deltaTime);
			}
			if (transform.position.y < -2.85)
				isDead = true;
		} else {
			Debug.Log("Score: " + score + "\nTime: " + Mathf.RoundToInt(Time.time) + "s");
			Destroy(gameObject);
		}
	}
}
