using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour {
	public bool req1;
	public bool req2;
	public bool req3;
	public bool req4;
	public bool req5;
	public bool req6;
	public List<bNodeScript> nodes;
	public List<int> nodeType;
	public List<GameObject> nodeHolding;
	private float menuReturnTime;
	private float menuReturnTime2;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < nodes.Count; i++) {
			nodeType [i] = nodes [i].nodeType - 2;
		}

		foreach (GameObject gunch in GameObject.FindGameObjectsWithTag("Node")) {
			nodes.Add (gunch.GetComponent<bNodeScript>());
		}
		menuReturnTime = Mathf.Infinity;
		menuReturnTime2 = Mathf.Infinity;
	}
	
	// Update is called once per frame
	void Update () {
		if (menuReturnTime < Time.time) {
			Camera.main.transform.position = new Vector3(GameObject.Find ("Map").transform.position.x, GameObject.Find ("Map").transform.position.y, GameObject.Find ("Map").transform.position.z - 5);
			menuReturnTime = Mathf.Infinity;
		}
		if (menuReturnTime2 < Time.time) {
			GameObject.Find ("SpellTome(Clone)").GetComponent<SpellScript> ().Cast ();
			Destroy (GameObject.Find ("SpellTome(Clone)"));
			GameObject.Find ("CreatureMaster").GetComponent<ShrubPopulation> ().UpdateBars ();
            GameObject.Find ("CreatureMaster").GetComponent<DeerPopulation>().UpdateBars();
            GameObject.Find ("CreatureMaster").GetComponent<WolfPopulation>().UpdateBars();
            menuReturnTime2 = Mathf.Infinity;
		}


	}

	void OnTriggerEnter2D(Collider2D coll) {
//		if (coll.gameObject.tag == "Projectile") {
//			GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().RemoveProjectileFromList (coll.gameObject);
//			Destroy (coll.gameObject);
//			GameObject.Find ("Bar6").GetComponent<barScript> ().UpdateFillSize (-.2f);
//			CheckDefeat ();
//		}
	}

