using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float Speed = 8;
	public float Range = 4;
	private float Moved = 0;
	private bool Launch = false;
	public Vector3 Destination;

	public void launch(Vector3 target) {
		Destination = target;
		Launch = true;
	}

	// Update is called once per frame
	void Update () {
		if (Launch) {
			Vector3 OldPosition = transform.position;
			float step = Speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, transform.position + Destination, step);
			Moved += Vector3.Distance (transform.position, OldPosition);
			if (Moved > Range) {
				Destroy (this);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D Collision) {
		if ((Collision.gameObject.tag == "Player" && Collision.gameObject.layer == 9) || (Collision.gameObject.tag == "Enemy" && Collision.gameObject.layer == 8) || Collision.gameObject.layer == 12) {
			Debug.Log ("Bullet have touch a target");
			Destroy (this);
		}
	}

	void OnDestroy() {
		Destroy (transform.root.gameObject);
	}
}
