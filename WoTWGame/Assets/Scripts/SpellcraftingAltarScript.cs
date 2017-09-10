﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcraftingAltarScript : MonoBehaviour {
	private bool touching;
	private bool inMenu;
	public GameObject spellMenu;
	private GameObject actionBar;
	// Use this for initialization
	void Start () {
		actionBar = GameObject.Find ("ActionBar");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && touching) {
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

        if(Input.GetKeyDown (KeyCode.Q) && inMenu)
        {
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
