using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public Animator credits;
	public Animator overlay;
	public TimeStopCanvas tsc;
	// Use this for initialization
	void Start () {
//		target1 = GetComponentsInChildren<Transform>()[1];
		overlay = GameObject.Find("MainOverlayCanvas").GetComponent<Animator>();
		credits = GameObject.Find ("Credits").GetComponent<Animator>();
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
			overlay.SetTrigger ("fade");
			tsc.pauseStop = false;
			tsc.areaStop = false;
			tsc.dialogueStop = false;
			tsc.CheckVisibility ();
			StartCoroutine (CreditsDelay ());
			StartCoroutine (MainMenuDelay ());
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().dialoguePaused = true;
			GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().CheckIfICanMove();

		}
	}
	IEnumerator CreditsDelay() {
		yield return new WaitForSeconds (27);
		credits.SetTrigger ("rollCredits");
	}
		
	IEnumerator MainMenuDelay() {
		yield return new WaitForSeconds (77);
		SceneManager.LoadScene ("MainMenu");
	}
}
