using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void quitApplication() {
		Application.Quit ();
	}

	public void OnClick() {
		SceneManager.LoadScene ("ex01");
	}

	// Update is called once per frame
	void Update () {
	}
}
