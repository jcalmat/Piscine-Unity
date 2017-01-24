using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour {

	public GameObject fireBall;
	public GameObject wall;
	private GameObject newFireBall;
	public GameObject DrainZone;

	public float dmg = 12;


	bool Canlaunch = true;
	bool CanDrain = true;
	float ReloadTime = 1;

	public void FireBallSpell (Vector3 destination)
	{
		if (!Canlaunch)
			return;
		
		newFireBall = GameObject.Instantiate (fireBall, transform.position, transform.rotation);
		newFireBall.GetComponent<FireBall> ().direction (destination);

		Canlaunch = false;
		Invoke("Reload", ReloadTime);
	}

	public void MultiFireBallSpell (Vector3 destination)
	{
		if (!Canlaunch)
			return;

		GameObject.Instantiate (fireBall, transform.position, transform.rotation).GetComponent<FireBall> ().direction (destination);
		//after (destination);
		StartCoroutine(after(destination));
		Canlaunch = false;
		Invoke("Reload", ReloadTime);
	}

	IEnumerator after (Vector3 destination) {
		yield return new WaitForSeconds(0.5F);
		GameObject.Instantiate (fireBall, transform.position, transform.rotation).GetComponent<FireBall> ().direction (destination);
		yield return new WaitForSeconds(0.5F);
		GameObject.Instantiate (fireBall, transform.position, transform.rotation).GetComponent<FireBall> ().direction (destination);
	}

	public void ChainLightning (GameObject target)
	{
		if (!Canlaunch)
			return;

		//float dist = Vector3.Distance (transform.position, target.transform.position);
		//if (dist < 25F || target.tag != "enemy")
		//	return;

		target.gameObject.GetComponent<LightningHit> ().TakeBolt(dmg);
		LightningHits (target.transform.position, 10F, 4);

		Canlaunch = false;
		Invoke("Reload", ReloadTime);
	}

	void LightningHits(Vector3 center, float radius, int remHits) {
		Debug.Log ("LightningHits !");

		int n;
		if (remHits < 1)
			return;
		remHits--;
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		List<Collider> enemys = new List<Collider> ();
		foreach (Collider col in hitColliders) {
			if (col.tag == "enemy" && !col.gameObject.GetComponent<LightningHit> ().HaveTakeBolt())
				enemys.Add (col);
		}

		n = enemys.Count;
		if (n < 0)
			return;
		n = Random.Range (0, n);

		//Create lightning between center and col[n]

		//GameObject newLine = new GameObject();
		//LineRenderer line = newLine.AddComponent<LineRenderer>();
		//line.enabled = true;
		//line.SetPosition(0, center);
		//line.SetPosition(1, enemys[n].transform.position);

		//!Create lightning between center and col[n]!

		LightningHits (enemys [n].transform.position, 10F, remHits - 1);
	}

	public void InvokeWall (Vector3 pos, Quaternion dir) {
		if (!Canlaunch)
			return;
		
		dir *= Quaternion.Euler(0, 90, 0);
		GameObject.Instantiate (wall, pos, dir);

		Canlaunch = false;
		Invoke("Reload", ReloadTime);
	}

	public void Blink (Vector3 pos)
	{
		if (!Canlaunch)
			return;

		this.transform.position = pos;

		Canlaunch = false;
		Invoke("Reload", ReloadTime);
	}

	public void DrainLife ()
	{
		if (!CanDrain)
			return;
		
		GameObject d = GameObject.Instantiate (DrainZone);
		d.gameObject.GetComponent<DrainScripts> ().startDrain (this.gameObject);

		CanDrain = false;
		Invoke("ReloadDrain", 8);
	}

	public void heal ()
	{
		if (!Canlaunch)
			return;

		this.gameObject.GetComponent<mayaScript> ().HP += 10;

		Canlaunch = false;
		Invoke("Reload", ReloadTime);
	}

	void Reload ()
	{
		Canlaunch = true;
	}

	void ReloadDrain ()
	{
		CanDrain = true;
	}
}