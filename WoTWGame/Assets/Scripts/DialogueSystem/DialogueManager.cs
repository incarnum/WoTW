using System.Collections;
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
    public bool firstGrowShrubsCast;
	public bool typing;
	private string sentence;
    private InventoryScript inventory;

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
        player.canMove = false;
        convoCount++;
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        print("cleared sentences");

        nameText.text = dialogue.name;

        foreach (string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            print("J1");
            print(sentences.Count);
            return;
        }

        sentence = sentences.Dequeue();
        print(sentences.Count);
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
        print("ClosingDBox");
        player.canMove = true;
        if (convoCount == 1)
        {
            inventory.berryNum += 3;
            inventory.berryText.GetComponent < Text >().text = inventory.berryNum.ToString();
            inventory.berryText2.GetComponent<Text>().text = inventory.berryNum.ToString();
        }
    }
}