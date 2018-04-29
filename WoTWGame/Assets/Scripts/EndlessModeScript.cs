using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessModeScript : MonoBehaviour {
    public GameObject[] corruptableCircles = new GameObject[6];
    public float endlessTimer;
    public float endlessTimerStart;
    public float highScore;
    public float currentScore;
    public int numCorrupted;
	// Use this for initialization
	void Start () {
        endlessTimer = endlessTimerStart;
	}
	
	// Update is called once per frame
	void Update () {
        endlessTimer -= Time.deltaTime;
        if(endlessTimer <= 0)
        {
            Corrupt();
            endlessTimer = endlessTimerStart;
        }
	}

    void Corrupt()
    {
        if(numCorrupted < 6)
        {
            int target = Random.Range(0, 6);
            PylonCoreScript pcs = corruptableCircles[target].GetComponent<PylonCoreScript>();
            CorruptedPylonCoreScript cpcs = corruptableCircles[target].GetComponent<CorruptedPylonCoreScript>();
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
                cpcs.pylon2.GetComponent<SpriteRenderer>().color = Color.white;
                cpcs.pylon3.GetComponent<CorruptedPylonScript>().enabled = true;
                cpcs.pylon3.GetComponent<PylonScipt>().enabled = false;
                //same here
                cpcs.pylon3.GetComponent<SpriteRenderer>().color = Color.white;
                //not sure about these either
                cpcs.cRing1.GetComponent<SpriteRenderer>().color = GetComponent<PylonCoreScript>().spellColor;
                cpcs.cRing2.GetComponent<SpriteRenderer>().color = GetComponent<PylonCoreScript>().spellColor;
                cpcs.pylon3.GetComponent<PylonScipt>().corrupted = true;
                cpcs.timeStopTrigger.SetActive(false);
                cpcs.x3a.SetActive(true);
                cpcs.x3b.SetActive(true);
                cpcs.x3c.SetActive(true);
            }
            else
            {
                Corrupt();
            }
        }
    }
}
