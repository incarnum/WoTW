using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMenuScript : MonoBehaviour {
	public GameObject menu1;
	public GameObject menu2;
	public GameObject menu3;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			openPop ();
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			openBuff ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			openMap ();
		}
	}

	public void openPop() {
		menu2.SetActive (false);
		menu3.SetActive (false);
		button2.SetActive (false);
		button3.SetActive (false);
		menu1.SetActive (!menu1.activeSelf);
		button1.SetActive (!button1.activeSelf);
	}

	public void openBuff() {
		menu1.SetActive (false);
		menu3.SetActive (false);
		button1.SetActive (false);
		button3.SetActive (false);
		menu2.SetActive (!menu2.activeSelf);
		button2.SetActive (!button2.activeSelf);
	}

	public void openMap() {
		menu1.SetActive (false);
		menu2.SetActive (false);
		button1.SetActive (false);
		button2.SetActive (false);
		menu3.SetActive (!menu3.activeSelf);
		button3.SetActive (!button3.activeSelf);
	}
}
