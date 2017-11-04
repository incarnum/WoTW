using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PylonCoreScript : MonoBehaviour {

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

	private GameObject spellCore;

	private SimpleEcologyMasterScript eco;

	private SpellMenuScript sms;

	public UIbuffScript bms;

	public GameObject spellPrefab;

	private bool touching;
	public PylonScipt pylon1;
	public PylonScipt pylon2;
	public PylonScipt pylon3;
	public string spellPreviewText;
	public GameObject spellPreviewTextbox;
	public RingSpinScript ring1;
	public RingSpinScript ring2;
	private bool castable;



	// Use this for initialization
	void Start () {
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ();
		spellCore = GameObject.Find ("Core");
		if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().noChargeMode) {
			instaCast = true;
		}
		sms = GameObject.Find("SpellMenu").GetComponent<SpellMenuScript>();
		//This overrides the scaling done by the canvas upon instantiation to avoid a resolution dependent bug.
		if (GameObject.Find ("MultiMenu") != null) {
			bms = GameObject.Find ("MultiMenu").GetComponentInChildren<UIbuffScript> (true);
		}
		target = -1;
		effect = -1;
		strength = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (touching && Input.GetKeyDown (KeyCode.E)) {
			if (target != -1 && effect != -1 && strength != -1 && castable && target != 3) {
				Cast ();
			} else if (target != -1 && effect != -1 && strength != -1 && castable) {
				CreateSpell ();
			} else {
				Debug.Log ("Need more ingredients");
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

	public void Cast() {
			if (effect == 0) {
				if (target == 0 || target == 3) {
					if(strength == 0)
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
					if(strength == 0)
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
					if(strength == 0)
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
				} else if (target == 2|| target == 5) {
					if(strength == 0)
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
					if(strength == 0)
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
				GameObject.Find ("IntroMusic").GetComponent<fadeAudioScript> ().beginFade (3f);
			}

			GameObject.Find ("CastSpellRing").GetComponent<Animator> ().SetTrigger ("Cast2");
		bms.UpdateUIBuffs ();
		pylon1.activeSelection = -1;
		pylon1.UpdateSprite();
		pylon2.activeSelection = -1;
		pylon2.UpdateSprite();
		pylon3.activeSelection = -1;
		pylon3.UpdateSprite();
		target = -1;
		effect = -1;
		strength = -1;
		PredictSpell ();
		ring1.SpeedBoost ();
		ring2.SpeedBoost ();
	}

	public void PredictSpell() {
		spellPreviewText = "";

		if (effect == 0 && strength == -1) {
			spellPreviewText += "Enlarge/shrink ";
		} else if (effect == 1 && strength == -1) {
			spellPreviewText += "Hasten/slow ";
		} else if (effect == 2 && strength == -1) {
			spellPreviewText += "Toughen/weaken ";
		}

		if (effect == 0 && strength == -4) {
			spellPreviewText += "Shrink ";
		} else if (effect == 0 && strength == 0) {
			spellPreviewText += "Reset size of ";
		} else if (effect == 0 && strength == 4) {
			spellPreviewText += "Enlarge ";
		}
		if (effect == 1 && strength == -4) {
			spellPreviewText += "Slow ";
		} else if (effect == 1 && strength == 0) {
			spellPreviewText += "Reset speed of ";
		} else if (effect == 1 && strength == 4) {
			spellPreviewText += "Hasten ";
		}
		if (effect == 2 && strength == -4) {
			spellPreviewText += "Weaken ";
		} else if (effect == 2 && strength == 0) {
			spellPreviewText += "Reset toughness of ";
		} else if (effect == 2 && strength == 4) {
			spellPreviewText += "Toughen ";
		}




		if (target == 0) {
			spellPreviewText += "shrubs";
		} else if (target == 1) {
			spellPreviewText += "deer";
		} else if (target == 2) {
			spellPreviewText += "wolves";
		}




		if (target == 3 || effect == 3 || strength == 3) {
			spellPreviewText = "Cleanse Corruption";
		}
		if (target != -1 && effect != -1 && strength != -1 && spellPreviewText != "Cleanse Corruption") {
			GetComponent<SpriteRenderer> ().enabled = true;
			castable = true;
			GameObject.Find ("CorePopUp").GetComponent<ProximityPopUpScript> ().isenabled = true;
		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
			castable = false;
			GameObject.Find ("CorePopUp").GetComponent<ProximityPopUpScript> ().isenabled = false;
		}
		if (target == 3 && effect == 3 && strength == 3) {
			GetComponent<SpriteRenderer> ().enabled = true;
			castable = true;
			GameObject.Find ("CorePopUp").GetComponent<ProximityPopUpScript> ().isenabled = true;
		}


		spellPreviewTextbox.GetComponent<TextMesh> ().text = spellPreviewText;
	}

	public void CreateSpell() {
		GameObject newSpell = Instantiate (spellPrefab) as GameObject;
		newSpell.GetComponent<SpellScript> ().target = target;
		newSpell.GetComponent<SpellScript> ().effect = effect;
		newSpell.GetComponent<SpellScript> ().strength = strength;
		newSpell.GetComponentsInChildren<Text> () [0].text = spellPreviewText;
		//newSpell.transform.position = new Vector3 (GameObject.Find ("TomeSpot").transform.position.x, GameObject.Find ("TomeSpot").transform.position.y - uncastTomes.Count, GameObject.Find ("TomeSpot").transform.position.z);
		PlaceSpell(newSpell);
		newSpell.GetComponent<SpellScript> ().bms = bms;
		pylon1.activeSelection = -1;
		pylon1.UpdateSprite();
		pylon2.activeSelection = -1;
		pylon2.UpdateSprite();
		pylon3.activeSelection = -1;
		pylon3.UpdateSprite();
		target = -1;
		effect = -1;
		strength = -1;
		PredictSpell ();
		ring1.SpeedBoost ();
		ring2.SpeedBoost ();

	}

	private void PlaceSpell(GameObject spellbook) {
		if (GameObject.Find ("ABSlot1").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot1").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot1").transform;
			GameObject.Find ("ABSlot1").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot2").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot2").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot2").transform;
			GameObject.Find ("ABSlot2").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot3").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot3").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot3").transform;
			GameObject.Find ("ABSlot3").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot4").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot4").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot4").transform;
			GameObject.Find ("ABSlot4").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot5").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot5").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot5").transform;
			GameObject.Find ("ABSlot5").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot6").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot6").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot6").transform;
			GameObject.Find ("ABSlot6").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		}
	}

}
