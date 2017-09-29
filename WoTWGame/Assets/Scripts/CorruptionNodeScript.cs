using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionNodeScript : MonoBehaviour {
	public float spreadInterval;
	private float nextSpreadTime;
	public bool canSpread;
	public GameObject corruptionPrefab;
	private int dir;
	public float spreadLikelyhood;
	private float compareToLikelyhood;


	public float slideSpeed;
	private float startTime;
	public float spreadDistanceX;
	public float spreadDistanceY;
	public Vector3 targetPosition;
	public bool activelySliding;
	public GameObject creator;

	private GameObject player;
	private Transform upperLeftBound;
	private Transform lowerRightBound;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		upperLeftBound = GameObject.Find ("UpperLeftBound").transform;
		lowerRightBound = GameObject.Find ("LowerRightBound").transform;
		GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList.Add (gameObject);
		if (canSpread) {
			nextSpreadTime = Time.time + spreadInterval;
		}
		GameObject.Find ("SludgeBar").GetComponent<barScript> ().SetFillSizeValue (GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList.Count * .04f);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpread && player.GetComponent<PlayerControllerScript>().paused == false) {
			if (nextSpreadTime <= Time.time) {
				Spread ();
				nextSpreadTime = Time.time + spreadInterval;
			}
		}

		if (activelySliding) {
			float distCovered = (Time.time - startTime) * slideSpeed;
			float fracJourney = distCovered / spreadDistanceX;
			transform.position = Vector3.Lerp (creator.transform.position, targetPosition, fracJourney);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color(1f,1f,1f, fracJourney);
			if (fracJourney > .99f) {
				activelySliding = false;
				gameObject.GetComponent<ZDepthStaticScript> ().RecheckZDepth ();
			}
		}
	}

	void Spread () {
		compareToLikelyhood = Random.value;
		if (compareToLikelyhood <= spreadLikelyhood) {
			GameObject newCorruption = Instantiate (corruptionPrefab) as GameObject;
			newCorruption.GetComponent<CorruptionNodeScript> ().creator = gameObject;
			newCorruption.GetComponent<CorruptionNodeScript> ().activelySliding = true;
			//randomly generate direction, test if direction is filled, if not spawn there
			dir = Random.Range (0, 4);
			if (dir == 0 && transform.position.y + spreadDistanceY + 1 < upperLeftBound.position.y) {
				newCorruption.GetComponent<CorruptionNodeScript> ().targetPosition = new Vector3 (transform.position.x, transform.position.y + spreadDistanceY, transform.position.z);
			} else if (dir == 1 && transform.position.x + spreadDistanceX + 1 < lowerRightBound.position.x) {
				newCorruption.GetComponent<CorruptionNodeScript> ().targetPosition = new Vector3 (transform.position.x + spreadDistanceX, transform.position.y, transform.position.z);
			} else if (dir == 2 && transform.position.y - spreadDistanceY - 1 > lowerRightBound.position.y) {
				newCorruption.GetComponent<CorruptionNodeScript> ().targetPosition = new Vector3 (transform.position.x, transform.position.y - spreadDistanceY, transform.position.z);
			} else if (dir == 3 && transform.position.x - spreadDistanceY - 1 > upperLeftBound.position.x) {
				newCorruption.GetComponent<CorruptionNodeScript> ().targetPosition = new Vector3 (transform.position.x - spreadDistanceX, transform.position.y, transform.position.z);
			} else {
				newCorruption.GetComponent<CorruptionNodeScript> ().targetPosition =  new Vector3 (transform.position.x, transform.position.y + spreadDistanceY, transform.position.z);

			}
		}
	}

	void OnDestroy() {
		if (GameObject.Find ("CreatureManager") != null) {
			GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList.Remove (gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (activelySliding) {
			if (col.gameObject.tag == "Corruption") {
				//Debug.Log ("corruption node hit another node");
				if (col.gameObject != creator) {
					Destroy (gameObject);
				}
			}
		}
	}
}
