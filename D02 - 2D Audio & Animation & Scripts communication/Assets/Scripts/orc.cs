using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orc : MonoBehaviour {

	public float maxHP;
	public float orcHP;
	public bool isDead = false;
	public Sprite deadSprite;

	// Use this for initialization
	void Start () {
		maxHP = orcHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (orcHP <= 0 && !isDead) {
			isDead = true;
			gameObject.GetComponent<Collider2D> ().enabled = false;
			GetComponent<Animator>().SetTrigger("die");
		}
	}
}
