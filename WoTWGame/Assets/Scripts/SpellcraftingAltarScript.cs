using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellcraftingAltarScript : MonoBehaviour {
	public bool touching;
	private bool inMenu;
	public bool corrupted;
	public bool tutMode;
	public GameObject spellMenu;
	private GameObject actionBar;
	public float sceneChangeDelay;
	private float timeOfSceneChange;
	// Use this for initialization
	void Start () {
		actionBar = GameObject.Find ("ActionBar");
		timeOfSceneChange = Mathf.Infinity;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && touching && !corrupted) {
			if (!inMenu)
			{
				if (GameObject.Find("Player").GetComponent<PlayerControllerScript>().paused == false)
				{
					GameObject.Find("Player").GetComponent<PlayerControllerScript>().Pause();
				}

				Camera.main.transform.position = new Vector3 (spellMenu.transform.position.x, spellMenu.transform.position.y, Camera.main.transform.position.z);
				//Time.timeScale = .1f;
				inMenu = true;
				actionBar.SetActive (true);
			}
			else
			{
				if (GameObject.Find("Player").GetComponent<PlayerControllerScript>().paused == true)
				{
					GameObject.Find("Player").GetComponent<PlayerControllerScript>().Pause();
				}
				Camera.main.transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, Camera.main.transform.position.z);
				//Time.timeScale = 1;
				inMenu = false;

			}

		}

		if (Input.GetKeyDown (KeyCode.Escape) && inMenu) {

			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
			Camera.main.transform.position = new Vector3 (GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, Camera.main.transform.position.z);
			//Time.timeScale = 1;
			inMenu = false;

		}

//		if(Input.GetKeyDown (KeyCode.Q) && inMenu)
//		{
//			inMenu = false;
//		}
//
		if (tutMode) {
			if (Time.time > timeOfSceneChange) {
				SceneManager.LoadScene ("Forest");
			}
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			touching = true;
			if (GameObject.Find ("SpellMaker") != null) {
				GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ().touchingAltar = true;
				GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ().CheckCastability ();
			}
			if (!corrupted)
				GetComponentsInChildren<SpriteRenderer> () [1].enabled = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			touching = false;
			if (GameObject.Find ("SpellMaker") != null) {
				GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ().touchingAltar = false;
				GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ().CheckCastability ();
			}
			if (!corrupted)
				GetComponentsInChildren<SpriteRenderer> () [1].enabled = false;
		}
	}

	public void Cleanse(){
		corrupted = false;
		//GetComponent<SpriteRenderer> ().color = Color.white;
		GetComponent<Animator> ().SetTrigger ("Cleanse");
		GameObject.Find("BlueFill").GetComponent<Animator> ().SetTrigger ("Cleanse");
		timeOfSceneChange = sceneChangeDelay + Time.time;
	}
}
