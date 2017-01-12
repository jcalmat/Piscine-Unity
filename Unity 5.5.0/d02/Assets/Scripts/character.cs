using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {

	public float speed = 10;
	public Vector3 endpoint;
	private Animator anim;
	public AudioClip[] walkSounds;
	public bool isSelected = false;
	public bool isAttacking = false;
	private double nextAttack = 0;
	public float attack = 3;
	public bool canAttack = false;

	public charactersManager manager;

	public GameObject currentEnemyAttacked;

	public enum characterDirection {
		UP,
		DOWN,
		RIGHT,
		LEFT,
		UP_RIGHT,
		UP_LEFT,
		DOWN_RIGHT,
		DOWN_LEFT,
		STAY
	};

	public characterDirection direction = characterDirection.STAY;


	void OnMouseDown() {
		if (!isSelected) {
			isSelected = true;

			if (Input.GetKey(KeyCode.LeftControl))
				manager.charactersList.Add(this);
			else {
				foreach (character character in manager.charactersList) {
					if(character.isSelected)
						character.isSelected = false;
				}
				manager.charactersList.Clear();
				manager.charactersList.Add(this);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "orc_town" || other.transform.tag == "orc" || other.transform.tag == "orc_townhall") {
			isAttacking = true;
			activateTrigger("stay");
			activateTrigger("attack");
			endpoint = transform.position;
		}
	}


	void OnCollisionExit2D(Collision2D other) {
		isAttacking = false;
//		activateTrigger("stay");
//		changeEndpoint (other.transform.position);
		//		currentEnemyAttacked = null;
	}

	void attackEnemy(GameObject enemy) {
//		transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
		if (isAttacking) {
			if (enemy.tag == "orc") {
				if (enemy.gameObject.GetComponent<orc> ().isDead) {
					currentEnemyAttacked = null;
					isAttacking = false;
					activateTrigger ("stay");
				} else {
					enemy.gameObject.GetComponent<orc> ().orcHP -= attack;
					Debug.Log ("Orc unit [" + enemy.gameObject.GetComponent<orc> ().orcHP + "/" + enemy.gameObject.GetComponent<orc> ().maxHP + "]HP has been attacked.");
				}
			} else if (enemy.tag == "orc_town" || enemy.tag == "orc_townhall") {
				if (enemy.gameObject.GetComponent<buildings> ().isDead) {
					currentEnemyAttacked = null;
					isAttacking = false;
					activateTrigger ("stay");
				} else {
					enemy.gameObject.GetComponent<buildings> ().buildingsHP -= attack;
					Debug.Log ("Orc building [" + enemy.gameObject.GetComponent<buildings> ().buildingsHP + "/" + enemy.gameObject.GetComponent<buildings> ().buildingMaxHP + "]HP has been attacked.");
				}
			}
		} else 
			changeEndpoint (enemy.transform.position);
		
		canAttack = false;
		nextAttack = Time.time + 0.4;
	}

	void Start() {
		manager = (charactersManager)GameObject.FindGameObjectWithTag("charactersManager").GetComponent<charactersManager>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > nextAttack) {
			canAttack = true;
		}
		if (currentEnemyAttacked != null && canAttack)
			attackEnemy (currentEnemyAttacked);
		else if (direction != characterDirection.STAY) {

			transform.position = Vector3.MoveTowards(transform.position, endpoint, speed * Time.deltaTime);
		}
		if (transform.position == endpoint && direction != characterDirection.STAY) {
			activateTrigger("stay");
			changeDirection(characterDirection.STAY);
		}
	}

	public void changeDirection(characterDirection _direction) {
		direction = _direction;
	}

	public void changeEndpoint(Vector3 _endpoint) {
		endpoint = _endpoint;
		Vector3 newDirection = new Vector3 (_endpoint.x - transform.position.x, _endpoint.y - transform.position.y, 0);
		if (newDirection.x == 0 && newDirection.y > 0) {
			activateTrigger ("walk_up");
			changeDirection (characterDirection.UP);
		} else if (newDirection.x == 0 && newDirection.y < 0) {
			activateTrigger ("walk_down");
			changeDirection (characterDirection.DOWN);
		} else if (newDirection.x < 0 && newDirection.y == 0) {
			activateTrigger ("walk_left");
			changeDirection (characterDirection.LEFT);
		} else if (newDirection.x > 0 && newDirection.y == 0) {
			activateTrigger ("walk_right");					
			changeDirection (characterDirection.RIGHT);
		} else if (newDirection.x < 0 && newDirection.y > 0) {
			activateTrigger ("walk_up_left");
			changeDirection (characterDirection.UP_LEFT);
		} else if (newDirection.x > 0 && newDirection.y > 0) {
			activateTrigger ("walk_up_right");
			changeDirection (characterDirection.UP_RIGHT);
		} else if (newDirection.x < 0 && newDirection.y < 0) {
			activateTrigger ("walk_down_left");
			changeDirection (characterDirection.DOWN_LEFT);
		} else if (newDirection.x > 0 && newDirection.y < 0) {
			activateTrigger ("walk_down_right");
			changeDirection (characterDirection.DOWN_RIGHT);
		}
	}

	public void activateTrigger(string trigger) {
		anim.SetTrigger(trigger);
	}
}
