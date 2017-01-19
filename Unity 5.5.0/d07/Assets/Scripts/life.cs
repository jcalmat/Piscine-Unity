using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life : MonoBehaviour {

	public float hp = 100;		

	// Use this for initialization
	void Start () {								
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0 && !GetComponent<AudioSource>().isPlaying) {
			gameManager.gm.playDead ();
			if (gameObject.transform.tag != "player")
				Destroy (gameObject);
			else
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

}
