using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour {
    public Camera mapCamera;
    private bool isWorldMap;
    public Vector3 mapPosition;
    public GameObject MapRender;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //checks to see if world map active
            if (!isWorldMap)
            {
                //moves to world map position
                mapCamera.transform.position = new Vector3(28.4f, -55.1f, -900f);
                mapCamera.orthographicSize = 67;
                isWorldMap = true;
                MapRender.transform.localPosition = Vector3.zero;
                MapRender.transform.localScale = new Vector3(0.4f, 0.4f);
            }
            else
            {
                //moves to last minimap location
                MoveCamera(mapPosition);
                isWorldMap = false;
                MapRender.transform.localPosition = new Vector3(272f, 121f);
                MapRender.transform.localScale = new Vector3(0.2f, 0.2f);
            }
        }
    }

    public void MoveCamera(Vector3 newPos)
    {
        //moves to new location
        //activated in MiniMapTrigger.cs and Update
        transform.position = mapPosition;
        mapCamera.orthographicSize = 17.5f;
    }
}
