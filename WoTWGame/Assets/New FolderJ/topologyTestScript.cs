using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topologyTestScript : MonoBehaviour {
	public bool touching;
	private GameObject player;
	public int direction;
	public float intensity;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (touching) {
			switch( direction) {
			case 1:
				float movex = (Input.GetAxisRaw ("Horizontal") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + movex, player.transform.position.z);
				break;
			case 2:
				float movex2 = (Input.GetAxisRaw ("Horizontal") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + movex2, player.transform.position.z);
				float movey2 = (Input.GetAxisRaw ("Vertical") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + movey2, player.transform.position.z);
				break;
			case 3:
				float movey3 = (Input.GetAxisRaw ("Vertical") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + movey3, player.transform.position.z);
				break;
			case 4:
				float movex4 = (Input.GetAxisRaw ("Horizontal") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - movex4, player.transform.position.z);
				float movey4 = (Input.GetAxisRaw ("Vertical") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + movey4, player.transform.position.z);
				break;
			case 5:
				float movex5 = (Input.GetAxisRaw ("Horizontal") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - movex5, player.transform.position.z);
				break;
			case 6:
				float movex6 = (Input.GetAxisRaw ("Horizontal") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - movex6, player.transform.position.z);
				float movey6 = (Input.GetAxisRaw ("Vertical") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - movey6, player.transform.position.z);
				break;
			case 7:
				float movex7 = (Input.GetAxisRaw ("Vertical") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - movex7, player.transform.position.z);
				break;
			case 8:
				float movex8 = (Input.GetAxisRaw ("Horizontal") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + movex8, player.transform.position.z);
				float movey8 = (Input.GetAxisRaw ("Vertical") * Time.deltaTime * intensity);
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - movey8, player.transform.position.z);
				break;

			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			touching = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			touching = false;
		}
	}
}
