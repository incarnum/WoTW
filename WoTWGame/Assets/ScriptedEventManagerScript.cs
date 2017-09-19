using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEventManagerScript : MonoBehaviour {
	public int phase;
	private float nextEventTime;
	private Animator playerAnim;
	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;
	public GameObject prefab4;

	public float delayToEvent1;
	public float delayToEvent5;
	// Use this for initialization
	void Start () {
		nextEventTime = delayToEvent1 + Time.time;
		playerAnim = GameObject.Find ("Player").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (nextEventTime < Time.time) {
			NextEvent ();
			nextEventTime = Mathf.Infinity;
		}
	}

	public void NextEvent() {
		Debug.Log ("event1");
		if (phase == 0) {
			CreateAtPlayerMoveBasedPosition (prefab1);
		} else if (phase == 1) {
			CreateAtPreordainedPosition (prefab2);
		} else if (phase == 2) {
			CreateAtPreordainedPosition2 (prefab3);
		} else if (phase == 3) {
			nextEventTime = Time.time + delayToEvent5;
		} else if (phase == 4) {
			GameObject.Find ("SWolf1").GetComponent<AssignedAnimalMovementScript> ().MoveToNextSpot();
			CreateAtPreordainedPosition3 (prefab4);
		}
		phase += 1;
	}



	void CreateAtPlayerMoveBasedPosition(GameObject pref){
		GameObject newCreature = Instantiate (pref) as GameObject;
		newCreature.transform.position = playerAnim.transform.position;
		if (playerAnim.GetFloat ("LastMoveX") > 0f) {
			newCreature.transform.position += new Vector3 (8, 0);
		} else if (playerAnim.GetFloat ("LastMoveX") < 0f) {
			newCreature.transform.position += new Vector3 (-8, 0);
		}
		if (playerAnim.GetFloat ("LastMoveY") > 0f) {
			newCreature.transform.position += new Vector3 (0, 6);
		} else if (playerAnim.GetFloat ("LastMoveY") < 0f) {
			newCreature.transform.position += new Vector3 (0, -6);
		}

	}

	void CreateAtPreordainedPosition(GameObject pref) {
		GameObject newCreature = Instantiate (pref) as GameObject;
		newCreature.transform.position = playerAnim.transform.position;
		newCreature.transform.position += new Vector3 (-8, 0);
		newCreature.GetComponentsInChildren<AssignedAnimalMovementScript>()[0].MoveToNextSpot ();
	}

	void CreateAtPreordainedPosition2(GameObject pref) {
		GameObject newCreature = Instantiate (pref) as GameObject;
		newCreature.transform.position = playerAnim.transform.position;
		newCreature.transform.position += new Vector3 (-8, -6);
		newCreature.GetComponentsInChildren<AssignedAnimalMovementScript> () [0].target = GameObject.Find ("SDeer1").transform.position;
		newCreature.GetComponentsInChildren<AssignedAnimalMovementScript>()[0].MoveToNextSpot ();
	}

	void CreateAtPreordainedPosition3(GameObject pref) {
		GameObject newCreature = Instantiate (pref) as GameObject;
		newCreature.transform.position = GameObject.Find ("Player").transform.position;
		newCreature.transform.position += new Vector3 (0, 9);
		newCreature.transform.name = "CorruptedAltar";
		//newCreature.GetComponentsInChildren<AssignedAnimalMovementScript> () [0].target = GameObject.Find ("SDeer1").transform.position;
		//newCreature.GetComponentsInChildren<AssignedAnimalMovementScript>()[0].MoveToNextSpot ();
	}

}
