using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDialogue : MonoBehaviour {
    public DialogueTrigger dialogueTrigger;
    private bool hasPlayed = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Want To Trigger");
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            dialogueTrigger.TriggerDialogue();
            print("Triggering Dialogue");
        }
    }
}
