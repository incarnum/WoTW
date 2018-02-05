using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUIScript : MonoBehaviour {
    public Text corrPop, pop, corrPopChange, popChange, leftChange, rightChange;
    public basePopulation bPop;
	// Use this for initialization
	void Start () {
        if (bPop.corrupting)
        {
            corrPopChange.text += bPop.corruptionRate.ToString("0");
        }
        else
        {
            corrPopChange.text = "0";
        }
        pop.text = bPop.pop.ToString("0");
        corrPop.text = bPop.corruptedPop.ToString("0");
	}
	
	// Update is called once per frame
	void Update () {
        if (bPop.corrupting)
        {
            corrPopChange.text = "";
            corrPopChange.text += bPop.corruptionRate.ToString("0");
        }
        else
        {
            corrPopChange.text = "0";
        }
        pop.text = "";
        pop.text = bPop.pop.ToString("0");
        corrPop.text = "";
        corrPop.text = bPop.corruptedPop.ToString("0");
    }
}
