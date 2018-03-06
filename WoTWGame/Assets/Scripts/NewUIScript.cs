using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewUIScript : MonoBehaviour {
    public Text corrPop, pop, corrPopChange, popChange, leftChange, rightChange;
    public basePopulation bPop;
	public Image barFill;
	public Image corrBarFill;
	public ScrollingRawImage barEffect1;
	public ScrollingRawImage barEffect2;
	public ScrollingRawImage corrBarEffect1;
	public ScrollingRawImage corrBarEffect2;
	public ScrollingRawImage topEffect1;
	public ScrollingRawImage topEffect2;
	public ScrollingRawImage rightEffect1;
	public ScrollingRawImage rightEffect2;
	public ScrollingRawImage leftEffect1;
	public ScrollingRawImage leftEffect2;
	private bool moving;
	private Vector3 targetLocation;
	private Vector3 startLocation;
	private float moveStartTime;
	public float moveDuration;
	public GameObject UpperLayer;
	public GameObject MouseoverInfo;
	// Use this for initialization
	void Start () {
//        if (bPop.corrupting)
//        {
//            corrPopChange.text += bPop.corruptionRate.ToString("0");
//        }
//        else
//        {
//            corrPopChange.text = "0";
//        }
			
	}
	
	// Update is called once per frame
	void Update () {
//        if (bPop.corrupting)
//        {
////            corrPopChange.text = "";
////            corrPopChange.text += bPop.corruptionRate.ToString("0");
//        }
//        else
//        {
//            corrPopChange.text = "0";
//        }
//        pop.text = "";
//        pop.text = bPop.pop.ToString("0");
//        corrPop.text = "";
//        corrPop.text = bPop.corruptedPop.ToString("0");
		barFill.fillAmount = bPop.biomass / 100f;
		corrBarFill.fillAmount = bPop.corruptedBiomass / 100f;
		barEffect1.verticalSpeed = bPop.simpleRateOfChange / 100f;
		barEffect2.verticalSpeed = bPop.simpleRateOfChange / 50f;
		topEffect1.verticalSpeed = bPop.simpleRateOfChange / 100f;
		topEffect2.verticalSpeed = bPop.simpleRateOfChange / 50f;
		rightEffect1.verticalSpeed = bPop.rightChange / 100f;
		rightEffect2.verticalSpeed = bPop.rightChange / 50f;
		leftEffect1.verticalSpeed = bPop.leftChange / 100f;
		leftEffect2.verticalSpeed = bPop.leftChange / 50f;
		if (bPop.simpleRateOfChange >= 0) {
			corrBarEffect1.verticalSpeed = .4f;
			corrBarEffect2.verticalSpeed = .2f;
		} else {
			corrBarEffect1.verticalSpeed = bPop.simpleRateOfChange / 50f;
			corrBarEffect2.verticalSpeed = bPop.simpleRateOfChange / 25f;
		}



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

	public void UpdateBarEffects() {

	}

//	public void OnPointerEnter(PointerEventData eventData) {
//		MouseoverInfo.SetActive (true);
//	}
//
//	public void OnPointerExit(PointerEventData eventData) {
//		MouseoverInfo.SetActive (false);
//	}
//
//	public void OnDisable() {
//		MouseoverInfo.SetActive (false);
//	}
}
