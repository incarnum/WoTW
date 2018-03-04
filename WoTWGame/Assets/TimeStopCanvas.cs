using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopCanvas : MonoBehaviour {
	public bool pauseStop;
	public bool areaStop;
	public bool dialogueStop;
	private float targetOpacity;
	private bool changing;
	private CanvasGroup cg;
	// Use this for initialization
	void Start () {
		cg = GetComponent<CanvasGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (changing) {
			if (targetOpacity == 1 && cg.alpha < 1) {
				cg.alpha += 3 * Time.deltaTime;
			} else if (targetOpacity == 0 && cg.alpha > 0) {
				cg.alpha -= 3 * Time.deltaTime;
			}
		}
	}

	public void CheckVisibility() {
		if (!pauseStop && !areaStop && !dialogueStop) {
			targetOpacity = 0f;
			changing = true;
		} else {
			targetOpacity = 1f;
			changing = true;
		}
	}
}
