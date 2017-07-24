using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask soundTokenMask;
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	private EnemyScript enemy;

	[HideInInspector]
	public Transform visibleTarget;

	private GameObject[] Doors;

	void Start() {
		Doors = GameObject.FindGameObjectsWithTag ("Door");
		enemy = GetComponent<EnemyScript> ();
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}

	void Update() {
		if (visibleTarget != null && enemy.Alive) {
			enemy.RefreshTarget (visibleTarget.gameObject);
			if (visibleTarget.tag == "Player") {
				enemy.Fire ();
			}
		}
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTarget = null;
		visibleTarget = FindVisibleTargetsInFullRange ();
		if (visibleTarget == null) {
			visibleTarget = FindInvisibleTargetsInReduceRange ();
		}
		if (visibleTarget == null) {
			visibleTarget = FindSoundToken ();
		}
	}

	Transform FindVisibleTargetsInFullRange() {
		Collider2D targetInViewRadius = Physics2D.OverlapCircle (transform.position, viewRadius, targetMask);
		if (targetInViewRadius != null) {
			Transform target = targetInViewRadius.transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (Vector3.Angle (-transform.up, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);
				if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					return target;
				}
			}
		}
		return null;
	}

	Transform FindInvisibleTargetsInSmallRange() {
		Collider2D targetInViewRadius = Physics2D.OverlapCircle (transform.position, viewRadius / 3, targetMask);
		if (targetInViewRadius != null) {
			Transform target = targetInViewRadius.transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			float dstToTarget = Vector3.Distance (transform.position, target.position);

			if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
				return target;
			} else {
				return GetBestWay (target);
			}
		}
		return null;
	}

	Transform FindInvisibleTargetsInReduceRange() {
		Collider2D targetInViewRadius = Physics2D.OverlapCircle (transform.position, viewRadius / 10, targetMask);
		if (targetInViewRadius != null) {
			Transform target = targetInViewRadius.transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			float dstToTarget = Vector3.Distance (transform.position, target.position);

			if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
				return target;
			} else {
				return GetBestWay (target);
			}
		}
		return null;
	}

	Transform FindSoundToken() {
		Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll (transform.position, viewRadius * 1.1f, soundTokenMask);
		if (targetInViewRadius.Length > 0) {
			float BestTime = 0;
			float ActuTime = 0;
			Transform BestSound = null;
			foreach (Collider2D target in targetInViewRadius) {
				ActuTime = target.GetComponent<SoundScript> ().CreatedTime;
				if (ActuTime > BestTime) {
					BestSound = target.transform;
					BestTime = ActuTime;
				}
			}
			Vector3 dirToTarget = (BestSound.position - transform.position).normalized;
			float dstToTarget = Vector3.Distance (transform.position, BestSound.position);
			if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
				return BestSound;
			} else {
				return GetBestWay (BestSound);
			}
		}
		return null;
	}


	Transform GetBestWay(Transform target) {
		Transform GoodDoor = null;
		float DoorEnemy = 0;
		float DoorPlayer = 0;
		float LastBestScore = 100;
		foreach (GameObject door in Doors) {
			DoorEnemy = Vector3.Distance (transform.position, door.transform.position);
			DoorPlayer = Vector3.Distance (target.position, door.transform.position);
			if (LastBestScore > ((DoorEnemy * DoorEnemy + DoorPlayer * DoorPlayer) / 2)) {
				LastBestScore = (DoorEnemy * DoorEnemy + DoorPlayer * DoorPlayer) / 2;
				GoodDoor = door.transform;
			}
		}
		if (GoodDoor != null) {
			enemy.RefreshTargetDoor (GoodDoor.gameObject);
		}
		return target;
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), -Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
	}
}
