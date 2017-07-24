using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHit : MonoBehaviour {
	bool blasted = false;
	enemyScript me;

	void Start () {
		me = gameObject.GetComponent<enemyScript> ();
	}

	public bool HaveTakeBolt ()
	{
		return blasted;
	}

	public IEnumerator TakeBolt (float damage) {
		blasted = true;
		me.takeDamage (damage);
		yield return new WaitForSeconds(1);
		blasted = false;
	}
}
