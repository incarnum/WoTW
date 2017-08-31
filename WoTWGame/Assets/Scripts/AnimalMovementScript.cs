using UnityEngine;
using System.Collections;

public class AnimalMovementScript : MonoBehaviour {
	public float speed;
	public float speed2;
	Vector2 movement;
	Rigidbody2D selfRigidbody;
	public int action;
	public int creatureType;
	// 0 = not moving
	// 1 = wandering
	// 2 = fleeing
	// 3 = chasing
	// Use this for initialization
	public float actionLength;
	private float actionStart;
	public bool canMove;
	public float h;
	public float v;
	private float s;
	private GameObject player;
	public float aggroRange;
	private Vector2 deerNucleus;
	public int concentrationFactor;
	public Animator anim;
	bool facingLeft;
	public int idle;

	void Start () {
		selfRigidbody = GetComponent<Rigidbody2D> ();
		actionStart = 0f;
		player = GameObject.Find ("Player");
		deerNucleus = GameObject.Find ("DeerNucleus").transform.position;
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		if (Time.time > actionStart + actionLength) {
			actionStart = Time.time;
			idle = Random.Range (0, 10);
			if (idle <= 4) {
				anim.SetBool ("Idle", false);
				h = Random.Range (-1, 1);
				v = Random.Range (-1, 1);
				s = speed2;
				if (h >= 0) {
					h = 1;
					transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				} else if (h < 0) {
					h = -1;
					transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				}
				if (v >= 0) {
					v = .7f;
					anim.SetTrigger ("Away");
				} else if (v < 0) {
					v = -.7f;
					anim.SetTrigger ("Toward");
				}

			}
			if (idle > 4) {
				s = 0;
				anim.SetBool ("Idle", true);
				Debug.Log (idle);
			}



		}

		if (player.GetComponent<PlayerControllerScript> ().paused == false) {
				Move (h, v, s);
		}
	}


	void Move(float h, float v, float s) {
		movement.Set (h, v);
		movement = movement.normalized * s * Time.deltaTime;
		selfRigidbody.MovePosition ((Vector2)gameObject.transform.position + movement);
//		Debug.Log ();

	}
		
}
