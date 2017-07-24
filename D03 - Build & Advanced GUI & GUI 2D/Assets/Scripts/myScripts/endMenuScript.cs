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
	public GameObject objmanager;
	private gameManager manager;
	private enum gameState{WIN, LOOSE, INGAME};
	private gameState state = gameState.INGAME;


	private int energyScore;
	private int hpscore;
	private int playerScore;

	// Use this for initialization
	void Start () {
		manager = objmanager.GetComponent<speedButtonsScript> ().manager;
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
		if (state == gameState.LOOSE) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		else if (state == gameState.WIN)
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	void loose() {
		manager.pause (true);
		title.text = "You loose :(";
		score.text = manager.score.ToString ();
		grade.text = "U SUXX";
		buttonText.text = "Retry";
		displayMenu ();
	}

	void win() {
		calculateScore ();
		manager.pause (true);
		title.text = "You win !";
		score.text = manager.score.ToString ();
		if (playerScore == 10)
				grade.text = "S";
		else if (playerScore > 8)
			grade.text = "A";
		else if (playerScore > 6)
			grade.text = "B";
		else if (playerScore > 4)
			grade.text = "C";
		else if (playerScore > 2)
			grade.text = "D";
		else
			grade.text = "-";
		buttonText.text = "Next level";
		displayMenu ();
	}

	void displayMenu() {
		GetComponent<CanvasGroup> ().alpha = 1;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		GetComponent<CanvasGroup> ().interactable = true;
	}

	public void hideMenu() {
		GetComponent<CanvasGroup> ().alpha = 0;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		GetComponent<CanvasGroup> ().interactable = false;
	}

	void calculateScore() {
		if (manager.playerHp == manager.playerMaxHp)
			hpscore = 5;
		else if (manager.playerHp > 17)
			hpscore = 4;
		else if (manager.playerHp > 13)
			hpscore = 3;
		else if (manager.playerHp > 7)
			hpscore = 2;
		else
			hpscore = 1;

		if (manager.playerEnergy > 500)
			energyScore = 5;
		else if (manager.playerEnergy > 300)
			energyScore = 4;
		else if (manager.playerEnergy > 250)
			energyScore = 3;
		else if (manager.playerEnergy > 100)
			energyScore = 2;
		else
			energyScore = 1;

		playerScore = energyScore + hpscore;
	}
}
