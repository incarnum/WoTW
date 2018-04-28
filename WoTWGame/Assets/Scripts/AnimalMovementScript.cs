using UnityEngine;
using System.Collections;

public class AnimalMovementScript : MonoBehaviour {
	public float speed;
	public float speed2;
	Vector2 movement;
	Rigidbody2D selfRigidbody;
	public int action;
	public int creatureType;
	public bool corrupted;
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
	public bool fadeOut;
	public GameObject munchIcon;
	public bool markedForDeath;
    private AudioSource munchSound;

	void Start () {
		selfRigidbody = GetComponent<Rigidbody2D> ();
		actionStart = 0f;
		player = GameObject.Find ("Player");
		deerNucleus = GameObject.Find ("DeerNucleus").transform.position;
		anim = GetComponent<Animator> ();
        munchSound = GameObject.Find("Snd_Munch").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (fadeOut) {
			GetComponent<SpriteRenderer> ().color = new Color (GetComponent<SpriteRenderer> ().color.r, GetComponent<SpriteRenderer> ().color.g, GetComponent<SpriteRenderer> ().color.b, GetComponent<SpriteRenderer> ().color.a - 1 * Time.deltaTime);
		}
	}

	void FixedUpdate () {
		if (Time.time > actionStart + actionLength && canMove == true) {
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
			}



		}

		if (canMove == true) {
				Move (h, v, s);
		}
	}

	void OnEnable() {
		PlayerControllerScript.OnPaused += PauseMovement;
		PlayerControllerScript.OnUnpaused += UnpauseMovement;
	}

	void OnDisable() {
		PlayerControllerScript.OnPaused -= PauseMovement;
		PlayerControllerScript.OnUnpaused -= UnpauseMovement;
	}


	void Move(float h, float v, float s) {
		movement.Set (h, v);
		movement = movement.normalized * s * Time.deltaTime;
		selfRigidbody.MovePosition ((Vector2)gameObject.transform.position + movement);
//		Debug.Log ();

	}

	private void PauseMovement() {
		canMove = false;
		anim.enabled = false;
	}

	private void UnpauseMovement() {
		canMove = true;
		anim.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.GetComponent<AnimalMovementScript> () != null) {
			if (creatureType == 2 && col.gameObject.GetComponent<AnimalMovementScript> ().creatureType == 1) {
				if (fadeOut == false) {
					GameObject munch = Instantiate (munchIcon) as GameObject;
					munch.transform.position = col.gameObject.transform.position;
                    if(gameObject.GetComponent<Renderer>().isVisible)
                    {
                        munchSound.pitch = Random.Range(1f, 3f);
                        munchSound.Play();
                    }
					Destroy (munch, 1.0f);
					col.gameObject.GetComponent<AnimalMovementScript> ().PauseMovementForTime (5);
				}
				if (col.gameObject.GetComponent<AnimalMovementScript> ().corrupted == false) {
					GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().deerCreatureList.Remove (col.gameObject);
				} else {
					GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().corruptedDeerCreatureList.Remove (col.gameObject);
				}
				if (col.gameObject.transform.position.y - transform.position.y > 0) {
					anim.SetTrigger ("Away");
				} else {
					anim.SetTrigger ("Toward");
				}
				if (col.gameObject.transform.position.x - transform.position.x > 0) {
					transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
				} else {
					transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				}

				Destroy (col.gameObject, 1.0f);
				col.gameObject.GetComponent<AnimalMovementScript> ().fadeOut = true;
				PauseMovementForTime (2.0f);
			}

			if (creatureType == 2 && col.gameObject.GetComponent<AnimalMovementScript> ().creatureType == 3) {
				Debug.Log ("wolf hit rabbit");
				if (fadeOut == false) {
					GameObject munch = Instantiate (munchIcon) as GameObject;
					munch.transform.position = col.gameObject.transform.position;
					if(gameObject.GetComponent<Renderer>().isVisible)
					{
						munchSound.pitch = Random.Range(1f, 3f);
						munchSound.Play();
					}
					Destroy (munch, 1.0f);
					col.gameObject.GetComponent<AnimalMovementScript> ().PauseMovementForTime (5);
				}
				if (col.gameObject.GetComponent<AnimalMovementScript> ().corrupted == false) {
					GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().rabbitCreatureList.Remove (col.gameObject);
				} else {
					GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().corruptedRabbitCreatureList.Remove (col.gameObject);
				}
				if (col.gameObject.transform.position.y - transform.position.y > 0) {
					anim.SetTrigger ("Away");
				} else {
					anim.SetTrigger ("Toward");
				}
				if (col.gameObject.transform.position.x - transform.position.x > 0) {
					transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
				} else {
					transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				}

				Destroy (col.gameObject, 1.0f);
				col.gameObject.GetComponent<AnimalMovementScript> ().fadeOut = true;
				PauseMovementForTime (2.0f);
			}

			if (creatureType == 4 && col.gameObject.GetComponent<AnimalMovementScript> ().creatureType == 3) {
				Debug.Log ("owl hit rabbit");
				if (fadeOut == false) {
					GameObject munch = Instantiate (munchIcon) as GameObject;
					munch.transform.position = col.gameObject.transform.position;
					if(gameObject.GetComponent<Renderer>().isVisible)
					{
						munchSound.pitch = Random.Range(1f, 3f);
						munchSound.Play();
					}
					Destroy (munch, 1.0f);
					col.gameObject.GetComponent<AnimalMovementScript> ().PauseMovementForTime (5);
				}
				if (col.gameObject.GetComponent<AnimalMovementScript> ().corrupted == false) {
					GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().rabbitCreatureList.Remove (col.gameObject);
				} else {
					GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().corruptedRabbitCreatureList.Remove (col.gameObject);
				}
				if (col.gameObject.transform.position.y - transform.position.y > 0) {
					anim.SetTrigger ("Away");
				} else {
					anim.SetTrigger ("Toward");
				}
				if (col.gameObject.transform.position.x - transform.position.x > 0) {
					transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
				} else {
					transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				}

				Destroy (col.gameObject, 1.0f);
				col.gameObject.GetComponent<AnimalMovementScript> ().fadeOut = true;
				PauseMovementForTime (2.0f);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (creatureType == 1 && col.gameObject.GetComponent<BushScript> () != null) {
			if (col.gameObject.GetComponent<BushScript> ().fadeOut == false) {
				GameObject munch = Instantiate (munchIcon) as GameObject;
                if (gameObject.GetComponent<Renderer>().isVisible)
                {
                    munchSound.pitch = Random.Range(1f, 3f);
                    munchSound.Play();
                }
				Destroy (munch, 1.0f);
				munch.transform.position = col.gameObject.transform.position;
			}
			if (col.gameObject.GetComponent<BushScript> ().isCorrupted == false) {
				GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().shrubCreatureList.Remove (col.gameObject);
			} else {
				GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ().corruptedShrubCreatureList.Remove (col.gameObject);
			}
			Destroy (col.gameObject, 1.0f);
			col.gameObject.GetComponent<BushScript> ().fadeOut = true;
			PauseMovementForTime (2.0f);

			if (col.gameObject.transform.position.y - transform.position.y > 0) {
				anim.SetTrigger ("Away");
			} else {
				anim.SetTrigger ("Toward");
			}
			if (col.gameObject.transform.position.x - transform.position.x > 0) {
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			} else {
				transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
		}
	}

	void PauseMovementForTime (float tim) {
		h = 0;
		v = 0;
		anim.SetBool ("Idle", true);
		actionStart = Time.time + tim;
	}
		
}
