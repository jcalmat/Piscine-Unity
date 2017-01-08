using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeLife : MonoBehaviour {

	public GameObject GO;
	private float speed;

	void Start () {
		speed = Random.Range(1f, 3f);

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if (transform.position.y < -4)
			Destroy(transform.gameObject);

		float precision = transform.position.y + 4;
		if ((transform.position.x == -1.41f) && (Input.GetKeyDown("a")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
		} else if ((transform.position.x == 0) && (Input.GetKeyDown("s")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
		} else if ((transform.position.x == 1.41f) && (Input.GetKeyDown("d")) && (transform.position.y < -3.6)) {
			Debug.Log("Precision: " + precision);
			Destroy(transform.gameObject);
		}
	}
}
