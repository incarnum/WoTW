using System.Collections;
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
    public float wolfPop2;
    public float rabbitPop;
    public float rabbitPop2;
    public float owlPop;
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
    private RabbitPopulation rabbit;
    private OwlPopulation owl;
    public NewUIScript shrubUI;
    public NewUIScript deerUI;
    public NewUIScript wolfUI;
    public NewUIScript rabbitUI;
    public NewUIScript owlUI;

	public float corruptionFallFactor;

    private bool firstFall;
    private DialogueTrigger sff;

    //Music Manager
    public AudioSource mainMusic;
    public AudioSource corruptMusic;
    private bool corruptPlaying;
    //Must be < 1
    public float warningPercent;
	public bool demo;

    // Use this for initialization
    void Start()
    {
        CMan = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>();
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        rabbit = GameObject.Find("CreatureManager").GetComponent<RabbitPopulation>();
        owl = GameObject.Find("CreatureManager").GetComponent<OwlPopulation>();
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
            //sff.TriggerDialogue();
        }
		if (!paused && !megaPaused && !areaTimeStop)
        {
            SimpleEcologize();
        }

    }

    public void SimpleEcologize()
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

        if (rabbit.enabled)
        {
            if(rabbit.biomass > wolf.biomass)
            {
                rabbit.rising1 = true;
            }
            else if(rabbit.biomass < wolf.biomass)
            {
                rabbit.rising1 = false;
            }
            if(rabbit.biomass > wolf.biomass + overShootValue)
            {
                wolf.rising2 = true;
            }
            else if(rabbit.biomass < wolf.biomass - overShootValue)
            {
                wolf.rising2 = false;
            }
            if (owl.enabled)
            {
                if (rabbit.biomass > owl.biomass + overShootValue)
                {
                    owl.rising1 = true;
                    rabbit.rising2 = true;
                }
                else if (rabbit.biomass < owl.biomass - overShootValue)
                {
                    owl.rising1 = false;
                    rabbit.rising2 = false;
                }
            }
        }


        //Below is the section that changes the populations according to what rising triggers are set

        //Because deer are the only creatures with 2 rising triggers (their pop changes both from the shrub pop and the wolf pop) they have an extra step
        //In order to have a way of telling what the net change to deer pop is every tick, the change from the bools has to be added together into
        //the float deer.rateOfChange, and then that float is applies to the actual population.
		shrub.rateOfChange = 0;
		deer.rateOfChange = 0;
		wolf.rateOfChange = 0;
        rabbit.rateOfChange = 0;
        owl.rateOfChange = 0;
		shrubUI.popChange.text = "";
		shrubUI.rightChange.text = "";
        //the format of these is:
        //population += (constant number chosen in order to keep the ecosystem balanced by default + modifier that is the result of buff * .2f to weaken the impact
        // of the buffs.
        //One of these is also multiplied by 2 at the end, I don't remember why specifically, but I think it's part of keeping things balanced.
		if (shrub.rising1 == true) {
			if (shrub.biomass < 99) {
				if (tempShrubCapBool == false || (shrub.biomass < tempShrubCap)) {
					shrubPop = (2 + shrub.up1) * overallSpeed * Time.deltaTime;
					shrubUI.popChange.text = (2 + shrub.up1).ToString();
					shrubUI.rightChange.text = (2 + shrub.up1).ToString();
					shrub.rightChange = (2 + shrub.up1);
					shrub.rateOfChange += shrubPop;
				}
			}
		}
        else
        {
            shrubPop = (2 + shrub.down1) * overallSpeed * Time.deltaTime;
			shrubUI.popChange.text = (-2 - shrub.down1).ToString();
			shrubUI.rightChange.text = (-2 - shrub.down1).ToString();
			shrub.rightChange = (-2 - shrub.down1);
			shrub.rateOfChange -= shrubPop;
        }
		shrub.simpleRateOfChange = shrub.rightChange;
		shrub.pop += shrub.rateOfChange;
		if (shrub.rateOfChange < 0)
		{
			shrub.corruptedPop += shrub.rateOfChange * corruptionFallFactor;
			if (shrub.corruptedPop < 0)
				shrub.corruptedPop = 0;
		}

        if(deer.enabled == true)
        {
			float simpleDeerRate = 0;
			if (deer.rising1 == true && deer.pop < 100)
            {
                deer.rateOfChange += (2 + deer.up1) * overallSpeed * Time.deltaTime;
				deerUI.leftChange.text = (2 + deer.up1).ToString();
				deer.leftChange = (2 + deer.up1);
                deerPop = deer.rateOfChange;
				simpleDeerRate += (2 + deer.up1);
            }
            else
            {
				if (wolf.enabled == false) {
					deer.rateOfChange -= (7 + deer.down2) * overallSpeed * Time.deltaTime;
					deerUI.leftChange.text = (-(7 + deer.down2)).ToString();
					deer.leftChange = (-(7 + deer.down2));
                    deerPop = deer.rateOfChange;
					simpleDeerRate += -(7 + deer.down2);
				} else {
					deer.rateOfChange -= (3 + deer.down2) * overallSpeed * Time.deltaTime;
					deerUI.leftChange.text = (-(3 + deer.down2)).ToString();
					deer.leftChange = (-(3 + deer.down2));
                    deerPop = deer.rateOfChange;
					simpleDeerRate += -(3 + deer.down2);
                }
				
            }
			if (deer.rising2 == true && deer.pop < 100)
            {
                deer.rateOfChange += (2 + deer.up2) * overallSpeed * Time.deltaTime;
				deerUI.rightChange.text = (2 + deer.up2).ToString();
				deer.rightChange = (2 + deer.up2);
                deerPop2 = deer.rateOfChange;
				simpleDeerRate += (2 + deer.up2);
            }
            else
            {
                deer.rateOfChange -= (1 + deer.down2) * overallSpeed * Time.deltaTime;

				deerUI.rightChange.text = ((1 + deer.down2)).ToString();
				deer.rightChange = ((1 + deer.down2));
                deerPop2 = deer.rateOfChange;
				simpleDeerRate += (1 + deer.down2);
            }

			deerUI.popChange.text = simpleDeerRate.ToString();
			deer.simpleRateOfChange = simpleDeerRate;
            deer.pop += deer.rateOfChange;
            if (deer.rateOfChange < 0)
            {
				deer.corruptedPop += deer.rateOfChange * corruptionFallFactor;
				if (deer.corruptedPop < 0)
					deer.corruptedPop = 0;
            }
        }
        
        if(wolf.enabled == true)
        {
			float simpleWolfRate = 0;
			if (wolf.rising1 == true && wolf.pop < 100)
            {
                wolfPop = (2 + wolf.up1) * overallSpeed * Time.deltaTime;
				wolfUI.leftChange.text = (2 + wolf.up1).ToString();
				wolf.leftChange = (2 + wolf.up1);
				wolf.rateOfChange += wolfPop;
				simpleWolfRate += (2 + wolf.up1);
            }
            else
            {
                wolfPop = (3 + wolf.down1) * overallSpeed * Time.deltaTime;
				wolfUI.leftChange.text = (-3 - wolfDown).ToString();
				wolf.leftChange = (-3 - wolfDown);
				wolf.rateOfChange -= wolfPop;
				simpleWolfRate += (-3 - wolfDown);
            }
			
            if (rabbit.enabled)
            {
				float simpleRabbitRate = 0;
                if (rabbit.rising1 && rabbit.pop < 100)
                {
                    rabbitPop = (1 + rabbit.up1) * overallSpeed * Time.deltaTime;
					rabbitUI.leftChange.text = (1 + rabbit.up1).ToString();
					rabbit.leftChange = (1 + rabbit.up1);
                    rabbit.rateOfChange += rabbitPop;
					simpleRabbitRate += (1 + rabbit.up1);
                }
                else
                {
                    rabbitPop = (1 + rabbit.down1) * overallSpeed * Time.deltaTime;
					rabbitUI.leftChange.text = (-1 - rabbit.down1).ToString();
					rabbit.leftChange = (-1 - rabbit.down1);
                    rabbit.rateOfChange -= rabbitPop;
					simpleRabbitRate += (-1 - rabbit.down1);
                }

                if (wolf.rising2 && wolf.pop < 100)
                {
                    wolfPop2 = (3 + wolf.up2) * overallSpeed * Time.deltaTime;
					wolfUI.rightChange.text = (3 + wolf.up2).ToString();
					wolf.rightChange = (3 + wolf.up2);
                    wolf.rateOfChange += wolfPop2;
					simpleWolfRate += (3 + wolf.up2);
                }
                else
                {
                    wolfPop2 = (2 + wolf.down2) * overallSpeed * Time.deltaTime;
					wolfUI.rightChange.text = (-2 - wolf.down2).ToString();
					wolf.rightChange = (-2 - wolf.down2);
                    wolf.rateOfChange -= wolfPop2;
					simpleWolfRate += (-2 - wolf.down2);
                }

                if (owl.enabled)
                {
					float simpleOwlRate = 0;
                    if(owl.rising1 && owl.pop < 100)
                    {
                        owlPop = (3 + owl.up1) * overallSpeed * Time.deltaTime;
						owlUI.leftChange.text = (3 + owl.up1).ToString();
						owl.leftChange = (3 + owl.up1);
                        owl.rateOfChange += owlPop;
						simpleOwlRate += (3 + owl.up1);
                    }
                    else
                    {
                        owlPop = (3 + owl.down1) * overallSpeed * Time.deltaTime;
						owlUI.leftChange.text = (-3 - owl.down1).ToString();
						owl.leftChange = (-3 - owl.down1);
                        owl.rateOfChange -= owlPop;
						simpleOwlRate += (-3 - owl.down1);
                    }
					owlUI.popChange.text = simpleOwlRate.ToString();
					owl.simpleRateOfChange = simpleOwlRate;
                    owl.pop += owl.rateOfChange;
                    if (owl.rateOfChange < 0)
                    {
                        owl.corruptedPop += owl.rateOfChange * corruptionFallFactor;
						if (owl.corruptedPop < 0)
							owl.corruptedPop = 0;
                    }

                    if (rabbit.rising2 && rabbit.pop < 100)
                    {
                        rabbitPop2 = (1 + rabbit.up2) * overallSpeed * Time.deltaTime;
						rabbitUI.rightChange.text = (1 + rabbit.up2).ToString();
						rabbit.rightChange = (1 + rabbit.up2);
                        rabbit.rateOfChange += rabbitPop2;
						simpleRabbitRate += (1 + rabbit.up2);
                    }
                    else
                    {
                        rabbitPop2 = (1 + rabbit.down2) * overallSpeed * Time.deltaTime;
						rabbitUI.rightChange.text = (-1 - rabbit.down2).ToString();
						rabbit.rightChange = (-1 - rabbit.down2);
                        rabbit.rateOfChange -= rabbitPop2;
						simpleRabbitRate += (-1 - rabbit.down2);
                    }
                }

				rabbitUI.popChange.text = simpleRabbitRate.ToString();
				rabbit.simpleRateOfChange = simpleRabbitRate;
                rabbit.pop += rabbit.rateOfChange;
                if (rabbit.rateOfChange < 0)
                {
                    rabbit.corruptedPop += rabbit.rateOfChange * corruptionFallFactor;
					if (rabbit.corruptedPop < 0)
						rabbit.corruptedPop = 0;
                }
            }
			wolfUI.popChange.text = simpleWolfRate.ToString();
			wolf.simpleRateOfChange = simpleWolfRate;
            wolf.pop += wolf.rateOfChange;
            if (wolf.rateOfChange < 0)
            {
                wolf.corruptedPop += wolf.rateOfChange * corruptionFallFactor;
				if (wolf.corruptedPop < 0)
					wolf.corruptedPop = 0;
            }
        }

		if (shrub.rightChange >= 0) {
			shrubUI.popChange.text = "+" + shrubUI.rightChange.text;
			shrubUI.rightChange.text = "+" + shrubUI.rightChange.text;
		}

		if (deer.leftChange >= 0)
			deerUI.leftChange.text = "+" + deerUI.leftChange.text;
		if (deer.rightChange >= 0)
			deerUI.rightChange.text = "+" + deerUI.rightChange.text;
		if (deer.simpleRateOfChange >= 0)
			deerUI.popChange.text = "+" + deer.simpleRateOfChange.ToString();
		
		if (wolf.leftChange >= 0)
			wolfUI.leftChange.text = "+" + wolfUI.leftChange.text;
		if (wolf.rightChange >= 0)
			wolfUI.rightChange.text = "+" + wolfUI.rightChange.text;
		if (wolf.simpleRateOfChange >= 0)
			wolfUI.popChange.text = "+" + wolf.simpleRateOfChange.ToString();
		
		if (rabbit.leftChange >= 0)
			rabbitUI.leftChange.text = "+" + rabbitUI.leftChange.text;
		if (rabbit.rightChange >= 0)
			rabbitUI.rightChange.text = "+" + rabbitUI.rightChange.text;
		if (rabbit.simpleRateOfChange >= 0)
			rabbitUI.popChange.text = "+" + rabbit.simpleRateOfChange.ToString();
		
		if (owl.leftChange >= 0) {
			owlUI.popChange.text = "+" + owlUI.leftChange.text;
			owlUI.leftChange.text = "+" + owlUI.leftChange.text;
		}

		if (shrub.pop >= 99) {
			shrubUI.popChange.text = "Max";
		}
		if (deer.pop >= 100) {
			deerUI.popChange.text = "Max";
		}
		if (wolf.pop >= 100) {
			wolfUI.popChange.text = "Max";
		}
		if (rabbit.pop >= 100) {
			rabbitUI.popChange.text = "Max";
		}
		if (owl.pop >= 100) {
			owlUI.popChange.text = "Max";
		}

        

        //populations find out if they have anough food based on their biomass and their foods biomass.
        shrub.biomass = shrub.pop;
        deer.biomass = deer.pop;
        wolf.biomass = wolf.pop;
        rabbit.biomass = rabbit.pop;
        owl.biomass = owl.pop;
        shrub.corruptedBiomass = shrub.corruptedPop;
        deer.corruptedBiomass = deer.corruptedPop;
        wolf.corruptedBiomass = wolf.corruptedPop;
        rabbit.corruptedBiomass = rabbit.corruptedPop;
        owl.corruptedBiomass = owl.corruptedPop;

    }

	static string simplifyNumber(float incomingNum) {
		if (incomingNum >= .2f && incomingNum < .6) {
			return "+1";
		} else if (incomingNum >= .6 && incomingNum < 1.1f) {
			return "+2";
		} else if (incomingNum >= 1.1f && incomingNum < 1.4f) {
			return "+3";
		} else if (incomingNum >= 1.4f) {
			return "+4";
		}

		if (incomingNum <= -.2f && incomingNum > -.6) {
			return "-1";
		} else if (incomingNum <= -.6 && incomingNum > -1.1f) {
			return "-2";
		} else if (incomingNum <= -1.1f && incomingNum > -1.4f) {
			return "-3";
		} else if (incomingNum <= -1.4f) {
			return "-4";
		}
		return "0";
			
	}
}
