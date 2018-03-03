using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PylonCoreScript : MonoBehaviour
{

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
    public bool wasCorrupted;
    public GameObject cpcs;

	public centerStoneGlowScript CenterStoneGlow;
	public GameObject corePopUp;
	private SpellFXController spellFX;

    private basePopulation pop;
    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;

    private bool firstCast;
    private DialogueManager dm;
    private DialogueTrigger gsbc;
    private AudioSource castSound;

	public Color corrColor;
	public Color shrubColor;
	public Color deerColor;
	public Color wolfColor;
	public Color rabbitColor;
	public Color owlColor;
	public Color spellColor;


    // Use this for initialization
    void Start()
    {
        //some of these things may be unnecessary. This script was made by copying over a lot of stuff from the original spellscript, since it casts spells.
        eco = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        cm = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>();
        spellCore = GameObject.Find("Core");
		spellFX = GetComponent<SpellFXController> ();
        if (GameObject.Find("Player").GetComponent<PlayerControllerScript>().noChargeMode)
        {
            instaCast = true;
        }
        if (GameObject.Find("MultiMenu") != null)
        {
            bms = GameObject.Find("MultiMenu").GetComponentInChildren<UIbuffScript>(true);
        }
        //int values of -1 always mean "empty"
        target = -1;
        effect = -1;
        strength = -1;
		sms = GameObject.Find ("SpellMenu").GetComponent<SpellMenuScript> ();
        gsbc = GameObject.Find("GrowShrubsBeenCast").GetComponent<DialogueTrigger>();
        dm = GameObject.Find("TutorialDialogue").GetComponent<DialogueManager>();
        firstCast = true;

        castSound = GameObject.Find("Snd_Cast").GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (wasCorrupted)
        {
            cpcs.GetComponent<CorruptedPylonCoreScript>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.E))
        {
            if (target != -1 && effect != -1 && strength != -1 && castable && target != 3)
            {
                //if there isn't nothing in each slot, the spell is castable, and the target isn't a corrupted berry, cast the spell
                //in retrospect this is redundant, as the castable bool will only be true if there's something in every slot
                Cast();
                castSound.Play();
            }
            else if (target != -1 && effect != -1 && strength != -1 && castable)
            {
                //this would trigger if there are corrupted berries in every slot, but corrupted berries aren't an option in this build, so it's not necessary
                CreateSpell();
            }
            else
            {
                Debug.Log("Need more ingredients");
            }
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
    //largely same as in spellscript, differences at the end of the function
    public void Cast()
    {
        if (target == 0)
        {
            pop = shrub;
        }
        else if (target == 1)
        {
            pop = deer;
        }
        else if (target == 2)
        {
            pop = wolf;
        }
        if (effect == 0)
        {
            if (strength == 0)
            {
                pop.size = pop.startSize;
				float corrPercent = pop.corruptedPop / pop.pop;
				pop.pop += 15f * -pop.sizeMod;
				pop.corruptedPop = corrPercent * pop.pop;
                pop.sizeMod = 0;
                pop.UpdateSize();
            }
            else
            {
                if (dm.firstGrowShrubsCast)
                {
                    dm.firstGrowShrubsCast = false;
                    print("shrubs growing");
                    gsbc.TriggerDialogue();
					GameObject.Find ("TutorialWall").SetActive (false);
                }
                float sizeCheck = (pop.size) + strength * 0.05f;
                if (sizeCheck >= minSize && sizeCheck <= maxSize)
                {
                    pop.size = sizeCheck;
                    pop.UpdateSize();
                    if (strength > 0)
                    {
                        pop.sizeMod += 1;
						float corrPercent = pop.corruptedPop / pop.pop;
						pop.pop += 15f;
						pop.corruptedPop = corrPercent * pop.pop;
                    }
                    else if (strength < 0)
                    {
                        pop.sizeMod -= 1;
						float corrPercent = pop.corruptedPop / pop.pop;
						pop.pop -= 15f;
						pop.corruptedPop = corrPercent * pop.pop;
                    }
                }
            }
        }
        else if (effect == 1)
        {
            if (strength == 0)
            {
                pop.speed = pop.startSpeed;
                pop.speedMod = 0;
                pop.up1 = 0;
                pop.timesSpeedChanged = 0;
                if (pop.food1 != null)
                {
					print (pop.timesSpeedChanged);
                    pop.food1.down2 -= sms.spellStrengthMod * pop.timesSpeedChanged;
                }
                if (pop.food2 != null)
                {
                    pop.food2.down2 -= sms.spellStrengthMod * pop.timesSpeedChanged;
                }
                pop.UpdateSpeed();
            }
            else
            {
				float speedCheck = pop.speed + strength * 0.05f;
				// speed check is temporarily disabled since it isn't working right
//                if (speedCheck >= minSpeed && speedCheck <= maxSpeed)
//                {
                    pop.up1 += strength;
                    if (pop.food1 != null)
                    {
                        pop.food1.down2 += strength;
                    }
                    if (pop.food2 != null)
                    {
                        pop.food2.down2 += strength;
                    }
                    pop.timesSpeedChanged += 1;
                    pop.speed += strength * .05f;
                    pop.UpdateSpeed();
                    if (strength > 0)
                    {
                        pop.speedMod += 1;
                    }
                    else if (strength < 0)
                    {
                        pop.speedMod -= 1;
                    }
//                }
            }
        }

        else if (effect == 2)
        {
            if (strength == 0)
            {
                pop.down1 = 0;
                if (pop.pred1 != null)
                {
                    if (pop.pred2 != null)
                    {
                        pop.down2 = sms.spellStrengthMod * pop.pred2.timesSpeedChanged;
                    }
                    pop.down2 = sms.spellStrengthMod * pop.pred1.timesSpeedChanged;
                }                
                shrub.toughMod = 0;
            }
            else
            {
                pop.down1 -= strength;
                pop.down2 -= strength;
                if (strength > 0)
                {
                    pop.toughMod += 1;
                }
                else if (strength < 0)
                {
                    pop.toughMod -= 1;
                }
            }
        }




            /*if (target == 0 || target == 3)
            {
                if (strength == 0)
                {
                    shrub.size = shrub.startSize;
                    shrub.sizeMod = 0;
                    foreach (GameObject garfield in cm.shrubCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(shrub.size, shrub.size);
                    }
                    foreach (GameObject garfield in cm.corruptedShrubCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(shrub.size, shrub.size);
                    }
                }
                else
                {
                    if (firstCast)
                    {
                        firstCast = false;
                        print("shrubs growing");
                        gsbc.TriggerDialogue();
                    }
                    float sizeCheck = (shrub.size) + strength * 0.05f;
                    if (sizeCheck >= minSize && sizeCheck <= maxSize)
                    {
                        shrub.size = sizeCheck;
                        foreach (GameObject garfield in cm.shrubCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(shrub.size, shrub.size);
                        }
                        foreach (GameObject garfield in cm.corruptedShrubCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(shrub.size, shrub.size);
                        }
                        if (strength > 0)
                        {
                            shrub.sizeMod += 1;
                        }
                        else if (strength < 0)
                        {
                            shrub.sizeMod -= 1;
                        }
                    }

                }

            }
            else if (target == 1 || target == 4)
            {
                if (strength == 0)
                {
                    deer.size = deer.startSize;
                    deer.sizeMod = 0;
                    foreach (GameObject garfield in cm.deerCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(deer.size, deer.size);
                    }
                    foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(deer.size, deer.size);
                    }
                }
                else
                {
                    float sizeCheck = (deer.size) + strength * 0.05f;
                    if (sizeCheck >= minSize && sizeCheck <= maxSize)
                    {
                        deer.size = sizeCheck;
                        foreach (GameObject garfield in cm.deerCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(deer.size, deer.size);
                        }
                        foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(deer.size, deer.size);
                        }
                        if (strength > 0)
                        {
                            deer.sizeMod += 1;
                        }
                        else if (strength < 0)
                        {
                            deer.sizeMod -= 1;
                        }
                    }
                }

            }
            else if (target == 2 || target == 5)
            {
                if (strength == 0)
                {
                    wolf.size = wolf.startSize;
                    wolf.sizeMod = 0;
                    foreach (GameObject garfield in cm.wolfCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(wolf.size, wolf.size);
                    }
                    foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                    {
                        garfield.transform.localScale = new Vector3(wolf.size, wolf.size);
                    }
                }
                else
                {
                    float sizeCheck = (wolf.size) + strength * 0.05f;
                    if (sizeCheck >= minSize && sizeCheck <= maxSize)
                    {
                        wolf.size = sizeCheck;
                        foreach (GameObject garfield in cm.wolfCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(wolf.size, wolf.size);
                        }
                        foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                        {
                            garfield.transform.localScale = new Vector3(wolf.size, wolf.size);
                        }
                        if (strength > 0)
                        {
                            wolf.sizeMod += 1;
                        }
                        else if (strength < 0)
                        {
                            wolf.sizeMod -= 1;
                        }
                    }
                }
            }
        } 

            if (effect == 1)
    {
        //hasten/slow, change increase; and decrease of food
        if (target == 0 || target == 3)
        {
            if (strength == 0)
            {
                shrub.up1 = 0;
                shrub.speedMod = 0;
            }
            else
            {
                shrub.up1 += strength;
                if (strength > 0)
                {
                    shrub.speedMod += 1;
                }
                else if (strength < 0)
                {
                    shrub.speedMod -= 1;
                }
            }
        }
        else if (target == 1 || target == 4)
        {
            if (strength == 0)
            {
                deer.up1 = 0;
                shrub.down1 -= sms.spellStrengthMod * deer.timesSpeedChanged;
                deer.speedMod = 0;
                deer.timesSpeedChanged = 0;
                deer.speed = deer.startSpeed;
                foreach (GameObject garfield in cm.deerCreatureList)
                {
                    garfield.GetComponent<AnimalMovementScript>().speed2 = deer.speed;
                }
                foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                {
                    garfield.GetComponent<AnimalMovementScript>().speed2 = deer.speed;
                }
            }
            else
            {
                float speedCheck = (deer.speed) + strength * 0.05f;
                if (speedCheck >= minSpeed && speedCheck <= maxSpeed)
                {
                    deer.up1 += strength;
                    shrub.down1 += strength;
                    deer.timesSpeedChanged += 1;
                    deer.speed += strength * .05f;
                    foreach (GameObject garfield in cm.deerCreatureList)
                    {
                        garfield.GetComponent<AnimalMovementScript>().speed2 = deer.speed;
                    }
                    foreach (GameObject garfield in cm.corruptedDeerCreatureList)
                    {
                        garfield.GetComponent<AnimalMovementScript>().speed2 = deer.speed;
                    }
                    if (strength > 0)
                    {
                        deer.speedMod += 1;
                    }
                    else if (strength < 0)
                    {
                        deer.speedMod -= 1;
                    }
                }
            }
        }
        else if (target == 2 || target == 5)
        {
            if (strength == 0)
            {
                wolf.up1 = 0;
                deer.down2 -= sms.spellStrengthMod * wolf.timesSpeedChanged;
                wolf.speedMod = 0;
                wolf.timesSpeedChanged = 0;
                wolf.speed = wolf.startSpeed;
                foreach (GameObject garfield in cm.wolfCreatureList)
                {
                    garfield.GetComponent<AnimalMovementScript>().speed2 = wolf.speed;
                }
                foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                {
                    garfield.GetComponent<AnimalMovementScript>().speed2 = wolf.speed;
                }
            }
            else
            {
                float speedCheck = (wolf.speed) + strength * 0.05f;
                if (speedCheck >= minSpeed && speedCheck <= maxSpeed)
                {
                    wolf.up1 += strength;
                    deer.down2 += strength;
                    wolf.timesSpeedChanged += 1;
                    wolf.speed += strength * .05f;
                    foreach (GameObject garfield in cm.wolfCreatureList)
                    {
                        garfield.GetComponent<AnimalMovementScript>().speed2 = wolf.speed;
                    }
                    foreach (GameObject garfield in cm.corruptedWolfCreatureList)
                    {
                        garfield.GetComponent<AnimalMovementScript>().speed2 = wolf.speed;
                    }
                    if (strength > 0)
                    {
                        wolf.speedMod += 1;
                    }
                    else if (strength < 0)
                    {
                        wolf.speedMod -= 1;
                    }
                }
            }
        }
    } 

            if (effect == 2)
        {
            //toughen/weaken, change decrease rates
            if (target == 0 || target == 3)
            {
                if (strength == 0)
                {
                    shrub.down1 = sms.spellStrengthMod * deer.timesSpeedChanged;
                    shrub.toughMod = 0;
                }
                else
                {
                    shrub.down1 -= strength;
                    if (strength > 0)
                    {
                        shrub.toughMod += 1;
                    }
                    else if (strength < 0)
                    {
                        shrub.toughMod -= 1;
                    }
                }
            }
            else if (target == 1 || target == 4)
            {
                if (strength == 0)
                {
                    deer.down1 = 0;
                    deer.down2 = sms.spellStrengthMod * wolf.timesSpeedChanged;
                    deer.toughMod = 0;
                }
                else
                {
                    deer.down1 -= strength;
                    deer.down2 -= strength;
                    if (strength > 0)
                    {
                        deer.toughMod += 1;
                    }
                    else if (strength < 0)
                    {
                        deer.toughMod -= 1;
                    }
                }
            }
            else if (target == 2 || target == 5)
            {
                if (strength == 0)
                {
                    wolf.down1 = 0;
                    wolf.toughMod = 0;
                }
                else
                {
                    wolf.down1 -= strength;
                    if (strength > 0)
                    {
                        wolf.toughMod += 1;
                    }
                    else if (strength < 0)
                    {
                        wolf.toughMod -= 1;
                    }
                }
            }
            */


        
        if (effect == 3)
        {
            foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList)
            {
                if ((gunch.transform.position - GameObject.Find("Player").transform.position).magnitude < 6)
                {
                    Destroy(gunch);
                }
            }
        }
        if (effect == 4)
        {
            foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList)
            {
                if ((gunch.transform.position - GameObject.Find("Player").transform.position).magnitude < 6)
                {
                    Destroy(gunch);
                }
            }
        }
        if (effect == 5)
        {
            foreach (GameObject gunch in GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList)
            {
                if ((gunch.transform.position - GameObject.Find("Player").transform.position).magnitude < 6)
                {
                    Destroy(gunch);
                }
            }
        }
        if (effect == 6)
        {
            GameObject.Find("CorruptedAltar").GetComponent<SpellcraftingAltarScript>().Cleanse();
            GameObject.Find("IntroMusic").GetComponent<fadeAudioScript>().beginFade(3f);
        }

		spellFX.playSpellCastEffect ();
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
        
    }

    public void PredictSpell()
    {
        //creates the preview text that appears in the circle. 
        spellPreviewText = "";

        if (effect == 0 && strength == -1)
        {
            spellPreviewText += "Enlarge/shrink ";
        }
        else if (effect == 1 && strength == -1)
        {
            spellPreviewText += "Hasten/slow ";
        }
        else if (effect == 2 && strength == -1)
        {
            spellPreviewText += "Toughen/weaken ";
        }

        if (effect == 0 && strength == -4)
        {
            spellPreviewText += "Shrink ";
        }
        else if (effect == 0 && strength == 0)
        {
            spellPreviewText += "Reset size of ";
        }
        else if (effect == 0 && strength == 4)
        {
            spellPreviewText += "Enlarge ";
        }
        if (effect == 1 && strength == -4)
        {
            spellPreviewText += "Slow ";
        }
        else if (effect == 1 && strength == 0)
        {
            spellPreviewText += "Reset speed of ";
        }
        else if (effect == 1 && strength == 4)
        {
            spellPreviewText += "Hasten ";
        }
        if (effect == 2 && strength == -4)
        {
            spellPreviewText += "Weaken ";
        }
        else if (effect == 2 && strength == 0)
        {
            spellPreviewText += "Reset toughness of ";
        }
        else if (effect == 2 && strength == 4)
        {
            spellPreviewText += "Toughen ";
        }




        if (target == 0)
        {
            spellPreviewText += "shrubs";
        }
        else if (target == 1)
        {
            spellPreviewText += "deer";
        }
        else if (target == 2)
        {
            spellPreviewText += "wolves";
        }



        //this cleanse corruption bit can be removed, it isn't actually possible in this corrupted pylon build
        if (target == 3 || effect == 3 || strength == 3)
        {
            spellPreviewText = "Cleanse Corruption";
        }
        if (target != -1 && effect != -1 && strength != -1 && spellPreviewText != "Cleanse Corruption")
        {
			CenterStoneGlow.SetColor (Color.white, .2f);
            castable = true;
			corePopUp.SetActive (true);
        }
        else
        {
			CenterStoneGlow.SetColor (Color.clear, .2f);
            castable = false;
			corePopUp.SetActive (false);
			corePopUp.GetComponent<SpriteRenderer> ().enabled = true;
        }
        if (target == 3 && effect == 3 && strength == 3)
        {
			CenterStoneGlow.SetColor (Color.white, .2f);
            castable = true;
			corePopUp.SetActive (true);
        }


        spellPreviewTextbox.GetComponent<TextMesh>().text = spellPreviewText;
		if (spellPreviewText != "") {
			spellPreviewTextbox.SetActive (true);
			spellPreviewTextbox.GetComponent<PylonTextBGScript> ().AdjustSize (spellPreviewText.Length / 7f);
		} else {
			spellPreviewTextbox.SetActive (false);
		}
    }

    public void CreateSpell()
    {
        //leftover function for creating spellbook for cleanse corruption, should never be called, can probably be removed
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

    private void PlaceSpell(GameObject spellbook)
    {
        //leftover function for placing spellbook on actionbar
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

