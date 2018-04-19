using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxScript : MonoBehaviour {
	public int position;
	public WorldAnalyzerScript analyzer;
	public bool touching;
	public GameObject saveWindow;
	public GameObject player;
	public GameObject saveText;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		analyzer = GameObject.Find ("LoadingManager").GetComponent<WorldAnalyzerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (touching && Input.GetButtonDown ("Select")) {
			if (saveWindow.activeSelf == true) {
				CloseWindow ();
			} else if (player.GetComponent<PlayerControllerScript>().canMove) {
				print ("open it");
				saveWindow.SetActive (true);
				player.GetComponent<PlayerControllerScript> ().dialoguePaused = true;
				player.GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "energy") {
			print ("Pet Fox");
			touching = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "energy") {
			touching = false;
		}
	}

	public void CloseWindow() {
		saveWindow.SetActive (false);
		player.GetComponent<PlayerControllerScript> ().dialoguePaused = false;
		player.GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
		saveText.SetActive (false);
	}

	public void Save() {
		analyzer.location = position;
		analyzer.Save ();
		saveText.SetActive (true);
	}


}
