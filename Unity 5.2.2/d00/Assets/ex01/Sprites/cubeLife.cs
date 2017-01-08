using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeLife : MonoBehaviour {

	private float speed;
	public cubeSpawner spawn;

	void Start () {
		speed = Random.Range(2f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if (transform.position.y < -4) {
			Destroy (transform.gameObject);
			if (tag == "a_letter")
				cubeSpawner.letterInstantiate [0] -= 1;
			else if (tag == "s_letter")
				cubeSpawner.letterInstantiate [1] -= 1;
			else if (tag == "d_letter")
				cubeSpawner.letterInstantiate [2] -= 1;			
		}

		float precision = transform.position.y + 4;
		if ((transform.position.x == -1.41f) && (Input.GetKeyDown("a")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
			cubeSpawner.letterInstantiate [0] -= 1;
		} else if ((transform.position.x == 0) && (Input.GetKeyDown("s")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
			cubeSpawner.letterInstantiate [1] -= 1;
		} else if ((transform.position.x == 1.41f) && (Input.GetKeyDown("d")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
			cubeSpawner.letterInstantiate [2] -= 1;
		}
	}
}
