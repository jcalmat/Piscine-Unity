using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour {

	public List<WeaponScript> WeaponList;

	// Use this for initialization
	void Start () {
		Instantiate (WeaponList [Random.Range (0, WeaponList.Count)], transform.position, Quaternion.Euler (Vector3.zero));;
		Destroy (this);
	}

	void OnDestroy() {
		Destroy (transform.root.gameObject);
	}
}
