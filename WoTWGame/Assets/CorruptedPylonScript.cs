using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedPylonScript : MonoBehaviour {
	//this script is specifically for pylons that just say "CORRUPTED" when you open them, and don't give you any options
	private bool windowActive;
	public GameObject window;
	private bool touching;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (touching && Input.GetKeyDown (KeyCode.E)) {
			if (!windowActive) {
				window.SetActive (true);
				player.GetComponent<PlayerControllerScript> ().canMove = false;
				player.GetComponent<PlayerControllerB> ().canMove = false;
				windowActive = true;

			} else {
				window.SetActive (false);
				player.GetComponent<PlayerControllerScript> ().canMove = true;
				player.GetComponent<PlayerControllerB> ().canMove = true;
				windowActive = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.Q) && windowActive) {
			window.SetActive (false);
			player.GetComponent<PlayerControllerScript> ().canMove = true;
			player.GetComponent<PlayerControllerB> ().canMove = true;
			windowActive = false;

		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			touching = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			touching = false;
		}
	}

}
