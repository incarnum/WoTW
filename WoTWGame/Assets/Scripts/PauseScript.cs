using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {
    //Pause menu buttons
    public GameObject resumeButton;
    public GameObject optionsButton;
    public GameObject mainMenuButton;
    public GameObject exitButton;
	public GameObject pauseMenu;
    public GameObject multiMenuCanvas;
    public GameObject overlayCanvas;

	//Selection effects
	public List<GameObject> selectionEffects;

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
	public bool areYouSureOpened;
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

    public GameObject musicSource;
    public GameObject soundSource;

	public GameObject screenFade;

    private GameObject eco;

	private Color DefaultTextColor;

	public GameManagerScript gameManager;

	private TimeStopCanvas timeStopCanvas;

	public GameObject areYouSure;


	// Use this for initialization
	void Start () {
        eco = GameObject.Find("SimpleEcologyMaster");
		timeStopCanvas = GameObject.Find ("TimeStopCanvas").GetComponent<TimeStopCanvas>();
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
		DefaultTextColor = applyButton.GetComponentsInChildren<Text> () [0].color;
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void PauseGame()
    {
		pauseMenu.SetActive(true);

        overlayCanvas.GetComponent<Canvas>().enabled = false;
//        multiMenuCanvas.SetActive(false);

        eco.GetComponent<SimpleEcologyMasterScript>().megaPaused = true;
        eco.GetComponent<SimpleEcologyMasterScript>().paused = true;
		if (eco.GetComponent<SimpleEcologyMasterScript> ().areaTimeStop == false) {
			timeStopCanvas.pauseStop = true;
			timeStopCanvas.CheckVisibility ();
		}

        paused = true;
    }

    public void ResumeGame()
    {
		pauseMenu.SetActive(false);

		foreach (GameObject gunch in selectionEffects) {
			gunch.SetActive (false);
		}

		resumeButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		optionsButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		mainMenuButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		exitButton.GetComponent<menuButtonScript> ().ResetTextSize ();

        overlayCanvas.GetComponent<Canvas>().enabled = true;
//        multiMenuCanvas.SetActive(true);

        eco.GetComponent<SimpleEcologyMasterScript>().megaPaused = false;
        eco.GetComponent<SimpleEcologyMasterScript>().paused = false;
		if (eco.GetComponent<SimpleEcologyMasterScript> ().areaTimeStop == false) {
			timeStopCanvas.pauseStop = false;
			timeStopCanvas.CheckVisibility ();
		}

        paused = false;
    }

    public void OpenOptions()
    {
        Debug.Log("Open Options");
        resumeButton.SetActive(false);
        optionsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        exitButton.SetActive(false);

		resumeButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		optionsButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		mainMenuButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		exitButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		foreach (GameObject gunch in selectionEffects) {
			gunch.SetActive (false);
		}


        optionsMenu.SetActive(true);

        optionsOpened = true;
        fullscreenGUI.GetComponent<UnityEngine.UI.Toggle>().isOn = Screen.fullScreen;
        musicGUI.GetComponent<UnityEngine.UI.Toggle>().isOn = musicSource.activeSelf;
        soundGUI.GetComponent<UnityEngine.UI.Toggle>().isOn = soundSource.activeSelf;
		CheckIfThereAreChanges ();


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
        int setting = resGUI.GetComponent<UnityEngine.UI.Dropdown>().value;
        resWidth = resOptions[setting].width;
        resHeight = resOptions[setting].height;
		fullScreen = fullscreenGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn;
        Screen.SetResolution(resWidth, resHeight, fullScreen);

        musicSource.SetActive(musicGUI.GetComponent<UnityEngine.UI.Toggle>().isOn);
        soundSource.SetActive(soundGUI.GetComponent<UnityEngine.UI.Toggle>().isOn);
		CheckIfThereAreChanges ();

		gameManager.musicBool = musicGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn;
		gameManager.soundBool = soundGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn;
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
		StartCoroutine (LoadAsynchronously());
		musicSource.GetComponent<fadeAudioScript> ().beginFade (1f);
		screenFade.SetActive (true);
    }

	IEnumerator LoadAsynchronously ()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync ("MainMenu");
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		while (!operation.isDone) {
			Debug.Log (operation.progress);
			yield return null;
		}
	}

	public void CheckIfThereAreChanges() {

		if (
			fullScreen == fullscreenGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn &&
			musicSource.activeSelf == musicGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn &&
			soundSource.activeSelf == soundGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn &&
			Screen.currentResolution.height == resOptions[resGUI.GetComponent<UnityEngine.UI.Dropdown>().value].height &&
			Screen.currentResolution.width == resOptions[resGUI.GetComponent<UnityEngine.UI.Dropdown>().value].width
		)
		{
			applyButton.GetComponent<Button> ().interactable = false;
			applyButton.GetComponentsInChildren<Text> () [0].color = Color.clear;
		} else {
			applyButton.GetComponent<Button> ().interactable = true;
			applyButton.GetComponentsInChildren<Text> () [0].color = DefaultTextColor;
		}
	}

	public void ToggleAreYouSure(bool boo) {
		areYouSureOpened = boo;
	}
}
