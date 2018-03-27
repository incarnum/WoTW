using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabOverlayScript : MonoBehaviour {
	private bool onPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Tab)) {
            Debug.Log("Pressed Tab");
			if (onPlayer == false) {
					transform.parent = GameObject.Find ("Player").transform;
					transform.localPosition = new Vector3 (2, 0, 0);
					onPlayer = true;
			} else {
				transform.parent = GameObject.Find ("Map").transform;
				transform.localPosition = new Vector3 (1, 0, 0);
				onPlayer = false;
			}
		}

//		if (Input.GetKeyUp (KeyCode.Tab)) {
//			transform.parent = GameObject.Find ("Map").transform;
//			transform.localPosition = new Vector3 (1, 0, 0);
//		}
	}
}
