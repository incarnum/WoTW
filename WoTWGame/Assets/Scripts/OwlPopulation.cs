using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlPopulation : basePopulation {

	// Use this for initialization
	void Start () {
        DoStart();
        pop = GetComponent<WolfPopulation>().pop;
        size = 1;
        startSize = 1;
        speed = 4;
        startSpeed = 4;
        timesSpeedChanged = 0;
        sizeMod = 0;
        speedMod = 0;
        toughMod = 0;
        up1 = 0;
        up2 = 0;
        down1 = 0;
        down2 = 0;
        food1 = GameObject.Find("CreatureManager").GetComponent<RabbitPopulation>();
        creatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().owlCreatureList;
        corruptedCreatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptedOwlCreatureList;
        eco = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
        DoUpdate();
        biomass = pop;
    }
	
	// Update is called once per frame
	void Update () {
        if (!eco.areaTimeStop && !eco.megaPaused)
        {
            DoUpdate();
        }
    }
}
