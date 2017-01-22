using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSounds : MonoBehaviour {

	public AudioClip[] audios;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = audios [Random.Range (0, audios.Length)];
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			source.clip = audios [Random.Range (0, audios.Length)];
			source.Play ();
		}
	}
}
