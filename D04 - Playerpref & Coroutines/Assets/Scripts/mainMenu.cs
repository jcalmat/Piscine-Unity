using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log (SceneManager.GetActiveScene ().buildIndex);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}
