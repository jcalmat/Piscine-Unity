using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public float invisible = 0;
	private bool isTriggered = false;
	private bool isRunning = false;

	public float speed;

	public AudioClip getKeySound;
	public AudioClip musicNormal;
	public AudioClip musicPanic;

	private bool haskey = false;

	// Use this for initialization
	void Start () {
//		StartCoroutine ("checkProgressBar");
	}

	public bool playerHasKey() {
		return haskey;
	}

	public void removeKey() {
		haskey = false;
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "key1" && !haskey) {
			AudioSource audio = other.GetComponent<AudioSource> ();
			audio.Play ();
			StartCoroutine(waitBeforeDelete (other.gameObject));
//			AudioSource audio = GetComponent<AudioSource>();
//			audio.clip = getKeySound;
//			audio.Play ();
//			waitTillSoundEnd (.5f);
			haskey = true;
		}
		else
			isTriggered = true;
	}

	void OnTriggerExit(Collider other) {
		isTriggered = false;
	}

	// Update is called once per frame
	void Update () {

		if (isTriggered || isRunning)
			playerSpotted (1);
		else
			if (invisible > 0 && !isRunning)
				invisible -= .5f;

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 10;
			isRunning = true;
		}
		else {
			speed = 5;
			isRunning = false;
		}

		if (Input.GetKey (KeyCode.W))
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if (Input.GetKey(KeyCode.S))
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		if (Input.GetKey(KeyCode.A))
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		if (Input.GetKey(KeyCode.D))
			transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

//	IEnumerator checkProgressBar() {
//		while (true) {
//			if (isTriggered)
//				invisible += 1;
//			else if (invisible > 0)
//				invisible -= 1;
//			yield return new WaitForSeconds (0.005f);
//		}
//	}

	public void playerSpotted(int value) {
		invisible += value;
//		isTriggered = true;
	}

	IEnumerator waitBeforeDelete(GameObject go) {
		yield return new WaitForSeconds (.2f);
		Destroy (go);

	}
}
