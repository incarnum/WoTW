using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerPopulation : basePopulation {

	// Use this for initialization
	void Start ()
    {
        DoStart();
		pop = GetComponent<ShrubPopulation>().biomass - 7;
        size = 1;
        startSize = 1;
        speed = 2;
        startSpeed = 2;
        timesSpeedChanged = 0;
        sizeMod = 0;
        speedMod = 0;
        toughMod = 0;
        up1 = 0;
        up2 = 0;
        down1 = 0;
        down2 = 0;
        food1 = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        pred1 = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        creatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().deerCreatureList;
        corruptedCreatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptedDeerCreatureList;
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ();
		DoUpdate();
		biomass = pop;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (!eco.areaTimeStop && !eco.megaPaused)
        DoUpdate();
	}
}
