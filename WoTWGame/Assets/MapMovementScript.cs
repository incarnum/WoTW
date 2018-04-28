using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMovementScript : MonoBehaviour {
	private RectTransform myRT;
	public RectTransform mapUpperLeft;
	public RectTransform mapLowerRight;
	public Transform worldUpperLeft;
	public Transform worldLowerRight;
	public Transform player;

	private float worldWidth;
	private float worldHeight;
	private float mapWidth;
	private float mapHeight;
	// Use this for initialization
	void Start () {


		player = GameObject.Find ("Player").transform;
		myRT = GetComponent<RectTransform> ();

	}
	
	// Update is called once per frame
	void Update () {
		worldWidth = worldLowerRight.localPosition.x - worldUpperLeft.localPosition.x;
		worldHeight = worldUpperLeft.localPosition.y - worldLowerRight.localPosition.y;

		float mapRelPosx = (player.position.x - worldUpperLeft.position.x) / worldWidth;
		float mapRelPosy = (player.position.y - worldLowerRight.position.y) / worldHeight;

		myRT.anchoredPosition = new Vector2 ((-mapRelPosx + .5f) * myRT.sizeDelta.x, (-mapRelPosy + .5f) * myRT.sizeDelta.y);
	}
}
