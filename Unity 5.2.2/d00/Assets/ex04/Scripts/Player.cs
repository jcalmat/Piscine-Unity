using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject player_1;
	public GameObject player_2;

	public int score = 0;
	private float speed = 25;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w") && player_1.transform.position.y < 5f)
				player_1.transform.Translate(Vector3.up * speed * Time.deltaTime);
		if (Input.GetKey("s") && player_1.transform.position.y > -5f)
				player_1.transform.Translate(Vector3.down * speed * Time.deltaTime);				
		if (Input.GetKey("up") && player_2.transform.position.y < 5f)
				player_2.transform.Translate(Vector3.up * speed * Time.deltaTime);
		if (Input.GetKey("down") && player_2.transform.position.y > -5f)
				player_2.transform.Translate(Vector3.down * speed * Time.deltaTime);
	}
}
