﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private PlayerControllerScript player;
    public int convoCount;
    public int cleansedNodes;
    public bool firstCorrCast;
	public bool secondCorrCast;
    public bool firstGrowShrubsCast;
	public bool typing;
    public bool tutorialActive = true;
	private string sentence;
    private InventoryScript inventory;
    private bool givenStartingBerries = false;
	private bool zMenuPopUpDone = false;

    private DeerPopulation deer;
    private WolfPopulation wolf;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.Find("Player").GetComponent<PlayerControllerScript>();
        inventory = GameObject.Find("Player").GetComponent<InventoryScript>();
        convoCount = 0;
        cleansedNodes = 0;
        firstCorrCast = true;
        firstGrowShrubsCast = true;
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        if (!tutorialActive)
        {
            deer.enabled = true;
            wolf.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
			if (typing == true) 
			{
				StopAllCoroutines();
				dialogueText.text = "";
				dialogueText.text += sentence;
				typing = false;
			} 
			else 
			{
				DisplayNextSentence ();
			}
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (tutorialActive)
        {
            player.canMove = false;
            player.GetComponent<PlayerControllerB>().canMove = false;
            convoCount++;
            animator.SetBool("IsOpen", true);
            sentences.Clear();
//            print("cleared sentences");

            nameText.text = dialogue.name;

            foreach (string s in dialogue.sentences)
            {
                sentences.Enqueue(s);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
//            print("J1");
//            print(sentences.Count);
            return;
        }

        sentence = sentences.Dequeue();
//        print(sentences.Count);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string s)
    {
		typing = true;
        dialogueText.text = "";
        foreach (char letter in s.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
		typing = false;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
//        print("ClosingDBox");
        player.canMove = true;
		player.GetComponent<PlayerControllerB> ().canMove = true;
        if (convoCount == 1 && !givenStartingBerries)
        {
            givenStartingBerries = true;
            inventory.berryNum += 3;
            inventory.berryText.GetComponent < Text >().text = inventory.berryNum.ToString();
        }
		if (convoCount == 4 && zMenuPopUpDone == false) {
			GameObject.Find ("ZButtonIndicator").transform.GetChild(0).gameObject.SetActive (true);
			zMenuPopUpDone = true;
		}
    }
}