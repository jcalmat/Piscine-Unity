using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour {

	public float invisible = 0;
	private bool isTriggered = false;
	private bool isRunning = false;

	public float speed;

	public AudioClip getKeySound;
	public AudioClip musicNormal;
	public AudioClip musicPanic;
	public GameObject foot;

	private enum currentAudioEnum {normal, panic};
	private currentAudioEnum currentAudio = currentAudioEnum.normal;
	private enum currentWalkSpeedEnum {stay, normal, speed};
	private currentWalkSpeedEnum currentWalkSpeed = currentWalkSpeedEnum.stay;

	public Scrollbar bar;

	private bool haskey = false;

	// Use this for initialization
	void Start () {
		StartCoroutine ("walkSound");
//		StartCoroutine ("checkProgressBar");
	}

	public bool playerHasKey() {
		return haskey;
	}

	public void removeKey() {
		haskey = false;
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "panamaPapers") {
			gameManager.gm.setMessage ("Congratulations Fisher, once again you helped us a lot.");
			StartCoroutine ("displayMessageBeforeRestart");
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "key1" && !haskey) {
			AudioSource audio = other.GetComponent<AudioSource> ();
			audio.Play ();
			StartCoroutine (waitBeforeDelete (other.gameObject));
			haskey = true;
			gameManager.gm.setMessage ("You got the key ! Now you have to find the hidden papers.");
		} else if (other.gameObject.tag == "light") {
			playerSpotted (.5f);
		}
//			isTriggered = true;
	}

	void OnTriggerExit(Collider other) {
		isTriggered = false;
	}

	// Update is called once per frame
	void Update () {

		bar.size = invisible / 100;

		if (isRunning)
			playerSpotted (1);
		else
			if (invisible > 0 && !isRunning)
				invisible -= .5f;

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 10;
			isRunning = true;
			currentWalkSpeed = currentWalkSpeedEnum.speed;
		}
		else {
			speed = 5;
			isRunning = false;
			currentWalkSpeed = currentWalkSpeedEnum.normal;
		}

		if (invisible >= 75 && currentAudio != currentAudioEnum.panic) {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.clip = musicPanic;
			audio.Play ();
			currentAudio = currentAudioEnum.panic;
		} else if (invisible < 75 && currentAudio != currentAudioEnum.normal) {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.clip = musicNormal;
			audio.Play ();
			currentAudio = currentAudioEnum.normal;
		}

		if (invisible >= 100) {
			gameManager.gm.setMessage ("You have been spotted, RIP in peace.");
			StartCoroutine ("displayMessageBeforeRestart");
		}

		if (Input.GetKey (KeyCode.W))
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if (Input.GetKey(KeyCode.S))
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		if (Input.GetKey(KeyCode.A))
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		if (Input.GetKey(KeyCode.D))
			transform.Translate (Vector3.right * speed * Time.deltaTime);

	
		if (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W)) {
			currentWalkSpeed = currentWalkSpeedEnum.stay;
		}
	}

	IEnumerator displayMessageBeforeRestart() {
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	}


	IEnumerator walkSound() {
		while (true) {
			float wait = 0;
			if (currentWalkSpeed == currentWalkSpeedEnum.normal) {
				Debug.Log ("walk sound");
				foot.GetComponent<AudioSource> ().Play ();
				wait = .5f;
			} else if (currentWalkSpeed == currentWalkSpeedEnum.speed) {
				foot.GetComponent<AudioSource> ().Play ();
				wait = 0.2f;
			} else {
				foot.GetComponent<AudioSource> ().Stop ();
			}
			yield return new WaitForSeconds(wait);
		}
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

	public void playerSpotted(float value) {
		invisible += value;
//		isTriggered = true;
	}

	IEnumerator waitBeforeDelete(GameObject go) {
		yield return new WaitForSeconds (.2f);
		Destroy (go);

	}
}
