using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class UniversalSaverScript {

	public static void SaveUniverse(GameManagerScript universe){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream;
		stream = new FileStream (Application.persistentDataPath + "/main.woods", FileMode.Create);
		UniversalData data = new UniversalData (universe);

		bf.Serialize (stream, data);
		stream.Close ();

	}

	public static UniversalData LoadUniverse() {
		if (File.Exists (Application.persistentDataPath + "/main.woods")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/main.woods", FileMode.Open);

			UniversalData data = bf.Deserialize (stream) as UniversalData;

			stream.Close ();
			return data;
		} else {
			Debug.Log ("Couldn't find file");
			return null;
		}

		Debug.Log ("Loaded universal data");
	}

}

[Serializable]
public class UniversalData {
	public bool musicBool;
	public bool soundBool;
	public int language;
	public bool hasBeatenGame;
    public int highScore;

	public UniversalData(GameManagerScript gms) {
		musicBool = gms.musicBool;
		soundBool = gms.soundBool;
		language = gms.language;
		hasBeatenGame = gms.hasBeatenGame;
        highScore = gms.highScore;
	}
}
