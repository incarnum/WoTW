using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonScipt : MonoBehaviour {
	public int pylonNum;
	private bool windowActive;
	public GameObject window;
	public GameObject selector;
	private bool touching;
	private GameObject player;
	private int currentSelection;
	public int activeSelection = -1;
	public List<GameObject> options;
	public PylonCoreScript core;
	public CorruptedPylonCoreScript core2;
	private bool validSelection;
	public GameObject holdingSprite;
	public SpriteRenderer glow;
	public GameObject descriptionText;
	public bool corrupted;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (touching && Input.GetKeyDown (KeyCode.E)) {
			if (!windowActive) {
				window.SetActive (true);
				player.GetComponent<PlayerControllerScript> ().canMove = false;
				player.GetComponent<PlayerControllerB> ().canMove = false;
				windowActive = true;
				UpdateText ();
				options [0].GetComponentsInChildren<TextMesh> () [1].text = ("X" + player.GetComponent<InventoryScript> ().berryNum);
				options [1].GetComponentsInChildren<TextMesh> () [1].text = ("X" + player.GetComponent<InventoryScript> ().antlerNum);
				options [2].GetComponentsInChildren<TextMesh> () [1].text = ("X" + player.GetComponent<InventoryScript> ().fangNum);
				//options [3].GetComponentsInChildren<TextMesh> () [1].text = ("X" + player.GetComponent<InventoryScript> ().corrBerryNum);
				CheckIfValid ();

			} else if (validSelection) {
				SelectCurrent ();
			} else {
				Debug.Log ("Not enough ingredients");
			}
		}
		if (Input.GetKeyDown (KeyCode.Q) && windowActive) {
			if (activeSelection != -1) {
				selector.transform.position = new Vector2 (options [activeSelection].transform.position.x - 2, options [activeSelection].transform.position.y);
				currentSelection = activeSelection;
			}
				window.SetActive (false);
				player.GetComponent<PlayerControllerScript> ().canMove = true;
				player.GetComponent<PlayerControllerB> ().canMove = true;
				windowActive = false;
			
		}

		//pressing R removes the item from the pylon
		if (Input.GetKeyDown (KeyCode.R) && windowActive) {
			if (activeSelection == 0) {
				player.GetComponent<InventoryScript> ().berryNum += 1;
			} else if (activeSelection == 1) {
				player.GetComponent<InventoryScript> ().antlerNum += 1;
			} else if (activeSelection == 2) {
				player.GetComponent<InventoryScript> ().fangNum += 1;
			} else if (activeSelection == 3) {
				player.GetComponent<InventoryScript> ().corrBerryNum += 1;
			}
			activeSelection = -1;
			window.SetActive (false);
			player.GetComponent<PlayerControllerScript> ().canMove = true;
			player.GetComponent<PlayerControllerB> ().canMove = true;
			windowActive = false;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
			UpdateSprite ();
			if (pylonNum == 0) {
				core.target = -1;
			} else if (pylonNum == 1) {
				core.effect = -1;
			} else if (pylonNum == 2) {
				core.strength = -1;
			}
			if (corrupted == false) {
				core.PredictSpell ();
			} else {
				core2.PredictSpell ();
			}

		}

		if (windowActive) {
			if (Input.GetKeyDown (KeyCode.S)) {
				currentSelection += 1;
				if (currentSelection + 1 > options.Count) {
					currentSelection = 0;
				}
				UpdateText ();
				selector.transform.position = new Vector2 (options [currentSelection].transform.position.x - 2, options [currentSelection].transform.position.y);
				CheckIfValid ();
			}

			if (Input.GetKeyDown (KeyCode.W)) {
				currentSelection -= 1;
				if (currentSelection < 0) {
					currentSelection = options.Count -1;
				}
				UpdateText ();
				selector.transform.position = new Vector2 (options [currentSelection].transform.position.x - 2, options [currentSelection].transform.position.y);
				CheckIfValid ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			touching = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			touching = false;
		}
	}

	void CheckIfValid() {
		if (currentSelection == 0 && player.GetComponent<InventoryScript> ().berryNum > 0 ||
			currentSelection == 1 && player.GetComponent<InventoryScript> ().antlerNum > 0 ||
			currentSelection == 2 && player.GetComponent<InventoryScript> ().fangNum > 0 ||
			currentSelection == 3 && player.GetComponent<InventoryScript> ().corrBerryNum > 0) {
			selector.GetComponent<SpriteRenderer> ().color = Color.white;
			validSelection = true;
		} else {
			selector.GetComponent<SpriteRenderer> ().color = Color.black;
			validSelection = false;
		}
	}

	void SelectCurrent() {
		if (activeSelection == 0) {
			player.GetComponent<InventoryScript> ().berryNum += 1;
		} else if (activeSelection == 1) {
			player.GetComponent<InventoryScript> ().antlerNum += 1;
		} else if (activeSelection == 2) {
			player.GetComponent<InventoryScript> ().fangNum += 1;
		} else if (activeSelection == 3) {
			player.GetComponent<InventoryScript> ().corrBerryNum += 1;
		}
		activeSelection = currentSelection;
		window.SetActive (false);
		player.GetComponent<PlayerControllerScript> ().canMove = true;
		player.GetComponent<PlayerControllerB> ().canMove = true;
		windowActive = false;
		if (!corrupted) {
			if (pylonNum == 0) {
				core.target = activeSelection;
			} else if (pylonNum == 1) {
				core.effect = activeSelection;
			} else if (pylonNum == 2) {
				if (activeSelection == 0) {
					core.strength = 4;
				} else if (activeSelection == 1) {
					core.strength = 0;
				} else if (activeSelection == 2) {
					core.strength = -4;
				} else if (activeSelection == 3) {
					core.strength = 3;
				}
			}
		} else {
			if (pylonNum == 0) {
				core2.target = activeSelection;
			} else if (pylonNum == 2) {
				core2.strength = activeSelection;
			}
		}
		if (activeSelection == 0) {
			player.GetComponent<InventoryScript> ().berryNum -= 1;
		} else if (activeSelection == 1) {
			player.GetComponent<InventoryScript> ().antlerNum -= 1;
		} else if (activeSelection == 2) {
			player.GetComponent<InventoryScript> ().fangNum -= 1;
		} else if (activeSelection == 3) {
			player.GetComponent<InventoryScript> ().corrBerryNum -= 1;
		}
		player.GetComponent<InventoryScript> ().UpdateNumbers ();
		UpdateSprite ();
		if (corrupted == false) {
			core.PredictSpell ();
		} else {
			core2.PredictSpell ();
		}
	}

	public void UpdateSprite() {
		holdingSprite.GetComponent<Animator> ().SetInteger ("itemnum", activeSelection);
		if (activeSelection == 0) {
			glow.color = Color.green;
		} else if (activeSelection == 1) {
			glow.color = Color.red;
		} else if (activeSelection == 2) {
			glow.color = Color.white;
		} else if (activeSelection == 3) {
			glow.color = Color.magenta;
		} if (activeSelection == -1) {
			glow.color = Color.gray;
		}
	}

	public void UpdateText() {
		int num = 0;
		if (pylonNum == 1) {
			num += 4;
		} else if (pylonNum == 2) {
			num += 8;
		}
		num += currentSelection;
		descriptionText.GetComponent<DescriptionTextScript> ().UpdateDescription (num);
	}

}
