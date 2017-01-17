using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	private int EnemyNumber = 0;
	public GameObject endMenu;
	[HideInInspector]public bool end = false;

	void Awake () {
		endMenu.SetActive (false);
		if (gm == null)
			gm = this;
	}

	public void EnemyAdded() {
		EnemyNumber++;
	}

	public void EnemyKilled() {
		EnemyNumber--;
		if (EnemyNumber <= 0) {
			Debug.Log ("No more enemy");
			end = true;
			if (endMenu != null) {
				endMenu.SetActive (true);
				endMenu.GetComponent<endMenuManager> ().state = endMenuManager.stateEnum.WIN;
			}
		}
	}

	public void PlayerKilled() {
		end = true;
		endMenu.SetActive (true);
		endMenu.GetComponent<endMenuManager>().state = endMenuManager.stateEnum.LOOSE;
	}

	public void ReachTheCar() {
		end = true;
		endMenu.SetActive (true);
		endMenu.GetComponent<endMenuManager> ().state = endMenuManager.stateEnum.WIN;
	}
}
