using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 200)
			transform.localScale = new Vector3(1f, 1f, 0);
		transform.localScale += new Vector3 (1f, 1f, 0);
		
	}
}
