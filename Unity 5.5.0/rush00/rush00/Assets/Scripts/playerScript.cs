using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float speed = 8f;
	public LayerMask ObstacleMask;
	[HideInInspector]public bool WeaponEquip = false;
	[HideInInspector]public WeaponScript Weapon;
	public GameObject BloodEffect;
	public GameObject legs;
	public List<AudioClip> DeathSound;
	public AudioClip dropWeaponSound;
	public AudioClip takeWeaponSound;
	[HideInInspector] public bool Alive = true;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.tag);
		if (other.tag == "finalCar") {
			GameManager.gm.ReachTheCar ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (!Alive || GameManager.gm.end) {
			return;
		}

		//Rotation
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

		//Take Weapon
		if (Input.GetKeyDown (KeyCode.E)) {
			//Check if player are on weapon
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.zero);
			if (hit && hit.collider.gameObject.tag == "Weapon") {
				if (WeaponEquip) {
					DropWeapon ();
				}
				TakeWeapon(hit.collider.gameObject);
			} else {
				Debug.Log ("Nothing to take");
			}
		}

		//Drop Weapon
		if (Input.GetMouseButtonDown (1)) {
			DropWeapon ();
		}

		//Fire
		if (Input.GetMouseButton (0)) {
			if (WeaponEquip) {
				Weapon.Fire ();
			}
		}

		//Movement
		Vector3 Move = Vector3.zero;
		if (Input.GetKey (KeyCode.W))
			Move += Vector3.up;			
		if (Input.GetKey (KeyCode.S))
			Move += Vector3.down;			
		if (Input.GetKey (KeyCode.A))
			Move += Vector3.left;			
		if (Input.GetKey (KeyCode.D))
			Move += Vector3.right;

		if (Move == Vector3.zero)
			legs.GetComponent<Animator> ().SetBool ("isMoving", false);
		else
			changePosition (Move);
	}

	void OnCollisionEnter2D(Collision2D Collision) {
		if (Collision.gameObject.tag == "Bullet") {
			Debug.Log ("Player Hit");
			Death ();
		}
	}

	void OnTriggerStay2D(Collider2D Collision) {
		if (Collision.gameObject.tag == "Door")
			speed = 2;
	}

	void OnTriggerExit2D(Collider2D Collision) {
		if (Collision.gameObject.tag == "Door")
			speed = 7;
	}

	void TakeWeapon(GameObject weapon) {
		Debug.Log ("Take Weapon");
		GetComponent<AudioSource> ().clip = takeWeaponSound;
		GetComponent<AudioSource> ().Play ();
		Weapon = weapon.GetComponent<WeaponScript>().Taken(transform);
		WeaponEquip = true;

	}

	void DropWeapon() {
		if (WeaponEquip) {
			Debug.Log ("Drop Weapon");
			GetComponent<AudioSource> ().clip = dropWeaponSound;
			GetComponent<AudioSource> ().Play ();
			Weapon.Dropped ();
			WeaponEquip = false;
		}
	}

	void changePosition(Vector3 direction) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, step);
		legs.GetComponent<Animator>().SetBool ("isMoving", true);
	}

	void Death() {
		Alive = false;
		Destroy (Instantiate (BloodEffect, transform.position, transform.rotation), 0.5f);
		if (!GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().clip = DeathSound [Random.Range (0, DeathSound.Count)];
			GetComponent<AudioSource> ().Play ();
		}
		GetComponent<Animator> ().SetBool ("dead", true);
		Destroy (gameObject, 1.5f);
		GameManager.gm.PlayerKilled ();
	}
}
