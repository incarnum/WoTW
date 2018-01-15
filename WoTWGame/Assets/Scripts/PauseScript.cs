using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject resGUI;
    public GameObject backButton;
    public GameObject applyButton;

    public bool paused;
    public bool optionsOpened;
    public Resolution[] resOptions;
    
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
        resOptions = Screen.resolutions;
        foreach(Resolution res in resOptions)
        {
            string resString = res.width + "x" + res.height;
            resGUI.GetComponent<UnityEngine.UI.Dropdown>().options.Add(new UnityEngine.UI.Dropdown.OptionData {text = resString});
        }
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
        fullscreenGUI.GetComponent<UnityEngine.UI.Toggle>().isOn = fullScreen;
        musicGUI.GetComponent<UnityEngine.UI.Toggle>().isOn = music;
        soundGUI.GetComponent<UnityEngine.UI.Toggle>().isOn = sound;

        //find current res value
        string currentRes = resWidth + "x" + resHeight;
        int setting = 0;
        for (int i = 0; i < resOptions.Length; i++)
        {
            string resString = resOptions[i].width + "x" + resOptions[i].height;
            if(currentRes.Equals(resString))
            {
                setting = i;
            }
        }
        resGUI.GetComponent<UnityEngine.UI.Dropdown>().value = setting;

        
    }
    
    public void Apply()
    {
        fullScreen = fullscreenGUI.GetComponent<UnityEngine.UI.Toggle>().isOn;

        music = musicGUI.GetComponent<UnityEngine.UI.Toggle>().isOn;

        sound = soundGUI.GetComponent<UnityEngine.UI.Toggle>().isOn;

        
        int setting = resGUI.GetComponent<UnityEngine.UI.Dropdown>().value;
        resWidth = resOptions[setting].width;
        resHeight = resOptions[setting].height;
        Screen.SetResolution(resWidth, resHeight, fullScreen);
        
    }

    public void ReturnToPause()
    {
        optionsMenu.SetActive(false);
        optionsOpened = false;

        resumeButton.SetActive(true);
        optionsButton.SetActive(true);
        mainMenuButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
