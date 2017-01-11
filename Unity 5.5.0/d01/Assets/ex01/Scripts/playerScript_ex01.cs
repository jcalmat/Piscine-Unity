using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex01 : MonoBehaviour {

	public playerScript_ex01[] characters;
	public int currentCharacter;
	public float speed = 0.2f;
	public float height;
	public bool canJump = true;
	public int currentLevel = 1;

	static bool thomas_exit = false;
	static bool claire_exit = false;
	static bool john_exit = false;

	private camera cam;
	// Use this for initialization
	void Start () {

		currentLevel = Application.loadedLevel;

		characters = new playerScript_ex01[4];
		GameObject[] go = GameObject.FindGameObjectsWithTag("hero");
		for(int i = 0; i < go.Length ; i++)
			characters[i] = go[i].GetComponent<playerScript_ex01>();

		cam = GameObject.Find("Main Camera").GetComponent<camera>();
	}
	
	void checkWinCondition() {
		//check if all characters reach the exit
		if (thomas_exit && claire_exit && john_exit){
			Debug.Log("Win!");
			thomas_exit = false;
			claire_exit = false;
			john_exit = false;
			loadLevel();
		}
	}

	void loadLevel() {
		if(currentLevel + 1 <= SceneManager.sceneCount - 1)
			SceneManager.LoadScene (++currentLevel);
		else
			SceneManager.LoadScene (1);
	}

	 void OnCollisionEnter2D(Collision2D coll) {
        if((coll.gameObject.tag == "wall" || coll.gameObject.tag == "hero" || coll.gameObject.tag == "platform" || coll.gameObject.tag == "vPlatform" || coll.gameObject.tag == "hPlatform") && coll.contacts.Length > 0) {
        	if (coll.gameObject.tag != "hero")
	        	transform.parent = coll.transform;
        	var normal = coll.contacts[0].normal;
			if (normal.y > 0) {
				canJump = true;
    		}
		}
    }

    void OnTriggerEnter2D(Collider2D other) {
		if (transform.name == "thomas" && other.gameObject.transform.name == "red_exit"){
			thomas_exit = true;
			checkWinCondition();
		}
		if (transform.name == "claire" && other.gameObject.transform.name == "blue_exit") {
			claire_exit = true;
			checkWinCondition();
		}
		if (transform.name == "john" && other.gameObject.transform.name == "yellow_exit") {
			john_exit = true;
			checkWinCondition();
		}
    }

    void OnTriggerExit2D(Collider2D other) {
		if (transform.name == "thomas" && other.gameObject.transform.name == "red_exit")
			thomas_exit = false;
		if (transform.name == "claire" && other.gameObject.transform.name == "blue_exit")
			claire_exit = false;
		if (transform.name == "john" && other.gameObject.transform.name == "yellow_exit")
			john_exit = false;			
    }

	// Update is called once per frame
	void Update () {

		//Reset the level
		if (Input.GetKey ("r"))
			SceneManager.LoadScene (currentLevel);
		//Get a character
		if (Input.GetKey("1"))
			currentCharacter = 0;
		if (Input.GetKey("2"))
			currentCharacter = 1;
		if (Input.GetKey("3"))
			currentCharacter = 2;
		//Move
		if (Input.GetKey("right"))
			characters[currentCharacter].transform.Translate(Vector3.right * speed * Time.deltaTime);
		if (Input.GetKey ("left"))
			characters [currentCharacter].transform.Translate (Vector3.left * speed * Time.deltaTime);

		if (characters[currentCharacter].canJump && Input.GetKeyDown("space")) {
			characters[currentCharacter].canJump = false;
			characters[currentCharacter].transform.Translate(Vector3.up * height * Time.deltaTime);
		}

		//Center the camera
		cam.centerCamera(characters[currentCharacter].transform.position.x, characters[currentCharacter].transform.position.y);

		//Change character speed & jump depending on the current one
		if (characters[currentCharacter].transform.name == "thomas") {
			speed = 0.3f;
			height = 40f;
		} else if (characters[currentCharacter].transform.name == "claire") {
			speed = 0.2f;
			height = 20f;
		} else if (characters[currentCharacter].transform.name == "john") {
			speed = 0.4f;
			height = 60f;
		}

		//Debug
		if (Input.GetKey("d")) {
			thomas_exit = true;
			claire_exit = true;
			john_exit = true;
		}
	}
}
