﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEcologyMasterScript : MonoBehaviour
{

    public bool paused;
    public bool megaPaused;
	public bool areaTimeStop;

    public float shrubPop;
    public float deerPop;
    public float deerPop2;
    public float wolfPop;
    public float shrubBiomass;
    public float deerBiomass;
    public float wolfBiomass;
//    public float corruptedShrubPop;
//    public float corruptedDeerPop;
//    public float corruptedWolfPop;
//    public float corruptedShrubBiomass;
//    public float corruptedDeerBiomass;
//    public float corruptedWolfBiomass;
    public float deerSpeed;
    public float wolfSpeed;

	public bool tempShrubCapBool;
	public float tempShrubCap;

    public float shrubSize;
    public float deerSize;
    public float wolfSize;

    //Mods: 0 = default, 1 = positve effect, -1 = negitive effect, should always start on 0, range is -3/+3
    public float startShrubSize;
    public float startDeerSize;
    public float startWolfSize;
    public float startDeerSpeed;
    public float startWolfSpeed;
    public int shrubSizeMod = 0;
    public int deerSizeMod = 0;
    public int wolfSizeMod = 0;
    public int shrubSpeedMod = 0;
    public int deerSpeedMod = 0;
    public int wolfSpeedMod = 0;
    public int shrubToughMod = 0;
    public int deerToughMod = 0;
    public int wolfToughMod = 0;
    public int timesDeerSpeedChanged = 0;
    public int timesWolfSpeedChanged = 0;


    public float ecoToWorldDivision;

    public bool shrubRising;
    public bool deerRising;
    public bool wolfRising;
    public bool deerRising2;

    public float shrubUp;
    public float shrubDown;
    public float deerUp1;
    public float deerDown1;
    public float wolfUp;
    public float wolfDown;
    public float deerUp2;
    public float deerDown2;

    public float corruptionRate;
    public float corruptionAcceleration;
    public bool corruptingShrubs;
    public bool corruptingDeer;
    public bool corruptingWolves;

    public float overallSpeed;
    public float overShootValue;
    //	public float shrubOvershoot;
    //	public float deerOvershoot;
    //	public float wolfOvershoot;

    private float lastAdjustment;
    public float adjustmentDelay;

    private CreatureManagerScript CMan;
    public UIBarScript shrubBarUI;
    public UIBarScript deerBarUI;
    public UIBarScript wolfBarUI;
    public UIBarScript corruptedShrubBarUI;
    public UIBarScript corruptedDeerBarUI;
    public UIBarScript corruptedWolfBarUI;

    private float rateOfShrubChange;
    private float rateOfDeerChange;
    private float rateOfWolfChange;

    public GameObject gameOver1;
    public GameObject gameOver2;
    public GameObject gameOver3;
    public GameObject gameOver4;
    public GameObject gameOver5;
    public GameObject gameOver6;
    public GameObject mainMenuButton;
    public GameObject exitGameButton;
    public GameObject victory;
    public GameObject menuCamera;
    public float shrubRate, deerRate, wolfRate, startShrubRate, startDeerRate, startWolfRate;

    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;
    public NewUIScript shrubUI;
    public NewUIScript deerUI;
    public NewUIScript wolfUI;

	public float corruptionFallFactor;

    private bool firstFall;
    private DialogueTrigger sff;

    //Music Manager
    private AudioSource mainMusic;
    private AudioSource corruptMusic;
    private bool corruptPlaying;
    //Must be < 1
    public float warningPercent;

    // Use this for initialization
    void Start()
    {
        CMan = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>();
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        sff = GameObject.Find("ShrubsFirstFall").GetComponent<DialogueTrigger>();
        shrubSize = 1;
        deerSize = 1;
        wolfSize = 1;

        startShrubSize = shrub.startSize;
        startDeerSize = deer.startSize;
        startWolfSize = wolf.startSize;
        startDeerSpeed = deer.startSpeed;
        startWolfSpeed = wolf.startSpeed;
        shrubRate = shrub.rate;
        startShrubRate = shrub.startRate;
        deerRate = deer.rate;
        startDeerRate = deer.startRate;
        wolfRate = wolf.rate;
        startWolfRate = wolf.startRate;



		if (!megaPaused)
        {
            if (shrub.corruptedPop > 0)
            {
                CMan.shrubNum = (shrub.pop - shrub.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                CMan.shrubNum = shrub.pop / ecoToWorldDivision;
            }

            if (deer.corruptedPop > 0)
            {
                CMan.deerNum = (deer.pop - deer.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                CMan.deerNum = deer.pop / ecoToWorldDivision;
            }

            if (wolf.corruptedPop > 0)
            {
                CMan.wolfNum = (wolf.pop - wolf.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                CMan.wolfNum = wolf.pop / ecoToWorldDivision;
            }

            CMan.corruptedShrubNum = shrub.corruptedPop / ecoToWorldDivision;
            CMan.corruptedDeerNum = deer.corruptedPop / ecoToWorldDivision;
            CMan.corruptedWolfNum = wolf.corruptedPop / ecoToWorldDivision;
            CMan.AdjustCreatures();
            CMan.AdjustPickips();
        }
        firstFall = true;

        mainMusic = GameObject.Find("WotW soundtrack").GetComponent<AudioSource>();
        corruptMusic = GameObject.Find("Mus_Corrupt").GetComponent<AudioSource>();
        corruptPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Music Toggle
        //gets current corruption % for each population
		float shrubPer = shrub.corruptedPop / shrub.pop;
		float deerPer = deer.corruptedPop / deer.pop;
		float wolfPer = wolf.corruptedPop / wolf.pop;
        //Start corrupt warning
        if((shrubPer >= warningPercent) || (deerPer >= warningPercent) || (wolfPer >= warningPercent))
        {
            if (!corruptPlaying)
            {
                mainMusic.volume = 0;
                corruptMusic.Play();
                corruptPlaying = true;
                
            }
            if(corruptPlaying && !corruptMusic.isPlaying)
            {
                mainMusic.volume = 1;
            }
        }
        //End corrupt warning
        else
        {
            if (corruptPlaying)
            {
                corruptPlaying = false;
            }
        }
        
        if (shrub.pop < deer.pop && firstFall)
        {
            firstFall = false;
            sff.TriggerDialogue();
        }
		if (!paused && !megaPaused && !areaTimeStop)
        {
            SimpleEcologize();
        }

    }

    void SimpleEcologize()
    {
        //sets the rising triggers. If a rising trigger is true, it makes the pop rise, if it's false, it makes it fall.
        //some triggers are set as soon as they go over or under another value, while others wait to go a little bit beyond 
        //to an overshoot value to simulate an ecosystem better
        if (deer.enabled == true)
        {
            if (shrub.biomass > (deer.biomass + overShootValue))
            {
                deer.rising1 = true;
            }
            if (shrub.biomass < (deer.biomass - overShootValue))
            {
                deer.rising1 = false;
            }
            if (shrub.biomass < deer.biomass)
            {
                shrub.rising1 = false;
            }
            else if (shrub.biomass > deer.biomass)
            {
                shrub.rising1 = true;
            }
        }
        else
        {
            shrub.rising1 = true;
        }

        if (wolf.enabled == true && deer.enabled == true)
        {
            if (deer.biomass > (wolf.biomass + overShootValue))
            {
                wolf.rising1 = true;
            }
            if (deer.biomass < (wolf.biomass - overShootValue))
            {
                wolf.rising1 = false;
            }
            if (deer.biomass < wolf.biomass)
            {
                deer.rising2 = false;
            }
            else if (deer.biomass > wolf.biomass)
            {
                deer.rising2 = true;
            }
        }
        else if (deer.enabled == true)
        {
            deer.rising2 = true;
        }
        


        //Below is the section that changes the populations according to what rising triggers are set

        //Because deer are the only creatures with 2 rising triggers (their pop changes both from the shrub pop and the wolf pop) they have an extra step
        //In order to have a way of telling what the net change to deer pop is every tick, the change from the bools has to be added together into
        //the float rateOfDeerChange, and then that float is applies to the actual population.
		rateOfShrubChange = 0;
		rateOfDeerChange = 0;
		rateOfWolfChange = 0;
        //the format of these is:
        //population += (constant number chosen in order to keep the ecosystem balanced by default + modifier that is the result of buff * .2f to weaken the impact
        // of the buffs.
        //One of these is also multiplied by 2 at the end, I don't remember why specifically, but I think it's part of keeping things balanced.
		if (shrub.rising1 == true) {
			if (shrub.biomass < 100) {
				if (tempShrubCapBool == false || (shrub.biomass < tempShrubCap)) {
					shrubPop = (2 + shrub.up1 * .2f) * overallSpeed * Time.deltaTime;
					shrubUI.popChange.text = (shrubPop * 100).ToString ("0.00");
					shrubUI.rightChange.text = (shrubPop * 100 ).ToString ("0.00");
					rateOfShrubChange += shrubPop;
				}
			}
		}
        else
        {
            shrubPop = (2 + shrub.down1 * .2f) * overallSpeed * Time.deltaTime;
			shrubUI.popChange.text = (shrubPop * -100).ToString("0.00");
			shrubUI.rightChange.text = (shrubPop * -100).ToString("0.00");
			rateOfShrubChange -= shrubPop;
        }
		shrub.pop += rateOfShrubChange;
		if (rateOfShrubChange < 0)
		{
			shrub.corruptedPop += rateOfShrubChange * corruptionFallFactor;
		}

        if(deer.enabled == true)
        {
			if (deer.rising1 == true && deer.pop < 100)
            {
                rateOfDeerChange += (2 + deer.up1 * .2f) * overallSpeed * Time.deltaTime;
                deerUI.leftChange.text = ((2 + deer.up1 * .2f) * overallSpeed * Time.deltaTime * 100).ToString("0.00");
                deerPop = rateOfDeerChange;
            }
            else
            {
				if (wolf.enabled == false) {
					rateOfDeerChange -= (7 + deer.down2 * .2f) * overallSpeed * Time.deltaTime;
                    deerUI.leftChange.text = (-((7 + deer.down2 * .2f) * overallSpeed * Time.deltaTime * 100)).ToString("0.00");
                    deerPop = rateOfDeerChange;
				} else {
					rateOfDeerChange -= (3 + deer.down2 * .2f) * overallSpeed * Time.deltaTime;
                    deerUI.leftChange.text = (-((3 + deer.down2 * .2f) * overallSpeed * Time.deltaTime * 100)).ToString("0.00");
                    deerPop = rateOfDeerChange;
                }
				
            }
			if (deer.rising2 == true && deer.pop < 100)
            {
                rateOfDeerChange += (2 + deer.up2 * .2f) * overallSpeed * Time.deltaTime;
                deerUI.rightChange.text = ((2 + deer.up2 * .2f) * overallSpeed * Time.deltaTime * 100).ToString("0.00");
                deerPop2 = rateOfDeerChange;
            }
            else
            {
                rateOfDeerChange -= (1 + deer.down2 * .2f) * overallSpeed * Time.deltaTime * 2;

                deerUI.rightChange.text = (-((1 + deer.down2 * .2f) * overallSpeed * Time.deltaTime * 2 * 100)).ToString("0.00");
                deerPop2 = rateOfDeerChange;
            }
            deerUI.popChange.text = rateOfDeerChange.ToString("0.00");
            deer.pop += rateOfDeerChange;
            if (rateOfDeerChange < 0)
            {
				deer.corruptedPop += rateOfDeerChange * corruptionFallFactor;
            }
        }
        
        if(wolf.enabled == true)
        {
			if (wolf.rising1 == true && wolf.pop < 100)
            {
                wolfPop = (2 + wolf.up1 * .2f) * overallSpeed * Time.deltaTime;
				wolfUI.leftChange.text = (wolfPop * 100).ToString("0.00");
				wolfUI.popChange.text = (wolfPop * 100).ToString("0.00");
				rateOfWolfChange += wolfPop;
            }
            else
            {
                wolfPop = (3 + wolf.down1 * .2f) * overallSpeed * Time.deltaTime;
				wolfUI.leftChange.text = (wolfPop * -100).ToString("0.00");
				wolfUI.popChange.text = (wolfPop * -100).ToString("0.00");
				rateOfWolfChange -= wolfPop;
            }
			wolf.pop += rateOfWolfChange;
			if (rateOfWolfChange < 0)
			{
				wolf.corruptedPop += rateOfWolfChange * corruptionFallFactor;
			}
        }
        

        

        //populations find out if they have anough food based on their biomass and their foods biomass. biomass is based on population times size.
        shrub.biomass = shrub.pop * shrub.size;
        deer.biomass = deer.pop * deer.size;
        wolf.biomass = wolf.pop * wolf.size;
        shrub.corruptedBiomass = shrub.corruptedPop * shrub.size;
        deer.corruptedBiomass = deer.corruptedPop * deer.size;
        wolf.corruptedBiomass = wolf.corruptedPop * wolf.size;

    }
}