//	public void CheckVictory(){
//		if (req1 && req2 && req3 && req4) {
//			GameObject.Find ("Victory Text").transform.position = new Vector3 (-4, 4, 0);
//			GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StopShooting ();
//		}
//	}

	public void CheckDefeat(){
		if (GameObject.Find ("Bar6").GetComponent<barScript> ().currentValue == 0) {
			GameObject.Find ("Failure Text").transform.position = new Vector3 (-4, 4, 0);
			GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StopShooting ();
		}
	}

	public void ChangeHoldStatus(GameObject nodeReference, GameObject heldProjectile) {
		//Debug.Log (nodes.IndexOf (nodeReference.GetComponent<bNodeScript> ()));
		nodeHolding[nodes.IndexOf(nodeReference.GetComponent<bNodeScript>())] = heldProjectile;
		for (int i = 0; i < nodeHolding.Count; i++) {
			if (nodeHolding[i] != null) {
				if (nodeType [i] == nodeHolding [i].GetComponent<ProjectileScript> ().projectileType - 2) {
					if (i == 0) {
						req1 = true;
					} else if (i == 1) {
						req2 = true;
					} else if (i == 2) {
						req3 = true;
					} else if (i == 3) {
						req4 = true;
					}
				}
			}
		}
	}

	public void DoACheck() {
		int counter = 0;
		foreach (bNodeScript gunch in nodes) {
			if (gunch.heldProj != null) {
				if (gunch.nodeType == gunch.heldProj.GetComponent<ProjectileScript> ().projectileType - 1
				   || gunch.heldProj.GetComponent<ProjectileScript> ().projectileType == 7) {
					counter += 1;
				}
			}
		}
		if (counter == nodes.Count) {
			Debug.Log ("Victory");
			GameObject.Find ("spellCast").transform.position = transform.position;
			//GameObject.Find ("Victory Text").transform.position = new Vector3 (-4, 4, 0);
			GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StopShooting ();
			menuReturnTime = Time.time + 2.5f;
			menuReturnTime2 = Time.time + 3f;


		}
	}

	public void Eject() {
		foreach (bNodeScript gunch in nodes) {
			if (gunch.heldProj != null) {
				if (gunch.nodeType != gunch.heldProj.GetComponent<ProjectileScript> ().projectileType - 1
					&& gunch.heldProj.GetComponent<ProjectileScript> ().projectileType != 7) {
					gunch.thisNodeIsFilled = false;
					Destroy (gunch.heldProj);
					DoACheck ();
					GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().numOfEjects -= 1;
					GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().UpdateAvailableBuffs ();
					return;
				}
			}
		}
	}

	public void Swap() {
		foreach (bNodeScript gunch in nodes) {
			if (gunch.heldProj != null) {
				if (gunch.nodeType != gunch.heldProj.GetComponent<ProjectileScript> ().projectileType
					&& gunch.heldProj.GetComponent<ProjectileScript> ().projectileType != 7) {
					foreach (bNodeScript grunch in nodes) {
						if (grunch.heldProj != null) {
							if (grunch.nodeType != grunch.heldProj.GetComponent<ProjectileScript> ().projectileType
							   && grunch.nodeType == gunch.heldProj.GetComponent<ProjectileScript> ().projectileType) {
								Debug.Log (grunch.heldProj.GetComponent<ProjectileScript>().projectileType);
								Debug.Log (gunch.heldProj.GetComponent<ProjectileScript>().projectileType);

								//make a variable to hold the position of a, set a's position to b's position. set b's position to the variable
								Vector3 tempPosVar = new Vector3 (gunch.heldProj.transform.position.x, gunch.heldProj.transform.position.y, gunch.heldProj.transform.position.z);
								gunch.heldProj.transform.position = grunch.heldProj.transform.position;
								grunch.heldProj.transform.position = tempPosVar;


								GameObject tempGOVar = gunch.heldProj;
								gunch.heldProj = grunch.heldProj;
								grunch.heldProj = tempGOVar;

								gunch.heldProj.transform.parent = gunch.transform;
								grunch.heldProj.transform.parent = grunch.transform;
								DoACheck ();
								GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().numOfSwaps -= 1;
								GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().UpdateAvailableBuffs ();
								return;
							}

						}
					}

					DoACheck ();
					return;
				}
			}
		}
	}

	public void Morph() {
		foreach (bNodeScript gunch in nodes) {
			if (gunch.heldProj != null) {
				if (gunch.nodeType != gunch.heldProj.GetComponent<ProjectileScript> ().projectileType
					&& gunch.heldProj.GetComponent<ProjectileScript> ().projectileType != 7) {
					GameObject newProjectile = Instantiate (GameObject.Find("Spawner").GetComponent<SpawnerScript>().projPrefabs[gunch.nodeType+1]) as GameObject;
					newProjectile.transform.position = gunch.transform.position;
					newProjectile.transform.parent = gunch.transform;
					newProjectile.GetComponent<ProjectileScript> ().moving = true;
					Destroy (gunch.heldProj);
					gunch.heldProj = newProjectile;
					DoACheck ();
					GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().numOfMorphs -= 1;
					GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().UpdateAvailableBuffs ();
					return;
				}
			}
		}
	}

	public void Purify() {
		foreach (bNodeScript gunch in nodes) {
			if (gunch.heldProj != null) {
				if (gunch.nodeType == 8) {
					gunch.thisNodeIsFilled = false;
					Destroy (gunch.heldProj);
					DoACheck ();
					GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().numOfPurifys -= 1;
					GameObject.Find ("Buff Master").GetComponent<BuffMasterScript> ().UpdateAvailableBuffs ();
					return;
				}
			}
		}
	}
}
