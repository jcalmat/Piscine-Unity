using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProfile : MonoBehaviour {

	public int[] unlockLevels;
	public int numberOfDeath = 0;
	public int numberOfRings = 0;
	public string bestScoreByLevel;

	public int angel_score = 0;
	public int oil_score = 0;
	public int flying_score = 0;
	public int chemical_score = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		numberOfDeath = PlayerPrefs.GetInt ("numberOfDeath");
		numberOfRings = PlayerPrefs.GetInt ("numberOfRings");
		angel_score = PlayerPrefs.GetInt ("angel_score");
		oil_score = PlayerPrefs.GetInt ("oil_score");
		flying_score = PlayerPrefs.GetInt ("flying_score");
		chemical_score = PlayerPrefs.GetInt ("chemical_score");
	}

	public int getAngelScore() {
		return angel_score;
	}

	public int getOilScore() {
		return oil_score;
	}

	public int getFlyingScore() {
		return flying_score;
	}

	public int getChemicalScore() {
		return chemical_score;
	}

	public int getPlayerDeath() {
		return numberOfDeath;
	}

	public int getPlayerRings() {
		return numberOfRings;
	}
}
