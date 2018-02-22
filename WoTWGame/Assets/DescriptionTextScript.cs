using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionTextScript : MonoBehaviour {
    [TextArea(1, 10)]
    public List<string> descriptions;
	public float rowLimit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateDescription(int whichDescription) {
		//this is the script that displays the ingredient tooltip
		//this script holds several strings
		//pylonscript tells this script which string to display

		//the string gets split up and reassembled with linebreaks, with the float rowLimit, describing how wide each row can be
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
			
	}
}
