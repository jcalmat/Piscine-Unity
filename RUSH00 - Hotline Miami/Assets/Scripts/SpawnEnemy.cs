using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

	public List<GameObject> Patern;
	public List<EnemyScript> EnemyList;

	// Use this for initialization
	void Start () {
		EnemyScript enemy = Instantiate (EnemyList [Random.Range (0, EnemyList.Count)], transform.position, transform.rotation);
		enemy.Patern = Patern;
		Destroy (this);
	}

	void OnDestroy() {
		Destroy (transform.root.gameObject);
	}
}
