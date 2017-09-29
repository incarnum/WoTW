using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverlapScript : MonoBehaviour {
	private float positionAdjustmentTime;
	public float adjustmentDuration;
	public bool initializing;
	public bool shouldCollsBeTriggers;

	void Start() {
		positionAdjustmentTime = Time.time + adjustmentDuration;
		initializing = true;
	}

	void Update() {
		if (initializing) {
			if (Time.time >= positionAdjustmentTime) {
				initializing = false;
				GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
				if (GetComponent<ZDepthStaticScript> () != null) {
					GetComponent<ZDepthStaticScript> ().RecheckZDepth ();
				}

				if (shouldCollsBeTriggers) {
					if (GetComponent<BoxCollider2D> () != null) {
						GetComponent<BoxCollider2D> ().isTrigger = true;
					}
					if (GetComponent<CircleCollider2D> () != null) {
						GetComponent<CircleCollider2D> ().isTrigger = true;
					}
				}
			}
		}
	}



























}


//	private Transform upperLeft;
//	private Transform lowerRight;
//	public bool touching;
//	public bool touchingStatic;
//	public bool justMoved;
//	public bool justMoved2;
//	public bool justMoved3;
//	public bool unMoveable;
//	public GameObject thingTouching;
//	// Use this for initialization
//	void Awake () {
//		upperLeft = GameObject.Find ("UpperLeftBound").transform;
//		lowerRight = GameObject.Find ("LowerRightBound").transform;
//
//	}
//
//	void Start () {
//		CheckForOverlaps ();
//	}
//
//	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown (KeyCode.J)) {
//			CheckForOverlaps ();
//		}
//
//		if (justMoved3) {
//			justMoved3 = false;
//			CheckForOverlaps ();
//		}
//		if (justMoved2) {
//			justMoved2 = false;
//			justMoved3 = true;
//		}
//		if (justMoved) {
//			justMoved = false;
//			justMoved2 = true;
//		}
//	}
//
////	public void CheckForOverlaps(){
//////		Debug.Log (GetComponent<Collider2D> ().IsTouching);
////		if (touching == true && !unMoveable) {
////			Debug.Log ("I need to move");
////			touching = false;
////			//touchingStatic = false;
////			transform.position = new Vector2 (Random.Range (upperLeft.position.x, lowerRight.position.x), Random.Range (upperLeft.position.y, lowerRight.position.y));
////			if (GetComponent<ZDepthStaticScript>() != null) {
////				GetComponent<ZDepthStaticScript> ().RecheckZDepth ();
////			}
////			justMoved = true;
//////			CheckForOverlaps ();
////		}  else {
////			unMoveable = true;
////		}
////		if (touchingStatic) {
////			Debug.Log ("I need to move STATIC");
////			touchingStatic = false;
////			transform.position = new Vector2 (Random.Range (upperLeft.position.x, lowerRight.position.x), Random.Range (upperLeft.position.y, lowerRight.position.y));
////			if (GetComponent<ZDepthStaticScript>() != null) {
////				GetComponent<ZDepthStaticScript> ().RecheckZDepth ();
////			}
////			justMoved = true;
////		}
////	}
//
//	public void CheckForOverlaps(){
//		//		Debug.Log (GetComponent<Collider2D> ().IsTouching);
//		if (touching == true && !unMoveable) {
//			Destroy (gameObject);
//		}
//	}
//
//
//	void OnTriggerEnter2D(Collider2D coll) {
////		if (!unMoveable) {
//			if (coll.gameObject.layer == 9 || coll.gameObject.layer == 11) {
//				touching = true;
//				//CheckForOverlaps ();
//			//Debug.Log(transform.position);
//			//Destroy(gameObject);
//			thingTouching = coll.gameObject;
////			}
//		}
//
//		if (coll.gameObject.layer == 9) {
//			touchingStatic = true;
//		}
//	}
//}
