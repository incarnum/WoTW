using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryScript : MonoBehaviour {
	public int berryNum;
	public int antlerNum;
	public int fangNum;
	public int mouseSkullNum;
	public int owlFeatherNum;
	public int sBFeatherNum;
	public int corrBerryNum;
	public int corrAntlerNum;
	public int corrFangNum;
    public int MaxMana;
    public int ManaCount;

	public GameObject berryText;
	public GameObject antlerText;
	public GameObject fangText;
	public GameObject mouseSkullText;
	public GameObject owlFeatherText;
	public GameObject sBfeatherText;
	public GameObject corrBerryText;
	public GameObject corrAntlerText;
	public GameObject corrFangText;

	public GameObject berryText2;
	public GameObject antlerText2;
	public GameObject fangText2;
	public GameObject mouseSkullText2;
	public GameObject owlFeatherText2;
	public GameObject sBfeatherText2;
	public GameObject corrBerryText2;
	public GameObject corrAntlerText2;
	public GameObject corrFangText2;

    public GameObject ManaText;
	// Use this for initialization
	void Start () {
		berryText = GameObject.Find ("BerryText");
		antlerText = GameObject.Find ("AntlerText");	
		fangText = GameObject.Find ("FangText");
		mouseSkullText = GameObject.Find ("MouseSkullText");
		antlerText2 = GameObject.Find ("AntlerText2");	
		fangText2 = GameObject.Find ("FangText2");
		berryText2 = GameObject.Find ("BerryText2");
		mouseSkullText2 = GameObject.Find ("MouseSkullText2");
		corrBerryText = GameObject.Find ("CorrBerryText");
		corrBerryText2 = GameObject.Find ("CorrBerryText2");
		corrAntlerText = GameObject.Find ("CorrAntlerText");
		corrAntlerText2 = GameObject.Find ("CorrAntlerText2");
		corrFangText = GameObject.Find ("CorrFangText");
		corrFangText2 = GameObject.Find ("CorrFangText2");

        ManaCount = MaxMana;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Backslash)) {
			berryNum += 20;
			antlerNum += 20;
			fangNum += 20;
			mouseSkullNum += 20;
			corrBerryNum += 20;
			corrAntlerNum += 20;
			corrFangNum += 20;
			UpdateNumbers ();
		}

        //Mana capping
        if(ManaCount > MaxMana)
        {
            ManaCount = MaxMana;
        }
        else if(ManaCount < 0)
        {
            ManaCount = 0;
        }

        //ManaText Update
        string strMana = "Mana: " + ManaCount;
        ManaText.GetComponent<Text>().text = strMana;
	}

	public void UpdateNumbers () {
		berryText.GetComponent<Text> ().text = berryNum.ToString ();
		antlerText.GetComponent<Text> ().text = antlerNum.ToString ();
		fangText.GetComponent<Text> ().text = fangNum.ToString ();
//		mouseSkullText.GetComponent<TextMesh> ().text = mouseSkullNum.ToString ();
		corrBerryText.GetComponent<Text> ().text = corrBerryNum.ToString ();
		corrAntlerText.GetComponent<Text> ().text = corrAntlerNum.ToString ();
		corrFangText.GetComponent<Text> ().text = corrFangNum.ToString ();

		berryText2.GetComponent<TextMesh> ().text = berryNum.ToString ();
		antlerText2.GetComponent<TextMesh> ().text = antlerNum.ToString ();
		fangText2.GetComponent<TextMesh> ().text = fangNum.ToString ();
//		mouseSkullText2.GetComponent<TextMesh> ().text = mouseSkullNum.ToString ();
		corrBerryText2.GetComponent<TextMesh> ().text = corrBerryNum.ToString ();
		corrAntlerText2.GetComponent<TextMesh> ().text = corrAntlerNum.ToString ();
		corrFangText2.GetComponent<TextMesh> ().text = corrFangNum.ToString ();
	}
}
