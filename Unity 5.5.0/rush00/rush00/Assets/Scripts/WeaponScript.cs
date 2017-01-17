using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public string Name = "NoName";
	public int MinAmmo = 3;
	public int MaxAmmo = 10;
	public float FireRate = 1;
	public Sprite BasicSprite;
	public Sprite EquipSprite;
	public BulletScript Bullet;
	public GameObject SoundToken;
	public AudioClip SoundNoAmmo;
	public AudioClip SoundFire;
	public bool NeedAmmo = true;
	private float nextFire = 0;
	private AudioSource Audio;
	[HideInInspector]public int Ammo = 0;
	private SpriteRenderer spriteRenderer;
	private bool InDrop = false;
	private bool Equip = false;
	private float DropSpeed = 10;

	void Start () {
		GetComponent<Rigidbody2D> ().mass = 0.0001f;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Audio = GetComponent<AudioSource>();
		Ammo = Random.Range (MinAmmo, MaxAmmo);
	}
	
	// Update is called once per frame
	void Update () {
		if (InDrop && DropSpeed > 0) {
			GetComponent<Rigidbody2D> ().AddForce (-transform.up * (DropSpeed / 100));
			DropSpeed = 0;
		} else if (!InDrop) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	IEnumerator StopDrop() {
		GetComponent<Animator> ().SetBool ("drop", true);
		GetComponent<Rigidbody2D> ().isKinematic = false;
		GetComponent<BoxCollider2D> ().isTrigger = false;
		InDrop = true;
		yield return new WaitForSeconds(0.5f);
		GetComponent<Animator> ().SetBool ("drop", false);
		InDrop = false;
		GetComponent<Rigidbody2D> ().isKinematic = true;
		GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	public bool Fire(bool IsEnnemy = false) {
		if (Time.time > nextFire) {
			if (!IsEnnemy) {
				if (NeedAmmo) {
					if (Ammo > 0) {
						Ammo--;
						InstantiateBullet ();
						return true;
					} else {
						if (!Audio.isPlaying) {
							Audio.clip = SoundNoAmmo;
							Audio.Play ();
						}
						Debug.Log ("No Ammo");
						return false;
					}
				} else {
					InstantiateBullet ();
				}
			} else {
				InstantiateEnemyBullet ();
				return true;
			}
		}
		return true;
	}

	void InstantiateBullet() {
		nextFire = Time.time + FireRate;
		Quaternion rotation = transform.parent.rotation * Quaternion.Euler (0, 0, -90);
		BulletScript bullet = Instantiate (Bullet, transform.position, rotation);
		bullet.launch (-transform.parent.transform.up);
		Audio.clip = SoundFire;
		Audio.Play ();
		if (NeedAmmo) {
			Destroy (Instantiate (SoundToken, transform.position, rotation), 10f);
		}
	}

	void InstantiateEnemyBullet() {
		if (transform.parent == null)
			return;
		nextFire = Time.time + FireRate;
		Quaternion rotation = transform.parent.rotation * Quaternion.Euler (0, 0, -90);
		BulletScript bullet = Instantiate (Bullet, transform.position, rotation);
		bullet.gameObject.layer = 10;
		bullet.launch (-transform.parent.transform.up);
		Audio.clip = SoundFire;
		Audio.Play ();
		//Destroy(Instantiate (SoundToken, transform.position, rotation), 2f);
	}

	public WeaponScript Taken(Transform NewParent) {
		Equip = true;
		GetComponent<SpriteRenderer> ().sprite = EquipSprite;
		GetComponent<SpriteRenderer> ().sortingLayerName = "EquipedWeapon";
		transform.parent = NewParent;
		Vector3 tmpposition = transform.localPosition;
		tmpposition.x = -0.1f;
		tmpposition.y = -0.3f;
		transform.localPosition = tmpposition;

		Quaternion tmp = transform.localRotation;
		tmp.z = 0;
		transform.localRotation = tmp;

		return this;
	}

	public void Dropped(bool isDeath = false) {
		if (Equip) {
			DropSpeed = isDeath ? 3 : 5;
			Debug.Log ("Dropped weapon");
			StartCoroutine ("StopDrop");
			Vector3 tmpposition = transform.localPosition;
			tmpposition.z = -1f;
			transform.localPosition = tmpposition;
			transform.parent = null;
			Equip = false;
			spriteRenderer.sprite = BasicSprite;
			spriteRenderer.sortingLayerName = "Weapon";
		}
	}
}
