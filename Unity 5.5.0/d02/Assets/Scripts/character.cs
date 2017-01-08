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
				Debug.Log("clear");
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
		endpoint = transform.position;
		if (other.transform.tag == "orc_town" || other.transform.tag == "orc" || other.transform.tag == "orc_townhall") {
			isAttacking = true;
			activateTrigger("attack");
			currentEnemyAttacked = other.gameObject;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		currentEnemyAttacked = null;
	}

	void attackEnemy(GameObject enemy) {
		enemy.gameObject.GetComponent<buildings>().buildingsHP -= attack;
		if (enemy.gameObject.GetComponent<buildings> ().isDead) {
			currentEnemyAttacked = null;
			activateTrigger("stay");
		}
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
		if (direction != characterDirection.STAY) {
			isAttacking = false;
			transform.position = Vector3.MoveTowards(transform.position, endpoint, speed * Time.deltaTime);
		}
		if (transform.position == endpoint && direction != characterDirection.STAY && !isAttacking) {
			activateTrigger("stay");
			changeDirection(characterDirection.STAY);
		}
	}

	public void changeDirection(characterDirection _direction) {
		direction = _direction;
	}

	public void changeEndpoint(Vector3 _endpoint) {
		endpoint = _endpoint;
	}

	public void activateTrigger(string trigger) {
		anim.SetTrigger(trigger);
	}
}
