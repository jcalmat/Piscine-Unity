using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

	public GameObject gameO;
	public float spawnTime;
	public float timer;

	private float[] possibleX = new float[3];
	public GameObject[] objects = new GameObject[3];
	public static int[] letterInstantiate = new int[3];
	private string[] objTag = new string[3];
	private int rand;

    void Start ()
    {
		letterInstantiate[0] = 0;
		letterInstantiate[1] = 0;
		letterInstantiate[2] = 0;
        possibleX[0] = -1.41f;
        possibleX[1] = 0f;
        possibleX[2] = 1.41f;
		objTag[0] = "a_letter";
		objTag[1] = "s_letter";
		objTag[2] = "d_letter";

	}

	void Update () {
		if (timer >= spawnTime) {
			int rand = Random.Range (0, 3);
			Debug.Log (letterInstantiate[rand]);
			if (letterInstantiate [rand] == 0) {
				timer = 0;
				letterInstantiate [rand] += 1;
				Vector3 newPos = new Vector3 (possibleX [rand], 4f, 0);
				GameObject obj = (GameObject)Instantiate (objects [rand], newPos, Quaternion.identity);
				obj.tag = objTag[rand];
			}
		}
		timer += Time.deltaTime;
	}
}
