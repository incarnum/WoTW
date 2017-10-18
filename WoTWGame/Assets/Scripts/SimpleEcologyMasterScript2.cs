﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEcologyMasterScript2 : MonoBehaviour {

	public bool paused;
	public bool megaPaused;

	public float shrubPop;
	public float deerPop;
	public float wolfPop;
	public float shrubBiomass;
	public float deerBiomass;
	public float wolfBiomass;
	public float corruptedShrubPop;
	public float corruptedDeerPop;
	public float corruptedWolfPop;
	public float corruptedShrubBiomass;
	public float corruptedDeerBiomass;
	public float corruptedWolfBiomass;
	public float deerSpeed;
	public float wolfSpeed;

	public float shrubSize;
	public float deerSize;
	public float wolfSize;

    //Mods: 0 = default, 1 = positve effect, -1 = negitive effect, should always start on 0, range is -3/+3
    public float startShrubSize;
    public float startDeerSize;
    public float startWolfSize;
    public float startDeerSpeed;
    public float startWolfSpeed;
    public int shrubSizeMod = 0;
    public int deerSizeMod = 0;
    public int wolfSizeMod = 0;
	public int shrubSpeedMod = 0;
    public int deerSpeedMod = 0;
    public int wolfSpeedMod = 0;
    public int shrubToughMod = 0;
    public int deerToughMod = 0;
    public int wolfToughMod = 0;
    public int timesDeerSpeedChanged = 0;
    public int timesWolfSpeedChanged = 0;


    public float ecoToWorldDivision;

	public bool shrubRising;
	public bool deerRising;
	public bool wolfRising;
	public bool deerRising2;
	public bool cShrubRising;
	public bool cDeerRising;
	public bool cWolfRising;
	public bool shrubFallingC;
	public bool deerFallingC;
	public bool wolfFallingC;

	public float hunter;
	public float forager;
	public float growth;
	public float heartiness;
	public float energy;

	public float shrubUp;
	public float shrubDown;
	public float deerUp1;
	public float deerDown1;
	public float wolfUp;
	public float wolfDown;
	public float deerUp2;
	public float deerDown2;

	public float corruptionRate;
	public float corruptionAcceleration;
	public bool corruptingShrubs;
	public bool corruptingDeer;
	public bool corruptingWolves;

	public float overallSpeed;
	public float overShootValue;
//	public float shrubOvershoot;
//	public float deerOvershoot;
//	public float wolfOvershoot;

	private float lastAdjustment;
	public float adjustmentDelay;

	private CreatureManagerScript CMan;
	private barScript shrubBar;
	private barScript deerBar;
	private barScript wolfBar;
	private barScript corruptedShrubBar;
	private barScript corruptedDeerBar;
	private barScript corruptedWolfBar;
	public UIBarScript shrubBarUI;
	public UIBarScript deerBarUI;
	public UIBarScript wolfBarUI;
	public UIBarScript corruptedShrubBarUI;
	public UIBarScript corruptedDeerBarUI;
	public UIBarScript corruptedWolfBarUI;
	private Animator shrubArrowsDeer;
	private Animator deerArrowsShrub;
	private Animator deerArrowsWolf;
	private Animator wolfArrowsDeer;
	private Animator corruptedShrubArrows;
	private Animator corruptedDeerArrows;
	private Animator corruptedWolfArrows;
	public Animator corruptedShrubArrowsUI;
	public Animator corruptedDeerArrowsUI;
	public Animator corruptedWolfArrowsUI;

	private float rateOfShrubChange;
	private float rateOfDeerChange;
	private float rateOfWolfChange;

	public GameObject gameOver1;
	public GameObject gameOver2;
	public GameObject gameOver3;
	public GameObject gameOver4;
	public GameObject gameOver5;
	public GameObject gameOver6;
    public GameObject mainMenuButton;
    public GameObject victory;
    public GameObject menuCamera;
	// Use this for initialization
	void Start () {
		CMan = GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript>();
		shrubBar = GameObject.Find ("ShrubBar").GetComponent<barScript> ();
		deerBar = GameObject.Find ("DeerBar").GetComponent<barScript> ();
		wolfBar = GameObject.Find ("WolfBar").GetComponent<barScript> ();
		corruptedShrubBar = GameObject.Find ("CorruptedShrubBar").GetComponent<barScript> ();
		corruptedDeerBar = GameObject.Find ("CorruptedDeerBar").GetComponent<barScript> ();
		corruptedWolfBar = GameObject.Find ("CorruptedWolfBar").GetComponent<barScript> ();
		shrubArrowsDeer = GameObject.Find ("shrubArrowsDeer").GetComponent<Animator> ();
		deerArrowsShrub = GameObject.Find ("deerArrowsShrub").GetComponent<Animator> ();
		deerArrowsWolf = GameObject.Find ("deerArrowsWolf").GetComponent<Animator> ();
		wolfArrowsDeer = GameObject.Find ("wolfArrowsDeer").GetComponent<Animator> ();
		corruptedShrubArrows = GameObject.Find ("corruptedShrubArrows").GetComponent<Animator> ();
		corruptedDeerArrows = GameObject.Find ("corruptedDeerArrows").GetComponent<Animator> ();
		corruptedWolfArrows = GameObject.Find ("corruptedWolfArrows").GetComponent<Animator> ();
		shrubSize = 1;
		deerSize = 1;
		wolfSize = 1;

        startShrubSize = shrubSize;
        startDeerSize = deerSize;
        startWolfSize = wolfSize;
        startDeerSpeed = deerSpeed;
        startWolfSpeed = wolfSpeed;

        

		if (!megaPaused) {
			if (corruptedShrubPop > 0) {
				CMan.shrubNum = (shrubPop - corruptedShrubPop) / ecoToWorldDivision;
			} else {
				CMan.shrubNum = shrubPop / ecoToWorldDivision;
			}

			if (corruptedDeerPop > 0) {
				CMan.deerNum = (deerPop - corruptedDeerPop) / ecoToWorldDivision;
			} else {
				CMan.deerNum = deerPop / ecoToWorldDivision;
			}

			if (corruptedWolfPop > 0) {
				CMan.wolfNum = (wolfPop - corruptedWolfPop) / ecoToWorldDivision;
			} else {
				CMan.wolfNum = wolfPop / ecoToWorldDivision;
			}

			CMan.corruptedShrubNum = corruptedShrubPop / ecoToWorldDivision;
			CMan.corruptedDeerNum = corruptedDeerPop / ecoToWorldDivision;
			CMan.corruptedWolfNum = corruptedWolfPop / ecoToWorldDivision;
			CMan.AdjustCreatures ();
			CMan.AdjustPickips ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused && !megaPaused) {
			CheckForFailure ();
			SimpleEcologize ();
			Corrupt ();
			UpdateBars ();
			if (Time.time > lastAdjustment + adjustmentDelay) {
				lastAdjustment = Time.time;

				if (corruptedShrubPop > 0) {
					CMan.shrubNum = (shrubPop - corruptedShrubPop) / ecoToWorldDivision;
				} else {CMan.shrubNum = shrubPop / ecoToWorldDivision;
				}

				if (corruptedDeerPop > 0) {
					CMan.deerNum = (deerPop - corruptedDeerPop) / ecoToWorldDivision;
				} else {CMan.deerNum = deerPop / ecoToWorldDivision;
				}

				if (corruptedWolfPop > 0) {
					CMan.wolfNum = (wolfPop - corruptedWolfPop) / ecoToWorldDivision;
				} else {CMan.wolfNum = wolfPop / ecoToWorldDivision;
				}

				CMan.corruptedShrubNum = corruptedShrubPop / ecoToWorldDivision;
				CMan.corruptedDeerNum = corruptedDeerPop / ecoToWorldDivision;
				CMan.corruptedWolfNum = corruptedWolfPop / ecoToWorldDivision;
				CMan.AdjustCreatures ();
				CMan.AdjustPickips ();
			}
		}

//        // Mod updates
//        if (shrubSize > startShrubSize)
//        {
//            shrubSizeMod = 1;
//        }
//        else if (shrubSize < startShrubSize)
//        {
//            shrubSizeMod = -1;
//        }
//        else
//        {
//            shrubSizeMod = 0;
//        }
//
//        if (deerSize > startDeerSize)
//        {
//            deerSizeMod = 1;
//        }
//        else if (deerSize < startDeerSize)
//        {
//            deerSizeMod = -1;
//        }
//        else
//        {
//            deerSizeMod = 0;
//        }
//
//        if (wolfSize > startWolfSize)
//        {
//            wolfSizeMod = 1;
//        }
//        else if (wolfSize < startWolfSize)
//        {
//            wolfSizeMod = -1;
//        }
//        else
//        {
//            wolfSizeMod = 0;
//        }
//
//        if (deerSpeed > startDeerSpeed)
//        {
//            deerSpeedMod = 1;
//        }
//        else if (deerSpeed < startDeerSpeed)
//        {
//           deerSpeedMod = -1;
//        }
//        else
//        {
//            deerSpeedMod = 0;
//        }
//
//        if (wolfSpeed > startWolfSpeed)
//        {
//            wolfSpeedMod = 1;
//        }
//        else if (deerSpeed < startDeerSpeed)
//        {
//            wolfSpeedMod = -1;
//        }
//        else
//        {
//            wolfSpeedMod = 0;
//        }
    }

	public void UpdateBars() {
		shrubBar.SetFillSizeValue (shrubBiomass / 100f);
		deerBar.SetFillSizeValue (deerBiomass / 100f);
		wolfBar.SetFillSizeValue (wolfBiomass / 100f);
		corruptedShrubBar.SetFillSizeValue (corruptedShrubBiomass / 100f);
		corruptedDeerBar.SetFillSizeValue (corruptedDeerBiomass / 100f);
		corruptedWolfBar.SetFillSizeValue (corruptedWolfBiomass / 100f);

		if (shrubBarUI.gameObject.activeSelf) {
			shrubBarUI.SetFillSizeValue (shrubBiomass / 100f);
			deerBarUI.SetFillSizeValue (deerBiomass / 100f);
			wolfBarUI.SetFillSizeValue (wolfBiomass / 100f);
			corruptedShrubBarUI.SetFillSizeValue (corruptedShrubBiomass / 100f);
			corruptedDeerBarUI.SetFillSizeValue (corruptedDeerBiomass / 100f);
			corruptedWolfBarUI.SetFillSizeValue (corruptedWolfBiomass / 100f);
		}
	}

	void SimpleEcologize() {
		if (shrubBiomass > (deerBiomass + overShootValue)) {
			deerRising = true;
			deerArrowsShrub.SetTrigger (deerUp1.ToString());
		}
		if (shrubBiomass < (deerBiomass - overShootValue)) {
			deerRising = false;
			deerArrowsShrub.SetTrigger (deerDown1.ToString());
		}
		if (shrubBiomass < deerBiomass) {
			shrubRising = false;
			shrubArrowsDeer.SetTrigger (shrubDown.ToString());
		} else if (shrubBiomass > deerBiomass) {
			shrubRising = true;
			shrubArrowsDeer.SetTrigger (shrubUp.ToString());
		}


		if (deerBiomass > (wolfBiomass + overShootValue)) {
			wolfRising = true;
			wolfArrowsDeer.SetTrigger (wolfUp.ToString());
		}
		if (deerBiomass < (wolfBiomass - overShootValue)) {
			wolfRising = false;
			wolfArrowsDeer.SetTrigger (wolfDown.ToString());
		}
		if (deerBiomass < wolfBiomass) {
			deerRising2 = false;
			deerArrowsWolf.SetTrigger (deerDown2.ToString());
		} else if (deerBiomass > wolfBiomass) {
			deerRising2 = true;
			deerArrowsWolf.SetTrigger (deerUp2.ToString());
		}
		//
		if (corruptedShrubPop < deerBiomass - overShootValue) {
			cShrubRising = true;
		} else if (corruptedShrubPop > deerBiomass + overShootValue) {
			cShrubRising = false;
		}
		if (corruptedDeerPop < wolfBiomass - overShootValue) {
			cDeerRising = true;
		} else if (corruptedDeerPop > wolfBiomass + overShootValue) {
			cDeerRising = false;
		}
		if (corruptedWolfPop < shrubBiomass - overShootValue) {
			cWolfRising = true;
		} else if (corruptedWolfPop > shrubBiomass + overShootValue) {
			cWolfRising = false;
		}

		if (corruptedShrubPop < deerBiomass) {
			deerFallingC = false;
		} else if (corruptedShrubPop > deerBiomass) {
			deerFallingC = true;
		}
		if (corruptedDeerPop < wolfBiomass) {
			wolfFallingC = false;
		} else if (corruptedDeerPop > wolfBiomass) {
			wolfFallingC = true;
		}
		if (corruptedWolfPop < shrubBiomass) {
			shrubFallingC = false;
		} else if (corruptedWolfPop > shrubBiomass) {
			shrubFallingC = true;
		}

		//
		rateOfDeerChange = 0;


		if (shrubRising == true) {
			shrubPop += (2 + shrubUp * .2f) * overallSpeed * Time.deltaTime;
			//corruptedShrubPop += (2 + shrubUp * .2f) * overallSpeed * Time.deltaTime * (corruptedShrubPop / shrubPop);
		} else {
			shrubPop -= (2 + shrubDown * .2f) * overallSpeed * Time.deltaTime;
			corruptedShrubPop -= (2 + shrubDown * .2f) * overallSpeed * Time.deltaTime;
		}

		if (deerRising == true) {
			rateOfDeerChange += (2 + deerUp1 * .2f) * overallSpeed * Time.deltaTime;
		} else {
			rateOfDeerChange -= (3 + deerDown1 * .2f) * overallSpeed * Time.deltaTime;
		}

		if (wolfRising == true) {
			wolfPop += (2 + wolfUp * .2f) * overallSpeed * Time.deltaTime;
			//corruptedWolfPop += (1.9f + wolfUp * .2f) * overallSpeed * Time.deltaTime * (corruptedWolfPop / wolfPop);
		} else {
			wolfPop -= (3 + wolfDown * .2f) * overallSpeed * Time.deltaTime;
			corruptedWolfPop -= (3 + wolfDown * .2f) * overallSpeed * Time.deltaTime;
		}

		if (deerRising2 == true) {
			rateOfDeerChange += (2 + deerUp2 * .2f) * overallSpeed * Time.deltaTime;
		} else {
			rateOfDeerChange -= (1 + deerDown2 * .2f) * overallSpeed * Time.deltaTime * 2;
		}
		deerPop += rateOfDeerChange;
		if (rateOfDeerChange < 0) {
			corruptedDeerPop += rateOfDeerChange;
		}

		shrubBiomass = shrubPop * shrubSize;
		deerBiomass = deerPop * deerSize;
		wolfBiomass = wolfPop * wolfSize;
		corruptedShrubBiomass = corruptedShrubPop * shrubSize;
		corruptedDeerBiomass = corruptedDeerPop * deerSize;
		corruptedWolfBiomass = corruptedWolfPop * wolfSize;

//		if (Mathf.Abs (shrubPop - deerPop) < 15) {
//			shrubOvershoot += .5f * Time.deltaTime * overallSpeed;
//		} else if (shrubOvershoot > 5) {
//			shrubOvershoot -= .5f * Time.deltaTime * overallSpeed;
//		}
//
//		if (Mathf.Abs (wolfPop - deerPop) < 15) {
//			deerOvershoot += .5f * Time.deltaTime * overallSpeed;
//		} else if (deerOvershoot > 5) {
//			deerOvershoot -= .5f * Time.deltaTime * overallSpeed;
//		}





//		if (deerPop < wolfPop) {
//			deerPop -= 2 * Time.deltaTime;
//			wolfPop -= 3 * Time.deltaTime;
//		}
	}

	void Corrupt() {
		corruptionRate = CMan.corruptionNodeList.Count * corruptionAcceleration;

		if (corruptedShrubPop <= 0) {
			corruptingShrubs = false;
//			corruptedShrubArrows.SetTrigger ("0");
//			corruptedShrubArrowsUI.SetTrigger ("0");
		}
		if (corruptedDeerPop <= 0) {
			corruptingDeer = false;
			corruptedDeerArrows.SetTrigger ("0");
		}
		if (corruptedWolfPop <= 0) {
			corruptingWolves = false;
			corruptedWolfArrows.SetTrigger ("0");
		}

		if (cShrubRising && corruptingShrubs) {
			corruptedShrubPop += 3 * overallSpeed * Time.deltaTime;
		}
		if (cDeerRising && corruptingDeer) {
			corruptedDeerPop += 3 * overallSpeed * Time.deltaTime;
		}
		if (cWolfRising && corruptingWolves) {
			corruptedWolfPop += 3 * overallSpeed * Time.deltaTime;
		}

		if (shrubFallingC) {
			shrubPop -= 2 * overallSpeed * Time.deltaTime;
		}
		if (deerFallingC) {
			deerPop -= 2 * overallSpeed * Time.deltaTime;
		}
		if (wolfFallingC) {
			wolfPop -= 2 * overallSpeed * Time.deltaTime;
		}

//		if (corruptingShrubs == true && shrubRising == true) {
//			corruptedShrubPop += corruptionRate * overallSpeed * Time.deltaTime;
//			if (corruptionRate < 1) {
//				corruptedShrubArrows.SetTrigger ("1");
////				corruptedShrubArrowsUI.SetTrigger ("1");
//			} else if (corruptionRate >= 1 && corruptionRate < 2) {
//				corruptedShrubArrows.SetTrigger ("2");
////				corruptedShrubArrowsUI.SetTrigger ("2");
//			} else if (corruptionRate >= 2 && corruptionRate < 3) {
//				corruptedShrubArrows.SetTrigger ("3");
////				corruptedShrubArrowsUI.SetTrigger ("3");
//			} else if (corruptionRate >= 3 && corruptionRate < 4) {
//				corruptedShrubArrows.SetTrigger ("4");
////				corruptedShrubArrowsUI.SetTrigger ("4");
//			} else if (corruptionRate >= 4 && corruptionRate < 5) {
//				corruptedShrubArrows.SetTrigger ("5");
////				corruptedShrubArrowsUI.SetTrigger ("5");
//			}
//		}
//		if (corruptingDeer == true) {
//			if (rateOfDeerChange >= 0f)
//			corruptedDeerPop += corruptionRate * overallSpeed * Time.deltaTime;
//			if (corruptionRate < 1) {
//				corruptedDeerArrows.SetTrigger ("1");
//			} else if (corruptionRate >= 1 && corruptionRate < 2) {
//				corruptedDeerArrows.SetTrigger ("2");
//			} else if (corruptionRate >= 2 && corruptionRate < 3) {
//				corruptedDeerArrows.SetTrigger ("3");
//			} else if (corruptionRate >= 3 && corruptionRate < 4) {
//				corruptedDeerArrows.SetTrigger ("4");
//			} else if (corruptionRate >= 4 && corruptionRate < 5) {
//				corruptedDeerArrows.SetTrigger ("5");
//			}
//		}
//		if (corruptingWolves == true && wolfRising == true) {
//			corruptedWolfPop += corruptionRate * overallSpeed * Time.deltaTime;
//			if (corruptionRate < 1) {
//				corruptedWolfArrows.SetTrigger ("1");
//			} else if (corruptionRate >= 1 && corruptionRate < 2) {
//				corruptedWolfArrows.SetTrigger ("2");
//			} else if (corruptionRate >= 2 && corruptionRate < 3) {
//				corruptedWolfArrows.SetTrigger ("3");
//			} else if (corruptionRate >= 3 && corruptionRate < 4) {
//				corruptedWolfArrows.SetTrigger ("4");
//			} else if (corruptionRate >= 4 && corruptionRate < 5) {
//				corruptedWolfArrows.SetTrigger ("5");
//			}
//		}

	}


	void CheckForFailure() {
		if (shrubPop <= 0) {
			Time.timeScale = 0;
			gameOver1.SetActive (true);
            mainMenuButton.SetActive(true);
			menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
		} else if (deerPop <= 0) {
			Time.timeScale = 0;
			gameOver2.SetActive (true);
            mainMenuButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
		} else if (wolfPop <= 0) {
			Time.timeScale = 0;
			gameOver3.SetActive (true);
            mainMenuButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
		}

		if (shrubPop <= corruptedShrubPop) {
			Time.timeScale = 0;
			gameOver4.SetActive (true);
            mainMenuButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
		} else if (deerPop <= corruptedDeerPop) {
			Time.timeScale = 0;
			gameOver5.SetActive (true);
            mainMenuButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
		} else if (wolfPop <= corruptedWolfPop) {
			Time.timeScale = 0;
			gameOver6.SetActive (true);
            mainMenuButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
		} else if (CMan.corruptionNodeList.Count == 0)
        {
            Time.timeScale = 0;
            victory.SetActive(true);
            mainMenuButton.SetActive(true);
            menuCamera.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
        }
	}
}
