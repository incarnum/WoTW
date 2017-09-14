using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMenuScript : MonoBehaviour {
	public GameObject menu1;
	public GameObject menu2;
	public GameObject menu3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			menu2.SetActive (false);
			menu3.SetActive (false);
			menu1.SetActive (!menu1.activeSelf);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			menu1.SetActive (false);
			menu3.SetActive (false);
			menu2.SetActive (!menu2.activeSelf);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			menu1.SetActive (false);
			menu2.SetActive (false);
			menu3.SetActive (!menu3.activeSelf);
		}
	}
}
