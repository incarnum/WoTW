using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessModeScript : MonoBehaviour {
    public GameObject[] corruptableCircles = new GameObject[6];
	public List<GameObject> uncorruptedCircles;

    public float endlessTimer;
    public float endlessTimerStart;
    public int highScore;
	public int currentScore;
    public int numCorrupted;
	public bool timerRunning;

	public MapIconManager mapIcon;
	// Use this for initialization
	void Start () {
		highScore = GameManagerScript.instance.highScore;
		endlessTimer = Mathf.Infinity;
		for (int i = 0; i < corruptableCircles.Length; i++) {
			if (corruptableCircles [i].GetComponent<CorruptedPylonCoreScript> ().isActiveAndEnabled == true) {
				numCorrupted += 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		numCorrupted = 0;
		for (int i = 0; i < corruptableCircles.Length; i++) {
			if (corruptableCircles [i].GetComponent<CorruptedPylonCoreScript> ().isActiveAndEnabled == true) {
				numCorrupted += 1;
			}
		}


        endlessTimer -= Time.deltaTime;
		if(endlessTimer <= 0 && timerRunning == true)
        {
            Corrupt();
            
        }

		if (numCorrupted < 6 && timerRunning == false) {
			endlessTimer = endlessTimerStart;
			timerRunning = true;
		}
	}

    void Corrupt()
    {


        if(numCorrupted < 6)
        {

			print ("trying to corrupt");
			uncorruptedCircles.Clear ();
			for (int i = 0; i < corruptableCircles.Length; i++) {
				if (corruptableCircles [i].GetComponent<CorruptedPylonCoreScript> ().isActiveAndEnabled == false) {
					uncorruptedCircles.Add (corruptableCircles [i]);
				}
			}
			timerRunning = false;

			int target = Random.Range(0, uncorruptedCircles.Count);
            PylonCoreScript pcs = uncorruptedCircles[target].GetComponent<PylonCoreScript>();
            CorruptedPylonCoreScript cpcs = uncorruptedCircles[target].GetComponent<CorruptedPylonCoreScript>();
            if (pcs.enabled == true)
            {
                pcs.enabled = false;
                cpcs.enabled = true;
                cpcs.health = 3;
                numCorrupted += 1;
                cpcs.pylon1.GetComponent<PylonScipt>().corrupted = true;
                cpcs.pylon2.GetComponent<CorruptedPylonScript>().enabled = true;
                cpcs.pylon2.GetComponent<PylonScipt>().enabled = false;
                //color needs changed, not sure what kind of purple it is supposed to be
				cpcs.pylon2.GetComponent<SpriteRenderer>().color = pcs.corrColor;
                cpcs.pylon3.GetComponent<CorruptedPylonScript>().enabled = true;
                cpcs.pylon3.GetComponent<PylonScipt>().enabled = false;
                //same here
				cpcs.pylon3.GetComponent<SpriteRenderer>().color = pcs.corrColor;
                //not sure about these either
                cpcs.cRing1.GetComponent<SpriteRenderer>().color = pcs.corrColor;
				cpcs.cRing2.GetComponent<SpriteRenderer>().color = pcs.corrColor;
                cpcs.pylon3.GetComponent<PylonScipt>().corrupted = true;
                cpcs.timeStopTrigger.SetActive(false);
                cpcs.x3a.SetActive(true);
                cpcs.x3b.SetActive(true);
                cpcs.x3c.SetActive(true);
				cpcs.corruptionNode.SetActive (true);
				mapIcon.UpdateMinimapIcons ();
            }
            else
            {
//                Corrupt();
//				this kind of recursion won't work in c#
            }
        }
    }
}
