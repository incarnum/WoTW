using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public static GameManagerScript instance = null;

	public bool musicBool = true;
	public bool soundBool = true;
    public int language = 0;//0 = English, 1 = Chinese, ?? 2 = Russian ??
	public int levelType = 0; //for now, number of animals

	public GameObject musicSource;
	public GameObject soundSource;

	public bool loadFromSave;
	public int gameMode;

	public bool hasBeatenGame;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		if (GameObject.Find ("PauseCanvas").GetComponent<MainMenuOptionScript> () != null) {
			GameObject.Find ("PauseCanvas").GetComponent<MainMenuOptionScript> ().gameManager = this;
		}

		if (GameObject.Find ("PauseCanvas").GetComponent<PauseScript> () != null) {
			GameObject.Find ("PauseCanvas").GetComponent<PauseScript> ().gameManager = this;
		}

	}

	void OnLevelWasLoaded (int level) {
		if (GameObject.Find ("PauseCanvas").GetComponent<MainMenuOptionScript> () != null) {
			GameObject.Find ("PauseCanvas").GetComponent<MainMenuOptionScript> ().gameManager = this;
		}

		if (GameObject.Find ("PauseCanvas").GetComponent<PauseScript> () != null) {
			GameObject.Find ("PauseCanvas").GetComponent<PauseScript> ().gameManager = this;
		}
		GameObject.Find ("Music").SetActive (musicBool);
		print (musicBool);
		GameObject.Find ("Sound Box").SetActive (soundBool);

		print ("game mode is " + gameMode + " with a levelType (animal number) of " + levelType);
		GameObject.Find ("LoadingManager").GetComponent<WorldAnalyzerScript> ().gm = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
