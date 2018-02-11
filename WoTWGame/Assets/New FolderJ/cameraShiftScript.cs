using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShiftScript : MonoBehaviour {
	public bool stage1;
	public bool stage2;
	private Transform target1;
	public float speed;
	private float startTime;
	public float duration;
	public Animator branch1;
	public Animator landscape;
	public Animator branch3;
	public Animator branch4;
	public Animator branch5;
	public Animator branch6;
	public Animator branch7;
	public Animator branch8;
	// Use this for initialization
	void Start () {
//		target1 = GetComponentsInChildren<Transform>()[1];
	}
	
	// Update is called once per frame
	void Update () {
//		if (stage1) {
//			player.transform.position = Vector3.Lerp(transform.position, target1.position, ((Time.time - startTime) / duration));
//		}
	}
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
//			stage1 = true;
//			startTime = Time.time;
			Camera.main.GetComponent<Animator>().SetTrigger("TreeCinematic");
			branch1.SetTrigger ("move7");
			branch3.SetTrigger ("move7");
			landscape.SetTrigger ("spin");
//			branch3.SetTrigger ("move2");
//			branch4.SetTrigger ("move4");
//			branch5.SetTrigger ("move5");
//			branch6.SetTrigger ("move6");
//			branch7.SetTrigger ("move7");
//			branch8.SetTrigger ("move8");

			// put it all in one animator

		}
	}
}
