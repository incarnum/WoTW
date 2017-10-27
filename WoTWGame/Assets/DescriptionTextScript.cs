using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionTextScript : MonoBehaviour {
	public List<string> descriptions;
	public float rowLimit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateDescription(int whichDescription) {
		string garfield = descriptions [whichDescription];
		GetComponent<TextMesh> ().text = "";
		string builder = "";
		string[] parts = garfield.Split (' ');
		for (int i = 0; i < parts.Length; i++)
		{
			GetComponent<TextMesh> ().text += parts[i] + " ";
			if (GetComponent<TextMesh>().GetComponent<Renderer>().bounds.extents.x > rowLimit) {
				GetComponent<TextMesh> ().text = builder + System.Environment.NewLine + parts[i] + " ";
			}
			builder = GetComponent<TextMesh> ().text;
		}

		//garfield = garfield.Replace ("@", System.Environment.NewLine);
		//GetComponent<TextMesh> ().text = garfield;
	}
}
