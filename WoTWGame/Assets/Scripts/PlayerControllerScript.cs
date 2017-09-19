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
	private Vector3 mapUpperLeft;
	private Vector3 mapLowerRight;
	private GameObject mapIcon;
	public GameObject corrIconPrefab;
	public List<GameObject> corrIconList;
	private GameObject actionBar;
	private GameObject multiMenu;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		worldUpperLeft = GameObject.Find ("WorldUpperLeft").transform.position;
		worldLowerRight = GameObject.Find ("WorldLowerRight").transform.position;
		mapUpperLeft = GameObject.Find ("MapUpperLeft").transform.position;
		mapLowerRight = GameObject.Find ("MapLowerRight").transform.position;
		mapIcon = GameObject.Find ("PlayerIcon");
		actionBar = GameObject.Find ("ActionBar");
		multiMenu = GameObject.Find ("MultiMenu");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			Pause ();
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
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (canMove) {
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");
			if (h != 0 || v != 0) {
				//anim.SetBool ("Moving", true);
				Move (h, v);
				//anim.SetFloat ("H", Input.GetAxisRaw ("Horizontal"));
				//anim.SetFloat ("V", Input.GetAxisRaw ("Vertical"));
			}  else {
				//anim.SetBool ("Moving", false);
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
			Camera.main.transform.position = new Vector3(GameObject.Find ("Map").transform.position.x, GameObject.Find ("Map").transform.position.y, GameObject.Find ("Map").transform.position.z - 5);
			//Time.timeScale = 0;
			paused = true;
			canMove = false;
			foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().deerCreatureList) {
				gunch.GetComponent<AnimalMovementScript> ().canMove = false;
			}
			foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().wolfCreatureList) {
				gunch.GetComponent<AnimalMovementScript> ().canMove = false;
			}
			GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ().paused = true;


			float wid = worldUpperLeft.x - worldLowerRight.x;
			float hig = worldUpperLeft.y - worldLowerRight.y;

			float widM = mapUpperLeft.x - mapLowerRight.x;
			float higM = mapUpperLeft.y - mapLowerRight.y;

			Vector2 relPos = transform.position - worldUpperLeft;

			mapIcon.transform.position = new Vector2 (mapUpperLeft.x + (relPos.x * (widM / wid)), mapUpperLeft.y + (relPos.y * (higM / hig)));

			//place icons on the map for corruption nodes.
			foreach (GameObject corr in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList) {
				GameObject corrIcon = Instantiate (corrIconPrefab) as GameObject;
				corrIconList.Add (corrIcon);
				Vector2 corrIconPos = corr.transform.position - worldUpperLeft;
				corrIcon.transform.position = new Vector2 (mapUpperLeft.x + (corrIconPos.x * (widM / wid)), mapUpperLeft.y + (corrIconPos.y * (higM / hig)));
			}
			actionBar.SetActive (false);
			if (multiMenu != null) {
				multiMenu.SetActive (false);
			}


		} else if (paused) {
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100f);
			paused = false;
			canMove = true;

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
			actionBar.SetActive (true);
			if (multiMenu != null) {
				multiMenu.SetActive (true);
			}
		}
	}
}
