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
                Pause();
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 400));
        }
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
                targetPosition = null;
                //anim.SetFloat ("H", Input.GetAxisRaw ("Horizontal"));
                //anim.SetFloat ("V", Input.GetAxisRaw ("Vertical"));
            }  else {
				//anim.SetBool ("Moving", false);
			}
            if (targetPosition != null)
            {
                rb.MovePosition(new Vector2(targetPosition.Value.x, targetPosition.Value.y));
            }
            if (targetPosition.Value.x == transform.position.x && targetPosition.Value.y == transform.position.y)
            {
                rb.velocity.Set(0, 0);
                targetPosition = null;
            }
		}
	}

	void Move (float h, float v) {
		movement.Set (h, v);
		movement = movement.normalized * speed * Time.deltaTime;
		movement.y = movement.y * .75f;
		rb.MovePosition ((Vector2)gameObject.transform.position + movement);
	}

	public void Pause () {
		if (!paused) {
			//Camera.main.transform.position = new Vector3(GameObject.Find ("Map").transform.position.x, GameObject.Find ("Map").transform.position.y, GameObject.Find ("Map").transform.position.z - 5);
			//Time.timeScale = 0;
			paused = true;
			GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ().TimeStopped ();
			canMove = false;
			GetComponent<PlayerControllerB> ().canMove = false;
            pauseCanvas.GetComponent<PauseScript>().PauseGame();
			foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().deerCreatureList) {
				gunch.GetComponent<AnimalMovementScript> ().canMove = false;
			}
			foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().wolfCreatureList) {
				gunch.GetComponent<AnimalMovementScript> ().canMove = false;
			}
			GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ().paused = true;

		} else if (paused) {
			//Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100f);
			paused = false;
			GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ().TimeResumed ();
			canMove = true;
			GetComponent<PlayerControllerB> ().canMove = true;
            pauseCanvas.GetComponent<PauseScript>().ResumeGame();

			foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().deerCreatureList) {
				gunch.GetComponent<AnimalMovementScript> ().canMove = true;
			}
			foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().wolfCreatureList) {
				gunch.GetComponent<AnimalMovementScript> ().canMove = true;
			}

			foreach (GameObject gunch in corrIconList) {
				Destroy (gunch);
			}
			corrIconList.Clear();

			GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ().paused = false;
		}
	}
}
