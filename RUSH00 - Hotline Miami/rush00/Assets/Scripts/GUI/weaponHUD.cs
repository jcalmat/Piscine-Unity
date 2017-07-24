using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponHUD : MonoBehaviour {

	public Text weaponName;
	public Text ammo;
	public playerScript player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.WeaponEquip) {
			ammo.text = player.Weapon.Ammo.ToString();
			weaponName.text = player.Weapon.Name;
		} else {
			weaponName.text = "No Weapon";
			ammo.text = "-";
		}
	}
}
