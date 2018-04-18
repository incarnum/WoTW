using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMap : MonoBehaviour {
    public GameObject MiniMap;
    public GameObject WorldMap;
    public bool isWorldMap;
    public PlayerControllerScript pcs;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(!pcs.paused && !pcs.popBarPaused && !pcs.dialoguePaused && !pcs.pylonPaused)
            {
                //checks to see if world map active
                if (!isWorldMap)
                {
                    //sets world map camera active and disables mini map
                    MiniMap.SetActive(false);
                    WorldMap.SetActive(true);
                    isWorldMap = true;
                    pcs.mapPaused = true;
                    pcs.CheckIfICanMove();
                }
                else
                {
                    //sets mini map camera active and disables world map
                    MiniMap.SetActive(true);
                    WorldMap.SetActive(false);
                    isWorldMap = false;
                    pcs.mapPaused = false;
                    pcs.CheckIfICanMove();
                }
            }
        }
    }
}
