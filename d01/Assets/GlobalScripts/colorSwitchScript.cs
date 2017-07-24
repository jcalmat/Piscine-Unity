using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorSwitchScript : MonoBehaviour {

	public GameObject platform;
	private Renderer renderer;
	private Renderer platformRenderer;
	public GameObject otherSwitch;

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.name == "thomas") {
			platform.layer = 11;
			otherSwitch.GetComponent<Renderer> ().material.color = Color.red;
			platformRenderer.material.color = Color.red;
			renderer.material.color = Color.red;
		} else if (other.name == "john") {
			platform.layer = 13;
			otherSwitch.GetComponent<Renderer> ().material.color = Color.yellow;
			platformRenderer.material.color = Color.yellow;
			renderer.material.color = Color.yellow;
		} else if (other.name == "claire") {
			platform.layer = 12;
			otherSwitch.GetComponent<Renderer> ().material.color = Color.blue;
			platformRenderer.material.color = Color.blue;
			renderer.material.color = Color.blue;

		}
		
	}

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();	
		platformRenderer = platform.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
