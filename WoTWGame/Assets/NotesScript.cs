using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour {
	public PlayerControllerScript player;
	public MapIconManager mim;
	public bool notesOpen;
	public int pageNumber;
	public GameObject book;
	public GameObject page0;
	public GameObject page1;
	public GameObject page2;
	public AudioSource buttonSounds;
	public AudioClip openMap;
	public AudioClip closeMap;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerControllerScript>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			if (!player.paused && !player.popBarPaused && !player.pylonPaused && !player.dialoguePaused && notesOpen == false) {
				OpenNotes ();
			} else if (notesOpen == true) {
				CloseNotes ();
				buttonSounds.PlayOneShot (closeMap);
			}
		}

		if (notesOpen) {
			if (Input.GetButtonDown("Left")) {
				if (pageNumber > 0) {
					pageNumber -= 1;
					buttonSounds.PlayOneShot (openMap);
				}
				UpdatePage ();
			}

			if (Input.GetButtonDown("Right")) {
				if (pageNumber < 2) {
					pageNumber += 1;
					buttonSounds.PlayOneShot (openMap);
				}
				UpdatePage ();
			}
		}
	}

	public void OpenNotes() {
		mim.CloseMap ();
		notesOpen = true;
		book.SetActive (true);
		player.notesPaused = true;
		player.CheckIfICanMove ();
		buttonSounds.PlayOneShot (openMap);
		UpdatePage ();
	}

	public void CloseNotes() {
		player.notesPaused = false;
		player.CheckIfICanMove ();
		notesOpen = false;
		book.SetActive (false);
		page0.SetActive (false);
		page1.SetActive (false);
		page2.SetActive (false);
	}

	public void UpdatePage() {
		if (pageNumber == 0) {
			page0.SetActive (true);
			page1.SetActive (false);
			page2.SetActive (false);
		} else 	if (pageNumber == 1) {
			page1.SetActive (true);
			page0.SetActive (false);
			page2.SetActive (false);
		} else 	if (pageNumber == 2) {
			page2.SetActive (true);
			page0.SetActive (false);
			page1.SetActive (false);
		}
	}
}
