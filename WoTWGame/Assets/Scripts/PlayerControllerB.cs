using UnityEngine;
using System.Collections;

public class PlayerControllerB : MonoBehaviour {

	public float speed = 6f;
	Vector2 movement;
	Rigidbody2D playerRigidbody;
	private Animator anim;
	public bool canMove = true;
	public float H;
	public float V;
	public float HPause;
	public float VPause;
	private float HDelay;
	private float VDelay;
	public Transform detector;


	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		anim.SetFloat ("LastMoveY", -1f);
	}

	void FixedUpdate () {
		if (canMove) {
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");

			if (h != 0 || v != 0) {
				anim.SetBool ("Walking", true);
				if (h > 0) {
					anim.SetFloat ("LastMoveX", -1f);
					transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
					H = 1f;
					detector.localPosition = new Vector2 (-1f, detector.localPosition.y);
					HDelay = Time.time + HPause;
				} else if (h < 0) {
					anim.SetFloat ("LastMoveX", -1f);
					transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
					H = -1f;
					detector.localPosition = new Vector2 (-1f, detector.localPosition.y);
					HDelay = Time.time + HPause;
				} else if (h == 0 && HDelay < Time.time) {
					anim.SetFloat ("LastMoveX", 0f);
					H = 0f;
					detector.localPosition = new Vector2 (0f, detector.localPosition.y);
				} 

				if (v > 0) {
					anim.SetFloat ("LastMoveY", 1f);
					V = 1f;
					VDelay = Time.time + VPause;
					detector.localPosition = new Vector2 (detector.localPosition.x, .6f);
				} else if (v < 0) {
					anim.SetFloat ("LastMoveY", -1f);

					V = -1f;
					VDelay = Time.time + VPause;
					detector.localPosition = new Vector2 (detector.localPosition.x, -1.4f);
				} else if (v == 0 && VDelay < Time.time) {
					anim.SetFloat ("LastMoveY", 0f);
					V = 0f;
					detector.localPosition = new Vector2 (detector.localPosition.x, -.5f);
				} 

			} else {
				anim.SetBool ("Walking", false);
			}
			//Move (h, v);
		} else {
			anim.SetBool ("Walking", false);
		}
	}

	void Move (float h, float v)
	{
		movement.Set (h, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition ((Vector2)gameObject.transform.position + movement);


	}


}
