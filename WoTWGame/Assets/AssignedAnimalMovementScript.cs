using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignedAnimalMovementScript : MonoBehaviour {
	public List<Transform> SpotsToMoveTo;
	public int phase;
	private bool walking;
	private Vector2 movement;
	private Rigidbody2D rb;
	public Vector2 target;
	public float speed;
	private Animator anim;
	public bool isWolf;
	public GameObject munchPrefab;
	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (walking) {
			Move (target.x - transform.position.x, target.y - transform.position.y, speed);
			if ((new Vector3(target.x, target.y, transform.position.z) - transform.position).magnitude < .5) {
				walking = false;
				Debug.Log ("you have arrived at your destination");
				anim.SetBool ("Idle", true);
				if (phase == 2 && GetComponent<sDeer1Script> () != null) {
					GetComponent<sDeer1Script> ().phase = 1;
				}
				if (isWolf) {
					if (GameObject.Find("SDeer1") != null) {
					Destroy (GameObject.Find ("SDeer1"));
					GameObject munch = Instantiate (munchPrefab);
					munch.transform.position = GameObject.Find ("SDeer1").transform.position;
					Destroy (munch, 1.0f);
				}
					GameObject.Find ("ScriptedEventManager").GetComponent<ScriptedEventManagerScript> ().NextEvent ();

				}
			}
		}
//		if (Input.GetKeyDown (KeyCode.H)) {
//			MoveToNextSpot ();
//		}
	}

	public void MoveToNextSpot() {
		walking = true;
		if (SpotsToMoveTo [phase] != null) {
			target = SpotsToMoveTo [phase].position;
		}
		anim.SetBool ("Idle", false);
		if (target.x - transform.position.x >= 0) {
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		} else if (target.x - transform.position.x < 0) {
			transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		if (target.y - transform.position.y >= 0) {
			anim.SetTrigger ("Away");
		} else if (target.y - transform.position.y < 0) {
			anim.SetTrigger ("Toward");
		}
		phase += 1;
	}

	public void MoveToSpecificSpot(int spotNum) {
		walking = true;
		if (SpotsToMoveTo [spotNum] != null) {
			target = SpotsToMoveTo [spotNum].position;
		}
		anim.SetBool ("Idle", false);
		if (target.x - transform.position.x >= 0) {
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		} else if (target.x - transform.position.x < 0) {
			transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		if (target.y - transform.position.y >= 0) {
			anim.SetTrigger ("Away");
		} else if (target.y - transform.position.y < 0) {
			anim.SetTrigger ("Toward");
		}
		phase += 1;
	}

	void Move(float h, float v, float s) {
		movement.Set (h, v);
		movement = movement.normalized * s * Time.deltaTime;
		rb.MovePosition ((Vector2)gameObject.transform.position + movement);
	}
}
