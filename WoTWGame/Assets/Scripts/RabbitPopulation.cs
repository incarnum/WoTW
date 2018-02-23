using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitPopulation : basePopulation {

	// Use this for initialization
	void Start () {
        DoStart();
        pop = GetComponent<ShrubPopulation>().pop;
        size = 1;
        startSize = 1;
        sizeMod = 0;
        speedMod = 0;
        toughMod = 0;
        up1 = 0;
        up2 = 0;
        down1 = 0;
        down2 = 0;
        pred1 = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        pred2 = GameObject.Find("CreatureManager").GetComponent<OwlPopulation>();
        creatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().rabbitCreatureList;
        corruptedCreatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptedRabbitCreatureList;
        eco = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!eco.areaTimeStop && !eco.megaPaused)
        {
            DoUpdate();
        }
            
    }
}
