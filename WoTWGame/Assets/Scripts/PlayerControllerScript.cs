﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour {
	public float speed;
	Vector2 movement;
	Rigidbody2D rb;
	public bool canMove;
	public bool paused;
	public bool popBarPaused;
	public bool dialoguePaused;
    public bool pylonPaused;
    public bool mapPaused;
	public bool notesPaused;
	public bool noChargeMode;
	private Vector3 worldUpperLeft;
	private Vector3 worldLowerRight;
	private GameObject mapIcon;
	public GameObject corrIconPrefab;
	public List<GameObject> corrIconList;
	private GameObject actionBar;
	private GameObject multiMenu;
	private GameObject buttonHolder;
    private GameObject pauseCanvas;
    public GameObject uiManager;
    public Vector3? targetPosition;
	public SimpleEcologyMasterScript eco;
	public GameObject map;
	public MapIconManager minimap;
	public NotesScript notes;
	public AudioSource buttonSounds;
	public AudioClip buttonMid;
	public AudioClip buttonLow;
	public AudioClip closeMap;
	public AudioClip closeBook;

	public delegate void PauseAction();
	public static event PauseAction OnPaused;
	public static event PauseAction OnUnpaused;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		worldUpperLeft = GameObject.Find ("WorldUpperLeft").transform.position;
		worldLowerRight = GameObject.Find ("WorldLowerRight").transform.position;
		actionBar = GameObject.Find ("ActionBar");
		multiMenu = GameObject.Find ("MultiMenu");
		buttonHolder = GameObject.Find ("ButtonHolder");
        pauseCanvas = GameObject.Find("PauseCanvas");
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
	}

	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
			if (pauseCanvas.GetComponent<PauseScript> ().optionsOpened) {
				pauseCanvas.GetComponent<PauseScript> ().ReturnToPause ();
				buttonSounds.PlayOneShot (buttonLow);
			} else if (pauseCanvas.GetComponent<PauseScript> ().areYouSureOpened) {
				pauseCanvas.GetComponent<PauseScript> ().areYouSure.SetActive (false);
				pauseCanvas.GetComponent<PauseScript> ().areYouSure2.SetActive (false);
				pauseCanvas.GetComponent<PauseScript> ().areYouSureOpened = false;
				buttonSounds.PlayOneShot (buttonLow);
			} else if (mapPaused == true) {
				minimap.CloseMap ();
				buttonSounds.PlayOneShot (closeMap);
			} else if (notesPaused == true) {
				notes.CloseNotes ();
				buttonSounds.PlayOneShot (closeBook);
			} else if (pylonPaused == true) {

			} else {
				ToggleMenuPause ();
            }


        }
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            uiManager.SetActive(!uiManager.activeSelf);
//        }
		//commented out because this is now being done in the button
            if (Input.GetKeyDown (KeyCode.Return)) {
			GameObject.Find ("MakeTome").GetComponent<MakeTomeButtonScript> ().CreateSpellButton ();
		}

		if (Input.GetKeyDown (KeyCode.RightBracket) && !eco.demo) {
			speed += 2;
		} else if (Input.GetKeyDown (KeyCode.LeftBracket) && !eco.demo) {
			speed -= 2;
		}

		if (Input.GetKey (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.R) && !eco.demo) {
			SceneManager.LoadScene ("MainMenu");
		}
		if (Input.GetKey (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.Alpha1) && !eco.demo) {
			SceneManager.LoadScene ("Forest");
		}
		if (Input.GetKey (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.Q) && !eco.demo) {
			Application.Quit();
		}
//        if (Input.GetKeyDown(KeyCode.Mouse0))
//        {
//            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 189));
//			//print (targetPosition);
//        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        
        //print(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -2)));
		if (canMove) {
            
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");
			if (h != 0 || v != 0) {
				//anim.SetBool ("Moving", true);
				Move (h, v);
//                targetPosition = null;
                //anim.SetFloat ("H", Input.GetAxisRaw ("Horizontal"));
                //anim.SetFloat ("V", Input.GetAxisRaw ("Vertical"));
            }  else {
				//anim.SetBool ("Moving", false);
			}
//            if (targetPosition != null)
//            {
//				Move(targetPosition.Value.x - transform.position.x, targetPosition.Value.y - transform.position.y);
//            }
//			if (targetPosition.Value.x < transform.position.x + .1 && targetPosition.Value.x > transform.position.x - .1 && targetPosition.Value.y < transform.position.y + .1 && targetPosition.Value.y > transform.position.y - .1)
//            {
//                rb.velocity.Set(0, 0);
//                targetPosition = null;
//            }
		}
	}

	void Move (float h, float v) {
		movement.Set (h, v);
		movement = movement.normalized * speed * Time.deltaTime;
		movement.y = movement.y * .75f;
		rb.MovePosition ((Vector2)gameObject.transform.position + movement);
	}

	public void Pause () {
			if (OnPaused != null)
				OnPaused ();
			CheckIfICanMove ();
		buttonSounds.PlayOneShot (buttonMid);
			GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ().TimeStopped ();
			GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ().paused = true;
	}

	public void UnPause() {
			if (OnUnpaused != null)
				OnUnpaused ();
			CheckIfICanMove ();
		buttonSounds.PlayOneShot (buttonLow);
			GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ().TimeResumed ();
			GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ().paused = false;
	}

	public void TogglePause() {
		if (!paused) {
			paused = true;
			Pause ();
		} else {
			paused = false;
			UnPause ();
		}
	}

	public void ToggleMenuPause() {
		if (!paused) {
			paused = true;
			Pause ();
			pauseCanvas.GetComponent<PauseScript>().PauseGame();
		} else {
			paused = false;
			UnPause ();
			pauseCanvas.GetComponent<PauseScript>().ResumeGame();
		}
	}

	public void CheckIfICanMove() {
		if (!paused && !popBarPaused && !dialoguePaused && !pylonPaused && !mapPaused && !notesPaused) {
			canMove = true;
			GetComponent<PlayerControllerB> ().canMove = true;

		} else {
			canMove = false;
			GetComponent<PlayerControllerB> ().canMove = false;
		}
	}
}
