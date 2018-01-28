using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPopulation : basePopulation {

	// Use this for initialization
	void Start () {
        DoStart();
		pop = GetComponent<DeerPopulation>().pop - 10;
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
        food1 = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        creatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().wolfCreatureList;
        corruptedCreatureList = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptedWolfCreatureList;
    }

    // Update is called once per frame
    void Update()
    {
        DoUpdate();
    }
}
