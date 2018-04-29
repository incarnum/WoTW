using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconManager : MonoBehaviour {
	public GameObject fullMap;
	public GameObject playerIcon;
	public GameObject player;
	public List<CorruptedPylonCoreScript> pylonCircles;
	public List<Animator> minimapIcons;
	public List<Animator> fullMapIcons;
	public AudioSource mapOpen;
	public AudioSource mapClose;
	public MapMovementScript mapMove;
	public AudioSource buttonSounds;
	public AudioClip openMap;
	public AudioClip closeMap;
	public NotesScript notes;

	// Use this for initialization
	void Start () {
		UpdateMinimapIcons ();
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			if (player.GetComponent<PlayerControllerScript> ().paused == false) {
				if (fullMap.activeSelf == false) {
					OpenMap ();
					buttonSounds.PlayOneShot (openMap);
					mapMove.PlacePlayer ();
				} else {
					CloseMap ();
					buttonSounds.PlayOneShot (closeMap);
				}
			}
		}
	}

	public void UpdateMinimapIcons() {
		for (int i = 0; i < pylonCircles.Count; i++) {
			if (pylonCircles [i].health == 0) {
				minimapIcons [i].Play ("Cleansed");
				fullMapIcons [i].Play ("Cleansed");
			} else if (pylonCircles [i].health == 1) {
				minimapIcons [i].Play ("Corr1");
				fullMapIcons [i].Play ("Corr1");
			} else if (pylonCircles [i].health == 2) {
				minimapIcons [i].Play ("Corr2");
				fullMapIcons [i].Play ("Corr2");
			} else if (pylonCircles [i].health == 3) {
				minimapIcons [i].Play ("Corr3");
				fullMapIcons [i].Play ("Corr3");
			}
		}
	}

	public void OpenMap() {
		fullMap.SetActive (true);
		notes.CloseNotes ();
		player.GetComponent<PlayerControllerScript> ().mapPaused = true;
		player.GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
		UpdateMinimapIcons ();
	}

	public void CloseMap() {
		fullMap.SetActive (false);
		player.GetComponent<PlayerControllerScript> ().mapPaused = false;
		player.GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
	}
}
