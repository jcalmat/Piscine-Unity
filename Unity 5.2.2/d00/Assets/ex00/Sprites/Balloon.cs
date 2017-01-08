using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	private float lastActionTime = 0;
	private float maxTimeInterval = 3.0f;
	private float niffler = 75;

	void Update () {
		if (Time.time - lastActionTime > maxTimeInterval) {
			Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time)  + "s");
			Destroy(gameObject);
			Application.Quit();
		}

		if (Input.GetKeyDown("space") && niffler > 10) {
			if (gameObject.transform.localScale.x >= 5) {
				Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time)  + "s");
				Destroy(gameObject);
				Application.Quit();
			}
			Vector3 temp = new Vector3(gameObject.transform.localScale.x + 0.2f, gameObject.transform.localScale.y + 0.2f, gameObject.transform.localScale.z);
			gameObject.transform.localScale = temp;
			lastActionTime = Time.time;
			niffler -= 10;
		} else {
			if (niffler < 75)
				niffler += 0.3f;
			if (Time.time - lastActionTime < maxTimeInterval) {
				if (gameObject.transform.localScale.x > 1) {
					Vector3 temp = new Vector3(gameObject.transform.localScale.x - 0.005f, gameObject.transform.localScale.y - 0.005f, gameObject.transform.localScale.z);
					gameObject.transform.localScale = temp;		
				}
			}
		}
	}
}
