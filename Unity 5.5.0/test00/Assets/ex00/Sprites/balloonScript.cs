using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonScript : MonoBehaviour {

	public GameObject GO;
	private float lastActionTime = 0;
	private float maxTimeInterval = 3.0f;
	private float niffler = 75;

	void Update () {
		if (Time.time - lastActionTime > maxTimeInterval) {
			Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time)  + "s");
			Destroy(GO);
			Application.Quit();
		}

		if (Input.GetKeyDown("space") && niffler > 10) {
			if (GO.transform.localScale.x >= 5) {
				Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time)  + "s");
				Destroy(GO);
				Application.Quit();
			}
			Vector3 temp = new Vector3(GO.transform.localScale.x + 0.2f, GO.transform.localScale.y + 0.2f, GO.transform.localScale.z);
			GO.transform.localScale = temp;
			lastActionTime = Time.time;
			niffler -= 10;
		} else {
			if (niffler < 75)
				niffler += 0.3f;
			if (Time.time - lastActionTime < maxTimeInterval) {
				if (GO.transform.localScale.x > 1) {
					Vector3 temp = new Vector3(GO.transform.localScale.x - 0.005f, GO.transform.localScale.y - 0.005f, GO.transform.localScale.z);
					GO.transform.localScale = temp;		
				}
			}
		}
	}
}
