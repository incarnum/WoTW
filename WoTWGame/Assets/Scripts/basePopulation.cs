using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basePopulation : MonoBehaviour {
    public float up1, up2, down1, down2, pop, biomass, corruptedPop, corruptedBiomass, speed, size, startSize, startSpeed, rate, startRate, corruptionAcceleration, overallSpeed;
    public int sizeMod, speedMod, toughMod, timesSpeedChanged;
    public bool rising1, rising2, corrupting;
    public GameObject gameOver;
    public GameObject corrGameOver;
    public GameObject mainMenuButton;
    public GameObject exitGameButton;
    public GameObject menuCamera;
    public GameObject bar, corrBar, barUI, corrBarUI;
	// Use this for initialization
	void Start () {
		
	}

    public void DoStart()
    {
        overallSpeed = 0.25f;
    }
	
	// Update is called once per frame
	public void DoUpdate () {
        CheckForFailure();
        UpdateBars();
        Corrupt();
	}

    public void UpdateBars()
    {
        bar.GetComponent<barScript>().SetFillSizeValue(biomass / 100f);
        corrBar.GetComponent<barScript>().SetFillSizeValue(corruptedBiomass / 100f);
        if (barUI.GetComponent<UIBarScript>().gameObject.activeSelf)
        {
            barUI.GetComponent<UIBarScript>().SetFillSizeValue(biomass / 100f);
            corrBarUI.GetComponent<UIBarScript>().SetFillSizeValue(corruptedBiomass / 100f);
        }
    }
    void Corrupt()
    {
        if(corruptedPop <= 0)
        {
            corrupting = false;
        }
        float corruptionRate = GameObject.Find("CreatureManager").GetComponent<CreatureManagerScript>().corruptionNodeList.Count * corruptionAcceleration;
        if(corrupting && rising1)
        {
            corruptedPop += corruptionRate * overallSpeed * Time.deltaTime * rate;
        }
        if (corrupting && rising2)
        {
            corruptedPop += corruptionRate * overallSpeed * Time.deltaTime * rate;
        }
    }
    void CheckForFailure()
    {
        if(pop <= 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
        }
        if (pop <= corruptedPop)
        {
            Time.timeScale = 0;
            corrGameOver.SetActive(true);
            mainMenuButton.SetActive(true);
            exitGameButton.SetActive(true);
        }
;    }
}
