using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureManagerScript : MonoBehaviour {
	//private Transform playerTrans;
	public List<GameObject> shrubCreatureList;
	public List<GameObject> berryList;
	public List<GameObject> shrubBallList;
	public GameObject shrubPrefab;
	public GameObject berryPrefab;
	public float shrubNum;

	public List<GameObject> deerCreatureList;
	public List<GameObject> antlerList;
	public List<GameObject> deerBallList;
	public GameObject deerPrefab;
	public GameObject antlerPrefab;
	public float deerNum;

	public List<GameObject> wolfCreatureList;
	public List<GameObject> fangList;
	public List<GameObject> wolfBallList;
	public GameObject wolfPrefab;
	public GameObject fangPrefab;
	public float wolfNum;

	public GameObject ballPrefab;

	public List<GameObject> corruptedShrubCreatureList;
	public List<GameObject> corruptedBerryList;
	public GameObject corruptedShrubPrefab;
	public GameObject corruptedBerryPrefab;
	public float corruptedShrubNum;

	public List<GameObject> corruptedDeerCreatureList;
	public List<GameObject> corruptedAntlerList;
	public GameObject corruptedDeerPrefab;
	public GameObject corruptedAntlerPrefab;
	public float corruptedDeerNum;

	public List<GameObject> corruptedWolfCreatureList;
	public List<GameObject> corruptedFangList;
	public GameObject corruptedWolfPrefab;
	public GameObject corruptedFangPrefab;
	public float corruptedWolfNum;

	public List<GameObject> corruptionNodeList;

	private Transform upperLeftBound;
	private Transform lowerRightBound;

	// Use this for initialization
	void Start () {
		upperLeftBound = GetComponentsInChildren<Transform> ()[1];
		lowerRightBound = GetComponentsInChildren<Transform> ()[2];
		AdjustCreatures ();
		AdjustPickips ();
//		playerTrans = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			AdjustCreatures();
			AdjustPickips ();
		}
	}

	public void AdjustCreatures() {
		//shrubs
		while (shrubCreatureList.Count > shrubNum + 1f) {
			Destroy (shrubCreatureList [0]);
			shrubCreatureList.RemoveAt (0);
//			bool garfBool = false;
//			int garfield = 0;
//			while (garfBool == false) {
//				if ((shrubCreatureList [garfield].transform.position - playerTrans.position).magnitude > 10) {
//					Destroy (shrubCreatureList [garfield]);
//					shrubCreatureList.RemoveAt (garfield);
//					garfBool = true;
//				} else {
//					garfield += 1;
//					if (garfield - 1 > shrubCreatureList.Count) {
//						Destroy (shrubCreatureList [0]);
//						shrubCreatureList.RemoveAt (0);
//					}
//				}
//			}
		}
		while (shrubCreatureList.Count < shrubNum) {
			CreateShrub ();
		}
		//deer
		while (deerCreatureList.Count > deerNum + 1f) {
			Destroy (deerCreatureList [0]);
			deerCreatureList.RemoveAt (0);
		}
		while (deerCreatureList.Count < deerNum) {
			CreateDeer ();
		}
		//wolves
		while (wolfCreatureList.Count > wolfNum + 1f) {
			Destroy (wolfCreatureList [0]);
			wolfCreatureList.RemoveAt (0);
			Debug.Log ("Killed wolf");
		}
		while (wolfCreatureList.Count < wolfNum) {
			CreateWolf ();
		}
		//corruptedshrubs
		while (corruptedShrubCreatureList.Count > corruptedShrubNum + 1f && corruptedShrubNum > 0) {
			Destroy (corruptedShrubCreatureList [0]);
			corruptedShrubCreatureList.RemoveAt (0);
		}
		while (corruptedShrubCreatureList.Count < corruptedShrubNum) {
			CreateCorruptedShrub ();
		}
		//corrupteddeer
		while (corruptedDeerCreatureList.Count > corruptedDeerNum + 1f && corruptedDeerNum > 0) {
			Destroy (corruptedDeerCreatureList [0]);
			corruptedDeerCreatureList.RemoveAt (0);
		}
		while (corruptedDeerCreatureList.Count < corruptedDeerNum) {
			CreateCorruptedDeer ();
		}
		//corruptedwolves
		while (corruptedWolfCreatureList.Count > corruptedWolfNum +1f && corruptedWolfNum > 0) {
			Destroy (corruptedWolfCreatureList [0]);
			corruptedWolfCreatureList.RemoveAt (0);
		}
		while (corruptedWolfCreatureList.Count < corruptedWolfNum) {
			CreateCorruptedWolf ();
		}
	
	}

	public void AdjustPickips() {

//		if (berryList.Count > shrubNum) {
//			while (berryList.Count > shrubNum) {
//				Destroy (berryList [0]);
//				berryList.RemoveAt (0);
//			}
//		}
//		if (berryList.Count < shrubNum) {
//			while (berryList.Count < shrubNum) {
//				CreateBerry ();
//			}
//		}

		while (antlerList.Count > deerNum + 1f) {
			Destroy (antlerList [0]);
			antlerList.RemoveAt (0);
		}

		while (antlerList.Count < deerNum) {
			CreateAntler ();
		}

		while (fangList.Count > wolfNum + 1f) {
			Destroy (fangList [0]);
			fangList.RemoveAt (0);
		}
		if (fangList.Count < wolfNum) {
			while (fangList.Count < wolfNum) {
				CreateFang ();
			}
		}

		while (corruptedAntlerList.Count > corruptedDeerNum + 1f && corruptedDeerNum > 0) {
			Destroy (corruptedAntlerList [0]);
			corruptedAntlerList.RemoveAt (0);
		}
		if (corruptedAntlerList.Count < corruptedDeerNum) {
			while (corruptedAntlerList.Count < corruptedDeerNum) {
				CreateCorruptedAntler ();
			}
		}

		while (corruptedFangList.Count > corruptedWolfNum + 1f && corruptedWolfNum > 0) {
			Destroy (corruptedFangList [0]);
			corruptedFangList.RemoveAt (0);
		}
		if (corruptedFangList.Count < corruptedWolfNum) {
			while (corruptedFangList.Count < corruptedWolfNum) {
				CreateCorruptedFang ();
			}
		}
	}

	public void CreateShrub() {
		GameObject newShrub = Instantiate (shrubPrefab) as GameObject;
		shrubCreatureList.Add (newShrub);
		newShrub.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newShrub.transform.position -= (newShrub.transform.position - GameObject.Find ("ShrubNucleus").transform.position) / 2;

		//GameObject newBall = Instantiate (ballPrefab) as GameObject;
		//shrubBallList.Add (newBall);
		//newBall.transform.position = GameObject.Find ("Shrub Bar Top").transform.position;
	}

	public void CreateCorruptedShrub() {
		GameObject newCorruptedShrub = Instantiate (corruptedShrubPrefab) as GameObject;
		corruptedShrubCreatureList.Add (newCorruptedShrub);
		newCorruptedShrub.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newCorruptedShrub.transform.position -= (newCorruptedShrub.transform.position - GameObject.Find ("ShrubNucleus").transform.position) / 2;
	}

	public void CreateBerry() {
		GameObject newBerry = Instantiate (berryPrefab) as GameObject;
		berryList.Add (newBerry);
		newBerry.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newBerry.transform.position -= (newBerry.transform.position - GameObject.Find ("ShrubNucleus").transform.position) / 2;
	}

	public void CreateCorruptedBerry() {
		GameObject newCorruptedBerry = Instantiate (corruptedBerryPrefab) as GameObject;
		corruptedBerryList.Add (newCorruptedBerry);
		newCorruptedBerry.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newCorruptedBerry.transform.position -= (newCorruptedBerry.transform.position - GameObject.Find ("ShrubNucleus").transform.position) / 2;
	}

	public void CreateDeer() {
		GameObject newDeer = Instantiate (deerPrefab) as GameObject;
		deerCreatureList.Add (newDeer);
		newDeer.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newDeer.transform.position -= (newDeer.transform.position - GameObject.Find ("DeerNucleus").transform.position) / 2;
	}

	public void CreateCorruptedDeer() {
		GameObject newCorruptedDeer = Instantiate (corruptedDeerPrefab) as GameObject;
		corruptedDeerCreatureList.Add (newCorruptedDeer);
		newCorruptedDeer.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newCorruptedDeer.transform.position -= (newCorruptedDeer.transform.position - GameObject.Find ("DeerNucleus").transform.position) / 2;
	}

	public void CreateAntler() {
		GameObject newAntler = Instantiate (antlerPrefab) as GameObject;
		antlerList.Add (newAntler);
		newAntler.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newAntler.transform.position -= (newAntler.transform.position - GameObject.Find ("DeerNucleus").transform.position) / 2;
	}

	public void CreateCorruptedAntler() {
		GameObject newAntler = Instantiate (corruptedAntlerPrefab) as GameObject;
		corruptedAntlerList.Add (newAntler);
		newAntler.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newAntler.transform.position -= (newAntler.transform.position - GameObject.Find ("DeerNucleus").transform.position) / 2;
	}

	public void CreateWolf() {
		GameObject newWolf = Instantiate (wolfPrefab) as GameObject;
		wolfCreatureList.Add (newWolf);
		newWolf.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newWolf.transform.position -= (newWolf.transform.position - GameObject.Find ("WolfNucleus").transform.position) / 2;
	}

	public void CreateCorruptedWolf() {
		GameObject newWolf = Instantiate (corruptedWolfPrefab) as GameObject;
		corruptedWolfCreatureList.Add (newWolf);
		newWolf.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newWolf.transform.position -= (newWolf.transform.position - GameObject.Find ("WolfNucleus").transform.position) / 2;
	}

	public void CreateFang() {
		GameObject newFang = Instantiate (fangPrefab) as GameObject;
		fangList.Add (newFang);
		newFang.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newFang.transform.position -= (newFang.transform.position - GameObject.Find ("WolfNucleus").transform.position) / 2;
	}

	public void CreateCorruptedFang() {
		GameObject newFang = Instantiate (corruptedFangPrefab) as GameObject;
		corruptedFangList.Add (newFang);
		newFang.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
		newFang.transform.position -= (newFang.transform.position - GameObject.Find ("WolfNucleus").transform.position) / 2;
	}
		
}
