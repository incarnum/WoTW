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
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		StartCoroutine (LoadAsynchronously());
		StartCoroutine (SayFrameALot());
	}

	public void Pressed() {
		GameManagerScript.instance.levelType = levelNum;
		StartCoroutine (LoadAsynchronously());
		ring.SpeedBoost ();
		menu.SetActive (false);
	}

	IEnumerator LoadAsynchronously ()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync ("Forest 3");
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		while (!operation.isDone) {
			Debug.Log (operation.progress);
			if (operation.progress >= .9f) {
				screenFade.SetActive (true);
				GameObject.Find ("Audio").GetComponent<fadeAudioScript> ().beginFade (1f);
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
