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
        pred1 = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        creatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().shrubCreatureList;
        corruptedCreatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptedShrubCreatureList;
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
    }

    // Update is called once per frame
    void Update()
    {
		//without this if statement corruption continues to spread while paused
		//I'm seeing a lot of little issues like this that make me think the 
		//corrupt function should just be moved back into simpleEcologyMaster -Jay
		if (!eco.areaTimeStop && !eco.megaPaused)
        DoUpdate();
    }
}
