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
				h = Random.Range (-1f, 1f);
				v = Random.Range (-1f, 1f);
				if (h >= 0) {
					h = 1;
					transform.localScale = new Vector3 (1f, 1f, 1f);
				} else if (h < 0) {
					h = -1;
					transform.localScale = new Vector3 (-1f, 1f, 1f);
				}
				if (v >= 0) {
					v = .7f;
					anim.SetTrigger ("Away");
				} else {
					v = -.7f;
					anim.SetTrigger ("Toward");
				}
				s = Random.Range (0f, speed2);
				if (canMove) {
					
				}
				
			
			actionStart = Time.time;

		}
		Move (h, v, s);
	}


	void Move(float h, float v, float s) {
		movement.Set (h, v);
		movement = movement.normalized * s * Time.deltaTime;
		selfRigidbody.MovePosition ((Vector2)gameObject.transform.position + movement);
//		Debug.Log ();

	}
		
}
