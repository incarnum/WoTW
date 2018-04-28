using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenuSaveReader : MonoBehaviour {
	public Text storyContinueInfo;
	public Text standardContinueInfo;
	public Text endlessContinueInfo;


	public GameObject storyNewMenu;
	public GameObject standardNewMenu;
	public GameObject endlessNewMenu;
	public GameObject storyContinueMenu;
	public GameObject standardContinueMenu;
	public GameObject endlessContinueMenu;

	public GameObject standardButton;
	public GameObject endlessButton;


	// Use this for initialization
	void Start () {
		ReadSaves ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ReadSaves() {
		if (File.Exists (Application.persistentDataPath + "/story.woods")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/story.woods", FileMode.Open);

			WorldData data = bf.Deserialize (stream) as WorldData;

			stream.Close ();
			Debug.Log (data.day + ", " + data.month + ", " + data.year);
			storyContinueMenu.SetActive (true);
			storyNewMenu.SetActive (false);
			string dateInfo = ("Saved " + data.month + ", " + data.day + ", " + data.year);
			storyContinueInfo.text = dateInfo;
		} else {
			Debug.Log ("No story save");
		}

		if (File.Exists (Application.persistentDataPath + "/standard.woods")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/standard.woods", FileMode.Open);

			WorldData data = bf.Deserialize (stream) as WorldData;

			stream.Close ();
			Debug.Log (data.day + ", " + data.month + ", " + data.year);
			standardContinueMenu.SetActive (true);
			standardNewMenu.SetActive (false);
			string dateInfo = ("Saved " + data.month + ", " + data.day + ", " + data.year);
			standardContinueInfo.text = dateInfo;
		} else {
			Debug.Log ("No standard save");
		}

		if (File.Exists (Application.persistentDataPath + "/endless.woods")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/endless.woods", FileMode.Open);

			WorldData data = bf.Deserialize (stream) as WorldData;

			stream.Close ();
			Debug.Log (data.day + ", " + data.month + ", " + data.year);
			endlessContinueMenu.SetActive (true);
			endlessNewMenu.SetActive (false);
			string dateInfo = ("Saved" + data.month + ", " + data.day + ", " + data.year);
			endlessContinueInfo.text = dateInfo;
		} else {
			Debug.Log ("No endless save");
		}

		if (File.Exists (Application.persistentDataPath + "/main.woods")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/main.woods", FileMode.Open);

			UniversalData data = bf.Deserialize (stream) as UniversalData;

			stream.Close ();

			if (data.hasBeatenGame == true) {
				standardButton.SetActive (true);
				endlessButton.SetActive (true);
			}

			//this is where you would get highscore information from data and set some string in the scene to show it.

		} else {
			Debug.Log ("No universal save");
		}
	}
}
