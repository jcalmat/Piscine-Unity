using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items : MonoBehaviour {

	public enum itemPositionEnum {GROUND, INVENTORY};
	public itemPositionEnum itemPosition = itemPositionEnum.GROUND;
	public float DMG;
	public float ATTACKSPEED;
	public int quality;
	// Use this for initialization
	void Start () {
		quality = Random.Range(0, 100);
		if (quality < 2)
			quality = 1;
		else if (quality < 7)
			quality = 2;
		else if (quality < 20)
			quality = 3;
		else if (quality < 40)
			quality = 4;
		else
			quality = 5;
		DMG += (25 - quality * 5);
		ATTACKSPEED += (5 - quality * 1);

		// Debug.Log("objet " + quality);
	}
	
	// Update is called once per frame
	void Update () {
		if (itemPosition == itemPositionEnum.INVENTORY)
			GetComponent<MeshRenderer> ().enabled = false;
		else
			GetComponent<MeshRenderer> ().enabled = true;
	}
}
