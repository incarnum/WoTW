using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour {
    private Camera mapCamera;
	// Use this for initialization
	void Start () {
        mapCamera = GetComponent<Camera>();
        print(mapCamera.aspect);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
