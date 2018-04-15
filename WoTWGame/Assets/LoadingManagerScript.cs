using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class LoadingManagerScript {

	public static void SaveWorld(WorldAnalyzerScript world){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath + "/world.sav", FileMode.Create);

		WorldData data = new WorldData (world);

		bf.Serialize (stream, data);
		stream.Close ();
	}

	public static WorldData LoadWorld() {
		if (File.Exists (Application.persistentDataPath + "/world.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/world.sav", FileMode.Open);

			WorldData data = bf.Deserialize (stream) as WorldData;

			stream.Close ();
			return data;
		} else {
			Debug.Log ("Couldn't find file");
			return null;
		}
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

	}
}
