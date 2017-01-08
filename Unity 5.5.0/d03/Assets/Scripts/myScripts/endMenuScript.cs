using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endMenuScript : MonoBehaviour {

	public Text title;
	public Text score;
	public Text grade;
	public Text buttonText;
	public gameManager manager;
	private enum gameState{WIN, LOOSE, INGAME};
	private gameState state = gameState.INGAME;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (manager.playerHp <= 0)
			state = gameState.LOOSE;
		else if (manager.lastWave)
			state = gameState.WIN;
		if (state == gameState.LOOSE)
			loose ();
		else if (state == gameState.WIN)
			win ();
	}

	public void buttonClicked() {
		if (state == gameState.LOOSE)
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		else if (state == gameState.WIN)
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);	
	}

	void loose() {
		manager.pause (true);
		title.text = "You loose :(";
		score.text = manager.score.ToString ();
		grade.text = "-";
		buttonText.text = "Retry";
		displayMenu ();
	}

	void win() {
		manager.pause (true);
		title.text = "You win !";
		score.text = manager.score.ToString ();
		grade.text = "-"; //TODO
		buttonText.text = "Next level";
		displayMenu ();
	}

	void displayMenu() {
		GetComponent<CanvasGroup> ().alpha = 1;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		GetComponent<CanvasGroup> ().interactable = true;
	}
}
