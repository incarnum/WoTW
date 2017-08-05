using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcraftingAltarScript : MonoBehaviour {
	private bool touching;
	private bool inMenu;
	public GameObject spellMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && touching) {
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
			Camera.main.transform.position = new Vector3 (spellMenu.transform.position.x, spellMenu.transform.position.y, Camera.main.transform.position.z);
			//Time.timeScale = .1f;
			inMenu = true;
		}

		if (Input.GetKeyDown (KeyCode.Escape) && inMenu) {
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
			Camera.main.transform.position = new Vector3 (GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, Camera.main.transform.position.z);
			//Time.timeScale = 1;
			inMenu = false;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			touching = true;
			GetComponentsInChildren<SpriteRenderer> () [1].enabled = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			touching = false;
			GetComponentsInChildren<SpriteRenderer> () [1].enabled = false;
		}
	}
}
