using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endMenuManager : MonoBehaviour {

	public Text title;
	public Text subtitle;
	public textRotation start;
	public textRotation exit;
	public AudioClip looseSound;
	public AudioClip winSound;
	public enum stateEnum {WIN, LOOSE, PAUSE, OTHER};
	public stateEnum state = stateEnum.OTHER;

	// Use this for initialization
	void Start () {
		Debug.Log (state);
		if (state == stateEnum.WIN) {
			GetComponent<AudioSource> ().clip = winSound;
			title.text = "YOU WIN";
			subtitle.text = "WAIT FOR THE NEXT PHONECALL";
			GetComponent<AudioSource> ().Play ();
		} else if (state == stateEnum.LOOSE) {
			GetComponent<AudioSource> ().clip = looseSound;
			title.text = "YOU LOOSE";
			subtitle.text = "I DARE YOU TO TRY AGAIN";
			GetComponent<AudioSource> ().Play ();
		} else if (state == stateEnum.PAUSE) {
			title.text = "PAUSE";
			subtitle.text = "DO YOU NEED A BREAK ?";
		}
	}

	void OnEnable() {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (start.isSelected) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex, LoadSceneMode.Single);
			} else if (exit.isSelected)
				SceneManager.LoadScene (0);
		} else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.UpArrow)) {
			start.isSelected = !start.isSelected;
			exit.isSelected = !start.isSelected;

			if (!start.isSelected)
				start.transform.rotation = Quaternion.Euler (0, 0, 0);
			else if (!exit.isSelected)
				exit.transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}
}
