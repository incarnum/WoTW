using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		Debug.Log ("gotoMain");
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("MainMenu");
	}
}
