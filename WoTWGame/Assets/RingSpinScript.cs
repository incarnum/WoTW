using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpinScript : MonoBehaviour {
	public float speed;
	public float boostSpeed;
	public float boostDuration;
	private bool speedBoost;
	private float boostEnd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (speedBoost == false) {
			transform.Rotate (Vector3.forward * Time.deltaTime * speed);
		} else {
			transform.Rotate (Vector3.forward * Time.deltaTime * boostSpeed);
			if (Time.time > boostEnd) {
				speedBoost = false;
			}
		}
	}

	public void SpeedBoost () {
		speedBoost = true;
		boostEnd = Time.time + boostDuration;
	}
}
