using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class LoadingManagerScript {

	public static void SaveWorld(WorldAnalyzerScript world, int gameMode){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream;
		switch (gameMode)
		{
		case 0:
			stream = new FileStream (Application.persistentDataPath + "/story.woods", FileMode.Create);
			break;
		case 1:
			stream = new FileStream (Application.persistentDataPath + "/standard.woods", FileMode.Create);
			break;
		case 2:
			stream = new FileStream (Application.persistentDataPath + "/standard.woods", FileMode.Create);
			break;
		case 3:
			stream = new FileStream (Application.persistentDataPath + "/standard.woods", FileMode.Create);
			break;
		case 4:
			stream = new FileStream (Application.persistentDataPath + "/endless.woods", FileMode.Create);
			break;
		case 5:
			stream = new FileStream (Application.persistentDataPath + "/endless.woods", FileMode.Create);
			break;
		case 6:
			stream = new FileStream (Application.persistentDataPath + "/endless.woods", FileMode.Create);
			break;
		default: 
			stream = new FileStream (Application.persistentDataPath + "/story.woods", FileMode.Create);
			Debug.Log ("Couldn't find that game mode");
			break;
		}
		WorldData data = new WorldData (world);

		bf.Serialize (stream, data);
		stream.Close ();

		Debug.Log ("Successfully saved file of game mode " + gameMode);
	}

	public static WorldData LoadWorld(int gameMode) {
		string saveFileName;
		switch (gameMode)
		{
		case 0:
			saveFileName = "/story.woods";
			break;
		case 1:
			saveFileName = "/standard.woods";			
			break;
		case 2:
			saveFileName = "/standard.woods";
			break;
		case 3:
			saveFileName = "/standard.woods";
			break;
		case 4:
			saveFileName = "/endless.woods";
			break;
		case 5:
			saveFileName = "/endless.woods";
			break;
		case 6:
			saveFileName = "/endless.woods";
			break;
		default: 
			Debug.Log ("I don't recognize that file name");
			saveFileName = "/story.woods";
			break;
		}

		if (File.Exists (Application.persistentDataPath + saveFileName)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + saveFileName, FileMode.Open);

			WorldData data = bf.Deserialize (stream) as WorldData;

			stream.Close ();
			return data;
		} else {
			Debug.Log ("Couldn't find file");
			return null;
		}

		Debug.Log ("Successfully loaded file from " + saveFileName);
	}

}

[Serializable]
public class WorldData {
	
	public int[] shrubStates = new int[5];
	public int[] deerStates = new int[5];
	public int[] wolfStates = new int[5];
	public int[] rabbitStates = new int[5];
	public int[] owlStates = new int[5];

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

	public WorldData(WorldAnalyzerScript world) {
		shrubStates = world.shrubStates;
		deerStates = world.deerStates;
		wolfStates = world.wolfStates;
		rabbitStates = world.rabbitStates;
		owlStates = world.owlStates;

		pylonHealth = world.pylonHealth;
		targetPylonInventory = world.targetPylonInventory;
		effectPylonInventory = world.effectPylonInventory;
		modifierPylonInventory = world.modifierPylonInventory;

		playerInventory = world.playerInventory;

		location = world.location;

		day = world.day;
		month = world.month;
		year = world.year;
		timeElapsed = world.timeElapsed;

		cleansedNodes = world.cleansedNodes;
		convoCount = world.convoCount;
		tutorialPhase = world.tutorialPhase;

	}
}
