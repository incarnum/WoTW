using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHolderScript : MonoBehaviour {
	public List<GameObject> treeList;
	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			treeList.Add (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
