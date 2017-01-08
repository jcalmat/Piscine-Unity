using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildings : MonoBehaviour {

	public float buildingsHP;
	public bool isDead = false;
	public Sprite deadSprite;
	private bool changedSprite = false;

	//townhall
	public float spawnTime = 10;
	public GameObject unit;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (buildingsHP <= 0) {
			isDead = true;
			gameObject.GetComponent<Collider2D> ().enabled = false;
			if (!changedSprite) {
				GetComponent<SpriteRenderer> ().sprite = deadSprite;
				changedSprite = true;
			}
		} else {
			if (transform.tag == "orc_townhall" || transform.tag == "human_townhall") {
				if (timer >= spawnTime) {
					timer = 0;
					GameObject.Instantiate(unit, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
				}
				timer += Time.deltaTime;
			}
		}
	}
}
