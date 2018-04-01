using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptedPylonCoreScript : MonoBehaviour
{

    public int target;
    public int effect;
    public float strength;
    public bool instaCast;
//    float maxSize = 1.8f;
//    float minSize = 0.4f;
//    float maxSpeed = 2.6f;
//    float minSpeed = 1.4f;
    private CreatureManagerScript cm;
    public bool indestructible;
    public GameObject errorPrefab;
    private corruptionManagerScript cms;
	public GameObject UIManagerObject;
	private SpellFXController spellFX;

    private SimpleEcologyMasterScript eco;

    public UIbuffScript bms;

    public GameObject spellPrefab;

    private bool touching;
    public PylonScipt pylon1;
    public PylonScipt pylon2;
    public PylonScipt pylon3;
	public centerStoneGlowScript pylonGlow1;
	public centerStoneGlowScript pylonGlow2;
	public centerStoneGlowScript pylonGlow3;
    public string spellPreviewText;
    public GameObject spellPreviewTextbox;
    public RingSpinScript ring1;
    public RingSpinScript ring2;
    private bool castable;
    public GameObject corruptionNode;
    public GameObject pcs;
    public float cooldown;
	public float generalCooldown;
    public int health;
    public GameObject cRing1;
    public GameObject cRing2;
	public centerStoneGlowScript CenterStoneGlow;
	public GameObject corePopUp;
    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;
	private RabbitPopulation rabbit;
	private OwlPopulation owl;
    private DialogueManager dm;
    private DialogueTrigger cbc;
    private DialogueTrigger clFN;
    private DialogueTrigger clSN;
    private DialogueTrigger clTN;
    private DialogueTrigger clFoN;
    private DialogueTrigger clFiN;
    private int nodeCount;
    private AudioSource castSound;
	public GameObject timeStopTrigger;
	public GameObject cooldownBar;
	public GameObject cooldownFill;
	private float lastColorChangeTime;
	private bool colorPulse;
	public Color pulseColor;
	public bool finalFlash;
	public GameObject x3;

    // Use this for initialization
    void Start()
    {
        cooldown = 0;
        //some of these things may be unnecessary. This script was made by copying over a lot of stuff from the original spellscript, since it casts spells.
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
		rabbit = GameObject.Find("CreatureManager").GetComponent<RabbitPopulation>();
		owl = GameObject.Find("CreatureManager").GetComponent<OwlPopulation>();
        eco = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
        cm = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>();
        cms = GameObject.Find("CorruptionManager").GetComponent<corruptionManagerScript>();
        cbc = GameObject.Find("CorruptionBeenCast").GetComponent<DialogueTrigger>();
        clFN = GameObject.Find("CleanseFirstNode").GetComponent<DialogueTrigger>();
        clSN = GameObject.Find("CleanseSecondNode").GetComponent<DialogueTrigger>();
        clTN = GameObject.Find("CleanseThirdNode").GetComponent<DialogueTrigger>();
        clFoN = GameObject.Find("CleanseFourthNode").GetComponent<DialogueTrigger>();
        clFiN = GameObject.Find("CleanseFinalNode").GetComponent<DialogueTrigger>();
        dm = GameObject.Find("TutorialDialogue").GetComponent<DialogueManager>();
		if (health == 3) {
			print (corruptionNode.transform.GetChild(0).name);
			corruptionNode.GetComponent<Animator> ().SetTrigger ("idle1");
			corruptionNode.transform.GetChild(0).gameObject.SetActive (true);
		} else if (health == 2) {
			corruptionNode.GetComponent<Animator> ().SetTrigger ("idle2");
			corruptionNode.transform.GetChild(0).gameObject.SetActive (false);
			corruptionNode.transform.GetChild(1).gameObject.SetActive (true);
		} else if (health == 1) {
			corruptionNode.GetComponent<Animator> ().SetTrigger ("idle3");
			corruptionNode.transform.GetChild(1).gameObject.SetActive (false);
			corruptionNode.transform.GetChild(2).gameObject.SetActive (true);
		}
        if (GameObject.Find("Player").GetComponent<PlayerControllerScript>().noChargeMode)
        {
            instaCast = true;
        }
        //This overrides the scaling done by the canvas upon instantiation to avoid a resolution dependent bug.
        if (GameObject.Find("MultiMenu") != null)
        {
            bms = GameObject.Find("MultiMenu").GetComponentInChildren<UIbuffScript>(true);
        }
        target = -1;
        //		effect = -1;
        //no choosable effect for corrupted pylon circles
        strength = -1;
        nodeCount = cm.corruptionNodeList.Count;
		spellFX = GetComponent<SpellFXController> ();
        castSound = GameObject.Find("Snd_Cast").GetComponentInChildren<AudioSource>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (touching && Input.GetButtonDown("Select") && cooldown <= 0)
        {
            if (target != -1 && castable && target != 3)
            {
                //if there isn't nothing in each slot, the spell is castable, and the target isn't a corrupted berry, cast the spell
                //in retrospect this is redundant, as the castable bool will only be true if there's something in every slot
                Cast();
                castSound.Play();
            }
            else if (target != -1 && strength != -1 && castable)
            {
                //this would trigger if there are corrupted berries in every slot, but corrupted berries aren't an option in this build, so it's not necessary
                CreateSpell();
            }
            else
            {
                Debug.Log("Need more ingredients");
            }
        }
		if (Input.GetKeyDown (KeyCode.P) && !eco.demo) {
			cooldown = 0f;
		}
		cooldown -= Time.deltaTime;
		if (cooldown > 0) {
			Cooldown ();
		}
    }
    //determine if the player is touching the core of the pylon circle, allowing them to cast
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "energy")
        {
            touching = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "energy")
        {
            touching = false;
        }
    }

    public void Cast()
    {
        health -= 1;
		corruptionNode.GetComponent<Animator> ().SetTrigger ("cleanse");
		CenterStoneGlow.SetColor (Color.clear, .5f);
		cooldownFill.GetComponent<centerStoneGlowScript> ().SetColor (Color.white, .1f);
		if (dm.secondCorrCast)
		{
			dm.secondCorrCast = false;
			cbc.TriggerDialogue();

		}

		if (dm.firstCorrCast)
		{
			dm.firstCorrCast = false;
			dm.secondCorrCast = true;
			//health -= 5;
			//cbc.TriggerDialogue();

		}
        if (health <= 0)
        {
            //Checking the number of nodes cleansed, and playing the corresponding dialogue
            if (dm.cleansedNodes == 0)
            {
                clFN.TriggerDialogue();
                deer.enabled = true;
				deer.DoStart ();
				eco.GetComponent<UIManager> ().ActivateDeer ();
				eco.tempShrubCapBool = false;
				shrub.corruptedPop -= cms.shrubPopStart;
                //Deer get activated
                //Polish: Deer moves across screen
            }
            else if (dm.cleansedNodes == 1)
            {
                clSN.TriggerDialogue();
                wolf.enabled = true;
				wolf.DoStart ();
				eco.GetComponent<UIManager> ().ActivateWolves ();
                eco.GetComponent<UIManager> ().ActivateRabbits ();
                eco.GetComponent<UIManager> ().ActivateOwls ();
				cms.phase += 1;
				cms.nextCorruptionTime = Time.time + cms.infectTime;
				cms.currentCorruptionRate = cms.phase1CorruptionRate;
                //Wolves Rabbits and Owls get activated
                //Polish: Wolf moves across the screen
            }
            else if (dm.cleansedNodes == 2)
            {

                //clTN.TriggerDialogue();
				//GameObject.Find ("CorruptionWall").SetActive (false);
                //rabbit.enabled = true;
            }
			else if (dm.cleansedNodes == 3)
            {
				if (!eco.demo) {
				rabbit.enabled = true;
				rabbit.DoStart ();
				eco.GetComponent<UIManager> ().ActivateRabbits ();
                //clFoN.TriggerDialogue();
                //owl.enabled = true;
				}
            }
            else if (dm.cleansedNodes == 4)
            {
				if (!eco.demo) {
					owl.enabled = true;
					owl.DoStart ();
					eco.GetComponent<UIManager> ().ActivateOwls ();
					//clFiN.TriggerDialogue();
					//no longer present in level
				} else {
					GameObject.Find ("DemoEndCanvas").transform.GetChild (0).gameObject.SetActive (true);
					GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused = true;
					GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
				}
            }
			else if (dm.cleansedNodes == 5)
			{
				if (!eco.demo) {
				clTN.TriggerDialogue();
				GameObject.Find ("CorruptionWall").SetActive (false);
				//rabbit.enabled = true;
				}
			}
            dm.cleansedNodes++;
            cm.corruptionNodeList.Remove(corruptionNode);
            Destroy(corruptionNode, 1f);
            shrub.rate = shrub.startRate;
            deer.rate = deer.startRate;
            wolf.rate = wolf.startRate;
            cms.shrubRange = cms.startShrubRange;
            cms.deerRange = cms.startDeerRange;
            cms.wolfRange = cms.startWolfRange;
            pylon1.GetComponent<PylonScipt>().corrupted = false;
            pylon2.GetComponent<CorruptedPylonScript>().enabled = false;
            pylon2.GetComponent<PylonScipt>().enabled = true;
            pylon2.GetComponent<SpriteRenderer>().color = Color.white;
            pylon3.GetComponent<CorruptedPylonScript>().enabled = false;
            pylon3.GetComponent<PylonScipt>().enabled = true;
            pylon3.GetComponent<SpriteRenderer>().color = Color.white;
            cRing1.GetComponent<SpriteRenderer> ().color = GetComponent<PylonCoreScript> ().spellColor;
			cRing2.GetComponent<SpriteRenderer>().color = GetComponent<PylonCoreScript> ().spellColor;
            pylon3.GetComponent<PylonScipt>().corrupted = false;
            pcs.GetComponent<PylonCoreScript>().enabled = true;
			timeStopTrigger.SetActive (true);
			x3.SetActive (false);

        }
        //this is where you put all the code for what corrupted spells do
		if (target == 0)
        {
//            if (strength == 0)
//            {
                shrub.corrupting = true;
//            }
//            if (shrub.corruptedPop < cms.shrubPopStart && shrub.pop > cms.minimumInfectionPop)
//            {
                shrub.corruptedPop += cms.shrubPopStart;
//            }
//            if (strength == 1)
//            {
//                shrub.rate *= 1.25f;
//            }
//            if (strength == 2)
//            {
//                cms.shrubRange -= 1;
//            }
        }

        else if (target == 1)
        {
//            if (strength == 0)
//            {
                deer.corrupting = true;
//                if (deer.corruptedPop < cms.deerPopStart && deer.pop > cms.minimumInfectionPop)
//                {
                    deer.corruptedPop += cms.deerPopStart;
//                }
//            }
//            if (strength == 1)
//            {
//                deer.rate *= 1.25f;
//            }
//            if (strength == 2)
//            {
//                cms.deerRange -= 1;
//            }
        }
        else if (target == 2)
        {
//            if (strength == 0)
//            {
                wolf.corrupting = true;
//                if (wolf.corruptedPop < cms.wolfPopStart && wolf.pop > cms.minimumInfectionPop)
//                {
                    wolf.corruptedPop += cms.wolfPopStart;
//               }
//            }
//            if (strength == 1)
//            {
//                wolf.rate *= 1.25f;
//            }
//            if (strength == 2)
//            {
//                cms.wolfRange -= 1;
//            }
        }
        //this is all the stuff that gets to be updated when the spell is cast
		spellFX.playCorrSpellEffect();
        //pylons are set back to being empty (their active selection being -1), then update their sprites to play the corresponding (empty) animation
        //the values in the core are set back to being empty (-1)
        pylon1.activeSelection = -2;
        pylon1.UpdateSprite();
        pylon2.activeSelection = -2;
        pylon2.UpdateSprite();
        pylon3.activeSelection = -2;
        pylon3.UpdateSprite();
        target = -1;
        effect = -1;
        strength = -1;
        //set the spell preview text to be empty since there are no ingredients
        PredictSpell();
        //visual effect for casting
        if (health > 0)
        {
			cooldown = generalCooldown;
        }
		cm.GetComponent<ShrubPopulation> ().DoUpdate();
		/*if (cm.GetComponent<DeerPopulation> ().enabled == true) {
			cm.GetComponent<DeerPopulation> ().DoUpdate ();
		}
		if (cm.GetComponent<WolfPopulation> ().enabled == true) {
			cm.GetComponent<WolfPopulation> ().DoUpdate ();
		}*/
    }

    public void PredictSpell()
    {
        //creates the preview text that appears in the circle. 
//        spellPreviewText = "";
//        if (cooldown >= 0)
//        {
//			spellPreviewText += "Cooldown: " + (Mathf.Floor(cooldown)).ToString();
//			if (spellPreviewText != "") {
//				spellPreviewTextbox.SetActive (true);
//				spellPreviewTextbox.GetComponent<PylonTextBGScript> ().AdjustSize (spellPreviewText.Length / 7f);
//			} else {
//				spellPreviewTextbox.SetActive (false);
//			}
//        }


        if (target != -1 && cooldown <= 0)
        {
			CenterStoneGlow.SetColor (Color.white, .5f);
            castable = true;
			corePopUp.SetActive (true);
            pylon1.castSpellShouldBeActive = true;
            pylon2.castSpellShouldBeActive = true;
            pylon3.castSpellShouldBeActive = true;
        }
        else
        {
			CenterStoneGlow.SetColor (Color.clear, .5f);
            castable = false;
			corePopUp.SetActive (false);
			corePopUp.GetComponent<SpriteRenderer> ().enabled = true;
            pylon1.castSpellShouldBeActive = false;
            pylon2.castSpellShouldBeActive = false;
            pylon3.castSpellShouldBeActive = false;
        }
		if (cooldown > 0)
		{
			CenterStoneGlow.SetColor (Color.clear, .5f);
			print ("Trying to set clear");
		}

        spellPreviewTextbox.GetComponent<TextMesh>().text = spellPreviewText;
    }

    //leftover function from spellbooks
    public void CreateSpell()
    {
        GameObject newSpell = Instantiate(spellPrefab) as GameObject;
        newSpell.GetComponent<SpellScript>().target = target;
        newSpell.GetComponent<SpellScript>().effect = effect;
        newSpell.GetComponent<SpellScript>().strength = strength;
        newSpell.GetComponentsInChildren<Text>()[0].text = spellPreviewText;
        PlaceSpell(newSpell);
        newSpell.GetComponent<SpellScript>().bms = bms;
        pylon1.activeSelection = -1;
        pylon1.UpdateSprite();
        pylon2.activeSelection = -1;
        pylon2.UpdateSprite();
        pylon3.activeSelection = -1;
        pylon3.UpdateSprite();
        target = -1;
        effect = -1;
        strength = -1;
        PredictSpell();
        ring1.SpeedBoost();
        ring2.SpeedBoost();

    }

    //leftover function from spellbooks
    private void PlaceSpell(GameObject spellbook)
    {
        if (GameObject.Find("ABSlot1").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot1").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot1").transform;
            GameObject.Find("ABSlot1").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot2").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot2").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot2").transform;
            GameObject.Find("ABSlot2").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot3").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot3").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot3").transform;
            GameObject.Find("ABSlot3").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot4").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot4").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot4").transform;
            GameObject.Find("ABSlot4").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot5").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot5").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot5").transform;
            GameObject.Find("ABSlot5").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot6").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot6").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot6").transform;
            GameObject.Find("ABSlot6").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
    }

	private void Cooldown() {
		cooldownBar.GetComponent<SpriteMask> ().alphaCutoff = (cooldown / generalCooldown) + -.02f;
		if (Time.time > lastColorChangeTime + 1f) {
			if (colorPulse == true) {
				pylonGlow1.SetColor (pulseColor, 1f);
				pylonGlow2.SetColor (pulseColor, 1f);
				pylonGlow3.SetColor (pulseColor, 1f);
			} else {
				pylonGlow1.SetColor (Color.clear, 1f);
				pylonGlow2.SetColor (Color.clear, 1f);
				pylonGlow3.SetColor (Color.clear, 1f);
			}
			colorPulse = !colorPulse;
			lastColorChangeTime = Time.time;
		}
		if (cooldown < 1f && finalFlash == false) {
			CenterStoneGlow.SetColor (Color.white, .5f);
			cooldownFill.GetComponent<centerStoneGlowScript> ().SetColor (Color.clear, .5f);
			finalFlash = true;
		}
		if (cooldown < .5f && finalFlash == true) {
			pylonGlow1.SetColor (Color.clear, 1f);
			pylonGlow2.SetColor (Color.clear, 1f);
			pylonGlow3.SetColor (Color.clear, 1f);
			cooldownBar.GetComponent<SpriteMask> ().alphaCutoff = 1f;
			CenterStoneGlow.SetColor (Color.clear, .5f);
			finalFlash = false;
		}
	}

}

