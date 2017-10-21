using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellMakerButtonScript : MonoBehaviour, IPointerClickHandler {
	public int button;
	private SpellMakerScript makerScript;
	public Animator ing1;
	public Animator ing2;
	public Animator ing3;
	// Use this for initialization
	void Start () {
		makerScript = GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(PointerEventData eventData){
		switch (button) {
		case 1:
			Debug.Log ("1");
			makerScript.effect = 0;
			foreach (GameObject garfield in makerScript.category1) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing1.SetTrigger ("berry");
			break;
		case 2:
			Debug.Log ("2");
			makerScript.effect = 1;
			foreach (GameObject garfield in makerScript.category1) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing1.SetTrigger ("antler");
			break;
		case 3:
			Debug.Log ("3");
			makerScript.effect = 2;
			foreach (GameObject garfield in makerScript.category1) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing1.SetTrigger ("fang");
			break;
		case 4:
			Debug.Log ("4");
			makerScript.target = 0;
			foreach (GameObject garfield in makerScript.category2) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing2.SetTrigger ("berry");
			break;
		case 5:
			Debug.Log ("5");
			makerScript.target = 1;
			foreach (GameObject garfield in makerScript.category2) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing2.SetTrigger ("antler");
			break;
		case 6:
			Debug.Log ("6");
			makerScript.target = 2;
			foreach (GameObject garfield in makerScript.category2) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing2.SetTrigger ("fang");
			break;
		case 7:
			Debug.Log ("7");
			makerScript.strength = 0;
			foreach (GameObject garfield in makerScript.category3) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing3.SetTrigger ("antler");
			break;
		case 8:
			Debug.Log ("8");
			makerScript.strength = -4;
			foreach (GameObject garfield in makerScript.category3) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing3.SetTrigger ("fang");
			break;
		case 9:
			Debug.Log ("9");
			makerScript.strength = 4;
			foreach (GameObject garfield in makerScript.category3) {
				garfield.GetComponentInChildren<Image> ().color = Color.gray;
			}
			ing3.SetTrigger ("berry");
			break;
		}
		GetComponentInChildren<Image> ().color = Color.white;
		makerScript.PredictSpell ();
		makerScript.CheckCastability ();
	}
}
