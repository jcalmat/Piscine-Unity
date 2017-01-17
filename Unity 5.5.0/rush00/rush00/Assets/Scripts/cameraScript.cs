using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cameraScript: MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
	public List<AudioClip> MusicList; 
	private AudioSource Music;

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	void Start ()
	{
		Music = GetComponent<AudioSource> ();
	}

	void Update () {
		if (!Music.isPlaying) {
			Music.clip = MusicList [Random.Range (0, MusicList.Count)];
			Music.Play ();
		}
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		if (player != null)
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}