using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    //Pause menu buttons
    public GameObject resumeButton;
    public GameObject optionsButton;
    public GameObject mainMenuButton;
    public GameObject exitButton;
    public GameObject multiMenuCanvas;
    public GameObject overlayCanvas;

    //Options menu layout
    public GameObject optionsMenu;
    public GameObject titleGUI;
    public GameObject fullscreenGUI;
    public GameObject musicGUI;
    public GameObject soundGUI;
    public GameObject backButton;
    public GameObject applyButton;

    public bool paused;
    public bool optionsOpened;
    
    //Saved settings
    public bool fullScreen;
    public bool sound;
    public bool music;
    public int refrechRate;
    public int resWidth;
    public int resHeight;

    //Temp settings
    public bool tempFullScreen;
    public bool tempSound;
    public bool tempMusic;
    public string tempResWidth;
    public string tempResHeight;




    private GameObject eco;



	// Use this for initialization
	void Start () {
        eco = GameObject.Find("SimpleEcologyMaster");
        paused = false;
        optionsOpened = false;
        fullScreen = Screen.fullScreen;
        resWidth = Screen.width;
        resHeight = Screen.height;
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

    public void OpenOptions()
    {
        Debug.Log("Open Options");
        resumeButton.SetActive(false);
        optionsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        exitButton.SetActive(false);

        optionsMenu.SetActive(true);

        optionsOpened = true;
        fullScreen = Screen.fullScreen;
      
        
        



    }
    
    public void Apply()
    {

    }

    public void ReturnToPause()
    {

    }
}
