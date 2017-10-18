using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellScriptJTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

    public int target;
    public int effect;
    public float strength;
    public bool instaCast;
    float maxSize = 1.8f;
    float minSize = 0.4f;
    float maxSpeed = 2.6f;
    float minSpeed = 1.4f;
    private CreatureManagerScript cm;
    public bool indestructible;
    public GameObject errorPrefab;

    public Transform parentToReturnTo = null;

    private GameObject spellCore;

    private SimpleEcologyMasterScript eco;

    private SpellMenuScript sms;
    private InventoryScript power;

    public UIbuffScript bms;

    private GameObject player;

    public int ingCost;
    public int ManaCost;

    // Use this for initialization
    void Awake() {
        if (GameObject.Find("MultiMenu") != null) {
            bms = GameObject.Find("MultiMenu").GetComponentInChildren<UIbuffScript>(true);
            player = GameObject.Find("Player");
        }
    }

    void Start() {
        eco = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
        cm = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>();
        spellCore = GameObject.Find("Core");
        if (GameObject.Find("Player").GetComponent<PlayerControllerScript>().noChargeMode) {
            instaCast = true;
        }
        sms = GameObject.Find("SpellMenu").GetComponent<SpellMenuScript>();
        //This overrides the scaling done by the canvas upon instantiation to avoid a resolution dependent bug.
        transform.localScale = new Vector3(.35f, .35f, .35f);
        if (GameObject.Find("MultiMenu") != null) {
            bms = GameObject.Find("MultiMenu").GetComponentInChildren<UIbuffScript>(true);
        }

    }

    // Update is called once per frame
    void Update() {

    }

    void Charge() {
        GameObject.Find("spellCast").transform.position = new Vector3(100f, 100f, 1f);
        Camera.main.transform.position = new Vector3(spellCore.transform.position.x, spellCore.transform.position.y, Camera.main.transform.position.z);
        GameObject.Find("Spawner").GetComponent<SpawnerScript>().StartShooting();
        GameObject.Find("JNode1").GetComponent<bNodeScript>().nodeType = effect;
        GameObject.Find("JNode2").GetComponent<bNodeScript>().nodeType = effect;
        GameObject.Find("JNode3").GetComponent<bNodeScript>().nodeType = effect;
        GameObject.Find("JNode4").GetComponent<bNodeScript>().nodeType = target;
        GameObject.Find("JNode1").GetComponent<bNodeScript>().UpdateColors();
        GameObject.Find("JNode2").GetComponent<bNodeScript>().UpdateColors();
        GameObject.Find("JNode3").GetComponent<bNodeScript>().UpdateColors();
        GameObject.Find("JNode4").GetComponent<bNodeScript>().UpdateColors();
        //instantiate a prefab of a spellcircle parented to the player
        //change a setting in playerscript to charging mode
    }

    void OnMouseOver() {
        //Cast ();
        if (Input.GetMouseButtonDown(0))
        {
            Select();
            if (!indestructible)
            {
                Destroy(gameObject);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {

        }

    }

    public void Cast() {
        //ingCost = 1 + player.GetComponent<PlaceMasterScript> ().spellCastCounter;
        if (effect != 6 || (GameObject.Find("CorruptedAltar").transform.position - GameObject.Find("Player").transform.position).magnitude < 6) {
            if (!indestructible)
                Destroy(gameObject);

            if (effect == 0) {
                //changes size, keeping biomass the same but changing raw population


                if (target == 0 || target == 3) {
                    if (strength == 0)
                    {
                        eco.shrubSize = eco.startShrubSize;
                        eco.shrubSizeMod = 0;
                        foreach (GameObject garfield in cm.shrubCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(eco.shrubSize, eco.shrubSize);
                        }
                        foreach (GameObject garfield in cm.corruptedShrubCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(eco.shrubSize, eco.shrubSize);
                        }
                    }
                    else
                    {
                        float sizeCheck = (eco.shrubSize) + strength * 0.05f;
                        if (sizeCheck >= minSize && sizeCheck <= maxSize)
                        {
                            eco.shrubSize = sizeCheck;
                            foreach (GameObject garfield in cm.shrubCreatureList)
                            {
                                garfield.transform.localScale = new Vector3(eco.shrubSize, eco.shrubSize);
                            }
                            foreach (GameObject garfield in cm.corruptedShrubCreatureList)
                            {
                                garfield.transform.localScale = new Vector3(eco.shrubSize, eco.shrubSize);
                            }
                            if (strength > 0) {
                                eco.shrubSizeMod += 1;
                            } else if (strength < 0) {
                                eco.shrubSizeMod -= 1;
                            }
                        }

                    }

                } else if (target == 1 || target == 4) {
                    if (strength == 0)
                    {
                        eco.deerSize = eco.startDeerSize;
                        eco.deerSizeMod = 0;
                        foreach (GameObject garfield in cm.deerCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(eco.deerSize, eco.deerSize);
                        }
                        foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(eco.deerSize, eco.deerSize);
                        }
                    }
                    else
                    {
                        float sizeCheck = (eco.deerSize) + strength * 0.05f;
                        if (sizeCheck >= minSize && sizeCheck <= maxSize)
                        {
                            eco.deerSize = sizeCheck;
                            foreach (GameObject garfield in cm.deerCreatureList)
                            {
                                garfield.transform.localScale = new Vector3(eco.deerSize, eco.deerSize);
                            }
                            foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                            {
                                garfield.transform.localScale = new Vector3(eco.deerSize, eco.deerSize);
                            }
                            if (strength > 0) {
                                eco.deerSizeMod += 1;
                            } else if (strength < 0) {
                                eco.deerSizeMod -= 1;
                            }
                        }
                    }

                } else if (target == 2 || target == 5) {
                    if (strength == 0)
                    {
                        eco.wolfSize = eco.startWolfSize;
                        eco.wolfSizeMod = 0;
                        foreach (GameObject garfield in cm.wolfCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(eco.wolfSize, eco.wolfSize);
                        }
                        foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(eco.wolfSize, eco.wolfSize);
                        }
                    }
                    else
                    {
                        float sizeCheck = (eco.wolfSize) + strength * 0.05f;
                        if (sizeCheck >= minSize && sizeCheck <= maxSize)
                        {
                            eco.wolfSize = sizeCheck;
                            foreach (GameObject garfield in cm.wolfCreatureList)
                            {
                                garfield.transform.localScale = new Vector3(eco.wolfSize, eco.wolfSize);
                            }
                            foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                            {
                                garfield.transform.localScale = new Vector3(eco.wolfSize, eco.wolfSize);
                            }
                            if (strength > 0) {
                                eco.wolfSizeMod += 1;
                            } else if (strength < 0) {
                                eco.wolfSizeMod -= 1;
                            }
                        }
                    }
                }
            }

            if (effect == 1) {
                //hasten/slow, change increase; and decrease of food
                if (target == 0 || target == 3) {
                    if (strength == 0)
                    {
                        eco.shrubUp = 0;
                        eco.shrubSpeedMod = 0;
                    }
                    else
                    {
                        eco.shrubUp += strength;
                        if (strength > 0) {
                            eco.shrubSpeedMod += 1;
                        } else if (strength < 0) {
                            eco.shrubSpeedMod -= 1;
                        }
                    }
                } else if (target == 1 || target == 4) {
                    if (strength == 0)
                    {
                        eco.deerUp1 = 0;
                        eco.shrubDown -= sms.spellStrengthMod * eco.timesDeerSpeedChanged;
                        eco.deerSpeedMod = 0;
                        eco.timesDeerSpeedChanged = 0;
                        eco.deerSpeed = eco.startDeerSpeed;
                        foreach (GameObject garfield in cm.deerCreatureList)
                        {
                            garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
                        }
                        foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                        {
                            garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
                        }
                    }
                    else
                    {
                        float speedCheck = (eco.deerSpeed) + strength * 0.05f;
                        if (speedCheck >= minSpeed && speedCheck <= maxSpeed)
                        {
                            eco.deerUp1 += strength;
                            eco.shrubDown += strength;
                            eco.timesDeerSpeedChanged += 1;
                            eco.deerSpeed += strength * .05f;
                            foreach (GameObject garfield in cm.deerCreatureList)
                            {
                                garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
                            }
                            foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                            {
                                garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
                            }
                            if (strength > 0) {
                                eco.deerSpeedMod += 1;
                            } else if (strength < 0) {
                                eco.deerSpeedMod -= 1;
                            }
                        }
                    }
                } else if (target == 2 || target == 5) {
                    if (strength == 0)
                    {
                        eco.wolfUp = 0;
                        eco.deerDown2 -= sms.spellStrengthMod * eco.timesWolfSpeedChanged;
                        eco.wolfSpeedMod = 0;
                        eco.timesWolfSpeedChanged = 0;
                        eco.wolfSpeed = eco.startWolfSpeed;
                        foreach (GameObject garfield in cm.wolfCreatureList)
                        {
                            garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
                        }
                        foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                        {
                            garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
                        }
                    }
                    else
                    {
                        float speedCheck = (eco.wolfSpeed) + strength * 0.05f;
                        if (speedCheck >= minSpeed && speedCheck <= maxSpeed)
                        {
                            eco.wolfUp += strength;
                            eco.deerDown2 += strength;
                            eco.timesWolfSpeedChanged += 1;
                            eco.wolfSpeed += strength * .05f;
                            foreach (GameObject garfield in cm.wolfCreatureList)
                            {
                                garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
                            }
                            foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                            {
                                garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
                            }
                            if (strength > 0) {
                                eco.wolfSpeedMod += 1;
                            } else if (strength < 0) {
                                eco.wolfSpeedMod -= 1;
                            }
                        }
                    }
                }
            }

            if (effect == 2) {
                //toughen/weaken, change decrease rates
                if (target == 0 || target == 3)
                {
                    if (strength == 0)
                    {
                        eco.shrubDown = sms.spellStrengthMod * eco.timesDeerSpeedChanged;
                        eco.shrubToughMod = 0;
                    }
                    else
                    {
                        eco.shrubDown -= strength;
                        if (strength > 0) {
                            eco.shrubToughMod += 1;
                        } else if (strength < 0) {
                            eco.shrubToughMod -= 1;
                        }
                    }
                }
                else if (target == 1 || target == 4)
                {
                    if (strength == 0)
                    {
                        eco.deerDown1 = 0;
                        eco.deerDown2 = sms.spellStrengthMod * eco.timesWolfSpeedChanged;
                        eco.deerToughMod = 0;
                    }
                    else
                    {
                        eco.deerDown1 -= strength;
                        eco.deerDown2 -= strength;
                        if (strength > 0) {
                            eco.deerToughMod += 1;
                        } else if (strength < 0) {
                            eco.deerToughMod -= 1;
                        }
                    }
                }
                else if (target == 2 || target == 5)
                {
                    if (strength == 0)
                    {
                        eco.wolfDown = 0;
                        eco.wolfToughMod = 0;
                    }
                    else
                    {
                        eco.wolfDown -= strength;
                        if (strength > 0) {
                            eco.wolfToughMod += 1;
                        } else if (strength < 0) {
                            eco.wolfToughMod -= 1;
                        }
                    }
                }


            } if (effect == 3) {
                foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList) {
                    if ((gunch.transform.position - GameObject.Find("Player").transform.position).magnitude < 6) {
                        Destroy(gunch);
                    }
                }
            } if (effect == 4) {
                foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList) {
                    if ((gunch.transform.position - GameObject.Find("Player").transform.position).magnitude < 6) {
                        Destroy(gunch);
                    }
                }
            } if (effect == 5) {
                foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList) {
                    if ((gunch.transform.position - GameObject.Find("Player").transform.position).magnitude < 6) {
                        Destroy(gunch);
                    }
                }
            } if (effect == 6) {
                GameObject.Find("CorruptedAltar").GetComponent<SpellcraftingAltarScript>().Cleanse();
                GameObject.Find("IntroMusic").GetComponent<fadeAudioScript>().beginFade(3f);
            }

            GameObject.Find("CastSpellRing").GetComponent<Animator>().SetTrigger("Cast2");
            //GameObject.Find ("CraftMenuCastFlash").GetComponent<Animator> ().SetTrigger ("cast");
            if (parentToReturnTo != null) {
                parentToReturnTo.GetComponent<SpellbookHolderScript>().holding = null;
            }
        } else {
            Debug.Log("FailedCast");
            GameObject errorMessage = Instantiate(errorPrefab) as GameObject;
            errorMessage.transform.SetParent(GameObject.Find("ActionBar").transform);
            errorMessage.GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);
            errorMessage.GetComponent<RectTransform>().localScale = new Vector3(.7f, .7f, .7f);
            Destroy(errorMessage, 6f);
        }
        bms.UpdateUIBuffs();
        player.GetComponent<PlaceMasterScript>().spellbookHolding = null;
        Color newCol = new Color(1f, 1f, 1f);
        GetComponent<Image>().color = newCol;
        if (effect == 0) {
            player.GetComponent<InventoryScript>().ManaCount -= ManaCost;
        } else if (effect == 1) {
            player.GetComponent<InventoryScript>().ManaCount -= ManaCost;
        } else if (effect == 2) {
            player.GetComponent<InventoryScript>().ManaCount -= ManaCost;
        } else if (effect == 4) {
            player.GetComponent<InventoryScript>().ManaCount -= ManaCost;
        }
        player.GetComponent<InventoryScript>().UpdateNumbers();
        player.GetComponent<PlaceMasterScript>().spellCastCounter += 1;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Select();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log ("StartDrag");
        //GetComponent<CanvasGroup> ().blocksRaycasts = false;
        //transform.SetParent (transform.parent.parent);
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log ("EndDrag");
        //GetComponent<CanvasGroup> ().blocksRaycasts = true;
        //transform.SetParent (parentToReturnTo);
        //GetComponent<RectTransform> ().localPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData) {
        //transform.position = eventData.position;
    }

    private void Select() {
        if ((effect == 0 || effect == 1 || effect == 2 || effect == 3) && player.GetComponent<InventoryScript>().ManaCount >= ManaCost) {
			player.GetComponent<PlaceMasterScript> ().spellbookHolding = gameObject;
			Color newCol = new Color (.5f, .5f, 1f);
			GetComponent<Image> ().color = newCol;
		}
		if (effect == 4 && player.GetComponent<InventoryScript> ().ManaCount >= ManaCost) {
			Cast ();
		}
	}

