using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDialogue : MonoBehaviour {
    public DialogueTrigger dialogueTrigger;
    public int convoCode;
    private bool hasPlayed = false;
    private DialogueManager dm;
    // Use this for initialization
    void Start () {
        dm = GameObject.Find("TutorialDialogue").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
//        print("Want To Trigger");
        if (convoCode == dm.convoCount && other.CompareTag("Player"))
        {
            dialogueTrigger.TriggerDialogue();
        }
    }
}
