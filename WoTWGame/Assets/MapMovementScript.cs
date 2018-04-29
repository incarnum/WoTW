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
	public RectTransform playerIcon;

	private float worldWidth;
	private float worldHeight;
	private float mapWidth;
	private float mapHeight;

	private float mapRelPosx;
	private float mapRelPosy;
	private float playerRelativePosY;
	// Use this for initialization
	void Start () {


		player = GameObject.Find ("Player").transform;
		myRT = GetComponent<RectTransform> ();


	}
	
	// Update is called once per frame
	void Update () {
		worldWidth = worldLowerRight.position.x - worldUpperLeft.position.x;
		worldHeight = worldUpperLeft.position.y - worldLowerRight.position.y;


		mapRelPosx = (player.position.x - worldUpperLeft.position.x) / worldWidth;
		mapRelPosy = (player.position.y - worldLowerRight.position.y) / worldHeight;
		myRT.anchoredPosition = new Vector2 ((-mapRelPosx + .5f) * myRT.sizeDelta.x, (-mapRelPosy + .5f) * myRT.sizeDelta.y);
	}

	public void PlacePlayer() {
		mapWidth = mapLowerRight.anchoredPosition.x - mapUpperLeft.anchoredPosition.x;
		mapHeight = mapUpperLeft.anchoredPosition.y - mapLowerRight.anchoredPosition.y;
		Vector2 playerIconPos = new Vector2 ((mapRelPosx - .5f) * mapWidth, (mapRelPosy - .5f) * mapHeight);
		playerIcon.anchoredPosition = playerIconPos;
	}
}
