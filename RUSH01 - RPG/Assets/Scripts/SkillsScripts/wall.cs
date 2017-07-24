using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime / 2);
		if (transform.position.y < -10)
			Destroy (this.gameObject);
	}
}
