using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour {
	public int[] shrubStats = new int[5];
	public int[] deerStats = new int[5];
	public int[] wolfStats = new int[5];
	public int[] rabbitStats = new int[5];
	public int[] owlStats = new int[5];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Save() {
		LoadingManagerScript.SaveWorld (GetComponent<WorldAnalyzerScript>());
	}

	public void Load() {
		shrubStats = LoadingManagerScript.LoadWorld ().shrubStates;
		deerStats = LoadingManagerScript.LoadWorld ().deerStates;
		wolfStats = LoadingManagerScript.LoadWorld ().wolfStates;
		rabbitStats = LoadingManagerScript.LoadWorld ().rabbitStates;
		owlStats = LoadingManagerScript.LoadWorld ().owlStates;


	}
}
