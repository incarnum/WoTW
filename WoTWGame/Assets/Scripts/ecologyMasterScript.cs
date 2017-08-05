using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecologyMasterScript : MonoBehaviour {
	public float shrubPop;
	public float deerPop;
	public float wolfPop;
	public float mousePop;
	public float owlPop;
	public float songbirdPop;

	public float shrubPopT;
	public float deerPopT;
	public float wolfPopT;
	public float mousePopT;
	public float owlPopT;
	public float songbirdPopT;

	public float shrubPopR;
	public float deerPopR;

	public float delay;

	public List<Vector2> shrubPoints;
	public List<Vector2> deerPoints;

	private float shrubTickCounter;
	private float deerTickCounter;

	public float posChangeValue;
	public float negChangeValue;
	public float randoBonus;
	// Use this for initialization
	void Start () {
		//		shrubPopT = shrubPop;
		//		deerPopT = deerPop;
		//		wolfPopT = wolfPop;
		//		mousePopT = mousePop;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			shrubPop = 10;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			shrubPop = 20;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			shrubPop = 30;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			shrubPop = 40;
		}
		Ecologize5 ();
	}

	void Ecologize () {
		if (shrubPop > deerPopT) {
			shrubPop += (shrubPop - deerPopT)/shrubPop * Time.deltaTime;
		}

		if (mousePop > wolfPopT) {
			mousePop += (mousePop - wolfPopT)/mousePop * Time.deltaTime;
		}

		if (deerPop > shrubPopT) {
			deerPop -= (deerPop - shrubPopT) * negChangeValue * .5f * Time.deltaTime;
			shrubPop -= (deerPop - shrubPopT) * negChangeValue * Time.deltaTime;
		} else if (deerPop < shrubPopT) {
			deerPop += (shrubPopT - deerPop) * posChangeValue * Time.deltaTime;
		}

		if (wolfPop > mousePopT + deerPopT) {
			wolfPop -= (wolfPop - mousePopT - deerPopT) * negChangeValue * Time.deltaTime;
			if (deerPopT > mousePopT) {
				deerPop -= (wolfPop - deerPopT - mousePopT) * negChangeValue * Time.deltaTime;
				mousePop -= (wolfPop - deerPopT - mousePopT) * negChangeValue * Time.deltaTime;
			} else if (mousePopT >= deerPopT) {
				mousePop -= (wolfPop - deerPopT - mousePopT) * negChangeValue * .75f * Time.deltaTime;
				deerPop -= (wolfPop - deerPopT - mousePopT) * negChangeValue * .25f * Time.deltaTime;
			}
		} else if (wolfPop < mousePopT + deerPopT) {
			wolfPop += ((deerPopT + mousePopT) - wolfPop) * posChangeValue * Time.deltaTime;
		}

		shrubPopT += (shrubPop - shrubPopT) * .5f * Time.deltaTime;
		deerPopT += (deerPop - deerPopT) * .5f * Time.deltaTime;
		wolfPopT += (wolfPop - wolfPopT) * .5f * Time.deltaTime;
		mousePopT += (mousePop - mousePopT) * .5f * Time.deltaTime;
		owlPopT += (owlPop - owlPopT) * .5f * Time.deltaTime;
		songbirdPopT += (songbirdPop - songbirdPopT) * .5f * Time.deltaTime;

		if (shrubPop < 0)
			shrubPop = 0;
		if (deerPop < 0)
			deerPop = 0;
		if (wolfPop < 0)
			wolfPop = 0;
		if (mousePop < 0)
			mousePop = 0;
		if (owlPop < 0)
			owlPop = 0;
		if (songbirdPop < 0)
			songbirdPop = 0;
		UpdateBars ();
	}

	void Ecologize2() {

		shrubPopT += (shrubPop - shrubPopT) * .2f * Time.deltaTime;
		deerPopT += (deerPop - deerPopT) * .2f * Time.deltaTime;
		wolfPopT += (wolfPop - wolfPopT) * .2f * Time.deltaTime;
		mousePopT += (mousePop - mousePopT) * .2f * Time.deltaTime;
		owlPopT += (owlPop - owlPopT) * .2f * Time.deltaTime;
		songbirdPopT += (songbirdPop - songbirdPopT) * .2f * Time.deltaTime;

		//shrubPop -= shrubPop * .1f * Time.deltaTime;
		shrubPop += (shrubPop - deerPopT) * .1f * Time.deltaTime;

		//deerPop -= deerPop * .1f * Time.deltaTime;
		deerPop += (shrubPopT - deerPop) * .3f * Time.deltaTime;

		//		wolfPop -= wolfPop * .1f * Time.deltaTime;
		//		if (deerPopT >= mousePopT) {
		//			deerPop -= (wolfPop - deerPopT)/deerPop;
		//			wolfPop += (wolfPop - deerPopT) / wolfPop;
		//		} else if (deerPopT < mousePopT) {
		//			mousePop -= (wolfPop - mousePopT)/mousePop;
		//			wolfPop += (wolfPop - mousePopT) / wolfPop;
		//		}

		UpdateBars ();

	}

	void Ecologize3() {
		shrubPop += shrubPopT * Time.deltaTime;
		deerPop += deerPopT * Time.deltaTime;

		shrubPopT += (shrubPop - deerPop)/shrubPop * Time.deltaTime * 10f;
		deerPopT += (shrubPop - deerPop)/deerPop * Time.deltaTime * 10f;





		if (shrubPop < 0)
			shrubPop = 0;
		if (deerPop < 0)
			deerPop = 0;
		if (wolfPop < 0)
			wolfPop = 0;
		if (mousePop < 0)
			mousePop = 0;
		if (owlPop < 0)
			owlPop = 0;
		if (songbirdPop < 0)
			songbirdPop = 0;
		UpdateBars ();
	}

	void Ecologize4() {
		shrubPopT = 1f / (Mathf.Ceil(Mathf.Abs(shrubPop - 50) * 2));
		deerPopT = 1f / (Mathf.Ceil(Mathf.Abs(deerPop - 50) * 2));

		shrubPop += ((shrubPop - deerPop)/shrubPop) * Time.deltaTime * shrubPopT * 1000f;
		deerPop += ((shrubPop - deerPop)/deerPop) * Time.deltaTime * deerPopT * 1000f;

		if (shrubPop < 0)
			shrubPop = 0;
		if (deerPop < 0)
			deerPop = 0;
		if (wolfPop < 0)
			wolfPop = 0;
		if (mousePop < 0)
			mousePop = 0;
		if (owlPop < 0)
			owlPop = 0;
		if (songbirdPop < 0)
			songbirdPop = 0;
		UpdateBars ();
	}


	void UpdateBars() {
		GameObject.Find ("ShrubBar").GetComponent<barScript> ().SetFillSizeValue (shrubPop / 100f);
		GameObject.Find ("DeerBar").GetComponent<barScript> ().SetFillSizeValue (deerPop / 100f);
		GameObject.Find ("WolfBar").GetComponent<barScript> ().SetFillSizeValue (wolfPop / 100f);
		GameObject.Find ("MouseBar").GetComponent<barScript> ().SetFillSizeValue (mousePop / 100f);
	}


	void Ecologize5() {
		shrubTickCounter += 1;
		deerTickCounter += 1;
		UpdateBars ();
		if (shrubTickCounter >= 20) {
			Vector2 v2 = new Vector2(shrubPop, Time.time);
			shrubPoints.Add(v2);
			shrubTickCounter = 0;
		}
		if (Time.time > delay) {
			if (Time.time - delay > shrubPoints [0].y) {
				shrubPopT = shrubPoints [0].x;
				shrubPoints.RemoveAt (0);
			}
		}


		if (deerTickCounter >= 20) {
			Vector2 v2 = new Vector2(deerPop, Time.time);
			deerPoints.Add(v2);
			deerTickCounter = 0;
		}
		if (Time.time > delay) {
			if (Time.time - delay > deerPoints [0].y) {
				deerPopT = deerPoints [0].x;
				deerPoints.RemoveAt (0);
			}
		}
		if (shrubPop < deerPop) {
			shrubPop -= 3 * Time.deltaTime * (deerPop - shrubPop) *.075f;
		} else {
			shrubPop += 3 * Time.deltaTime * (shrubPop - deerPop) *.075f;
		}

		if (shrubPopT < deerPopT) {
			deerPop -= 5 * Time.deltaTime * (deerPopT - shrubPopT) *.075f;
		} else if (Time.time > delay){
			deerPop += 5 * Time.deltaTime * (shrubPopT - deerPopT) *.075f;
		}

		if (shrubPop < 50) {
			shrubPop += (Mathf.Abs (shrubPop - 50f) * Time.deltaTime * .05f);
		}

		if (deerPop < 50) {
			deerPop += (Mathf.Abs (deerPop - 50f) * Time.deltaTime * .05f);
		}




		//shrubPopT = 1f / (Mathf.Ceil(Mathf.Abs(shrubPop - 50) * 2));
		//deerPopT = 1f / (Mathf.Ceil(Mathf.Abs(deerPop - 50) * 2));


		//shrubPopR += (shrubPopT - deerPopT) * 1f * Time.deltaTime;
		//deerPopR += (shrubPopT - deerPopT) * 2f * Time.deltaTime;

		//shrubPop += shrubPopR * .0005f;
		//deerPop += deerPopR * .0005f;

	}
}
