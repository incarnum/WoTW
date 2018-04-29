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
	public bool secondCorrCast;
    public bool firstGrowShrubsCast;
	public bool typing;
    public bool tutorialActive = true;
	public bool windowUp;
	private string sentence;
    private InventoryScript inventory;
    private bool givenStartingBerries = false;
	private bool zMenuPopUpDone = false;
	private TimeStopCanvas tsc;

    private DeerPopulation deer;
    private WolfPopulation wolf;

    private Queue<string> sentences;
    private Canvas overLay;

	public TutorialUIManagerScript tm;
    public GameManagerScript gm;

	public float advanceDelay;
	private float advanceDelayEndTime;

	public AudioSource source;
	public AudioClip buttonHigh;
	public AudioClip buttonLow;

    // Use this for initialization
    void Start()
    {
        overLay = GameObject.Find("MainOverlayCanvas").GetComponent<Canvas>();
        sentences = new Queue<string>();
        player = GameObject.Find("Player").GetComponent<PlayerControllerScript>();
        inventory = GameObject.Find("Player").GetComponent<InventoryScript>();
		tsc = GameObject.Find ("TimeStopCanvas").GetComponent<TimeStopCanvas> ();
        convoCount = 0;
        cleansedNodes = 0;
        firstCorrCast = true;
        firstGrowShrubsCast = true;
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
		gm = GameObject.Find("GameManager(Clone)").GetComponent<GameManagerScript>();
		//Changed the .Find above to include the word clone so it correctly finds the game manager -Jay
        if (!tutorialActive)
        {
            deer.enabled = true;
            wolf.enabled = true;
        }
    }

    void Update()
    {
		if ((Input.GetKeyDown(KeyCode.E) ||Input.GetKeyDown(KeyCode.Space)) && overLay.enabled && windowUp)
        {
			Advance ();
        }
    }

    public void StartDialogue(List<Dialogue> dialogue)
    {
        if (tutorialActive)
        {
			player.dialoguePaused = true;
			tsc.dialogueStop = true;
			tsc.CheckVisibility ();
			player.CheckIfICanMove ();
            convoCount++;
            animator.SetBool("IsOpen", true);
            sentences.Clear();
			windowUp = true;
			source.PlayOneShot (buttonHigh);
//            print("cleared sentences");

            nameText.text = dialogue[gm.language].name;

            foreach (string s in dialogue[gm.language].sentences)//dialogue[gm.language].sentences
            {
                sentences.Enqueue(s);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
		GameObject.Find ("DialogueBoxButtonGlow").GetComponent<UIGlowScript> ().SetColor (Color.clear, .05f);
        if (sentences.Count == 0)
        {
            EndDialogue();
			source.PlayOneShot (buttonLow);
//            print("J1");
//            print(sentences.Count);
            return;
        }
		source.PlayOneShot (buttonHigh);
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
		StartCoroutine (DelaySetColor ());
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
//        print("ClosingDBox");
		player.dialoguePaused = false;
		player.CheckIfICanMove ();
		tsc.dialogueStop = false;
		tsc.CheckVisibility ();
        if (convoCount == 1 && !givenStartingBerries)
        {
            givenStartingBerries = true;
            inventory.berryNum += 3;
            inventory.berryText.GetComponent < Text >().text = inventory.berryNum.ToString();
        }
		windowUp = false;
		tm.NextPhase ();
    }

	public void Advance() {
		if (typing == true) 
		{
			StopAllCoroutines();
			dialogueText.text = "";
			dialogueText.text += sentence;
			typing = false;
			StartCoroutine (DelaySetColor ());
			advanceDelayEndTime = Time.time + advanceDelay;
		} 
		else if (Time.time > advanceDelayEndTime)
		{
			DisplayNextSentence ();
		}
	}

	IEnumerator DelaySetColor() {
		yield return new WaitForSeconds (advanceDelay);
		GameObject.Find ("DialogueBoxButtonGlow").GetComponent<UIGlowScript> ().SetColor (Color.white, .1f);
	}
}