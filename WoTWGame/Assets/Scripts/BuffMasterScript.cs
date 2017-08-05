using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMasterScript : MonoBehaviour {
	public List<Transform> iconPositions;
	public List<GameObject> icons;
	public int numOfEjects;
	public int numOfSwaps;
	public int numOfMorphs;
	public int numOfPurifys;
	private float amountOfTimeStop;
	private bool timeIsStopped;
	private CoreScript coreRef;

	// Use this for initialization
	void Start () {
		numOfEjects = 100;
		//numOfSwaps = Random.Range (1, 4);
		//numOfMorphs = Random.Range (1, 4);
		//numOfPurifys = Random.Range (1, 4);
		amountOfTimeStop = 100;
		UpdateAvailableBuffs ();
		coreRef = GameObject.Find ("Core").GetComponent<CoreScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			if (numOfEjects > 0) {
				//Eject ();
				coreRef.Eject();
				UpdateAvailableBuffs();
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)){
			if (numOfSwaps > 0) {
				UpdateAvailableBuffs();
				coreRef.Swap ();
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)){
			if (numOfMorphs > 0) {
				//Morph ();
				coreRef.Morph();
				UpdateAvailableBuffs();
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)){
			if (numOfPurifys > 0) {
				coreRef.Purify ();
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (amountOfTimeStop > 0) {
				StartTimeStop ();
			}
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			if (amountOfTimeStop > 0) {
				EndTimeStop ();
			}
		}

		if (timeIsStopped) {
			EveryTickTimeStop();
		}
	}

	public void UpdateAvailableBuffs() {
		if (numOfEjects > 0) {
			icons [0].transform.position = iconPositions [0].position;
		} else {
			icons [0].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfEjects > 1) {
			icons [1].transform.position = new Vector3 (iconPositions [0].position.x, iconPositions [0].position.y + 1, iconPositions [0].position.z);
		} else {
			icons [1].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfEjects > 2) {
			icons [2].transform.position = new Vector3 (iconPositions [0].position.x, iconPositions [0].position.y + 2, iconPositions [0].position.z);
		} else {
			icons [2].transform.position = new Vector3 (-100, -10, 1);
		}
		
		if (numOfSwaps > 0) {
			icons [3].transform.position = iconPositions [1].position;
		} else {
			icons [3].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfSwaps > 1) {
			icons [4].transform.position = new Vector3 (iconPositions [1].position.x, iconPositions [1].position.y + 1, iconPositions [1].position.z);
		} else {
			icons [4].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfSwaps > 2) {
			icons [5].transform.position = new Vector3 (iconPositions [1].position.x, iconPositions [1].position.y + 2, iconPositions [1].position.z);
		} else {
			icons [5].transform.position = new Vector3 (-100, -10, 1);
		}
			
		if (numOfMorphs > 0) {
			icons [6].transform.position = iconPositions [2].position;
		} else {
			icons [6].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfMorphs > 1) {
			icons [7].transform.position = new Vector3 (iconPositions [2].position.x, iconPositions [2].position.y + 1, iconPositions [2].position.z);
		} else {
			icons [7].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfMorphs > 2) {
			icons [8].transform.position = new Vector3 (iconPositions [2].position.x, iconPositions [2].position.y + 2, iconPositions [2].position.z);
		} else {
			icons [8].transform.position = new Vector3 (-100, -10, 1);
		}

		if (numOfPurifys > 0) {
			icons [9].transform.position = iconPositions [3].position;
		} else {
			icons [9].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfPurifys > 1) {
			icons [10].transform.position = new Vector3 (iconPositions [3].position.x, iconPositions [3].position.y + 1, iconPositions [3].position.z);
		} else {
			icons [10].transform.position = new Vector3 (-100, -10, 1);
		}
		if (numOfPurifys > 2) {
			icons [11].transform.position = new Vector3 (iconPositions [3].position.x, iconPositions [3].position.y + 2, iconPositions [3].position.z);
		} else {
			icons [11].transform.position = new Vector3 (-100, -10, 1);
		}
	}

	void Eject() {
		for (int i = 0; i < coreRef.nodeHolding.Count; i++) {
			if (coreRef.nodeHolding [i] != null) {
				if (coreRef.nodeHolding [i].GetComponent<ProjectileScript> ().projectileType != (coreRef.nodeType [i] + 1) 
				&& coreRef.nodeHolding [i].GetComponent<ProjectileScript> ().projectileType != 7) {

					//Debug.Log (coreRef.nodeHolding [i].GetComponent<ProjectileScript>().projectileType);
					//Debug.Log (coreRef.nodeType [i]);
					GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().spawnedProjectiles.Remove (coreRef.nodeHolding [i]);
					coreRef.nodes[i].GetComponent<bNodeScript> ().thisNodeIsFilled = false;
					Destroy (coreRef.nodeHolding[i]);
					numOfEjects -= 1;
					return;
				}
			}
		}
	}

	void Swap() {

	}

	void Morph() {
		for (int i = 0; i < coreRef.nodeHolding.Count; i++) {
			if (coreRef.nodeHolding [i] != null) {
				Debug.Log (coreRef.nodeHolding [i].GetComponent<ProjectileScript>().projectileType);
				Debug.Log (coreRef.nodeType [i]);
				if (coreRef.nodeHolding [i].GetComponent<ProjectileScript> ().projectileType != coreRef.nodeType [i] +1
					&& coreRef.nodeHolding [i].GetComponent<ProjectileScript> ().projectileType != 1) {

					Vector3 tempPos = new Vector3 (coreRef.nodeHolding [i].transform.position.x, coreRef.nodeHolding [i].transform.position.y, coreRef.nodeHolding [i].transform.position.z);
					Destroy (coreRef.nodeHolding[i]);
					GameObject newProjectile = Instantiate (GameObject.Find("Spawner").GetComponent<SpawnerScript>().projPrefabs[coreRef.nodeType[i]+1]) as GameObject;
					newProjectile.transform.position = tempPos;
					newProjectile.transform.parent = coreRef.nodes [i].transform;

					coreRef.ChangeHoldStatus (coreRef.nodes[i].gameObject, newProjectile);

					GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().spawnedProjectiles.Remove (coreRef.nodeHolding [i]);
					GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().spawnedProjectiles.Add (newProjectile);
					coreRef.nodeHolding [i] = newProjectile;
					newProjectile.GetComponent<ProjectileScript> ().moving = true;
					numOfMorphs -= 1;
					return;
				}
			}

		}
	}

	void Purify() {

	}

	void StartTimeStop() {
		timeIsStopped = true;
		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().PauseProjectiles ();
	}

	void EveryTickTimeStop() {
		icons [12].GetComponent<barScript> ().UpdateFillSize (-.2f * Time.deltaTime);
		amountOfTimeStop -= .2f * Time.deltaTime;
		if (amountOfTimeStop <= 0) {
			EndTimeStop ();
		}
	}

	void EndTimeStop() {
		timeIsStopped = false;
		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().UnPauseProjectiles ();
	}
		

	//hold info of each node type, and object each node is holding

	//1 eject
	//2 swap
	//3 morph
	//4 purify

	//space stop time


}

//for (int i = 0; i < numOfEjects; i++) {
//	GameObject newIcon = Instantiate (iconPrefabs [0]) as GameObject;
//
//	newIcon.transform.position = 
//		new Vector3(iconPositions [0].position.x, iconPositions[0].position.y + 1 * i, iconPositions[0].position.z);
//
//	ejectIcons.Add (newIcon);
//}