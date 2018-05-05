using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubPopulation : basePopulation
{

    // Use this for initialization
    void Start()
    {
        DoStart();
        notShrubs = false;
		if (popNumberOverride == false) {
			pop = startPop;
			size = 1;
			startSize = 1;
			sizeMod = 0;
			speedMod = 0;
			toughMod = 0;
			up1 = 0;
			up2 = 0;
			down1 = 0;
			down2 = 0;
		}
        pred1 = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        creatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().shrubCreatureList;
        corruptedCreatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptedShrubCreatureList;
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ();
		DoUpdate ();
		biomass = pop;
    }

    // Update is called once per frame
    void Update()
    {
		if (!eco.areaTimeStop && !eco.megaPaused)
        DoUpdate();
    }
}
