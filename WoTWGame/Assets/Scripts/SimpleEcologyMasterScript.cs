using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEcologyMasterScript : MonoBehaviour
{

    public bool paused;
    public bool megaPaused;

    public float shrubPop;
    public float deerPop;
    public float wolfPop;
    public float shrubBiomass;
    public float deerBiomass;
    public float wolfBiomass;
    public float corruptedShrubPop;
    public float corruptedDeerPop;
    public float corruptedWolfPop;
    public float corruptedShrubBiomass;
    public float corruptedDeerBiomass;
    public float corruptedWolfBiomass;
    public float deerSpeed;
    public float wolfSpeed;

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

    public float hunter;
    public float forager;
    public float growth;
    public float heartiness;
    public float energy;

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
    private barScript shrubBar;
    private barScript deerBar;
    private barScript wolfBar;
    private barScript corruptedShrubBar;
    private barScript corruptedDeerBar;
    private barScript corruptedWolfBar;
    public UIBarScript shrubBarUI;
    public UIBarScript deerBarUI;
    public UIBarScript wolfBarUI;
    public UIBarScript corruptedShrubBarUI;
    public UIBarScript corruptedDeerBarUI;
    public UIBarScript corruptedWolfBarUI;
    private Animator shrubArrowsDeer;
    private Animator deerArrowsShrub;
    private Animator deerArrowsWolf;
    private Animator wolfArrowsDeer;
    private Animator corruptedShrubArrows;
    private Animator corruptedDeerArrows;
    private Animator corruptedWolfArrows;
    public Animator corruptedShrubArrowsUI;
    public Animator corruptedDeerArrowsUI;
    public Animator corruptedWolfArrowsUI;

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
    private bool firstFall;
    private DialogueTrigger sff;
    // Use this for initialization
    void Start()
    {
        CMan = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>();
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        shrubBar = GameObject.Find("ShrubBar").GetComponent<barScript>();
        deerBar = GameObject.Find("DeerBar").GetComponent<barScript>();
        wolfBar = GameObject.Find("WolfBar").GetComponent<barScript>();
        corruptedShrubBar = GameObject.Find("CorruptedShrubBar").GetComponent<barScript>();
        corruptedDeerBar = GameObject.Find("CorruptedDeerBar").GetComponent<barScript>();
        corruptedWolfBar = GameObject.Find("CorruptedWolfBar").GetComponent<barScript>();
        shrubArrowsDeer = GameObject.Find("shrubArrowsDeer").GetComponent<Animator>();
        deerArrowsShrub = GameObject.Find("deerArrowsShrub").GetComponent<Animator>();
        deerArrowsWolf = GameObject.Find("deerArrowsWolf").GetComponent<Animator>();
        wolfArrowsDeer = GameObject.Find("wolfArrowsDeer").GetComponent<Animator>();
        corruptedShrubArrows = GameObject.Find("corruptedShrubArrows").GetComponent<Animator>();
        corruptedDeerArrows = GameObject.Find("corruptedDeerArrows").GetComponent<Animator>();
        corruptedWolfArrows = GameObject.Find("corruptedWolfArrows").GetComponent<Animator>();
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
    }

    // Update is called once per frame
    void Update()
    {
        if (shrub.pop < deer.pop && firstFall)
        {
            firstFall = false;
            sff.TriggerDialogue();
        }
        if (!paused && !megaPaused)
        {
            //CheckForFailure();
            SimpleEcologize();
            /*Corrupt();
            UpdateBars();
            if (Time.time > lastAdjustment + adjustmentDelay)
            {
                lastAdjustment = Time.time;

                if (corruptedShrubPop > 0)
                {
                    CMan.shrubNum = (shrubPop - corruptedShrubPop) / ecoToWorldDivision;
                }
                else
                {
                    CMan.shrubNum = shrubPop / ecoToWorldDivision;
                }

                if (corruptedDeerPop > 0)
                {
                    CMan.deerNum = (deerPop - corruptedDeerPop) / ecoToWorldDivision;
                }
                else
                {
                    CMan.deerNum = deerPop / ecoToWorldDivision;
                }

                if (corruptedWolfPop > 0)
                {
                    CMan.wolfNum = (wolfPop - corruptedWolfPop) / ecoToWorldDivision;
                }
                else
                {
                    CMan.wolfNum = wolfPop / ecoToWorldDivision;
                }

                CMan.corruptedShrubNum = corruptedShrubPop / ecoToWorldDivision;
                CMan.corruptedDeerNum = corruptedDeerPop / ecoToWorldDivision;
                CMan.corruptedWolfNum = corruptedWolfPop / ecoToWorldDivision;
                CMan.AdjustCreatures();
                CMan.AdjustPickips();
            } */
        }

        //        // Mod updates
        //        if (shrubSize > startShrubSize)
        //        {
        //            shrubSizeMod = 1;
        //        }
        //        else if (shrubSize < startShrubSize)
        //        {
        //            shrubSizeMod = -1;
        //        }
        //        else
        //        {
        //            shrubSizeMod = 0;
        //        }
        //
        //        if (deerSize > startDeerSize)
        //        {
        //            deerSizeMod = 1;
        //        }
        //        else if (deerSize < startDeerSize)
        //        {
        //            deerSizeMod = -1;
        //        }
        //        else
        //        {
        //            deerSizeMod = 0;
        //        }
        //
        //        if (wolfSize > startWolfSize)
        //        {
        //            wolfSizeMod = 1;
        //        }
        //        else if (wolfSize < startWolfSize)
        //        {
        //            wolfSizeMod = -1;
        //        }
        //        else
        //        {
        //            wolfSizeMod = 0;
        //        }
        //
        //        if (deerSpeed > startDeerSpeed)
        //        {
        //            deerSpeedMod = 1;
        //        }
        //        else if (deerSpeed < startDeerSpeed)
        //        {
        //           deerSpeedMod = -1;
        //        }
        //        else
        //        {
        //            deerSpeedMod = 0;
        //        }
        //
        //        if (wolfSpeed > startWolfSpeed)
        //        {
        //            wolfSpeedMod = 1;
        //        }
        //        else if (deerSpeed < startDeerSpeed)
        //        {
        //            wolfSpeedMod = -1;
        //        }
        //        else
        //        {
        //            wolfSpeedMod = 0;
        //        }
    }

    /*public void UpdateBars()
    {
        shrubBar.SetFillSizeValue(shrubBiomass / 100f);
        deerBar.SetFillSizeValue(deerBiomass / 100f);
        wolfBar.SetFillSizeValue(wolfBiomass / 100f);
        corruptedShrubBar.SetFillSizeValue(corruptedShrubBiomass / 100f);
        corruptedDeerBar.SetFillSizeValue(corruptedDeerBiomass / 100f);
        corruptedWolfBar.SetFillSizeValue(corruptedWolfBiomass / 100f);

        if (shrubBarUI.gameObject.activeSelf)
        {
            shrubBarUI.SetFillSizeValue(shrubBiomass / 100f);
            deerBarUI.SetFillSizeValue(deerBiomass / 100f);
            wolfBarUI.SetFillSizeValue(wolfBiomass / 100f);
            corruptedShrubBarUI.SetFillSizeValue(corruptedShrubBiomass / 100f);
            corruptedDeerBarUI.SetFillSizeValue(corruptedDeerBiomass / 100f);
            corruptedWolfBarUI.SetFillSizeValue(corruptedWolfBiomass / 100f);
        }
    }*/

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
                deerArrowsShrub.SetTrigger(deer.up1.ToString());
            }
            if (shrub.biomass < (deer.biomass - overShootValue))
            {
                deer.rising1 = false;
                deerArrowsShrub.SetTrigger(deer.down1.ToString());
            }
            if (shrub.biomass < deer.biomass)
            {
                shrub.rising1 = false;
                shrubArrowsDeer.SetTrigger(shrub.down1.ToString());
            }
            else if (shrub.biomass > deer.biomass)
            {
                shrub.rising1 = true;
                shrubArrowsDeer.SetTrigger(shrub.up1.ToString());
            }
        }
        else
        {
            shrub.rising1 = true;

            shrubArrowsDeer.SetTrigger(shrub.up1.ToString());
        }

        if (wolf.enabled == true && deer.enabled == true)
        {
            if (deer.biomass > (wolf.biomass + overShootValue))
            {
                wolf.rising1 = true;
                wolfArrowsDeer.SetTrigger(wolf.up1.ToString());
            }
            if (deer.biomass < (wolf.biomass - overShootValue))
            {
                wolf.rising1 = false;
                wolfArrowsDeer.SetTrigger(wolf.down1.ToString());
            }
            if (deer.biomass < wolf.biomass)
            {
                deer.rising2 = false;
                deerArrowsWolf.SetTrigger(deer.down2.ToString());
            }
            else if (deer.biomass > wolf.biomass)
            {
                deer.rising2 = true;
                deerArrowsWolf.SetTrigger(deer.up2.ToString());
            }
        }
        else if (deer.enabled == true)
        {
            deer.rising2 = true;

            deerArrowsWolf.SetTrigger(deer.up2.ToString());
        }
        


        //Below is the section that changes the populations according to what rising triggers are set

        //Because deer are the only creatures with 2 rising triggers (their pop changes both from the shrub pop and the wolf pop) they have an extra step
        //In order to have a way of telling what the net change to deer pop is every tick, the change from the bools has to be added together into
        //the float rateOfDeerChange, and then that float is applies to the actual population.
        rateOfDeerChange = 0;

        //the format of these is:
        //population += (constant number chosen in order to keep the ecosystem balanced by default + modifier that is the result of buff * .2f to weaken the impact
        // of the buffs.
        //One of these is also multiplied by 2 at the end, I don't remember why specifically, but I think it's part of keeping things balanced.
        if (shrub.rising1 == true)
        {
            shrub.pop += (2 + shrub.up1 * .2f) * overallSpeed * Time.deltaTime;
            //shrub.corruptedPop += (2 + shrub.up1 * .2f) * overallSpeed * Time.deltaTime * (shrub.corruptedPop / shrub.pop);
        }
        else
        {
            shrub.pop -= (2 + shrub.down1 * .2f) * overallSpeed * Time.deltaTime;
            shrub.corruptedPop -= (2 + shrub.down1 * .2f) * overallSpeed * Time.deltaTime;
        }

        if(deer.enabled == true)
        {
            if (deer.rising1 == true)
            {
                rateOfDeerChange += (2 + deer.up1 * .2f) * overallSpeed * Time.deltaTime;
            }
            else
            {
                rateOfDeerChange -= (3 + deer.down2 * .2f) * overallSpeed * Time.deltaTime;
            }
            if (deer.rising2 == true)
            {
                rateOfDeerChange += (2 + deer.up2 * .2f) * overallSpeed * Time.deltaTime;
            }
            else
            {
                rateOfDeerChange -= (1 + deer.down2 * .2f) * overallSpeed * Time.deltaTime * 2;
            }
            deer.pop += rateOfDeerChange;
            if (rateOfDeerChange < 0)
            {
                deer.corruptedPop += rateOfDeerChange;
            }
        }
        
        if(wolf.enabled == true)
        {
            if (wolf.rising1 == true)
            {
                wolf.pop += (2 + wolf.up1 * .2f) * overallSpeed * Time.deltaTime;
                //wolf.corruptedPop += (1.9f + wolf.up1 * .2f) * overallSpeed * Time.deltaTime * (wolf.corruptedPop / wolf.pop);
            }
            else
            {
                wolf.pop -= (3 + wolf.down1 * .2f) * overallSpeed * Time.deltaTime;
                wolf.corruptedPop -= (3 + wolf.down1 * .2f) * overallSpeed * Time.deltaTime;
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

    /*void Corrupt()
    {
        //this section increases corruption on corrupted populations
        corruptionRate = CMan.corruptionNodeList.Count * corruptionAcceleration;

        if (corruptedShrubPop <= 0)
        {
            corruptingShrubs = false;
            //			corruptedShrubArrows.SetTrigger ("0");
            //			corruptedShrubArrowsUI.SetTrigger ("0");
        }
        if (corruptedDeerPop <= 0)
        {
            corruptingDeer = false;
            corruptedDeerArrows.SetTrigger("0");
        }
        if (corruptedWolfPop <= 0)
        {
            corruptingWolves = false;
            corruptedWolfArrows.SetTrigger("0");
        }
        //section below is just setting animations for arrows that aren't currently implemented
        if (corruptingShrubs == true && shrubRising == true)
        {
            corruptedShrubPop += corruptionRate * overallSpeed * Time.deltaTime * shrubRate;
            if (corruptionRate < 1)
            {
                corruptedShrubArrows.SetTrigger("1");
                //				corruptedShrubArrowsUI.SetTrigger ("1");
            }
            else if (corruptionRate >= 1 && corruptionRate < 2)
            {
                corruptedShrubArrows.SetTrigger("2");
                //				corruptedShrubArrowsUI.SetTrigger ("2");
            }
            else if (corruptionRate >= 2 && corruptionRate < 3)
            {
                corruptedShrubArrows.SetTrigger("3");
                //				corruptedShrubArrowsUI.SetTrigger ("3");
            }
            else if (corruptionRate >= 3 && corruptionRate < 4)
            {
                corruptedShrubArrows.SetTrigger("4");
                //				corruptedShrubArrowsUI.SetTrigger ("4");
            }
            else if (corruptionRate >= 4 && corruptionRate < 5)
            {
                corruptedShrubArrows.SetTrigger("5");
                //				corruptedShrubArrowsUI.SetTrigger ("5");
            }
        }
        if (corruptingDeer == true)
        {
            if (rateOfDeerChange >= 0f)
                corruptedDeerPop += corruptionRate * overallSpeed * Time.deltaTime * deerRate;
            if (corruptionRate < 1)
            {
                corruptedDeerArrows.SetTrigger("1");
            }
            else if (corruptionRate >= 1 && corruptionRate < 2)
            {
                corruptedDeerArrows.SetTrigger("2");
            }
            else if (corruptionRate >= 2 && corruptionRate < 3)
            {
                corruptedDeerArrows.SetTrigger("3");
            }
            else if (corruptionRate >= 3 && corruptionRate < 4)
            {
                corruptedDeerArrows.SetTrigger("4");
            }
            else if (corruptionRate >= 4 && corruptionRate < 5)
            {
                corruptedDeerArrows.SetTrigger("5");
            }
        }
        if (corruptingWolves == true && wolfRising == true)
        {
            corruptedWolfPop += corruptionRate * overallSpeed * Time.deltaTime * wolfRate;
            if (corruptionRate < 1)
            {
                corruptedWolfArrows.SetTrigger("1");
            }
            else if (corruptionRate >= 1 && corruptionRate < 2)
            {
                corruptedWolfArrows.SetTrigger("2");
            }
            else if (corruptionRate >= 2 && corruptionRate < 3)
            {
                corruptedWolfArrows.SetTrigger("3");
            }
            else if (corruptionRate >= 3 && corruptionRate < 4)
            {
                corruptedWolfArrows.SetTrigger("4");
            }
            else if (corruptionRate >= 4 && corruptionRate < 5)
            {
                corruptedWolfArrows.SetTrigger("5");
            }
        }

    }

    void CheckForFailure()
    {
        //see if the player has lost
        if (shrubPop <= 0)
        {
            Time.timeScale = 0;
            gameOver1.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
        else if (deerPop <= 0)
        {
            Time.timeScale = 0;
            gameOver2.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
        else if (wolfPop <= 0)
        {
            Time.timeScale = 0;
            gameOver3.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }

        if (shrubPop <= corruptedShrubPop)
        {
            Time.timeScale = 0;
            gameOver4.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
        else if (deerPop <= corruptedDeerPop)
        {
            Time.timeScale = 0;
            gameOver5.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
        else if (wolfPop <= corruptedWolfPop)
        {
            Time.timeScale = 0;
            gameOver6.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
        else if (CMan.corruptionNodeList.Count == 0)
        {
            Time.timeScale = 0;
            victory.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
    } */
}
