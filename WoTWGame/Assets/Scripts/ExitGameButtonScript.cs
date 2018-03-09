using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButtonScript : MonoBehaviour {
    public GameObject SureMenu;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Exit() {
		Application.Quit ();
	}
    public void OnMouseDown()
    {
        SureMenu.SetActive(true);
    }
}
