using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passMachine : MonoBehaviour {

	public AudioClip wrongKey;
	public AudioClip openDoor;
	public GameObject door;

	void OnCollisionEnter(Collision other) {
		
		if (other.gameObject.tag == "player") {
			if (!other.gameObject.GetComponent<player> ().playerHasKey()) {
				AudioSource audio = gameObject.GetComponent<AudioSource> ();
				audio.clip = wrongKey;
				audio.Play ();
				gameManager.gm.setMessage ("You don t have the key. Please try again.");
			} else {
		//c		other.gameObject.GetComponent<player> ().removeKey ();
				door.GetComponent<Animator> ().SetTrigger ("openDoor");
				gameManager.gm.setMessage ("Quick, take them before somebody notice you!");
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
