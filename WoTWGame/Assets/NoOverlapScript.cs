using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverlapScript : MonoBehaviour {
	private Transform upperLeft;
	private Transform lowerRight;
	private bool touching;
	public bool justMoved;
	public bool justMoved2;
	private bool unMoveable;
	// Use this for initialization
	void Awake () {
		upperLeft = GameObject.Find ("UpperLeftBound").transform;
		lowerRight = GameObject.Find ("LowerRightBound").transform;

	}

	void Start () {
		CheckForOverlaps ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			CheckForOverlaps ();
		}


		if (justMoved2) {
			justMoved2 = false;
			CheckForOverlaps ();
			Debug.Log ("ding");
		}
		if (justMoved) {
			justMoved = false;
			justMoved2 = true;
		}
	}

	public void CheckForOverlaps(){
//		Debug.Log (GetComponent<Collider2D> ().IsTouching);
		if (touching == true) {
			Debug.Log ("I need to move");
			touching = false;
			transform.position = new Vector2 (Random.Range (upperLeft.position.x, lowerRight.position.x), Random.Range (upperLeft.position.y, lowerRight.position.y));
			if (GetComponent<ZDepthStaticScript>() != null) {
				GetComponent<ZDepthStaticScript> ().RecheckZDepth ();
			}
			justMoved = true;
//			CheckForOverlaps ();
		}  else {
			//unMoveable = true;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!unMoveable) {
			if (coll.gameObject.layer == 9 || coll.gameObject.layer == 11) {
				touching = true;
				//CheckForOverlaps ();
			//Debug.Log(transform.position);
			//Destroy(gameObject);
			}
		}
	}
}
