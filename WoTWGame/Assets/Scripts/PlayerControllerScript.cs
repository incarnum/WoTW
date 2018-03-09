using System.Collections;
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
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.GetComponent<PauseScript>().optionsOpened)
            {
                pauseCanvas.GetComponent<PauseScript>().ReturnToPause();
            }
            else
            {
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

		if (Input.GetKeyDown (KeyCode.RightBracket)) {
			speed += 2;
		} else if (Input.GetKeyDown (KeyCode.LeftBracket)) {
			speed -= 2;
		}

		if (Input.GetKey (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("MainMenu");
		}
		if (Input.GetKey (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("Forest");
		}
		if (Input.GetKey (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.Q)) {
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
			GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ().TimeStopped ();
			GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ().paused = true;
	}

	public void UnPause() {
			if (OnUnpaused != null)
				OnUnpaused ();
			CheckIfICanMove ();
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
		if (!paused && !popBarPaused && !dialoguePaused && !pylonPaused && !mapPaused) {
			canMove = true;
			GetComponent<PlayerControllerB> ().canMove = true;

		} else {
			canMove = false;
			GetComponent<PlayerControllerB> ().canMove = false;
		}
	}
}
