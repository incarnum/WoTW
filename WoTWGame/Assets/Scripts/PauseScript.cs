using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    public GameObject resumeButton;
    public GameObject optionsButton;
    public GameObject mainMenuButton;
    public GameObject exitButton;
    public GameObject multiMenuCanvas;
    public GameObject overlayCanvas;

    public SimpleEcologyMasterScript eco;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void PauseGame()
    {
        resumeButton.SetActive(true);
        optionsButton.SetActive(true);
        mainMenuButton.SetActive(true);
        exitButton.SetActive(true);

        overlayCanvas.SetActive(false);
        multiMenuCanvas.SetActive(false);

        eco.paused = true;
        eco.megaPaused = true;
    }

    public void ResumeGame()
    {
        resumeButton.SetActive(false);
        optionsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        exitButton.SetActive(false);

        overlayCanvas.SetActive(true);
        multiMenuCanvas.SetActive(true);

        eco.paused = false;
        eco.megaPaused = false;
    }
}
