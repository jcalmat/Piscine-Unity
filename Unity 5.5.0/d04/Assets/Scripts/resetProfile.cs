using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetProfile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void deletePlayerPrefs() {
		Debug.Log ("delete player prefs");
		PlayerPrefs.DeleteAll ();
	}

	// Update is called once per frame
	void Update () {
	}
}
