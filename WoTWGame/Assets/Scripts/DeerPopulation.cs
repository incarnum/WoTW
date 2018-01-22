using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerPopulation : basePopulation {

	// Use this for initialization
	void Start () {
        pop = 50;
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
