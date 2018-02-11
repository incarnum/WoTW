using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarGlowScript : MonoBehaviour {
	private GameObject player;
	private ParticleSystem ps;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		ps = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log((player.transform.position - transform.position).magnitude);
		if ((player.transform.position - transform.position).magnitude < 6) {
			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, (1 - (transform.position - player.transform.position).magnitude / 6 + .4f));
			var main = ps.main;
			main.startColor = new Color (.5f, .5f, 1, (1 - (transform.position - player.transform.position).magnitude / 6));

		}
	}
}
