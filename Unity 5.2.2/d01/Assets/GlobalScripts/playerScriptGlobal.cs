using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScriptGlobal : MonoBehaviour {

	public playerScriptGlobal[] characters;
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

		Application.LoadLevel(currentLevel);

		characters = new playerScriptGlobal[4];
		GameObject[] go = GameObject.FindGameObjectsWithTag("hero");
		for(int i = 0; i < go.Length ; i++)
			characters[i] = go[i].GetComponent<playerScriptGlobal>();

		cam = GameObject.Find("Main Camera").GetComponent<camera>();
	}
	
	 void OnCollisionEnter2D(Collision2D coll) {
        	Debug.Log(coll.gameObject.tag);
        if((coll.gameObject.tag == "wall" || coll.gameObject.tag == "hero" || coll.gameObject.tag == "platform" || coll.gameObject.tag == "vPlatform" || coll.gameObject.tag == "hPlatform") && coll.contacts.Length > 0) {
        	var normal = coll.contacts[0].normal;
    		if (normal.y > 0) {
      			characters[currentCharacter].canJump = true;
    		}
		}
    }

    void OnTriggerEnter2D(Collider2D other) {
		if (transform.name == "thomas" && other.gameObject.transform.name == "red_exit")
			thomas_exit = true;
		if (transform.name == "claire" && other.gameObject.transform.name == "blue_exit")
			claire_exit = true;
		if (transform.name == "john" && other.gameObject.transform.name == "yellow_exit")
			john_exit = true;
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

		//check if all characters reach the exit
		if (thomas_exit && claire_exit && john_exit){
			if(currentLevel + 1 <= Application.levelCount)
				Application.LoadLevel(++currentLevel);
			else
				Application.LoadLevel(1);
		}

		//Reset the level
		if (Input.GetKey("r"))
			Application.LoadLevel(currentLevel);
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
		if (Input.GetKey("left"))
			characters[currentCharacter].transform.Translate(Vector3.left * speed * Time.deltaTime);			

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
	}
}
