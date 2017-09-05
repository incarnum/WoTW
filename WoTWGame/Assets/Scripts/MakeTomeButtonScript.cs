using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeTomeButtonScript : MonoBehaviour {
    public bool tutMode;
	public GameObject spellRing1;
	public GameObject spellRing2;
	public GameObject spellLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
        string result = "";
        if (tutMode)
        {
           result = GameObject.Find("SpellMenu").GetComponent<SpellMenuScript>().spellPreviewString;

        }
        if (tutMode != true || result == "Enlarge Deer")
        {
            GameObject.Find("SpellMenu").GetComponent<SpellMenuScript>().CreateSpell();
			spellRing1.GetComponent<RingSpinScript> ().SpeedBoost ();
			spellRing2.GetComponent<RingSpinScript> ().SpeedBoost ();
			spellLight.GetComponent<Animator> ().SetTrigger ("Cast");
        }
        
	}
}
