using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlideScript : MonoBehaviour {
	public Vector2 startPosition;
	public Vector2 targetPosition;
	public float startTime;
	public float moveDuration;
	private bool moving;
	private RectTransform rt;
	// Use this for initialization
	void Awake () {
		rt = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			rt.localPosition = Vector2.Lerp (startPosition, targetPosition, (Time.time - startTime) / moveDuration);
			if (Time.time - startTime >= moveDuration) {
				moving = false;
			}
		}
	}

	public void Move(Vector2 target, float md) {
		moveDuration = md;
		startTime = Time.time;
		targetPosition = target;
		startPosition = GetComponent<RectTransform> ().localPosition;
		moving = true;
	}
}
