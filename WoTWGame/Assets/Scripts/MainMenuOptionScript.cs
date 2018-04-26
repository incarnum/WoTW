using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuOptionScript : MonoBehaviour {
    //Pause menu buttons
    public GameObject optionsButton;

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
	public GameObject creditsBack;
	public GameObject standardObject;
	public GameObject endlessObject;

    public bool paused;
    public bool optionsOpened;
	public bool creditsOpened;
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

	private Color DefaultTextColor;
	public ScrollRect scroller;
	public GameObject credits;

	public GameManagerScript gameManager;



	// Use this for initialization
	void Start () {
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
		scroller.scrollSensitivity = 20;

		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			print ("this is the main menu");
			UniversalData unloader = UniversalSaverScript.LoadUniverse ();
			gameManager.musicBool = unloader.musicBool;
			gameManager.soundBool = unloader.soundBool;
			gameManager.language = unloader.language;
			gameManager.hasBeatenGame = unloader.hasBeatenGame;

			musicSource.SetActive(unloader.musicBool);
			soundSource.SetActive(unloader.soundBool);
			musicGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn = unloader.musicBool;
			soundGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn = unloader.soundBool;
			if (unloader.hasBeatenGame) {
				standardObject.SetActive (true);
				endlessObject.SetActive (true);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void OpenOptions()
    {
        Debug.Log("Open Options");
        //optionsButton.SetActive(false);
		optionsButton.GetComponent<menuButtonScript> ().ResetTextSize ();
		foreach (GameObject gunch in selectionEffects) {
			gunch.SetActive (false);
		}

		if (!optionsOpened) {
			optionsMenu.SetActive (true);

			optionsOpened = true;
			fullscreenGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn = Screen.fullScreen;
			musicGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn = musicSource.activeSelf;
			soundGUI.GetComponent<UnityEngine.UI.Toggle> ().isOn = soundSource.activeSelf;
			CheckIfThereAreChanges ();


			//find current res value
			string currentRes = resWidth + "x" + resHeight;
			int setting = 0;
			for (int i = 0; i < resOptions.Length; i++) {
				string resString = resOptions [i].width + "x" + resOptions [i].height;
				if (currentRes.Equals (resString)) {
					setting = i;
				}
			}
			resGUI.GetComponent<UnityEngine.UI.Dropdown> ().value = setting;
		} else {
			ReturnToPause ();
		}
        
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
		UniversalSaverScript.SaveUniverse (gameManager);
    }
		

    public void ReturnToPause()
    {
        optionsMenu.SetActive(false);
        optionsOpened = false;
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

	public void OpenCredits() {
		optionsMenu.SetActive (false);
		optionsOpened = false;
		credits.SetActive (true);
		creditsOpened = true;
	}

	public void CloseCredits1() {
		credits.SetActive (false);
		creditsOpened = false;
		//optionsMenu.SetActive (true);
		//optionsOpened = true;
		OpenOptions ();
	}

	public void CloseCredits2() {
		credits.SetActive (false);
		creditsOpened = false;
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
