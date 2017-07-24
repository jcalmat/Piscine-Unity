using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mayaScript : MonoBehaviour {

	public LayerMask mask;
	private Vector3 destination;
	[HideInInspector] public NavMeshAgent agent;
	private Animator animator;
	private SkillsManager skills;
	private GameObject currentEnemy;
	private bool isAttacking = false;
	[HideInInspector] public float STR = 10;
	[HideInInspector] public float AGI = 15;
	[HideInInspector] public float CON = 12;
	[HideInInspector] public float Armor = 50;
	public float HP = 20;
	public float minDamage_base = 5;
	public float minDamage = 5;
	public float maxDamage_base = 10;
	public float maxDamage = 10;
	[HideInInspector] public int Level = 1;
	public float XP = 0;
	[HideInInspector] public float Money = 0;
	[HideInInspector] public float XPToNextLevel = 50;
	public int spendPoints = 0;
	public float maxHP;
	public Scrollbar lifebar;
	public Text lifeText;
	public Scrollbar xpbar;
	public Text xpText;
	public GameObject enemyBarGameObject;
	public Scrollbar enemyScrollbar;
	public Text enemyBarText;
	public List<GameObject> inventory;
	public Animation AttackAnim;
	public bool canMove = true;

	void Start () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		skills = GetComponent<SkillsManager> ();
		maxHP = 5 * CON;
		HP = maxHP;

		StartCoroutine ("regainLife");
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform.tag == "healPotion") {
			HP += 20;
			if (HP > maxHP)
				HP = maxHP;
			Destroy (other.gameObject);
		}
	}

	void LateUpdate() {
		//HP = 5 * CON;
		if (HP > maxHP)
			HP = maxHP;
		maxHP = 5 * CON;
//		minDamage = STR / 2;
//		maxDamage = minDamage + minDamage / 5;
	}

	void Update () {
		RaycastHit hit;
		minDamage = STR / 2;
		maxDamage = minDamage + minDamage / 5;
		//press Q for fireball
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				destination.y = transform.position.y + 1;
				skills.FireBallSpell (destination);
			}
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				destination.y = transform.position.y + 1;
				skills.MultiFireBallSpell (destination);
			}
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				skills.InvokeWall (destination, transform.rotation);
			}
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				if (Vector3.Distance(destination, transform.position) < 15)
					skills.Blink (destination);
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				if (hit.transform.gameObject.tag == "enemy" && Vector3.Distance(hit.transform.position, transform.position) < 20) {
					skills.ChainLightning (hit.transform.gameObject);
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			skills.DrainLife ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			skills.heal ();
		}
			

		//Key Mouse0 -> raycast -> hit enemy / hit map
		if (Input.GetKey (KeyCode.Mouse0) && canMove) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				destination = hit.point;
				if (hit.transform.gameObject.tag == "enemy") {
					currentEnemy = hit.transform.gameObject;
					if (currentEnemy.GetComponent<enemyScript> ().HP > 0)
						enemyBarGameObject.SetActive (true);
					if (Vector3.Distance (currentEnemy.transform.position, transform.position) > 2.3f)
						agent.destination = currentEnemy.transform.position;
					else
						transform.LookAt (currentEnemy.transform.position);
				} else if (hit.transform.gameObject.tag == "weapon" && hit.transform.gameObject.GetComponent<items>().itemPosition == items.itemPositionEnum.GROUND && inventory.Count < 13
				&& (hit.transform.position - transform.position).magnitude < 3.0f) {
					inventory.Add (hit.transform.gameObject);
					hit.transform.gameObject.GetComponent<items> ().itemPosition = items.itemPositionEnum.INVENTORY;
					// minDamage += hit.transform.gameObject.GetComponent<items> ().DMG;
					// maxDamage = minDamage + minDamage / 5;
//					Animation anim = GetComponent<Animation>();
//					anim["AttackMaya"].speed = hit.transform.gameObject.GetComponent<items> ().ATTACKSPEED / 5;
//					AnimationUtility.GetAnimationClips(GameObject).speed += hit.transform.gameObject.GetComponent<items> ().ATTACKSPEED / 5;
				} else {
					enemyBarGameObject.SetActive (false);
					untargetEnemy ();
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

		//Display life bar/xp bar
		if (HP < 0) {
			HP = 0;
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		lifebar.size = HP / maxHP;
		xpbar.size = XP / XPToNextLevel;
		lifeText.text = ((int)HP).ToString() + " / " + ((int)maxHP).ToString();
		xpText.text = "Level " + Level.ToString ();
		if (currentEnemy != null) {
			enemyBarText.text = ((int)currentEnemy.GetComponent<enemyScript> ().HP).ToString () + " / " + ((int)currentEnemy.GetComponent<enemyScript> ().maxHP).ToString () + " - Level " + currentEnemy.GetComponent<enemyScript>().Level.ToString();
			enemyScrollbar.size = currentEnemy.GetComponent<enemyScript> ().HP / currentEnemy.GetComponent<enemyScript> ().maxHP;
		}
	}

	void dealDamage() {
		//dealDamage() is called at the end of the animation. Checking if the player missed his attack and deal damage if it's not the case
		if (currentEnemy != null && Random.Range (0, 100) < 95 + AGI - currentEnemy.GetComponent<enemyScript> ().AGI) {
			currentEnemy.GetComponent<enemyScript> ().takeDamage (Random.Range (minDamage, maxDamage) * (1 - currentEnemy.GetComponent<enemyScript> ().Armor / 200));
			if (currentEnemy.GetComponent<enemyScript> ().HP <= 0) {
				enemyBarGameObject.SetActive (false);
			}
		} else
			Debug.Log ("miss !");

		untargetEnemy ();
	}

	void untargetEnemy() {
		if (currentEnemy != null) {
			currentEnemy = null;
		}
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

	IEnumerator regainLife() {
		while (true) {
			if (HP < maxHP)
				HP += 1;
			yield return new WaitForSeconds (2f);
		}
	}
}
