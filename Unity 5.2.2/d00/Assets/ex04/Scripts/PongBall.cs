using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour {
	
	public float speed = 5;
	public Vector3 direction;
	public Player player_1;
	public Player player_2;
	
	//checkIncrement is used to count the score and increase the speed only once by pipe
	private bool checkIncrement = false;
	
	
	// Use this for initialization
	void Start () {
		GameObject p1 = GameObject.Find("player_1");
		GameObject p2 = GameObject.Find("player_2");
		player_1 = p1.GetComponent<Player>();
		player_2 = p2.GetComponent<Player>();
		resetBall();
	}
	
	void resetBall() {
		int x = Random.Range(-3, 3);
		if (x == 0)
			x = 1;
		int y = Random.Range(-3, 3);
		if (y == 0)
			y = 1;	
		direction = new Vector3(x, y, 0f);
		transform.position = new Vector3(0, 0, 0);
	}
	
	void displayScore() {
		Debug.Log("Player 1: " + player_1.score + " | Player 2: " + player_2.score);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(direction * speed * Time.deltaTime);
		
		
		//Bounces on the wall
		if (transform.position.y > 4.8 || transform.position.y < -4.8)
			direction = new Vector3(direction.x, -direction.y, direction.z);
		else if (checkIncrement && (transform.position.x >= 7.8 && (player_2.transform.position.y + 2f >= transform.position.y && player_2.transform.position.y - 2 <= transform.position.y)) || checkIncrement && (transform.position.x <= -7.8 && (player_1.transform.position.y + 2f >= transform.position.y && player_1.transform.position.y - 2 <= transform.position.y))) {
			checkIncrement = false;		
			direction = new Vector3(-direction.x, direction.y, direction.z);
		}
		else if (transform.position.x >= 7.8) {
			player_1.score++;
			displayScore();
			resetBall();
		} else if (transform.position.x <= -7.8) {
			player_2.score++;
			displayScore();
			resetBall();
		}
		else if (transform.position.x < 8 && transform.position.x > -8)
			checkIncrement = true;
	}
}
