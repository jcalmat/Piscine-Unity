using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {

	public textRotation start;
	public Text startText;
	public Text exitText;
	public textRotation exit;
	private bool stopSelection = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (start.isSelected) {
				stopSelection = true;
				StartCoroutine ("loadLevel");
				startText.text = "Loading...";
				exitText.text = "";
				//				SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
			}
			else if (exit.isSelected)
				Application.Quit ();
		} else if (!stopSelection && (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.UpArrow))) {
			start.isSelected = !start.isSelected;
			exit.isSelected = !start.isSelected;

			if (!start.isSelected)
				start.transform.rotation = Quaternion.Euler (0, 0, 0);
			else if (!exit.isSelected)
				exit.transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}

	IEnumerator loadLevel() {

			// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(1);

			// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
			while (!async.isDone) {
				yield return null;
		}
	}
}
