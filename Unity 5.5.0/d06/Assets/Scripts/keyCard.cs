using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("rotate");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator rotate() {
		while (true) {
			transform.Rotate (Vector3.right);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
