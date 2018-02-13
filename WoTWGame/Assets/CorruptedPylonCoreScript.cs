﻿using System.Collections;
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
    public string spellPreviewText;
    public GameObject spellPreviewTextbox;
    public RingSpinScript ring1;
    public RingSpinScript ring2;
    private bool castable;
    public GameObject corruptionNode;
    public GameObject pcs;
    public float cooldown;
    public int health;
    public GameObject cRing1;
    public GameObject cRing2;
	public centerStoneGlowScript CenterStoneGlow;
	public GameObject corePopUp;
    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;
    private DialogueManager dm;
    private DialogueTrigger cbc;
    private DialogueTrigger clFN;
    private DialogueTrigger clSN;
    private DialogueTrigger clTN;
    private DialogueTrigger clFoN;
    private DialogueTrigger clFiN;
    private int nodeCount;
    private AudioSource castSound;


    // Use this for initialization
    void Start()
    {
        cooldown = 0;
        //some of these things may be unnecessary. This script was made by copying over a lot of stuff from the original spellscript, since it casts spells.
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
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
        cooldown -= Time.deltaTime;
        if (touching && Input.GetKeyDown(KeyCode.E) && cooldown <= 0)
        {
            if (target != -1 && strength != -1 && castable && target != 3)
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
        if (cooldown >= 0)
        {
            PredictSpell();
        }
    }
    //determine if the player is touching the core of the pylon circle, allowing them to cast
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touching = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touching = false;
        }
    }

    public void Cast()
    {
        health -= 1;
		print ("health lowered by 1");
        if (health <= 0)
        {
            //Checking the number of nodes cleansed, and playing the corresponding dialogue
            if (dm.cleansedNodes == 0)
            {
                clFN.TriggerDialogue();
                deer.enabled = true;
				eco.GetComponent<UIManager> ().ActivateDeer ();
				eco.tempShrubCapBool = false;
                //Deer get activated
                //Polish: Deer moves across screen
            }
            else if (dm.cleansedNodes == 1)
            {
                clSN.TriggerDialogue();
                wolf.enabled = true;
				eco.GetComponent<UIManager> ().ActivateWolves ();
				cms.phase += 1;
				cms.nextCorruptionTime = Time.time + cms.infectTime;
				cms.currentCorruptionRate = cms.phase1CorruptionRate;
                //Wolves get activated
                //Polish: Wolf moves across the screen
            }
            else if (dm.cleansedNodes == 2)
            {
                clTN.TriggerDialogue();
				GameObject.Find ("CorruptionWall").SetActive (false);
                //rabbit.enabled = true;
            }
            else if (dm.cleansedNodes == 3)
            {
                clFoN.TriggerDialogue();
                //owl.enabled = true;
            }
            else if (dm.cleansedNodes == 4)
            {
                clFiN.TriggerDialogue();
                //no longer present in level
            }
            dm.cleansedNodes++;
            cm.corruptionNodeList.Remove(corruptionNode);
            Destroy(corruptionNode);
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
			cRing1.GetComponent<SpriteRenderer> ().color = GetComponent<PylonCoreScript> ().spellColor;
			cRing2.GetComponent<SpriteRenderer>().color = GetComponent<PylonCoreScript> ().spellColor;
            pylon3.GetComponent<PylonScipt>().corrupted = false;
            pcs.GetComponent<PylonCoreScript>().enabled = true;

        }
        //this is where you put all the code for what corrupted spells do
        if (target == 0)
        {
            if (strength == 0)
            {
                if (dm.firstCorrCast)
                {
                    dm.firstCorrCast = false;
                    health -= 1;
                    cbc.TriggerDialogue();

                }
                shrub.corrupting = true;
            }
            if (shrub.corruptedPop < cms.shrubPopStart && shrub.pop > cms.minimumInfectionPop)
            {
                shrub.corruptedPop = cms.shrubPopStart;
            }
            if (strength == 1)
            {
                shrub.rate *= 1.25f;
            }
            if (strength == 2)
            {
                cms.shrubRange -= 1;
            }
        }

        else if (target == 1)
        {
            if (strength == 0)
            {
                deer.corrupting = true;
                if (deer.corruptedPop < cms.deerPopStart && deer.pop > cms.minimumInfectionPop)
                {
                    deer.corruptedPop = cms.deerPopStart;
                }
            }
            if (strength == 1)
            {
                deer.rate *= 1.25f;
            }
            if (strength == 2)
            {
                cms.deerRange -= 1;
            }
        }
        else if (target == 2)
        {
            if (strength == 0)
            {
                wolf.corrupting = true;
                if (wolf.corruptedPop < cms.wolfPopStart && wolf.pop > cms.minimumInfectionPop)
                {
                    wolf.corruptedPop = cms.wolfPopStart;
                }
            }
            if (strength == 1)
            {
                wolf.rate *= 1.25f;
            }
            if (strength == 2)
            {
                cms.wolfRange -= 1;
            }
        }
        //this is all the stuff that gets to be updated when the spell is cast
		spellFX.playCorrSpellEffect();
        //pylons are set back to being empty (their active selection being -1), then update their sprites to play the corresponding (empty) animation
        //the values in the core are set back to being empty (-1)
        pylon1.activeSelection = -1;
        pylon1.UpdateSprite();
        pylon2.activeSelection = -1;
        pylon2.UpdateSprite();
        pylon3.activeSelection = -1;
        pylon3.UpdateSprite();
        target = -1;
        effect = -1;
        strength = -1;
        //set the spell preview text to be empty since there are no ingredients
        PredictSpell();
        //visual effect for casting
        if (health > 0)
        {
            cooldown = 10f;
        }


    }

    public void PredictSpell()
    {
        //creates the preview text that appears in the circle. 
        spellPreviewText = "";
        if (cooldown >= 0)
        {
            spellPreviewText += "Cooldown: " + cooldown.ToString("F2");
        }
//        else
//        {
//            if (strength == 0)
//            {
//                spellPreviewText += "Corrupt ";
//            }
//            else if (strength == 1)
//            {
//                spellPreviewText += "Sicken ";
//            }
//            else if (strength == 2)
//            {
//                spellPreviewText += "Exhaust ";
//            }
//
//
//
//
//            if (target == 0)
//            {
//                spellPreviewText += "shrubs";
//            }
//            else if (target == 1)
//            {
//                spellPreviewText += "deer";
//            }
//            else if (target == 2)
//            {
//                spellPreviewText += "wolves";
//            }
//        }




        if (cooldown > 0)
        {
			CenterStoneGlow.SetColor (Color.white);
        }
        if (target != -1 && strength != -1 && cooldown <= 0)
        {
			CenterStoneGlow.SetColor (Color.white);
            castable = true;
			corePopUp.SetActive (true);
        }
        else
        {
			CenterStoneGlow.SetColor (Color.clear);
            castable = false;
			corePopUp.SetActive (false);
			corePopUp.GetComponent<SpriteRenderer> ().enabled = true;
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

}

