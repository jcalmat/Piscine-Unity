using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	public float breath = 75;
	private bool dead = false;

	void endGame() {
		dead = true;
		Debug.Log ("Balloon life time: " + Mathf.RoundToInt (Time.time) + "s");
		Destroy (gameObject);
	}

	void Update () {
		if (!dead) {
			if (gameObject.transform.localScale.x <= 0.1)
				endGame ();
			if (Input.GetKeyDown ("space") && breath > 10) {
				if (gameObject.transform.localScale.x >= 5) {
					endGame ();
				}
				Vector3 temp = new Vector3 (gameObject.transform.localScale.x + 0.2f, gameObject.transform.localScale.y + 0.2f, gameObject.transform.localScale.z);
				gameObject.transform.localScale = temp;
				breath -= 10;
			} else {
				if (breath < 75)
					breath += 0.3f;
						Vector3 temp = new Vector3 (gameObject.transform.localScale.x - 0.005f, gameObject.transform.localScale.y - 0.005f, gameObject.transform.localScale.z);
						gameObject.transform.localScale = temp;
			}
		}
	}
}
