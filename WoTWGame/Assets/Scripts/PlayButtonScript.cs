using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour {
	public RingSpinUI ring;
	public GameObject screenFade;
	public GameObject menu;
	public int levelNum;
	public GameManagerScript gameManager;

	public int overallMode;
	public int gameMode;
	public bool loadFromSaveButton;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		gameManager = GameManagerScript.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		StartCoroutine (LoadAsynchronously());
		StartCoroutine (SayFrameALot());
	}

	public void Pressed() {
		GameManagerScript.instance.gameMode = gameMode;
		GameManagerScript.instance.loadFromSave = loadFromSaveButton;
		StartCoroutine (LoadAsynchronously());
		ring.SpeedBoost ();
		menu.SetActive (false);
	}

	public void ChooseCreatureNumber(int i) {
		if (overallMode == 0) {
			//story play button
			//this shouldn't happen since you don't select a species number for story
			Debug.Log ("This shouldn't be called, you can't choose a creature num for story mode");
		} else if (overallMode == 1) {
			gameMode = i - 2;
		} else if (overallMode == 2) {
			gameMode = i + 1;
		}
	}

	IEnumerator LoadAsynchronously ()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync ("Forest 3");
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		while (!operation.isDone) {
			Debug.Log (operation.progress);
			if (operation.progress >= .9f) {
				screenFade.SetActive (true);
				GameObject.Find ("Music").GetComponent<fadeAudioScript> ().beginFade (1f);
				yield return new WaitForSeconds (1);
			}
			yield return null;
		}
	}

	IEnumerator SayFrameALot()
	{
		print ("frame");
		Application.backgroundLoadingPriority = ThreadPriority.High;
		yield return null;
	}
		
}