//	public void OnDrop(PointerEventData eventData){
//		if (GameObject.Find ("Player").GetComponent<PlaceMasterScript> ().spellbookTargetDrop != null) {
//			transform.position = GameObject.Find ("Player").GetComponent<PlaceMasterScript> ().spellbookTargetDrop.transform.position;
//		}
//	}
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class SpellScript : MonoBehaviour {
//
//	public int target;
//	public int effect;
//	public int strength;
//	public bool instaCast;
//
//	private GameObject spellCore;
//
//	private SimpleEcologyMasterScript eco;
//	// Use this for initialization
//	void Start () {
//		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
//		spellCore = GameObject.Find ("Core");
//		if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().noChargeMode) {
//			instaCast = true;
//		}
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//	}
//
//	void Charge() {
//		GameObject.Find ("spellCast").transform.position = new Vector3 (100f, 100f, 1f);
//		Camera.main.transform.position = new Vector3(spellCore.transform.position.x, spellCore.transform.position.y, Camera.main.transform.position.z);
//		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StartShooting ();
//		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().nodeType = effect;
//		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().nodeType = effect;
//		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().nodeType = effect;
//		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().nodeType = target;
//		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().UpdateColors ();
//		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().UpdateColors ();
//		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().UpdateColors ();
//		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().UpdateColors ();
//		//instantiate a prefab of a spellcircle parented to the player
//		//change a setting in playerscript to charging mode
//	}
//
//	void OnMouseDown() {
//		//Cast ();
//		if (instaCast) {
//			Cast ();
//		} else {
//			Charge ();
//		}
//		Destroy (gameObject);
//	}
//
//	public void Cast() {
//		if (effect == 0) {
//			//population change
//			if (target == 0) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 1) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 2) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			} else if (target == 3) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 4) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 5) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			}
//		}
//
//		if (effect == 1) {
//			//population change
//			if (target == 0) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 1) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 2) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			} else if (target == 3) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 4) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 5) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			}
//		}
//
//		if (effect == 2) {
//			//population change
//			if (target == 0) {
//				eco.shrubPop -= strength * 10;
//				eco.corruptedShrubPop -= strength * 10;
//			} else if (target == 1) {
//				eco.deerPop -= strength * 10;
//				eco.corruptedDeerPop -= strength * 10;
//			} else if (target == 2) {
//				eco.wolfPop -= strength * 10;
//				eco.corruptedWolfPop -= strength * 10;
//			} else if (target == 3) {
//				eco.shrubPop -= strength * 10;
//				eco.corruptedShrubPop -= strength * 10;
//			} else if (target == 4) {
//				eco.deerPop -= strength * 10;
//				eco.corruptedDeerPop -= strength * 10;
//			} else if (target == 5) {
//				eco.wolfPop -= strength * 10;
//				eco.corruptedWolfPop -= strength * 10;
//			}
//
//		} if (effect == 3) {
//			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
//				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
//					Destroy (gunch);
//				}
//			}
//		} if (effect == 4) {
//			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
//				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
//					Destroy (gunch);
//				}
//			}
//		} if (effect == 5) {
//			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
//				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
//					Destroy (gunch);
//				}
//			}
//		}
//
//		GameObject.Find ("SpellMenu").GetComponent<SpellMenuScript> ().uncastTomes.Remove (gameObject);
//	}
//}
