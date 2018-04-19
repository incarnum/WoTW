using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public List<Dialogue> dialogue;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GameObject.Find("TutorialDialogue").GetComponent<DialogueManager>();
    }

    public void TriggerDialogue ()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
