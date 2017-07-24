using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	public GameObject[] characters;
	private int currentCharacter = 0;
	public float speed = 0.2f;
	public float height = 10f;

	private camera cam;
	// Use this for initialization
	void Start () {
		characters = GameObject.FindGameObjectsWithTag("hero");
		cam = GameObject.Find("Main Camera").GetComponent<camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//Reset the level
		if (Input.GetKey("r"))
			Application.LoadLevel(0);
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
		if (Input.GetKeyDown("space"))
			characters[currentCharacter].transform.Translate(Vector3.up * height * Time.deltaTime);
		//Center the camera
		cam.centerCamera(characters[currentCharacter].transform.position.x, characters[currentCharacter].transform.position.y);

		//Avoid rotation of the current character
		characters[currentCharacter].transform.rotation = new Quaternion(characters[currentCharacter].transform.rotation.x, characters[currentCharacter].transform.rotation.y, 0f, 0f);
	}
}
