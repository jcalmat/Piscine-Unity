using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawner : MonoBehaviour {

	public GameObject gameO;
	public float spawnTime;
	public float timer;

	private float[] possibleX = new float[3];

    void Start ()
    {
        possibleX[0] = -1.41f;
        possibleX[1] = 0f;
        possibleX[2] = 1.41f;
    }

	void Update () {
		if (timer >= spawnTime) {
			timer = 0;
			int rand = Random.Range(0, 3);
            Vector3 newPos = new Vector3(possibleX[rand], 4f, 0);
            GameObject.Instantiate(gameO, newPos, Quaternion.identity);
		}
		timer += Time.deltaTime;
	}
}
