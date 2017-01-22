using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainScripts : MonoBehaviour {

	private mayaScript maya;
	private GameObject mayaG;
	
	public void startDrain (GameObject m)
	{
		mayaG = m;
		maya = m.gameObject.GetComponent<mayaScript> ();
		Invoke("stop", 5);
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.tag == "enemy") {
			col.gameObject.GetComponent<enemyScript> ().takeDamage (0.1f);
			if (maya.HP < maya.maxHP)
				maya.HP += 1;
		}
	}

	void Update () {
		if (!maya)
			return;

		transform.position = mayaG.transform.position;
	}

	void stop ()
	{
		Destroy (this.gameObject);
	}
}
