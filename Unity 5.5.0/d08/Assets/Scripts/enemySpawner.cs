using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

	public GameObject[] enemy;
	private GameObject currentEnemy;
	private bool enemyWillSpawn = false;

	// Use this for initialization
	void Start () {
		currentEnemy = GameObject.Instantiate (enemy [Random.Range (0, 2)], transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentEnemy == null && !enemyWillSpawn) {
			StartCoroutine ("spawn");
		}
	}

	IEnumerator spawn() {
		enemyWillSpawn = true;
		yield return new WaitForSeconds (15);
		Debug.Log ("Spawing new enemy at " + transform.position);
		currentEnemy = GameObject.Instantiate (enemy [Random.Range (0, 2)], transform.position, Quaternion.identity);
		enemyWillSpawn = false;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position, transform.position + transform.up * 15);
	}
}
