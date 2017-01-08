using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudUserInfos : MonoBehaviour {

	public gameManager manager;
	public Text userHP;
	public Text userEnergy;

	// Update is called once per frame
	void Update () {
		int playerHP = manager.playerHp;
		if (playerHP < 0)
			playerHP = 0;
		userHP.text = playerHP.ToString() + "hp";
		userEnergy.text = manager.playerEnergy.ToString();
	}
}
