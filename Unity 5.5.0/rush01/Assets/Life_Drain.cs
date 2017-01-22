using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Drain : MonoBehaviour {

	// Use this for initialization
	void OnTriggerStay (Collider col) {
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<enemyScript>().HP -= 1;
		}
	}
}
