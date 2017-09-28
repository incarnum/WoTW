using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverlapScript : MonoBehaviour {
	private Transform upperleft;
	private Transform lowerRight;
	private bool touching;
	// Use this for initialization
	void Start () {
		upperleft = GameObject.Find ("UpperLeftBound").transform;
		lowerRight = GameObject.Find ("LowerRightBound").transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			CheckForOverlaps ();
		}
	}

	public void CheckForOverlaps(){
//		Debug.Log (GetComponent<Collider2D> ().IsTouching);
		if (GetComponent<Collider2D> ().IsTouchingLayers (9) || GetComponent<Collider2D> ().IsTouchingLayers (11)) {

		}
		if (touching == true) {
			Debug.Log ("I need to move");
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.layer == 9 || coll.gameObject.layer == 11) {
			touching = true;
		}
	}
}
