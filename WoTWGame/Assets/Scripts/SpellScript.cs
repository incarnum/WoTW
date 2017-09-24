using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	public int target;
	public int effect;
	public float strength;
	public bool instaCast;
    public float maxSize = 1.2f;
    public float minSize = 0.8f;
	private CreatureManagerScript cm;
	public bool indestructible;

	public Transform parentToReturnTo = null;

	private GameObject spellCore;

	private SimpleEcologyMasterScript eco;
	// Use this for initialization
	void Start () {
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ();
		spellCore = GameObject.Find ("Core");
		if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().noChargeMode) {
			instaCast = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Charge() {
		GameObject.Find ("spellCast").transform.position = new Vector3 (100f, 100f, 1f);
		Camera.main.transform.position = new Vector3(spellCore.transform.position.x, spellCore.transform.position.y, Camera.main.transform.position.z);
		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StartShooting ();
		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().nodeType = effect;
		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().nodeType = effect;
		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().nodeType = effect;
		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().nodeType = target;
		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().UpdateColors ();
		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().UpdateColors ();
		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().UpdateColors ();
		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().UpdateColors ();
		//instantiate a prefab of a spellcircle parented to the player
		//change a setting in playerscript to charging mode
	}

	void OnMouseDown() {
		//Cast ();
		if (instaCast) {
			Cast ();
		} else {
			Charge ();
		}
		if (!indestructible)
		Destroy (gameObject);
	}

	public void Cast() {
		
		if (effect == 0) {
            //changes size, keeping biomass the same but changing raw population
			if (target == 0 || target == 3) {
                eco.shrubSize += strength * .1f;
                foreach (GameObject garfield in cm.shrubCreatureList) {
                    garfield.transform.localScale = new Vector3(eco.shrubSize, eco.shrubSize);
                }
                foreach (GameObject garfield in cm.corruptedShrubCreatureList) {
                    garfield.transform.localScale = new Vector3(eco.shrubSize, eco.shrubSize);
                }
			} else if (target == 1 || target == 4) {
//                if (eco.deerSize < maxSize && eco.deerSize > minSize)
                {
                    eco.deerSize += strength * .1f;
                    foreach (GameObject garfield in cm.deerCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(eco.deerSize, eco.deerSize);
                    }
                    foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(eco.deerSize, eco.deerSize);
                    }
                    Debug.Log(eco.deerSize);
                }
//               else if (eco.deerSize == maxSize || eco.deerSize == minSize)
//                {
//                    Debug.Log("Check-a-ro!");
//                }
			} else if (target == 2 || target == 5) {
				eco.wolfSize += strength * .05f;
				foreach (GameObject garfield in cm.wolfCreatureList) {
					garfield.transform.localScale = new Vector3(eco.wolfSize, eco.wolfSize);
				}
				foreach (GameObject garfield in cm.corruptedWolfCreatureList) {
					garfield.transform.localScale = new Vector3(eco.wolfSize, eco.wolfSize);
				}
			}
		}

		if (effect == 1) {
			//hasten/slow, change increase; and decrease of food
			if (target == 0) {
				eco.shrubUp += strength;
			} else if (target == 1) {
				eco.deerUp1 += strength;
				eco.shrubDown += strength;
				eco.deerSpeed += strength * .6f;
				foreach (GameObject garfield in cm.deerCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
				}
				foreach (GameObject garfield in cm.corruptedDeerCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
				}
			} else if (target == 2) {
				eco.wolfUp += strength;
				eco.deerDown2 += strength;
				eco.wolfSpeed += strength * .6f;
				foreach (GameObject garfield in cm.wolfCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
				}
				foreach (GameObject garfield in cm.corruptedWolfCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
				}
			} else if (target == 3) {
				eco.shrubUp += strength;
			} else if (target == 4) {
				eco.deerUp1 += strength;
				eco.shrubDown += strength;
				eco.deerSpeed += strength * .6f;
				foreach (GameObject garfield in cm.deerCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
				}
				foreach (GameObject garfield in cm.corruptedDeerCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.deerSpeed;
				}
			} else if (target == 5) {
				eco.wolfUp += strength;
				eco.deerDown2 += strength;
				eco.wolfSpeed += strength * .6f;
				foreach (GameObject garfield in cm.wolfCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
				}
				foreach (GameObject garfield in cm.corruptedWolfCreatureList) {
					garfield.GetComponent<AnimalMovementScript>().speed2 = eco.wolfSpeed;
				}
			}
		}

		if (effect == 2) {
			//toughen/weaken, change decrease rates
			if (target == 0) {
				eco.shrubDown -= strength;
			} else if (target == 1) {
				eco.deerDown1 -= strength;
				eco.deerDown2 -= strength;
			} else if (target == 2) {
				eco.wolfDown -= strength;
			} else if (target == 3) {
				eco.shrubDown -= strength;
			} else if (target == 4) {
				eco.deerDown1 -= strength;
				eco.deerDown2 -= strength;
			} else if (target == 5) {
				eco.wolfDown -= strength;
			}
				

		} if (effect == 3) {
			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
					Destroy (gunch);
				}
			}
		} if (effect == 4) {
			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
					Destroy (gunch);
				}
			}
		} if (effect == 5) {
			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
					Destroy (gunch);
				}
			}
		} if (effect == 6) {
			GameObject.Find ("CorruptedAltar").GetComponent<SpellcraftingAltarScript> ().Cleanse ();
		}
			
		GameObject.Find ("CastSpellRing").GetComponent<Animator> ().SetTrigger ("Cast2");
		//GameObject.Find ("CraftMenuCastFlash").GetComponent<Animator> ().SetTrigger ("cast");
	}

	public void OnPointerClick(PointerEventData eventData){
		Cast ();
		parentToReturnTo.GetComponent<SpellbookHolderScript> ().holding = null;
		if (!indestructible)
		Destroy (gameObject);
	}

	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log ("StartDrag");
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		transform.SetParent (transform.parent.parent);
	}

	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("EndDrag");
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		transform.SetParent (parentToReturnTo);
		GetComponent<RectTransform> ().localPosition = Vector2.zero;
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = eventData.position;
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
