using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townHallSpawner : MonoBehaviour {

	//townhall
	public float spawnTime = 10;
	public GameObject unit;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= spawnTime) {
			timer = 0;
			GameObject.Instantiate(unit, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
		}
		timer += Time.deltaTime;	
	}
}
