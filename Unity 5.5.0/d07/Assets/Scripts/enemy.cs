using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour {

	public float radius = 1000f;
	public Collider[] colliders;
	public LayerMask mask;

	private float life = 100;

	private GameObject nearestTank;
	private float nearestDistance = float.MaxValue;
	private float distance;

	public GameObject Tower;

	public AudioClip missileSound;
	public AudioClip gunSound;
	public GameObject particleGun;
	public GameObject particleMissile;

	private RaycastHit raycastHit;
	private bool fireGun = false;

	void Start() {
		StartCoroutine ("playGunSound");
	}

	void Update() {
		colliders = Physics.OverlapSphere (transform.position, radius, mask);
		foreach(Collider tank in colliders) {
			float distance = Vector3.Distance(transform.position, tank.gameObject.transform.position);
			if (distance < nearestDistance && distance > 1) {
				nearestDistance = distance;
				nearestTank = tank.gameObject;
				Tower.transform.LookAt (nearestTank.transform);
			}
		}

//		Vector3 fwd = Tower.transform.TransformDirection (new Vector3(0, 0, 2));
//		Debug.Log(fwd)
//		if (Physics.Raycast (Tower.transform.position, fwd, out raycastHit, 50)) {
//			fireGun = true;
		if (raycastHit.transform != null)
			Debug.DrawLine (transform.position, raycastHit.point, Color.red);
//		} else
//			fireGun = false;
//		GetComponent<NavMeshAgent> ().destination = nearestTank.transform.position;
	}

	void fire() {
		if (raycastHit.transform != null && raycastHit.transform.tag == "tank" || raycastHit.transform.tag == "player") {
			if (Random.Range (1, 5) == 2)
				return;
			raycastHit.transform.GetComponent<life> ().hp -= 10;
			Tower.GetComponent<AudioSource> ().clip = gunSound;
			Instantiate (particleGun, raycastHit.point, Quaternion.identity);
		}
	}

	IEnumerator playGunSound() {
		while (true) {
			if (nearestTank != null) {
				Vector3 fwd = Tower.transform.TransformDirection (new Vector3 (0, 0, 2));
				Debug.Log (fwd);
				if (Physics.Raycast (Tower.transform.position, fwd, out raycastHit, 50)) {
					fireGun = true;
				} else
					fireGun = false;
				GetComponent<NavMeshAgent> ().destination = nearestTank.transform.position;
				if (fireGun) {
					fire ();
					Tower.GetComponent<AudioSource> ().Play ();
				}
			}
			yield return new WaitForSeconds (.8f);
		}
	}

}
