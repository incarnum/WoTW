﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonScipt : MonoBehaviour {
	public int pylonNum; //what role of pylon this is. 0 = target, 1 = effect, 2 = modifier
	private bool windowActive; //is the window open
	public GameObject window; //the ui window for choosing an ingredient
    public GameObject corrWindow;
	public GameObject selector; //the icon that shows what you currently have selected, changes color based on validity
    public GameObject corrSelector;
	private bool touching; //is the player touching the pylon
	private GameObject player;
	private int currentSelection; //the ingredient the player has highlighted. This script displays a tooltip explaining what the ing does if used in this slot
	public int activeSelection = -1; //the ingredient the pylon is currently storing. This is used by the core script to determine what spell is made.
	public List<GameObject> options;
    public List<GameObject> corrOptions;
    public PylonCoreScript core; //the object/script that casts the spells
	public CorruptedPylonCoreScript core2; //the object/script that casts the cleanse corruption spells. Pylons at corrupted pylon circles need and use this rather than regular core
	private bool validSelection; //a bool for if the player has enough of the currently selected ingredient to put it on the pylon
	public GameObject holdingSprite; //the ingredient sprite that floats above the pylon
	public SpriteRenderer glow; //the color changing runes on the pylon
	public GameObject descriptionText; //the tooltip explaining what an ingredient does
    public GameObject corrDescriptionText;
	public bool corrupted; //is this pylon at a corrupted pylon circle
    public GameObject cpcs;
    public int corrCost;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
        corrCost = 1;
        core = cpcs.GetComponent<PylonCoreScript>();
        core2 = cpcs.GetComponent<CorruptedPylonCoreScript>();
	}
	
	// Update is called once per frame
	void Update () {
        
		if (touching && Input.GetKeyDown (KeyCode.E) && !corrupted) {
			if (!windowActive) {
                if (corrupted)
                {
                    corrWindow.SetActive(true);
                }
                //opens window, 
                else
                {
                    window.SetActive(true);
                }
				player.GetComponent<PlayerControllerScript> ().canMove = false;
				player.GetComponent<PlayerControllerB> ().canMove = false;
				windowActive = true;
				//updates the tooltip saying what the current selection does
				UpdateText ();
                //sets number display for how many ingredients the player has
                if (corrupted)
                {
                    corrOptions[0].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().berryNum);
                    corrOptions[1].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().antlerNum);
                    corrOptions[2].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().fangNum);
                }
                else
                {
                    options[0].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().berryNum);
                    options[1].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().antlerNum);
                    options[2].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().fangNum);
                }
				CheckIfValid ();

			} else if (validSelection) {
				SelectCurrent ();
			} else {
				Debug.Log ("Not enough ingredients");
			}
		}
        else if (touching && Input.GetKeyDown(KeyCode.E) && cpcs.GetComponent<CorruptedPylonCoreScript>().cooldown <= 0)
        {
            if (!windowActive)
            {
                if (corrupted)
                {
                    corrWindow.SetActive(true);
                }
                //opens window, 
                else
                {
                    window.SetActive(true);
                }
                player.GetComponent<PlayerControllerScript>().canMove = false;
                player.GetComponent<PlayerControllerB>().canMove = false;
                windowActive = true;
                //updates the tooltip saying what the current selection does
                UpdateText();
                //sets number display for how many ingredients the player has
                if(corrupted)
                {
                    corrOptions[0].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().berryNum);
                    corrOptions[1].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().antlerNum);
                    corrOptions[2].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().fangNum);
                }
                else
                {
                    options[0].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().berryNum);
                    options[1].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().antlerNum);
                    options[2].GetComponentsInChildren<TextMesh>()[1].text = ("X" + player.GetComponent<InventoryScript>().fangNum);
                }
                CheckIfValid();

            }
            else if (validSelection)
            {
                SelectCurrent();
            }
            else
            {
                Debug.Log("Not enough ingredients");
            }
        }
        if (Input.GetKeyDown (KeyCode.Q) && windowActive) {
			if (activeSelection != -1) {
                if (corrupted)
                {
                    corrSelector.transform.position = new Vector2(corrOptions[activeSelection].transform.position.x - 2, corrOptions[activeSelection].transform.position.y);
                }
                else
                {
                    selector.transform.position = new Vector2(options[activeSelection].transform.position.x - 2, options[activeSelection].transform.position.y);
                }
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
				player.GetComponent<InventoryScript> ().berryNum += corrCost;
			} else if (activeSelection == 1) {
				player.GetComponent<InventoryScript> ().antlerNum += corrCost;
			} else if (activeSelection == 2) {
				player.GetComponent<InventoryScript> ().fangNum += corrCost;
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
			//moving the selector up and down to see different options
			if (Input.GetKeyDown (KeyCode.S)) {
				currentSelection += 1;
				if (currentSelection + 1 > options.Count) {
					currentSelection = 0;
				}
				UpdateText ();
                if (corrupted)
                {
                    corrSelector.transform.position = new Vector2(corrOptions[currentSelection].transform.position.x - 2, corrOptions[currentSelection].transform.position.y);
                }
                else
                {
                    selector.transform.position = new Vector2(options[currentSelection].transform.position.x - 2, options[currentSelection].transform.position.y);
                }
                CheckIfValid ();
			}

			if (Input.GetKeyDown (KeyCode.W)) {
				currentSelection -= 1;
				if (currentSelection < 0) {
					currentSelection = options.Count -1;
				}
				UpdateText ();
                if (corrupted)
                {
                    corrSelector.transform.position = new Vector2(corrOptions[currentSelection].transform.position.x - 2, corrOptions[currentSelection].transform.position.y);
                }
                else
                {
                    selector.transform.position = new Vector2(options[currentSelection].transform.position.x - 2, options[currentSelection].transform.position.y);
                }
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
		//does the player have enough of this ingredient to choose it
		if (currentSelection == 0 && player.GetComponent<InventoryScript> ().berryNum > 0 ||
			currentSelection == 1 && player.GetComponent<InventoryScript> ().antlerNum > 0 ||
			currentSelection == 2 && player.GetComponent<InventoryScript> ().fangNum > 0 ||
			currentSelection == 3 && player.GetComponent<InventoryScript> ().corrBerryNum > 0) {
            if (corrupted)
            {
                corrSelector.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                selector.GetComponent<SpriteRenderer>().color = Color.white;
            }
            validSelection = true;
		} else {
            if (corrupted)
            {
                corrSelector.GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                selector.GetComponent<SpriteRenderer>().color = Color.black;
            }
            validSelection = false;
		}
	}

	void SelectCurrent() {
		//turn the current selection into the active selection

		//this first bit is refunding the player whatever ingredient was already in this pylon
		if (activeSelection == 0) {
			player.GetComponent<InventoryScript> ().berryNum += corrCost;
		} else if (activeSelection == 1) {
			player.GetComponent<InventoryScript> ().antlerNum += corrCost;
		} else if (activeSelection == 2) {
			player.GetComponent<InventoryScript> ().fangNum += corrCost;
		} else if (activeSelection == 3) {
			player.GetComponent<InventoryScript> ().corrBerryNum += 1;
		}
		activeSelection = currentSelection;
        if (corrupted)
        {
            corrWindow.SetActive(false);
        }
        //opens window, 
        else
        {
            window.SetActive(false);
        }
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
			//corrupted pylons can only have a pylon num of 0 or 2, since 1 (effect) doesn't have any options,and uses a different script
			if (pylonNum == 0) {
				core2.target = activeSelection;
			} else if (pylonNum == 2) {
				core2.strength = activeSelection;
			}
		}
		if (activeSelection == 0) {
			player.GetComponent<InventoryScript> ().berryNum -= corrCost;
		} else if (activeSelection == 1) {
			player.GetComponent<InventoryScript> ().antlerNum -= corrCost;
		} else if (activeSelection == 2) {
			player.GetComponent<InventoryScript> ().fangNum -= corrCost;
		} else if (activeSelection == 3) {
			player.GetComponent<InventoryScript> ().corrBerryNum -= 1;
		}
		player.GetComponent<InventoryScript> ().UpdateNumbers ();
		UpdateSprite ();
		//updates the spell prediction using the correct core script
		if (corrupted == false) {
			core.PredictSpell ();
		} else {
			core2.PredictSpell ();
		}
	}

	public void UpdateSprite() {
		//sets the animation of holding sprite to the animation that shows the right ingredient
		holdingSprite.GetComponent<Animator> ().SetInteger ("itemnum", activeSelection);
		//changes the color of the runes on the pylon
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
		//the object descriptionText is a ui textbox that displays the ingredient tooltip
		//descriptionText has a script called DescriptionTextScript, which holds a list containing several strings
		//the order of the strings is target berry, target antler, target fang, target corruption, effect berry, effect antler, etc,
		//basically each target, then each effect, then each modifier
		//if this is pylon 0 (target pylon), this starts from the beginning of the list, if its pylon 1 it starts from 4, pylon 2, from 8
		int num = 0;
		if (pylonNum == 1) {
			num += 4;
		} else if (pylonNum == 2) {
			num += 8;
		}
		//it then adds the number of the current(highlighted) selection, causing the correct tooltip to be displayed
		num += currentSelection;
        if (corrupted)
        {
            corrDescriptionText.GetComponent<DescriptionTextScript>().UpdateDescription(num);
        }
        else
        {
            descriptionText.GetComponent<DescriptionTextScript>().UpdateDescription(num);
        }
	}

}
