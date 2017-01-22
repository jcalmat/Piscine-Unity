using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyScript : MonoBehaviour {

	private GameObject maya;
	public float radius = 10;
	public LayerMask mask;
	private NavMeshAgent agent;
	private Animator animator;
	private bool isAttacking = false;
	public float STR = 10;
	public float AGI = 15;
	public float CON = 12;
	public float Armor = 50;
	public float HP = 20;
	public float minDamage = 5;
	public float maxDamage = 10;
	public int Level = 1;
	public float XP = 20;
	[HideInInspector] public float Money;
	[HideInInspector] public float maxHP;
	public GameObject lifebar;
	public GameObject potion;
	public Text prefab_dmg;
//	public GameObject[] weapons;
	mayaScript Smaya;

	// Use this for initialization
	void Start () {
		Level = GameManager.gm.getMayaLevel ();
		int points = 5 * (Level - 1);
		for (int i = 0; i < points; i++) {
			switch (Random.Range (0, 4)) {
			case 1:
				STR++;
				break;
			case 2:
				AGI++;
				break;
			case 3:
				CON++;
				break;
			case 4:
				Armor++;
				break;
			default:
				break;
			}
		}
		Money = Level * 3.5f;
		HP = 3 * CON;
		maxHP = HP;
		minDamage = STR / 3;
		maxDamage = minDamage + 4;
		maxHP = HP;
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

		//Display life bar
	//	lifebar.GetComponentInChildren<Scrollbar>().size = HP / maxHP;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radius);
	}

	public void takeDamage(float damage) {
		if (HP <= 0)
			return;
		if (!maya) {
			Collider[] colliders = Physics.OverlapSphere (transform.position, 25, mask);
			if (colliders.Length > 0) {
				maya = colliders [0].transform.gameObject;
				if (HP > 0 && !isAttacking && maya.GetComponent<mayaScript>().HP > 0)
					agent.destination = maya.gameObject.transform.position;
			}
		}
		Smaya = maya.GetComponent<mayaScript> ();

		HP -= damage;
		// Instantiate(prefab_dmg, transform.position, Quaternion.identity);
		if (HP <= 0) {
			agent.Stop ();
			Smaya.XP += XP;
			if (Smaya.XP > Smaya.XPToNextLevel) {
				Smaya.Level++;
				Smaya.spendPoints += 5;
				Smaya.XP -= Smaya.XPToNextLevel;
			}
			Smaya.Money += Money;
			animator.SetTrigger ("death");
			StartCoroutine ("bodyDisappear");
		}

		// gameObject.GetComponent<Collider>().enabled = false;
		// gameObject.GetComponent<NavMeshAgent>().enabled = false;
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
		if (Random.Range (0, 10) > 5)
			GameObject.Instantiate (potion, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
		if (Random.Range (0, 10) > 7) {
			GameObject weapon = GameManager.gm.weapons [(Random.Range (0, GameManager.gm.weapons.Length))];
			GameObject go = GameObject.Instantiate (weapon, new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z), weapon.transform.rotation);
			go.GetComponent<items> ().DMG = Random.Range (3 * maya.GetComponent<mayaScript> ().Level - 2, 3 * maya.GetComponent<mayaScript> ().Level + 2);
		}
		yield return new WaitForSeconds (4.25f);
		agent.enabled = false;
		Destroy (gameObject, 1f);
		//	transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y - 100, transform.position.z), Time.deltaTime * 10);
	}
}
