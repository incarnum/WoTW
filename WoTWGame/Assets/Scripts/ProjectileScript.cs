using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
	Vector2 movement;
	Rigidbody2D rb;
	public float projectileType;
	public bool moving;
	public bool paused;
	public float speed;
	public Transform coreTrans;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		moving = !moving;
		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().spawnedProjectiles.Add (gameObject);
		coreTrans = GameObject.Find ("Core").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving && !paused) {
			Move (coreTrans.position.x - transform.position.x, coreTrans.position.y - transform.position.y, speed);
		}
		
	}

	void Move (float h, float v, float s) {
		movement.Set (h, v);
		movement = movement.normalized * s * Time.deltaTime;
		rb.MovePosition ((Vector2)gameObject.transform.position + movement);
	}

	void OnDestroy () {
		if (GameObject.Find ("Spawner") != null) {
			GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().spawnedProjectiles.Remove (gameObject);
		}
	}


}
