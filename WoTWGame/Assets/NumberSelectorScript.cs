using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSelectorScript : MonoBehaviour {
	public GameObject myButton;
	public int speciesNum;
	private Text myText;
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		speciesNum = 3;
		myText.text = speciesNum.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Increase() {
		if (speciesNum < 5) {
			speciesNum += 1;
		}
		myText.text = speciesNum.ToString();
	}

	public void Decrease() {
		if (speciesNum > 3) {
			speciesNum -= 1;
		}
		myText.text = speciesNum.ToString();
	}
}
