using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTrigger : MonoBehaviour
{
    public GameObject mapCamera;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            mapCamera.GetComponent<MiniMapScript>().mapPosition = transform.position;
            mapCamera.GetComponent<MiniMapScript>().MoveCamera(mapCamera.GetComponent<MiniMapScript>().mapPosition);
        }
    }
}