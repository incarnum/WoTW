using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUIScript : MonoBehaviour {
    public Text corrPop, pop, corrPopChange, popChange, leftChange, rightChange;
    public basePopulation bPop;
	public RectTransform barFill;
	public RectTransform corrBarFill;
	private bool moving;
	private Vector3 targetLocation;
	private Vector3 startLocation;
	private float moveStartTime;
	public float moveDuration;
	public GameObject UpperLayer;
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
			
	}
	
	// Update is called once per frame
	void Update () {
        if (bPop.corrupting)
        {
//            corrPopChange.text = "";
//            corrPopChange.text += bPop.corruptionRate.ToString("0");
        }
        else
        {
            corrPopChange.text = "0";
        }
//        pop.text = "";
//        pop.text = bPop.pop.ToString("0");
//        corrPop.text = "";
//        corrPop.text = bPop.corruptedPop.ToString("0");
		barFill.localPosition = new Vector3(0, .93f * bPop.biomass -115, 0);
		corrBarFill.localPosition = new Vector3(0, .93f * bPop.corruptedBiomass -115, 0);

		if (moving) {
			GetComponent<RectTransform> ().localPosition = Vector3.Lerp (startLocation, targetLocation, (Time.time - moveStartTime) / moveDuration);
			if ((Time.time - moveStartTime) >= moveDuration) {
				moving = false;
			}
		}
    }

	public void Move(Vector3 targ) {
		moving = true;
		moveStartTime = Time.time;
		startLocation = GetComponent<RectTransform> ().localPosition;
		targetLocation = targ;
	}
}
