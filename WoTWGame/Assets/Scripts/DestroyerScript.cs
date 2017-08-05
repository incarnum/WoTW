using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Projectile" && coll.GetComponent<ProjectileScript>().moving == true) {
			GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().RemoveProjectileFromList (coll.gameObject);
			Destroy (coll.gameObject);
		}
	}
}
