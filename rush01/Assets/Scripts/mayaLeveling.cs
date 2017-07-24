using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mayaLeveling : MonoBehaviour {

	public mayaScript maya;
	public Text STR;
	public Text AGI;
	public Text CONS;
	public Text ARMOR;
	public Text MINDMG;
	public Text MAXDMG;
	public Text MONEY;
	public Text points;

	public GameObject upgradeMenu;
	public GameObject statsMenu;

	void Update() {
		if (maya.spendPoints == 0)
			upgradeMenu.SetActive (false);
		points.text = maya.spendPoints.ToString();
		STR.text = maya.STR.ToString();
		AGI.text = maya.AGI.ToString();
		CONS.text = maya.CON.ToString();
		ARMOR.text = maya.Armor.ToString();
		MINDMG.text = maya.minDamage.ToString();
		MAXDMG.text = maya.maxDamage.ToString();
		MONEY.text = maya.Money.ToString();
	}


	public void pushDMG() {
		if (maya.spendPoints > 0) {
			maya.STR++;
			maya.spendPoints--;
		}
	}

	public void pushAGI() {
		if (maya.spendPoints > 0) {
			maya.AGI++;
			maya.spendPoints--;
		}
	}

	public void pushCON() {
		if (maya.spendPoints > 0) {
			maya.CON++;
			maya.spendPoints--;
		}
	}

	public void displayUpgradeMenu() {
		if (!upgradeMenu.activeSelf)
			maya.agent.destination = maya.transform.position;
		upgradeMenu.SetActive (!upgradeMenu.activeSelf);
		statsMenu.SetActive (upgradeMenu.activeSelf);
	}
}
