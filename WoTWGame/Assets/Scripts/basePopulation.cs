﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basePopulation : MonoBehaviour {
    public float up1, up2, down1, down2, pop, biomass, startPop, corruptedPop, corruptedBiomass, speed, size, startSize, startSpeed, rate, startRate, corruptionAcceleration, corruptionRate, overallSpeed;
    public int sizeMod, speedMod, toughMod, timesSpeedChanged;
    public bool rising1, rising2, corrupting, notShrubs;
	public GameObject gameOverScreen;
	public GameObject gameOver;
	public GameObject darkenScreen;
	public GameObject enpurpleScreen;
    public GameObject corrGameOver;
    public GameObject menuCamera;
    public GameObject bar, corrBar, barUI, corrBarUI;
    public List<GameObject> creatureList;
    public List<GameObject> corruptedCreatureList;
    public basePopulation food1, food2, pred1, pred2;
	public SimpleEcologyMasterScript eco;
	public corruptionManagerScript cm;
    public float rateOfChange;
    // Use this for initialization
    void Start () {
		
	}

    public void DoStart()
    {
        overallSpeed = 0.25f;
        corruptionAcceleration = 2;
        rate = 0.15f;
        startRate = rate;
        notShrubs = true;
    }
	
	// Update is called once per frame
	public void DoUpdate () {
        CheckForFailure();
        UpdateBars();
        Corrupt();
	}

    public void UpdateBars()
    {
		// commented the two lines below out because as far as I can tell they just referred to old bars -jay
//        bar.GetComponent<barScript>().SetFillSizeValue(biomass / 100f);
//        corrBar.GetComponent<barScript>().SetFillSizeValue(corruptedBiomass / 100f);


//        if (barUI.GetComponent<UIBarScript>().gameObject.activeSelf)
//        {
//            barUI.GetComponent<UIBarScript>().SetFillSizeValue(biomass / 100f);
//            corrBarUI.GetComponent<UIBarScript>().SetFillSizeValue(corruptedBiomass / 100f);
//        }
    }

    public void UpdateSize()
    {
        foreach (GameObject garfield in creatureList)
        {
            garfield.transform.localScale = new Vector3(size, size);
        }
        foreach (GameObject garfield in corruptedCreatureList)
        {
            garfield.transform.localScale = new Vector3(size, size);
        }
    }

    public void UpdateSpeed()
    {
        foreach (GameObject garfield in creatureList)
        {
            if (notShrubs)
            {
            garfield.GetComponent<AnimalMovementScript>().speed2 = speed;
            }
        }
    }

    void Corrupt()
    {
        if(corruptedPop <= 0)
        {
			if (corrupting == true) {
				cm.CheckStartingTimer ();
			}
            corrupting = false;
        }
		corruptionRate = cm.currentCorruptionRate;
        if(corrupting && rateOfChange > 0)
        {
            corruptedPop += corruptionRate * overallSpeed * Time.deltaTime * 4;
        }
    }

    void CheckForFailure()
    {
        if(pop <= 0)
        {
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused = true;
			gameOverScreen.SetActive (true);
            gameOver.SetActive(true);
			darkenScreen.SetActive (true);
//			GameObject.Find ("MultiMenu").SetActive (false);
        } else if (pop <= corruptedPop)
        {
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused = true;
			gameOverScreen.SetActive (true);
            corrGameOver.SetActive(true);
			enpurpleScreen.SetActive (true);
//			GameObject.Find ("MultiMenu").SetActive (false);
        }
;    }
}
