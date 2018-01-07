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

    public bool paused;

    private GameObject eco;



	// Use this for initialization
	void Start () {
        eco = GameObject.Find("SimpleEcologyMaster");
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void PauseGame()
    {
        Debug.Log("Paused");
        resumeButton.SetActive(true);
        optionsButton.SetActive(true);
        mainMenuButton.SetActive(true);
        exitButton.SetActive(true);

        overlayCanvas.SetActive(false);
        multiMenuCanvas.SetActive(false);

        eco.GetComponent<SimpleEcologyMasterScript>().megaPaused = true;
        eco.GetComponent<SimpleEcologyMasterScript>().paused = true;

        paused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Unpaused");
        resumeButton.SetActive(false);
        optionsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        exitButton.SetActive(false);

        overlayCanvas.SetActive(true);
        multiMenuCanvas.SetActive(true);

        eco.GetComponent<SimpleEcologyMasterScript>().megaPaused = false;
        eco.GetComponent<SimpleEcologyMasterScript>().paused = false;

        paused = false;
    }
}
