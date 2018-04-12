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
    float maxSize = 1.75f;
    float minSize = 0.25f;
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
    private RabbitPopulation rabbit;
    private OwlPopulation owl;

    private bool firstCast;
    private DialogueManager dm;
    private DialogueTrigger gsbc;
    private AudioSource castSound;

	public Text errorTextbox;

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
        rabbit = GameObject.Find("CreatureManager").GetComponent<RabbitPopulation>();
        owl = GameObject.Find("CreatureManager").GetComponent<OwlPopulation>();
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
        //int values of -2 always mean "empty"
        target = -2;
        effect = -2;
        strength = -2;
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
        if (touching && Input.GetButtonDown("Select"))
        {
            if (target != -2 && effect != -2 && strength != -2 && castable && target != 3)
            {
                //if there isn't nothing in each slot, the spell is castable, and the target isn't a corrupted berry, cast the spell
                //in retrospect this is redundant, as the castable bool will only be true if there's something in every slot
                Cast();
                castSound.Play();
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
        else if (target == 3)
        {
            pop = rabbit;
        }
        else if (target == 4)
        {
            pop = owl;
        }
		if (effect == 0) {
			if (strength == 0) {
				pop.size = pop.startSize;
				float corrPercent = pop.corruptedPop / pop.pop;
				pop.pop += 15f * -pop.sizeMod;
				pop.corruptedPop = corrPercent * pop.pop;
				pop.sizeMod = 0;
				pop.UpdateSize ();
			} else {
				if (dm.firstGrowShrubsCast) {
					dm.firstGrowShrubsCast = false;
					print ("shrubs growing");
					gsbc.TriggerDialogue ();
					GameObject.Find ("TutorialWall").SetActive (false);
				}
				float sizeCheck = (pop.size) + strength * 0.25f;
				if (sizeCheck >= minSize && sizeCheck <= maxSize) {
					pop.size = sizeCheck;
					pop.UpdateSize ();
					if (strength > 0) {
						pop.sizeMod += 1;
						float corrPercent = pop.corruptedPop / pop.pop;
						pop.pop += 15f;
						pop.corruptedPop = corrPercent * pop.pop;
					} else if (strength < 0) {
						pop.sizeMod -= 1;
						float corrPercent = pop.corruptedPop / pop.pop;
						pop.pop -= 15f;
						pop.corruptedPop = corrPercent * pop.pop;
					}
				} else {
					GiveError ();
					return;
				}
			}
		} else if (effect == 1) {
			if (pop.speedMod + strength <= 3 && pop.speedMod - strength >= -3) {
				pop.speedMod += Mathf.RoundToInt (strength);
				pop.UpdateUpDown ();
				if (pop.food1 != null)
					pop.food1.UpdateUpDown ();
				if (pop.food2 != null)
					pop.food2.UpdateUpDown ();
				pop.UpdateSpeed ();
			} else {
				GiveError ();
				return;
			}
		}

        else if (effect == 2)
        {
			if (pop.toughMod + strength <= 3 && pop.toughMod - strength >= -3) {
				pop.toughMod += Mathf.RoundToInt (strength);
				pop.UpdateUpDown ();
			} else {
				GiveError ();
				return;
			}
        }

		spellFX.playSpellCastEffect ();
        //pylons are set back to being empty (their active selection being -1), then update their sprites to play the corresponding (empty) animation
        //the values in the core are set back to being empty (-1)
        pylon1.activeSelection = -2;
        pylon1.UpdateSprite();
        pylon2.activeSelection = -2;
        pylon2.UpdateSprite();
        pylon3.activeSelection = -2;
        pylon3.UpdateSprite();
        target = -2;
        effect = -2;
        strength = -2;
        //set the spell preview text to be empty since there are no ingredients
        PredictSpell();
        //visual effect for casting
        
    }

    public void PredictSpell()
    {
        //creates the preview text that appears in the circle. 
        spellPreviewText = "";

        if (effect == 0 && strength == -2)
        {
            spellPreviewText += "Enlarge/shrink ";
        }
        else if (effect == 1 && strength == -2)
        {
            spellPreviewText += "Hasten/slow ";
        }
        else if (effect == 2 && strength == -2)
        {
            spellPreviewText += "Toughen/weaken ";
        }

        if (effect == 0 && strength == -1)
        {
            spellPreviewText += "Shrink ";
        }
        else if (effect == 0 && strength == 0)
        {
            spellPreviewText += "Reset size of ";
        }
        else if (effect == 0 && strength == 1)
        {
            spellPreviewText += "Enlarge ";
        }
        if (effect == 1 && strength == -1)
        {
            spellPreviewText += "Slow ";
        }
        else if (effect == 1 && strength == 0)
        {
            spellPreviewText += "Reset speed of ";
        }
        else if (effect == 1 && strength == 1)
        {
            spellPreviewText += "Hasten ";
        }
        if (effect == 2 && strength == -1)
        {
            spellPreviewText += "Weaken ";
        }
        else if (effect == 2 && strength == 0)
        {
            spellPreviewText += "Reset toughness of ";
        }
        else if (effect == 2 && strength == 1)
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
        else if (target == 3)
        {
            spellPreviewText += "rabbits";
        }
        else if (target == 4)
        {
            spellPreviewText += "owls";
        }



        //this cleanse corruption bit can be removed, it isn't actually possible in this corrupted pylon build
        if (target == 3 || effect == 3 || strength == 3)
        {
            spellPreviewText = "Cleanse Corruption";
        }
        if (target != -2 && effect != -2 && strength != -2 && spellPreviewText != "Cleanse Corruption")
        {
			CenterStoneGlow.SetColor (Color.white, .2f);
            castable = true;

            pylon1.castSpellShouldBeActive = true;
            pylon2.castSpellShouldBeActive = true;
            pylon3.castSpellShouldBeActive = true;
        }
        else
        {
			CenterStoneGlow.SetColor (Color.clear, .2f);
            castable = false;
            corePopUp.SetActive(false);
            pylon1.castSpellShouldBeActive = false;
            pylon2.castSpellShouldBeActive = false;
            pylon3.castSpellShouldBeActive = false;
            corePopUp.GetComponent<SpriteRenderer> ().enabled = true;
        }
        if (target == 3 && effect == 3 && strength == 3)
        {
			CenterStoneGlow.SetColor (Color.white, .2f);
            castable = true;
            pylon1.castSpellShouldBeActive = true;
            pylon2.castSpellShouldBeActive = true;
            pylon3.castSpellShouldBeActive = true;
        }


        spellPreviewTextbox.GetComponent<TextMesh>().text = spellPreviewText;
		if (spellPreviewText != "") {
			spellPreviewTextbox.SetActive (true);
			spellPreviewTextbox.GetComponent<PylonTextBGScript> ().AdjustSize (spellPreviewText.Length / 7f);
		} else {
			spellPreviewTextbox.SetActive (false);
		}

		errorTextbox.transform.parent.gameObject.SetActive (false);
    }

	private void GiveError() {
		string errorText = " ";
		if (target == 0) 
			errorText += "Shrubs";
		if (target == 1) 
			errorText += "Deer";
		if (target == 2) 
			errorText += "Wolves";
		if (target == 3) 
			errorText += "Rabbits";
		if (target == 4) 
			errorText += "Owls";
		errorText += " can't get any ";
		if (strength < 0) {
			if (effect == 0)
				errorText += "smaller";
			if (effect == 1)
				errorText += "slower";
			if (effect == 2)
				errorText += "weaker";
		} else if (strength > 0) {
			if (effect == 0)
				errorText += "larger";
			if (effect == 1)
				errorText += "faster";
			if (effect == 2)
				errorText += "tougher";
		}

		print (errorText);
		errorTextbox.transform.parent.gameObject.SetActive (true);
		errorTextbox.text = errorText;
	}

}

