using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFXController : MonoBehaviour {
	public RingSpinScript ring1;
	public RingSpinScript ring2;
	public Animator castSpellRing;
	public centerStoneGlowScript pylonGlow1;
	public centerStoneGlowScript pylonGlow2;
	public centerStoneGlowScript pylonGlow3;
	public Animator pylonSwirl1;
	public Animator pylonSwirl2;
	public Animator pylonSwirl3;
	public Animator centralSwirl;
	private bool playing;
	private int phase;
	private float startTime;
	private bool played1;
	private bool played2;
	private bool played3;
	private bool played4;
	private bool played5;
	private bool played6;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			if (played1 == false && Time.time > startTime + 0f) {
				pylonSwirl1.SetTrigger ("swirl");
				pylonGlow1.SetColor (Color.white);
				pylonGlow2.SetColor (Color.white);
				pylonGlow3.SetColor (Color.white);
				played1 = true;
			}
			if (played2 == false && Time.time > startTime + .2f) {
				pylonSwirl2.SetTrigger ("swirl");
				played2 = true;
			}
			if (played3 == false && Time.time > startTime + .4f) {
				pylonSwirl3.SetTrigger ("swirl");
				played3 = true;
			}
			if (played4 == false && Time.time > startTime + .6f) {
				centralSwirl.SetTrigger ("swirl");
				played4 = true;
			}

			if (played5 == false && Time.time > startTime + 1.2f) {
				
				castSpellRing.SetTrigger ("Cast2");
				played5 = true;
			}

			if (played6 == false && Time.time > startTime + 1.6f) {
				pylonGlow1.SetColor (Color.clear);
				pylonGlow2.SetColor (Color.clear);
				pylonGlow3.SetColor (Color.clear);
				played6 = true;
				playing = false;
			}
		}
	}

	public void playSpellCastEffect() {
		ring1.SpeedBoost();
		ring2.SpeedBoost();




		playing = true;
		played1 = false;
		played2 = false;
		played3 = false;
		played4 = false;
		played5 = false;
		played6 = false;
		startTime = Time.time;
	}

	public void playCorrSpellEffect() {
		castSpellRing.SetTrigger ("Cast2");
		ring1.SpeedBoost();
		ring2.SpeedBoost();
	}
}
