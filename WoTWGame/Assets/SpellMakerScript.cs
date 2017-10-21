using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellMakerScript : MonoBehaviour, IPointerClickHandler {
	public List<GameObject> category1;
	public List<GameObject> category2;
	public List<GameObject> category3;
	public string spellPreviewString;
	public Text spellPreviewTextbox;
	public Image castImage;
	public Image castCleanseImage;
	private int reqBerr;
	private int reqAnt;
	private int reqFang;
	public bool touchingAltar;
	private Color darkGray = new Color(.4f, .4f, .4f);
	private Color lightGray = new Color(.7f, .7f, .7f);

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
	private GameObject player;

	public Transform parentToReturnTo = null;

	private GameObject spellCore;

	private SimpleEcologyMasterScript eco;

	private SpellMenuScript sms;

	public UIbuffScript bms;
	// Use this for initialization
	void Start () {
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ();
		player = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void OnPointerClick(PointerEventData eventData){
		Cast ();
	}

	public void PredictSpell() {
		spellPreviewString = "";
		if (strength == -4) {
			if (effect == 0) {
				spellPreviewString += "Shrink ";
			} else if (effect == 1) {
				spellPreviewString += "Slow ";
			} else if (effect == 2) {
				spellPreviewString += "Weaken ";
			}
		} else if (strength == 0) {
			if (effect == 0) {
				spellPreviewString += "Reset size of ";
			} else if (effect == 1) {
				spellPreviewString += "Reset speed of ";
			} else if (effect == 2) {
				spellPreviewString += "Reset toughness of ";
			}
		} else if (strength == 4) {
			if (effect == 0) {
				spellPreviewString += "Enlarge ";
			} else if (effect == 1) {
				spellPreviewString += "Hasten ";
			} else if (effect == 2) {
				spellPreviewString += "Toughen ";
			}
		}

		if (target == 0) {
			spellPreviewString += "shrubs";
		} else if (target == 1) {
			spellPreviewString += "deer";
		} else if (target == 2) {
			spellPreviewString += "wolves";
		}

		spellPreviewTextbox.text = spellPreviewString;
	}

	public void CheckCastability() {
		touchingAltar = GameObject.Find ("SpellcraftingAltar").GetComponent<SpellcraftingAltarScript> ().touching;
		reqBerr = 0;
		reqAnt = 0;
		reqFang = 0;
		if (target == 0) {
			reqBerr += 1;
		} else if (target == 1) {
			reqAnt += 1;
		} else if (target == 2) {
			reqFang += 1;
		}
		if (effect == 0) {
			reqBerr += 1;
		} else if (effect == 1) {
			reqAnt += 1;
		} else if (effect == 2) {
			reqFang += 1;
		}
		if (strength == 4) {
			reqBerr += 1;
		} else if (strength == 0) {
			reqAnt += 1;
		} else if (strength == 4) {
			reqFang += 1;
		}
		if (player.GetComponent<InventoryScript> ().berryNum >= reqBerr && player.GetComponent<InventoryScript> ().antlerNum >= reqAnt && player.GetComponent<InventoryScript> ().fangNum >= reqFang) {
			castImage.color = lightGray;
			castImage.transform.parent.GetComponent<SpellMakerCastButton> ().castable = false;
		} else {
			castImage.color = darkGray;
			castImage.transform.parent.GetComponent<SpellMakerCastButton> ().castable = false;
		}

		if (player.GetComponent<InventoryScript> ().corrBerryNum > 0 && player.GetComponent<InventoryScript> ().corrAntlerNum > 0 && player.GetComponent<InventoryScript> ().corrFangNum > 0) {
			castCleanseImage.color = Color.white;
			castCleanseImage.transform.parent.GetComponent<SpellMakerCastButton> ().castable = true;
		} else {
			castCleanseImage.color = darkGray;
			castCleanseImage.transform.parent.GetComponent<SpellMakerCastButton> ().castable = false;
		}
		if (castImage.color == lightGray && touchingAltar) {
			castImage.color = Color.white;
			castImage.transform.parent.GetComponent<SpellMakerCastButton> ().castable = true;
		}
		if (castImage.color == Color.white && !touchingAltar) {
			castImage.color = lightGray;
			castImage.transform.parent.GetComponent<SpellMakerCastButton> ().castable = false;
		}
	}

	public void Cast() {
		if (effect != 6 || (GameObject.Find ("CorruptedAltar").transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
			if (!indestructible)
				Destroy (gameObject);
			player.GetComponent<InventoryScript> ().berryNum -= reqBerr;
			player.GetComponent<InventoryScript> ().antlerNum -= reqAnt;
			player.GetComponent<InventoryScript> ().fangNum -= reqFang;
			player.GetComponent<InventoryScript> ().UpdateNumbers();
			CheckCastability ();
			if (effect == 0) {
				//changes size, keeping biomass the same but changing raw population


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
			//GameObject.Find ("CraftMenuCastFlash").GetComponent<Animator> ().SetTrigger ("cast");
			if (parentToReturnTo != null) {
				parentToReturnTo.GetComponent<SpellbookHolderScript> ().holding = null;
			}
		} else {
			Debug.Log ("FailedCast");
			GameObject errorMessage = Instantiate (errorPrefab) as GameObject;
			errorMessage.transform.SetParent(GameObject.Find("ActionBar").transform);
			errorMessage.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 100, 0);
			errorMessage.GetComponent<RectTransform> ().localScale = new Vector3 (.7f, .7f, .7f);
			Destroy (errorMessage, 6f);
		}
//		bms.UpdateUIBuffs ();
	}

	public void CastCleanse() {
		GameObject.Find ("CastSpellRing").GetComponent<Animator> ().SetTrigger ("Cast2");
		foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
			if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
				Destroy (gunch);
			}
		}
		player.GetComponent<InventoryScript> ().corrBerryNum -= 1;
		player.GetComponent<InventoryScript> ().corrAntlerNum -= 1;
		player.GetComponent<InventoryScript> ().corrFangNum -= 1;
		player.GetComponent<InventoryScript> ().UpdateNumbers();
		CheckCastability ();
	}
}
