using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basePopulation : MonoBehaviour {
    public float up1, up2, down1, down2, pop, biomass, startPop, corruptedPop, corruptedBiomass, speed, size, startSize, startSpeed, rate, startRate, corruptionAcceleration, corruptionRate, overallSpeed;
    public int sizeMod, speedMod, toughMod, timesSpeedChanged;
    public bool rising1, rising2, corrupting, notShrubs;
	public float rootSpeed;
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
	public float simpleRateOfChange;
	public float leftChange;
	public float rightChange;
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
		float tempSizeNum = (sizeMod + 4f) / 3f;

        foreach (GameObject garfield in creatureList)
        {
			garfield.transform.localScale = new Vector3(tempSizeNum, tempSizeNum);
        }
        foreach (GameObject garfield in corruptedCreatureList)
        {
			if (garfield != null)
			garfield.transform.localScale = new Vector3(tempSizeNum, tempSizeNum);
        }
    }

    public void UpdateSpeed()
    {
		float tempSpeedNum = ((speedMod + 4f) / 3f) * speed;
		print ("updating speed to " + tempSpeedNum);
        foreach (GameObject garfield in creatureList)
        {
            if (notShrubs)
            {
				if (garfield != null)
				garfield.GetComponent<AnimalMovementScript>().speed2 = tempSpeedNum;
            }
        }
    }

	public void UpdateUpDown()
	{
		up1 = speedMod;
		up2 = speedMod;
		down1 = -toughMod;
		down2 = -toughMod;
		if (pred1 != null) {
			down1 -= pred1.speedMod;
		}
		if (pred2 != null) {
			down2 -= pred2.speedMod;
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
			if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused == false) {
				GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
				GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused = true;
				GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
				gameOverScreen.SetActive (true);
				gameOver.SetActive (true);
				darkenScreen.SetActive (true);
			}
//			GameObject.Find ("MultiMenu").SetActive (false);
        } else if (pop <= corruptedPop)
        {
			if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused == false) {
				GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().Pause ();
				GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused = true;
				GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
				gameOverScreen.SetActive (true);
	            corrGameOver.SetActive(true);
				enpurpleScreen.SetActive (true);
	//			GameObject.Find ("MultiMenu").SetActive (false)
			};
        }
;    }
}
