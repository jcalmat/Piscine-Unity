using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pauseMenuScript : MonoBehaviour {

	public gameManager manager;
	public GameObject exitConfirm;
	public GameObject pauseMenu;
	private bool pause = false;

	// Use this for initialization
	void Start () {
		pauseMenu.GetComponent<CanvasGroup> ().alpha = 0;
		pauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		pauseMenu.GetComponent<CanvasGroup> ().interactable = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !pause)
			displayMenu ();
	}

	public void continueButtonClicked() {
		hideMenu ();
	}

	public void exitFirstButtonClicked() {
		displayExitMenu ();
	}

	public void secondContinueButtonClicked() {
		hideExitMenu ();
	}

	public void exitSecondButtonClicked() {
		SceneManager.LoadScene ("ex00");
	}

	void displayExitMenu() {
		pauseMenu.GetComponent<CanvasGroup> ().alpha = 0.5f;
		pauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		pauseMenu.GetComponent<CanvasGroup> ().interactable = false;
		exitConfirm.GetComponent<CanvasGroup> ().alpha = 1;
		exitConfirm.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		exitConfirm.GetComponent<CanvasGroup> ().interactable = true;
	}

	public void hideExitMenu() {
		Debug.Log ("hide exit menu");
		exitConfirm.GetComponent<CanvasGroup> ().alpha = 0;
		exitConfirm.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		exitConfirm.GetComponent<CanvasGroup> ().interactable = false;
		displayMenu ();
	}

	void displayMenu() {
		pause = true;
		manager.pause (true);
		pauseMenu.GetComponent<CanvasGroup> ().alpha = 1;
		pauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		pauseMenu.GetComponent<CanvasGroup> ().interactable = true;
	}

	public void hideMenu() {
		pause = false;
		manager.pause (false);
		pauseMenu.GetComponent<CanvasGroup> ().alpha = 0;
		pauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		pauseMenu.GetComponent<CanvasGroup> ().interactable = false;
	}
}
