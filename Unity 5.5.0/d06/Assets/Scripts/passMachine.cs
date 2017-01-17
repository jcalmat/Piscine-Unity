using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passMachine : MonoBehaviour {

	public AudioClip wrongKey;
	public AudioClip openDoor;
	public GameObject door;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "player") {
			if (other.gameObject.GetComponent<player> ().playerHasKey()) {
				Debug.Log ("open Door");
				other.gameObject.GetComponent<player> ().removeKey ();
				door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z + 40), 100 * Time.deltaTime);
			} else {
				AudioSource audio = gameObject.GetComponent<AudioSource> ();
				audio.clip = wrongKey;
				audio.Play ();
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
