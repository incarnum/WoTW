using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAnalyzerScript : MonoBehaviour {
	public int gameMode;

	public int[] shrubStates = new int[5];
	public int[] deerStates = new int[5];
	public int[] wolfStates = new int[5];
	public int[] rabbitStates = new int[5];
	public int[] owlStates = new int[5];

	//arrays that hold one piece of information for each pylon circle. A circle has the same index across all arrays.
	public int[] pylonHealth = new int[9];
	public int[] targetPylonInventory = new int[9];
	public int[] effectPylonInventory = new int[9];
	public int[] modifierPylonInventory = new int[9];

	public int[] playerInventory = new int[5];

	public int location;

	public int day;
	public int month;
	public int year;
	public float timeElapsed;
	public int cleansedNodes;
	public int convoCount;
	public int tutorialPhase;

	public ShrubPopulation shrubs;
	public DeerPopulation deer;
	public WolfPopulation wolves;
	public RabbitPopulation rabbits;
	public OwlPopulation owls;

	public List<CorruptedPylonCoreScript> cores;
	public List<PylonScipt> targetPylons;
	public List<PylonScipt> effectPylons;
	public List<PylonScipt> modifierPylons;

	public InventoryScript inv;
	public SimpleEcologyMasterScript eco;
	public DialogueManager dm;
	public TutorialUIManagerScript tm;
	public List<Transform> spawnPositions;
	public GameManagerScript gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager(Clone)").GetComponent<GameManagerScript>();
		gameMode = gm.gameMode;
		if (gm.loadFromSave) {
			Load ();
		} else if (gm.gameMode == 1 || gm.gameMode == 2 || gm.gameMode == 3) {
			SetUpStandard ();
		} else if (gm.gameMode == 4 || gm.gameMode == 4 || gm.gameMode == 6) {
			SetUpEndless ();
		}
	}

	public void Save() {
		//ecosystem
		shrubStates [0] = Mathf.RoundToInt(shrubs.pop);
		shrubStates [1] = Mathf.RoundToInt(shrubs.corruptedPop);
		shrubStates [2] = shrubs.sizeMod;
		shrubStates [3] = shrubs.speedMod;
		shrubStates [4] = shrubs.toughMod;
		deerStates [0] = Mathf.RoundToInt(deer.pop);
		deerStates [1] = Mathf.RoundToInt(deer.corruptedPop);
		deerStates [2] = deer.sizeMod;
		deerStates [3] = deer.speedMod;
		deerStates [4] = deer.toughMod;
		wolfStates [0] = Mathf.RoundToInt(wolves.pop);
		wolfStates [1] = Mathf.RoundToInt(wolves.corruptedPop);
		wolfStates [2] = wolves.sizeMod;
		wolfStates [3] = wolves.speedMod;
		wolfStates [4] = wolves.toughMod;
		rabbitStates [0] = Mathf.RoundToInt(rabbits.pop);
		rabbitStates [1] = Mathf.RoundToInt(rabbits.corruptedPop);
		rabbitStates [2] = rabbits.sizeMod;
		rabbitStates [3] = rabbits.speedMod;
		rabbitStates [4] = rabbits.toughMod;
		owlStates [0] = Mathf.RoundToInt(owls.pop);
		owlStates [1] = Mathf.RoundToInt(owls.corruptedPop);
		owlStates [2] = owls.sizeMod;
		owlStates [3] = owls.speedMod;
		owlStates [4] = owls.toughMod;

		//pylon circles
		for (int i = 0; i <= cores.Count - 1; i++) {
			pylonHealth [i] = cores [i].health;
		}
		for (int i = 0; i <= targetPylons.Count - 1; i++) {
			targetPylonInventory [i] = targetPylons [i].activeSelection;
		}
		for (int i = 0; i <= effectPylons.Count - 1; i++) {
			effectPylonInventory [i] = effectPylons [i].activeSelection;
		}
		for (int i = 0; i <= modifierPylons.Count - 1; i++) {
			modifierPylonInventory [i] = modifierPylons [i].activeSelection;
		}

		//player
		playerInventory [0] = inv.berryNum;
		playerInventory [1] = inv.antlerNum;
		playerInventory [2] = inv.fangNum;
		playerInventory [3] = inv.rabbitFootNum;
		playerInventory [4] = inv.owlFeatherNum;

		//progression
		cleansedNodes = dm.cleansedNodes;
		convoCount = dm.convoCount;
		tutorialPhase = tm.phase;

		//system
		day = System.DateTime.Now.Day;
		month = System.DateTime.Now.Month;
		year = System.DateTime.Now.Year;
		timeElapsed = Time.time;
		//doesn't take into account time from previous saves. I'm not adding that until I think of an actual use for this number

		LoadingManagerScript.SaveWorld (this, gameMode);

		//feeds this class with these variables into the function that writes data to a file
	}

	public void Load() {
		WorldData unloader = LoadingManagerScript.LoadWorld (gameMode);
		//calls a function that returns a class defined in LoadingManagerScript, which contains matching variables for everything in this script

		shrubs.pop = unloader.shrubStates [0];
		shrubs.corruptedPop = unloader.shrubStates[1];
		shrubs.sizeMod = unloader.shrubStates[2];
		shrubs.speedMod = unloader.shrubStates[3];
		shrubs.toughMod = unloader.shrubStates[4];

		deer.pop = unloader.deerStates [0];
		deer.corruptedPop = unloader.deerStates[1];
		deer.sizeMod = unloader.deerStates[2];
		deer.speedMod = unloader.deerStates[3];
		deer.toughMod = unloader.deerStates[4];

		wolves.pop = unloader.wolfStates [0];
		wolves.corruptedPop = unloader.wolfStates[1];
		wolves.sizeMod = unloader.wolfStates[2];
		wolves.speedMod = unloader.wolfStates[3];
		wolves.toughMod = unloader.wolfStates[4];

		rabbits.pop = unloader.rabbitStates [0];
		rabbits.corruptedPop = unloader.rabbitStates[1];
		rabbits.sizeMod = unloader.rabbitStates[2];
		rabbits.speedMod = unloader.rabbitStates[3];
		rabbits.toughMod = unloader.rabbitStates[4];

		owls.pop = unloader.owlStates [0];
		owls.corruptedPop = unloader.owlStates[1];
		owls.sizeMod = unloader.owlStates[2];
		owls.speedMod = unloader.owlStates[3];
		owls.toughMod = unloader.owlStates[4];

		if (deer.pop > 0) {
			deer.enabled = true;
			deer.DoStart ();
			eco.GetComponent<UIManager> ().ActivateDeer ();
			eco.tempShrubCapBool = false;
		}

		if (wolves.pop > 0) {
			wolves.enabled = true;
			wolves.DoStart ();
			eco.GetComponent<UIManager> ().ActivateWolves ();
		}

		if (rabbits.pop > 0) {
			rabbits.enabled = true;
			rabbits.DoStart ();
			eco.GetComponent<UIManager> ().ActivateRabbits ();
		}

		if (owls.pop > 0) {
			owls.enabled = true;
			owls.DoStart ();
			eco.GetComponent<UIManager> ().ActivateOwls ();
		}

		for (int i = 0; i <= cores.Count - 1; i++) {
			cores [i].LoadPylonCoreState(unloader.pylonHealth[i]);
		}
		for (int i = 0; i <= targetPylons.Count - 1; i++) {
			targetPylons [i].SelectCurrent(unloader.targetPylonInventory[i]);
			targetPylons [i].UpdateSprite();
		}
		for (int i = 0; i <= effectPylons.Count - 1; i++) {
			effectPylons [i].SelectCurrent(unloader.effectPylonInventory[i]);
			effectPylons [i].UpdateSprite();
		}
		for (int i = 0; i <= modifierPylons.Count - 1; i++) {
			modifierPylons [i].SelectCurrent(unloader.modifierPylonInventory[i]);
			modifierPylons [i].UpdateSprite();
		}

		inv.berryNum = unloader.playerInventory [0];
		inv.antlerNum = unloader.playerInventory [1];
		inv.fangNum = unloader.playerInventory [2];
		inv.rabbitFootNum = unloader.playerInventory [3];
		inv.owlFeatherNum = unloader.playerInventory [4];
		inv.UpdateNumbers ();
		inv.transform.position = spawnPositions [unloader.location].position;

		dm.cleansedNodes = unloader.cleansedNodes;
		dm.convoCount = unloader.convoCount;
		tm.NextPhase ();

		for (int i = 0; i < unloader.tutorialPhase; i++) {
			tm.NextPhase ();
		}
	}

	public void SetUpStandard() {
		inv.transform.position = spawnPositions [3].position;
		print ("STARTING NEW STANDARD GAME");



		shrubs.pop = 55;
		deer.pop = 50;
		wolves.pop = 45;

		deer.enabled = true;
		eco.GetComponent<UIManager> ().ActivateDeer ();
		eco.tempShrubCapBool = false;

		wolves.enabled = true;
		eco.GetComponent<UIManager> ().ActivateWolves ();

		if (gameMode >= 2) {
			rabbits.pop = 55;
			rabbits.enabled = true;
			eco.GetComponent<UIManager> ().ActivateRabbits ();

		}

		if (gameMode == 3) {
			owls.pop = 45;
			owls.enabled = true;
			eco.GetComponent<UIManager> ().ActivateOwls ();

		}

		for (int i = 0; i < 10; i++) {
			tm.NextPhase ();
		}
		dm.convoCount = 100;
	}

	public void SetUpEndless() {
		inv.transform.position = spawnPositions [3].position;
		print ("STARTING NEW ENDLESS GAME");

		shrubs.pop = 55;
		deer.pop = 50;
		wolves.pop = 45;

		deer.enabled = true;
		eco.GetComponent<UIManager> ().ActivateDeer ();
		eco.tempShrubCapBool = false;

		wolves.enabled = true;
		eco.GetComponent<UIManager> ().ActivateWolves ();

		if (gameMode >= 5) {
			rabbits.pop = 55;
			rabbits.enabled = true;
			eco.GetComponent<UIManager> ().ActivateRabbits ();

		}

		if (gameMode == 6) {
			owls.pop = 45;
			owls.enabled = true;
			eco.GetComponent<UIManager> ().ActivateOwls ();

		}

		for (int i = 0; i < 10; i++) {
			tm.NextPhase ();
		}
		dm.convoCount = 100;
	}
}
