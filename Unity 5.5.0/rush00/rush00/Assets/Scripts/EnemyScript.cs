using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float speed = 8f;
	public GameObject BloodEffect;
	public List<Sprite> HeadSprite;
	public List<Sprite> BodySprite;
	public List<GameObject> Patern;
	private int PaternStep = 0;
	public LayerMask obstacleMask;
	[Range(0,360)]
	public float viewAngle;
	public GameObject Alert;
	public GameObject Stunt;
	public GameObject Head;
	public GameObject Body;
	public GameObject Legs;
	private GameObject Target;
	private GameObject TargetDoor;
	[HideInInspector]public bool Alive = true;

	public List<WeaponScript> weapons;
	public List<AudioClip> DeathSound;
	public WeaponScript currentWeapon;
	private int Dodge;

	// Use this for initialization
	void Start () {
		GameManager.gm.EnemyAdded ();
		currentWeapon = GameObject.Instantiate(weapons[Random.Range(0, weapons.Count)], transform.position, Quaternion.Euler(Vector3.zero));

		currentWeapon.Taken(transform);
		Head.GetComponent<SpriteRenderer>().sprite = HeadSprite[Random.Range(0, HeadSprite.Count)];
		Body.GetComponent<SpriteRenderer>().sprite = BodySprite[Random.Range(0, BodySprite.Count)];
		StartCoroutine ("SelectDodge");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gm.end)
			return;
		Vector3 Move = PathFinding();
		if (Move == Vector3.zero) {
			if (Target != null) {
				RotateToDest (Target.transform.position);
			}
			Legs.GetComponent<Animator> ().SetBool ("isMoving", false);
		} else {
			RotateToDest (Move);
			changePosition (Move);
		}
	}

	public void RefreshTarget(GameObject target) {
		Alert.SetActive (true);
		Target = target;
		StopCoroutine ("StopFollow");
		StartCoroutine ("StopFollow");
	}

	public void RefreshTargetDoor(GameObject target) {
		TargetDoor = target;
	}

	IEnumerator StopFollow() {
		yield return new WaitForSeconds(3f);
		Alert.SetActive (false);
		Target = null;
		TargetDoor = null;
	}

	IEnumerator SearchEnemy() {
		Legs.GetComponent<Animator> ().SetBool ("Searching", true);
		yield return new WaitForSeconds(5f);
		Legs.GetComponent<Animator> ().SetBool ("Searching", false);
	}

	IEnumerator Stun() {
		if (Alive) {
			Alive = false;
			Stunt.SetActive (true);
			yield return new WaitForSeconds (1f);
			Alive = true;
		}
		Stunt.SetActive (false);
	}

	IEnumerator SelectDodge() {
		while (true) {
			Dodge = Random.Range (0, 100);
			yield return new WaitForSeconds (3f);
		}
	}

	Vector3 PathFinding () {
		if (Target != null && Alive) {
			bool canSeeTarget = CanSeeTarget ();
			if (Vector3.Distance (Target.transform.position, transform.position) < (currentWeapon.Bullet.Range * 0.9) && canSeeTarget) {
				if (Target.tag == "SoundToken") {
					return Target.transform.position;
				}
				if (Vector3.Distance (Target.transform.position, transform.position) < (currentWeapon.Bullet.Range * 0.3)) {
					return -transform.up;
				}
			} else if (canSeeTarget) {
				if (Target.tag == "SoundToken") {
					return Target.transform.position;
				}
				if (Vector3.Distance (Target.transform.position, transform.position) > currentWeapon.Bullet.Range) {
					return Target.transform.position;
				} else {
					return Vector3.zero;
				}
			} else if (TargetDoor != null) {
				if (Vector3.Distance (transform.position, TargetDoor.transform.position) < 1 && CanSeePositionTarget(TargetDoor.transform.position)) {
					Debug.Log ("I search the door");
					return Target.transform.position;
				} else {
					Debug.Log ("I see the door");
					return TargetDoor.transform.position;
				}
			}
		} else if (Patern.Count > 0) {
			if (Vector3.Distance (transform.position, Patern [PaternStep].transform.position) < 0.3) {
				PaternStep++;
			}
			if (PaternStep == Patern.Count) {
				PaternStep = 0;
			}
			return Patern [PaternStep].transform.position;
		}
		return Vector3.zero;
	}

	void RotateToDest(Vector3 destination) {
		Vector3 diff = destination- transform.position;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
	}

	bool CanSeeTarget() {
		Vector3 dirToTarget = (Target.transform.position - transform.position).normalized;
		float dstToTarget = Vector3.Distance (transform.position, Target.transform.position);
		return !Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask);
	}

	bool CanSeePositionTarget(Vector3 position) {
		Vector3 dirToTarget = (position - transform.position).normalized;
		float dstToTarget = Vector3.Distance (transform.position, position);
		return Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask);
	}

	void changePosition(Vector3 direction) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, direction, step);
		Legs.GetComponent<Animator>().SetBool ("isMoving", true);
	}

	void OnCollisionEnter2D(Collision2D Collision) {
		if (Collision.gameObject.tag == "Bullet") {
			Debug.Log ("Enemy Hit");
			Death ();
		}
		if (Collision.gameObject.tag == "Weapon") {
			if (Collision.gameObject.GetComponent<WeaponScript> ().NeedAmmo) {
				StartCoroutine ("Stun");
			} else {
				Death ();
			}
		}
	}

	public void Fire() {
		if (Alive) {
			currentWeapon.Fire (true);
		}
	}

	void Death() {
		if (Random.Range (0, 3) < 1) {
			currentWeapon.Dropped ();
		}
		Alive = false;
		if (!GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().clip = DeathSound [Random.Range (0, DeathSound.Count)];
			GetComponent<AudioSource> ().Play ();
		}
		GetComponent<Animator> ().SetBool ("Alive", false);
		Alert.SetActive (false);
//		GetComponent<Animator> ().Play ("EnemyDeath");
		Destroy (Instantiate (BloodEffect, transform.position, transform.rotation), 0.5f);
		Destroy (this);
	}

	void OnDestroy() {
		GameManager.gm.EnemyKilled ();
		Destroy (transform.root.gameObject);
	}
}
