using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mayaScript : MonoBehaviour {

	public LayerMask mask;
	private Vector3 destination;
	private NavMeshAgent agent;
	private Animator animator;
	private GameObject currentEnemy;
	private bool isAttacking = false;
	[HideInInspector] public float STR = 10;
	[HideInInspector] public float AGI = 15;
	[HideInInspector] public float CON = 12;
	[HideInInspector] public float Armor = 50;
	public float HP = 20;
	[HideInInspector] public float minDamage = 5;
	[HideInInspector] public float maxDamage = 10;
	[HideInInspector] public float Level = 1;
	[HideInInspector] public float XP = 0;
	[HideInInspector] public float Money = 0;
	[HideInInspector] float XPToNextLevel;

	void Start () {
		HP = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;

		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();	
	}
	
	void Update () {
		
		//Key Mouse0 -> raycast -> hit enemy / hit map
		if (Input.GetKey (KeyCode.Mouse0)) {
			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				if (hit.transform.gameObject.tag == "enemy") {
					currentEnemy = hit.transform.gameObject;
					if (Vector3.Distance (currentEnemy.transform.position, transform.position) > 2.3f)
						agent.destination = currentEnemy.transform.position;
					else
						transform.LookAt (currentEnemy.transform.position);
				} else {
					currentEnemy = null;
					agent.destination = hit.point;
				}
			}
		}

		//Checking if enemy is near the player and if he is not dead. In this case -> attack
		if (currentEnemy != null && currentEnemy.GetComponent<enemyScript> ().HP > 0 && Vector3.Distance (currentEnemy.transform.position, transform.position) < 2) {
			animator.SetBool ("attack", true);
		} else if (currentEnemy != null && Vector3.Distance (currentEnemy.transform.position, transform.position) > 2) {
			animator.SetBool ("attack", false);
			agent.destination = currentEnemy.transform.position;
		}

		//Checking agent velocity magnitude to know if player reaches his destination
		if (agent.velocity.magnitude > 0.2)
			animator.SetBool ("run", true);
		else
			animator.SetBool ("run", false);
	}

	void dealDamage() {
		//dealDamage() is called at the end of the animation. Checking if the player missed his attack and deal damage if it's not the case
		if (currentEnemy != null && Random.Range (0, 100) < 75 + AGI - currentEnemy.GetComponent<enemyScript> ().AGI) {
			currentEnemy.GetComponent<enemyScript> ().takeDamage (Random.Range (minDamage, maxDamage) * (1 - currentEnemy.GetComponent<enemyScript> ().Armor / 200));
			if (currentEnemy.GetComponent<enemyScript> ().HP <= 0) {
				XP += currentEnemy.GetComponent<enemyScript> ().XP;
				Money += currentEnemy.GetComponent<enemyScript> ().Money;
			}
		} else
			Debug.Log ("miss !");

		//Setting currentEnemy = null not to attack him again
		currentEnemy = null;
		animator.SetBool ("attack", false);
	}

	public void takeDamage(float damage) {
		HP -= damage;
		if (HP <= 0) {
			agent.Stop ();
			animator.SetTrigger ("death");
		}
	}

	void OnDrawGizmos() {
		//Draw some shit in the editor
		Gizmos.color = Color.red;
		if (currentEnemy != null)
			Gizmos.DrawLine (transform.position, currentEnemy.transform.position);
	}
}
