using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public List<GameObject> projPrefabs;
	public List<Transform> poss;
	public List<GameObject> spawnedProjectiles;
	public List<GameObject> nodes;

	public float spawnInterval;
	private float nextUpdateTime;
	private float pauseSpawnTimeMemory;

	private Vector3 spawnPos;
	private GameObject spawnPref;

	public int nearbyShrubs;
	public int nearbyDeer;
	public int nearbyWolves;
	public int omniProjNumber;
	public int antiProjNumber;
	// Use this for initialization
	void Start () {
		nextUpdateTime = Mathf.Infinity;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			CreateProjectile ();
//		}
		if (Time.time > nextUpdateTime) {
			CreateProjectile ();
			nextUpdateTime += spawnInterval;
		}

		if (Input.GetKeyDown (KeyCode.LeftParen)){
			spawnInterval -= .25f;
		}

		if (Input.GetKeyDown (KeyCode.RightParen)){
			spawnInterval += .25f;
		}
		if (Input.GetKeyDown (KeyCode.P)){
			StopShooting ();
			StartShooting ();
		}
	}

	void CreateProjectile () {
		int rand = Random.Range(0,8);
		if (rand == 0) {
			spawnPos = poss[0].position;
		} else if (rand == 1) {
			spawnPos = poss[1].position;
		} else if (rand == 2) {
			spawnPos = poss[2].position;
		} else if (rand == 3) {
			spawnPos = poss[3].position;
		} else if (rand == 4) {
			spawnPos = poss[4].position;
		} else if (rand == 5) {
			spawnPos = poss[5].position;
		} else if (rand == 6) {
			spawnPos = poss[6].position;
		} else if (rand == 7) {
			spawnPos = poss[7].position;
		}
		int rand2 = Random.Range (0, 5 + omniProjNumber + antiProjNumber + nearbyShrubs + nearbyDeer + nearbyWolves);
		if (rand2 < 1 + omniProjNumber) {
			spawnPref = projPrefabs[0];
		} else if (rand2 >= 1 + omniProjNumber && rand2 < 2 + omniProjNumber + antiProjNumber) {
			spawnPref = projPrefabs[1];
		} else if (rand2 >= 2 + omniProjNumber + antiProjNumber && rand2 < 3 + omniProjNumber + antiProjNumber + nearbyShrubs) {
			spawnPref = projPrefabs[2];
		} else if (rand2 >= 3 + omniProjNumber + antiProjNumber + nearbyShrubs && rand2 < 4 + omniProjNumber + antiProjNumber + nearbyShrubs + nearbyDeer) {
			spawnPref = projPrefabs[3];
		} else if (rand2 >= 4 + omniProjNumber + antiProjNumber + nearbyShrubs + nearbyDeer && rand2 < 5 + omniProjNumber + antiProjNumber + nearbyShrubs + nearbyDeer + nearbyWolves) {
			spawnPref = projPrefabs[4];
		} 
//		else if (rand2 == 5) {
//			spawnPref = projPrefabs[5];
//		} else if (rand2 == 6) {
//			spawnPref = projPrefabs[6];
//		} else if (rand2 == 7) {
//			spawnPref = projPrefabs[7];
//		}

		GameObject newProjectile = Instantiate (spawnPref) as GameObject;
		newProjectile.transform.position = spawnPos;
	}

	public void StopShooting() {
		nextUpdateTime = Mathf.Infinity;

		foreach (GameObject gunch in spawnedProjectiles) {
			Destroy (gunch);
		}

		foreach (GameObject gunch in nodes) {
			gunch.GetComponent<bNodeScript> ().thisNodeIsFilled = false;
		}

	}

	public void StartShooting() {
		nextUpdateTime = Time.time;
		nearbyShrubs = 0;
		nearbyDeer = 0;
		nearbyWolves = 0;
		Vector3 player = GameObject.Find ("Player").transform.position;
		foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().shrubCreatureList) {
			if ((gunch.transform.position - player).magnitude < 7) {
				nearbyShrubs += 1;
			}
		}
		foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().deerCreatureList) {
			if ((gunch.transform.position - player).magnitude < 7) {
				nearbyDeer += 1;
			}
		}
		foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().wolfCreatureList) {
			if ((gunch.transform.position - player).magnitude < 7) {
				nearbyWolves += 1;
			}
		}
	}

	//this is just used for the fill up function in bNodeScript, which hasnt yet been updated
	public void RemoveProjectileFromList(GameObject pro){
		spawnedProjectiles.Remove(pro);
	}

	public void PauseProjectiles() {
		for (int i = 0; i < spawnedProjectiles.Count; i++) {
			spawnedProjectiles [i].GetComponent<ProjectileScript> ().paused = true;
		}
		pauseSpawnTimeMemory = nextUpdateTime - Time.time;
		nextUpdateTime = Mathf.Infinity;
	}

	public void UnPauseProjectiles() {
		for (int i = 0; i < spawnedProjectiles.Count; i++) {
			spawnedProjectiles [i].GetComponent<ProjectileScript> ().paused = false;
		}
		nextUpdateTime = Time.time + pauseSpawnTimeMemory;
	}
		
}
