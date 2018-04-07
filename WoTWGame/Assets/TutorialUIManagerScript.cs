using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManagerScript : MonoBehaviour {
	public int phase;
	public GameObject movementInfo;
	public GameObject tabToOpen;
	public GameObject popBarAlert;
	public GameObject barFillInfo;
	public GameObject popRateInfo;
	public GameObject buffDisplayInfo;
	public GameObject influenceInfo;
	public GameObject cycleBarsInfo;
	public GameObject corruptionInfo;
	public GameObject tabTextObject;
	public GameObject UIManager;
	public UIManager uiManagerScript;

	public GameObject tutorialBuffStopper;
	public GameObject shrubInfluence;

	// Use this for initialization
	void Start () {
		movementInfo.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)
			|| Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
			movementInfo.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.Tab)) {
            tabToOpen.SetActive(!tabToOpen.activeSelf);
			if (phase <= 100) {
				tabToOpen.SetActive (false);
				popBarAlert.SetActive (false);
			}
		}
	}

	public void NextPhase() {
		if (phase == 0) {
			UIManager.SetActive (true);
			uiManagerScript.enabled = true;
			tabTextObject.SetActive (true);
			tabToOpen.SetActive (true);
			barFillInfo.SetActive (true);
			popRateInfo.SetActive (true);
		} else if (phase == 2) {
			barFillInfo.SetActive (false);
			popRateInfo.SetActive (false);
			popBarAlert.SetActive (true);
			tutorialBuffStopper.SetActive (true);
			buffDisplayInfo.SetActive (true);
		} else if (phase == 4) {
			popBarAlert.SetActive (true);
			buffDisplayInfo.SetActive (false);
			cycleBarsInfo.SetActive (true);
			influenceInfo.SetActive (true);
			shrubInfluence.SetActive (true);
		} else if (phase == 5) {
			cycleBarsInfo.SetActive (false);
			influenceInfo.SetActive (false);
			popBarAlert.SetActive (true);
			corruptionInfo.SetActive (true);
		} else if (phase == 6) {
			corruptionInfo.SetActive (false);
		}

		phase += 1;
	}

	//phase 0: movement controls
	// first dialogue trigger
	//phase 1: tab to open pop bar, pop bar has info on what the bar is and what the number at the top is
	// walk into spell circle for next dialogue trigger, then cast enlarge
	//phase 3: ! by pop bar, pop bar has buff display and info about buff display
	// walk to corrupted spell circle and get dialogue, then cast and get dialogue
	//phase 4: ! by pop bar, pop bar has deer in it and info about influence number
	// walk to next corrupted spell circle and cast cleanse, get dialogue
	//phase 5: ! by pop bar, pop bar has info showing corruption on it
}
