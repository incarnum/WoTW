using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bNodeScript : MonoBehaviour {

	//the number of this node for lists of all nodes, assigned in editor
	public int bNodeNumber;
	//what kind of projectile this node is looking for
	public int nodeType;
	//projectile held by node
	public GameObject heldProj;

	private CoreScript coreRef;

	//for fill up mode
	public float currentFill;
	public float maxFill;
	public float fillRate;
	public float currentFillTo;
	public float fillIncrement;

	//what type of node this is
	public bool randomNodeType;
	public bool fillUpNodeType;
	public bool thisNodeIsFilled;
	//bar reference for fill up mode
	private GameObject targetBar;

	// Use this for initialization
	void Start () {
		if (bNodeNumber == 1) {
			targetBar = GameObject.Find ("Bar1");
			gameObject.name = "JNode1";
		} else if (bNodeNumber == 2) {
			targetBar = GameObject.Find ("Bar2");
			gameObject.name = "JNode2";
		} else if (bNodeNumber == 3) {
			targetBar = GameObject.Find ("Bar3");
			gameObject.name = "JNode3";
		} else if (bNodeNumber == 4) {
			targetBar = GameObject.Find ("Bar4");
			gameObject.name = "JNode4";
		}


//		if (randomNodeType)
//		nodeType = Random.Range (1, 7);
//		
		UpdateColors();

		coreRef = GameObject.Find ("Core").GetComponent<CoreScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fillUpNodeType) {
			IncreaseFillSize ();
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Projectile") {

			if (fillUpNodeType) {
				FillUpFunction (coll);
			} else if (!fillUpNodeType) {
				SnapFunction (coll);
			}
		}
	}
		

	void IncreaseFillSize(){
		if (currentFill <= currentFillTo) {
			currentFill += fillRate * Time.deltaTime;
			GetComponentsInChildren<Transform> ()[1].localScale = new Vector3 (currentFill, currentFill, currentFill);
		}
	}

	void FillUpFunction(Collider2D coll){
		if (coll.GetComponent<ProjectileScript> ().projectileType == nodeType || coll.GetComponent<ProjectileScript> ().projectileType == 1) {
			if (currentFillTo < maxFill) {
				currentFillTo += fillIncrement;
				targetBar.GetComponent<barScript> ().UpdateFillSize (.25f);
				if (currentFillTo >= maxFill) {
					if (bNodeNumber == 1) {
						GameObject.Find ("Core").GetComponent<CoreScript> ().req1 = true;
						//GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
					} else if (bNodeNumber == 2) {
						GameObject.Find ("Core").GetComponent<CoreScript> ().req2 = true;
						//GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
					} else if (bNodeNumber == 3) {
						GameObject.Find ("Core").GetComponent<CoreScript> ().req3 = true;
						//GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
					} else if (bNodeNumber == 4) {
						GameObject.Find ("Core").GetComponent<CoreScript> ().req4 = true;
						//GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
					}
				}
			} else {
				GameObject.Find ("Bar6").GetComponent<barScript> ().UpdateFillSize (-.2f);
				GameObject.Find ("Core").GetComponent<CoreScript> ().CheckDefeat ();
			}
		} else if (coll.GetComponent<ProjectileScript> ().projectileType != nodeType) {
			GameObject.Find ("Bar6").GetComponent<barScript> ().UpdateFillSize (-.2f);
			GameObject.Find ("Core").GetComponent<CoreScript> ().CheckDefeat ();
		}
		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().RemoveProjectileFromList (coll.gameObject);
		Destroy (coll.gameObject);
	}

	void SnapFunction(Collider2D coll) {
		
		if (!thisNodeIsFilled) {
			//GameObject.Find ("Core").GetComponent<CoreScript> ().ChangeHoldStatus (gameObject, coll.gameObject);
			coll.GetComponent<ProjectileScript> ().moving = false;
			coll.transform.position = transform.position;
			coll.transform.parent = transform;
			thisNodeIsFilled = true;
			heldProj = coll.gameObject;
			coreRef.DoACheck ();


//			if (coll.GetComponent<ProjectileScript> ().projectileType == nodeType || coll.GetComponent<ProjectileScript> ().projectileType == 1) {
//				if (bNodeNumber == 1) {
//					GameObject.Find ("Core").GetComponent<CoreScript> ().req1 = true;
//					GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
//				} else if (bNodeNumber == 2) {
//					GameObject.Find ("Core").GetComponent<CoreScript> ().req2 = true;
//					GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
//				} else if (bNodeNumber == 3) {
//					GameObject.Find ("Core").GetComponent<CoreScript> ().req3 = true;
//					GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
//				} else if (bNodeNumber == 4) {
//					GameObject.Find ("Core").GetComponent<CoreScript> ().req4 = true;
//					GameObject.Find ("Core").GetComponent<CoreScript> ().CheckVictory ();
//				}
//			}
		} else {
			Debug.Log ("We've been hit!");
			Destroy (coll.gameObject);
		}
	}

	public void UpdateColors(){
		if (nodeType == 8) {
			GetComponent<SpriteRenderer> ().color = new Color32(0xC3, 0xD1, 0xE8, 0xFF);
		} else if (nodeType == 0) {
			GetComponent<SpriteRenderer> ().color = new Color32(0x07, 0xEA, 0x67, 0xFF);
		} else if (nodeType == 1) {
			GetComponent<SpriteRenderer> ().color = new Color32(0x2A, 0x14, 0xD2, 0xFF);
		} else if (nodeType == 2) {
			GetComponent<SpriteRenderer> ().color = new Color32(0xFF, 0x4D, 0x38, 0xFF);
		} else if (nodeType == 4) {
			GetComponent<SpriteRenderer> ().color = new Color32(0x7E, 0x61, 0x2E, 0xFF);
		} else if (nodeType == 5) {
			GetComponent<SpriteRenderer> ().color = new Color32(0xDA, 0x08, 0xB1, 0xFF);
		} else if (nodeType == 6) {
			GetComponent<SpriteRenderer> ().color = new Color32(0xE3, 0xDD, 0x19, 0xFF);
		}
	}
}

//color C3D1E8FF