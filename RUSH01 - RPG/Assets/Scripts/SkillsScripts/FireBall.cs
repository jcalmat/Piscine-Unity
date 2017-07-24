using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

//	private Renderer rend;

	private Vector3 dir;
	private Rigidbody rb;
	private enemyScript enemy;
	public float damage = 20F;
	public float lifetime = 2;

	float speed = 12F;

	void Start() {
//		rend = GetComponent<Renderer>();
	}

	public void direction (Vector3 newdir) {
		rb = GetComponent<Rigidbody> ();
		dir = newdir - transform.position;
		rb.velocity = dir.normalized * speed;
		Destroy(gameObject, lifetime);
	}

	void OnTriggerEnter (Collider col) {
		Debug.Log (col.tag);
		if (col.tag == "Player")
			return;
		if (col.tag == "enemy") {
			enemy = col.gameObject.GetComponent<enemyScript> ();
			enemy.takeDamage (damage);
		}
		Destroy (gameObject);
	}
}


/*
 
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				destination.y = transform.position.y + 1;
				skills.FireBallSpell (destination);
			}
		}

*/