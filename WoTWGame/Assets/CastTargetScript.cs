using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTargetScript : MonoBehaviour {
	private GameObject player;
	public int animalType;
    public int ManaGain;
    public bool powered;
    public int currentTime;
    public int TimeOut;
    
    // Use this for initialization
    void Start () {
		player = GameObject.Find ("Player");
        currentTime = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (powered == false)
        {
            currentTime += 1;
            if(currentTime >= TimeOut)
            {
                currentTime = 0;
                powered = true;
            }
        }
	}

	void OnMouseDown () {
		if (player.GetComponent<InventoryScript>().ManaCount < player.GetComponent<InventoryScript>().MaxMana && powered) {
            player.GetComponent<InventoryScript>().ManaCount += ManaGain;
            powered = false;
        }
	}
}
