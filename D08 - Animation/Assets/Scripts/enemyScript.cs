using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour {

	private GameObject maya;
	public float radius;
	public LayerMask mask;
	private NavMeshAgent agent;
	private Animator animator;
	private bool isAttacking = false;
	[HideInInspector] public float STR = 10;
	[HideInInspector] public float AGI = 15;
	[HideInInspector] public float CON = 12;
	[HideInInspector] public float Armor = 50;
	[HideInInspector] public float HP = 20;
	[HideInInspector] public float minDamage = 5;
	[HideInInspector] public float maxDamage = 10;
	[HideInInspector] public float Level = 1;
	[HideInInspector] public float XP = 100;
	[HideInInspector] public float Money = 0;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		Collider[] colliders = Physics.OverlapSphere (transform.position, radius, mask);
		if (colliders.Length > 0) {
			maya = colliders [0].transform.gameObject;
			if (HP > 0 && !isAttacking && maya.GetComponent<mayaScript>().HP > 0)
				agent.destination = maya.gameObject.transform.position;
		}

		if (agent.velocity.magnitude > 0.2) {
			if (maya.GetComponent<mayaScript>().HP > 0 && Vector3.Distance (transform.position, maya.transform.position) < 2) {
				transform.LookAt (maya.transform.position);
				animator.SetBool ("attack", true);
				isAttacking = true;
			} else {
				isAttacking = false;
				animator.SetBool ("run", true);
			}
		}
		else
			animator.SetBool ("run", false);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radius);
	}

	public void takeDamage(float damage) {
		HP -= damage;
		if (HP <= 0) {
			agent.Stop ();
			animator.SetTrigger ("death");
			StartCoroutine ("bodyDisappear");
		}
	}

	void dealDamage() {
		if (maya != null && Random.Range (0, 100) < 75 + AGI - maya.GetComponent<mayaScript> ().AGI) {
			maya.GetComponent<mayaScript> ().takeDamage (Random.Range (minDamage, maxDamage) * (1 - maya.GetComponent<mayaScript> ().Armor / 200));
		} else
			Debug.Log ("miss !");

		//Setting currentEnemy = null not to attack him again
		isAttacking = false;
		animator.SetBool ("attack", false);
	}

	IEnumerator bodyDisappear() {
		yield return new WaitForSeconds (4.25f);
		agent.enabled = false;
		Destroy (gameObject, 1f);
		//	transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y - 100, transform.position.z), Time.deltaTime * 10);
	}
}
