using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	private float speed;

	void Start () {
		speed = Random.Range(2f, 3f);
	}
		
	// Update is called once per frame
	void Update () {

		float precision = transform.position.y + 4;

		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if (transform.position.y < -4) {
			Destroy (transform.gameObject);
			if (tag == "a_letter")
				CubeSpawner.letterInstantiate [0] -= 1;
			else if (tag == "s_letter")
				CubeSpawner.letterInstantiate [1] -= 1;
			else if (tag == "d_letter")
				CubeSpawner.letterInstantiate [2] -= 1;			
		} else if ((transform.position.x == -1.41f) && (Input.GetKeyDown("a")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
			CubeSpawner.letterInstantiate [0] -= 1;
		} else if ((transform.position.x == 0) && (Input.GetKeyDown("s")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
			CubeSpawner.letterInstantiate [1] -= 1;
		} else if ((transform.position.x == 1.41f) && (Input.GetKeyDown("d")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
			CubeSpawner.letterInstantiate [2] -= 1;
		}
	}
}
