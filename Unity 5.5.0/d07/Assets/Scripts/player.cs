using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

	public float speed = 5f;
	private float rotationY;
	private float rotationX;
	public GameObject Tower;
	public GameObject Wheel;
	public AudioClip missileSound;
	public AudioClip gunSound;
	private float sensitivityY = 10f;
	private float boost = 100;
	public float range = 100;
	private int ammo = 10;
	public GameObject particleGun;
	public GameObject particleMissile;
	private bool fireGun = false;
	private RaycastHit raycastHit;
	public Text hpText;
	public Text ammoText;
	public int maxAmmo = 10;
	public LayerMask mask;
	public Image crosshair;

	void Start () {
		StartCoroutine ("playGunSound");
	}
	
	// Update is called once per frame
	void Update () {

		hpText.text = GetComponent<life>().hp.ToString() + " hp";
		if (GetComponent<life> ().hp <= 25)
			hpText.color = Color.red;
		else if (GetComponent<life> ().hp <= 50)
			hpText.color = Color.yellow;
		else if (GetComponent<life> ().hp <= 100)
			hpText.color = Color.white;


		if (ammo <= 3)
			ammoText.color = Color.red;
		else if (ammo <= 5)
			ammoText.color = Color.yellow;
		else if (ammo <= 10)
			ammoText.color = Color.white;

		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

		float rotationX = Tower.transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityY;
	//	GetComponent<Rigidbody>().freezeRotation = true;
		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY * 10;
//		rotationY = Mathf.Clamp (rotationY, 72, 72);

		Tower.transform.localEulerAngles = new Vector3 (0, rotationX, 0);
		//		transform.Rotate (0, 20 * Time.deltaTime, 0);

		Vector3 fwd = Tower.transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast(Tower.transform.position, fwd, out raycastHit, range)) {
			if (raycastHit.transform.tag == "tank")
				crosshair.color = Color.red;
			else
				crosshair.color = Color.white;
				if (Input.GetKey (KeyCode.Mouse0)) {
					fireGun = true;
					Tower.GetComponent<AudioSource> ().clip = gunSound;
					Instantiate (particleGun, raycastHit.point, Quaternion.identity);
				} else if (Input.GetKeyDown (KeyCode.Mouse1) && ammo > 0) {
				if (raycastHit.transform.tag == "tank")
					raycastHit.transform.GetComponent<life> ().hp -= 40;
					Instantiate (particleMissile, raycastHit.point, Quaternion.identity);
					Tower.GetComponent<AudioSource> ().clip = missileSound;
					Tower.GetComponent<AudioSource> ().Play ();
					ammo -= 1;
					ammoText.text = ammo.ToString () + " / " + maxAmmo.ToString ();
				} else {
					fireGun = false;
				}
			if (raycastHit.transform != null)
				Debug.DrawLine(transform.position, raycastHit.point, Color.red);
		} else
			crosshair.color = Color.white;

		if (Input.GetKey (KeyCode.W))
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.S))
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (0, 10, 0);
		if (Input.GetKey (KeyCode.A))
			transform.Rotate (0, -10, 0);
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (boost > 0) {
				boost -= 10;
				speed = 7;
			}
		} else {
			speed = 5;
		}
		if (boost < 100)
			boost += 5;
	}

	IEnumerator playGunSound() {
		while (true) {
			Debug.Log (fireGun);
			Debug.Log ("coroutine");
			if (raycastHit.transform != null) {
				if (fireGun && raycastHit.transform.tag == "tank") {
					raycastHit.transform.GetComponent<life> ().hp -= 10;
					Tower.GetComponent<AudioSource> ().Play ();
				}
			}
			yield return new WaitForSeconds (.1f);
		}
	}
}
