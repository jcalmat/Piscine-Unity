using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudTextsScript : MonoBehaviour {

	public towerScript towerscript;
	public Text dmg;
	public Text cost;
	public Text range;
	public Text reload;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		dmg.text = towerscript.damage.ToString();
		cost.text = towerscript.energy.ToString ();
		range.text = towerscript.range.ToString ();
		reload.text = towerscript.fireRate.ToString ();
	}
}
