using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedSpellDialogue : MonoBehaviour {
    public DialogueTrigger dialogueTrigger;
    private ShrubPopulation shrub;
    private corruptionManagerScript corrMan;
    private bool hasPlayed = false;
	// Use this for initialization
	void Start () {
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        corrMan = GameObject.Find("CorruptionManager").GetComponent<corruptionManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (shrub.corrupting && !hasPlayed)
        {
            corrMan.infectTime = 5;
            hasPlayed = true;
            dialogueTrigger.TriggerDialogue();
        }
	}
}
