using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {
	public GameObject target;
	public int effect;
	public int modifier;
	public bool beingMoved;

	public bool movable;
	private GameObject player;
	public GameObject placeSpot;
	public Vector3 handposition;

	public int cardType;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		if (cardType == 1) {
			target = GameObject.Find ("Berries1");
		} else if (cardType == 2) {
			target = GameObject.Find ("Deer");
		} else if (cardType == 3) {
			target = GameObject.Find ("Wolves");
		} else if (cardType == 4) {
			target = GameObject.Find ("Mice1");
		} else if (cardType == 5) {
			target = GameObject.Find ("Owls");
		} else if (cardType == 6) {
			target = GameObject.Find ("Songbirds");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (beingMoved) {
			Vector3 bla = new Vector3 (0, 0, 20);
			handposition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			handposition += bla;
			transform.position = handposition;
		}
	}

	void OnMouseDown () {
		player.GetComponent<PlayerPlaceScript> ().holdingObj = gameObject;
		player.GetComponent<PlayerPlaceScript> ().holdingBool = true;
		//gameObject.GetComponent<Animator> ().SetTrigger ("Normal");
		beingMoved = true;
	}

    void OnMouseUp() {
		beingMoved = false;
    }

//	void OnMouseOver() {
//		if (Input.GetMouseButtonDown (1)) {
//			if (placeSpot.GetComponent<PlaceSpotScript> ()) {
//				placeSpot.GetComponent<PlaceSpotScript> ().holdingObj = null;
//				placeSpot.GetComponent<PlaceSpotScript> ().holdingBool = false;
//			}
//			if (cardType == 1) {
//				GameObject.Find ("ShrubCardBank").GetComponent<CardBankScript> ().numberOfCards += 1;
//				GameObject.Find ("ShrubCardBank").GetComponent<CardBankScript> ().TextUpdate ();
//			} else if (cardType == 2) {
//				GameObject.Find ("DeerCardBank").GetComponent<CardBankScript> ().numberOfCards += 1;
//				GameObject.Find ("DeerCardBank").GetComponent<CardBankScript> ().TextUpdate ();
//			} else if (cardType == 3) {
//				GameObject.Find ("WolfCardBank").GetComponent<CardBankScript> ().numberOfCards += 1;
//				GameObject.Find ("WolfCardBank").GetComponent<CardBankScript> ().TextUpdate ();
//			} else if (cardType == 4) {
//				GameObject.Find ("MouseCardBank").GetComponent<CardBankScript> ().numberOfCards += 1;
//				GameObject.Find ("MouseCardBank").GetComponent<CardBankScript> ().TextUpdate ();
//			} else if (cardType == 5) {
//				GameObject.Find ("OwlCardBank").GetComponent<CardBankScript> ().numberOfCards += 1;
//				GameObject.Find ("OwlCardBank").GetComponent<CardBankScript> ().TextUpdate ();
//			} else if (cardType == 6) {
//				GameObject.Find ("SongbirdCardBank").GetComponent<CardBankScript> ().numberOfCards += 1;
//				GameObject.Find ("SongbirdCardBank").GetComponent<CardBankScript> ().TextUpdate ();
//			}
//			Destroy (gameObject);
//		}
//	}
}
