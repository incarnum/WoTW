using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMLoader : MonoBehaviour {
	public GameObject gameManager;
	// Use this for initialization
	void Awake () {
		if (GameManagerScript.instance == null)
			Instantiate (gameManager);
	}
}
