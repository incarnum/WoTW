using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryScript : MonoBehaviour {
	public int berryNum;
	public int antlerNum;
	public int fangNum;
	public int rabbitFootNum;
	public int owlFeatherNum;
	public int sBFeatherNum;
	public int corrBerryNum;
	public int corrAntlerNum;
	public int corrFangNum;

	public GameObject berryText;
	public GameObject antlerText;
	public GameObject fangText;
	public GameObject rabbitFootText;
	public GameObject owlFeatherText;


	public GameObject berryText2;
	public GameObject antlerText2;
	public GameObject fangText2;
	public GameObject rabbitFootText2;
	public GameObject owlFeatherText2;
	// Use this for initialization
	void Start () {
		berryText = GameObject.Find ("BerryText");
		antlerText = GameObject.Find ("AntlerText");	
		fangText = GameObject.Find ("FangText");
		rabbitFootText = GameObject.Find ("RabbitFootText");
		owlFeatherText = GameObject.Find ("OwlFeatherText");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Backslash)) {
			berryNum += 20;
			antlerNum += 20;
			fangNum += 20;
			rabbitFootNum += 20;
			owlFeatherNum += 20;
			UpdateNumbers ();
		}
	}

	public void UpdateNumbers () {
		berryText.GetComponent<Text> ().text = berryNum.ToString ();
		antlerText.GetComponent<Text> ().text = antlerNum.ToString ();
		fangText.GetComponent<Text> ().text = fangNum.ToString ();
		rabbitFootText.GetComponent<Text>().text = rabbitFootNum.ToString ();
		owlFeatherText.GetComponent<Text>().text = owlFeatherNum.ToString ();
	}
}
