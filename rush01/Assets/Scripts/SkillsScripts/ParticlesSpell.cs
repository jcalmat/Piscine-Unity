using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ParticlesSpell : MonoBehaviour {

	public int Heal = 0;
	public int BuffDamage = 0;
	public int BasicDamage = 0;
	public float Tick;
	public float TickRate = 1;
	public ParticleSystem ImpactTerrain;
	public ParticleSystem ImpactEnemy;
	public Creature Caster;

	void Start() {
		Tick = Time.time + TickRate;
	}

	void Update() {
		if (Heal > 0 && Tick < Time.time) {
			Tick = Time.time + TickRate;
			Caster.Heal (Heal * (Caster.Constitution / 10));
		}
	}

	void OnParticleCollision(GameObject other) {
		if (Caster != null) {
			ParticleSystem.Particle[] Particules = new ParticleSystem.Particle[GetComponent<ParticleSystem> ().main.maxParticles];
			int NbParticule = GetComponent<ParticleSystem> ().GetParticles (Particules);
			Vector3 TmpCollision;
			ParticleSystem tmp;
			for (int i = 0; i < NbParticule; i++) {
				TmpCollision = transform.TransformPoint (Particules [0].position);
				if (Caster.tag != other.tag && (other.tag == "Enemy" || other.tag == "Player")) {
					other.gameObject.GetComponent<Creature>().OnDamaged (Caster, Random.Range(Caster.DamageMin, Caster.DamageMax) * BasicDamage);
					if (ImpactEnemy) {
						tmp = Instantiate (ImpactEnemy, TmpCollision, transform.rotation).GetComponent<ParticleSystem> ();
						tmp.Play ();
						Destroy (tmp.transform.root.gameObject, 2);
					}

				} else {
					if (ImpactTerrain) {
						tmp = Instantiate (ImpactTerrain, TmpCollision, transform.rotation).GetComponent<ParticleSystem> ();
						tmp.Play ();
						Destroy (tmp.transform.root.gameObject, 2);
					}
				}
			}
			GetComponent<ParticleSystem> ().Stop ();
		}
	}
}
*/