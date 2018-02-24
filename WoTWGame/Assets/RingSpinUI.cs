using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingSpinUI : MonoBehaviour {
	public float speed;
	public float boostSpeed;
	public float boostDuration;
	private bool speedBoost;
	private float boostEnd;
	private RectTransform rt;
	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
	}

	// Update is called once per frame
	void Update () {
		if (speedBoost == false) {
			rt.Rotate (Vector3.forward * Time.deltaTime * speed);
		} else {
			rt.Rotate (Vector3.forward * Time.deltaTime * boostSpeed);
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
