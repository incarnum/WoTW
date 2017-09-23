using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellMenuScript : MonoBehaviour {

	public int target;
	public int effectType;

	public GameObject spellPrefab;

	public GameObject node1;

	public GameObject node2;
	public GameObject node3;
	public GameObject node4;
	public GameObject node5;
	public GameObject ingWarningPrefab;
	public GameObject whereSpellTome;
	public List<GameObject> uncastTomes;
	public Transform warningSpot;
	public TextMesh spellPreviewTextbox;

	public string spellPreviewString;

	public float spellStrengthMod;

	//make nodescript with held, every time one gets filled, call a function here that changes the effect type if all were empty, deny fills of wrong type by having nodes check against node type
	//in nodescript: if effecttype is null, or equals that being dropped


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateSpell() {
		GameObject newSpell = Instantiate (spellPrefab) as GameObject;
		newSpell.GetComponent<SpellScript> ().target = node1.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType;
		if (node3.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript>().objectType == 0) {
			newSpell.GetComponent<SpellScript> ().strength = spellStrengthMod;
		} else if (node3.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript>().objectType == 2) {
			newSpell.GetComponent<SpellScript> ().strength = -spellStrengthMod;
		}
		newSpell.GetComponent<SpellScript> ().effect = node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType;

		newSpell.GetComponentsInChildren<Text> () [0].text = spellPreviewString;
		//newSpell.transform.position = new Vector3 (GameObject.Find ("TomeSpot").transform.position.x, GameObject.Find ("TomeSpot").transform.position.y - uncastTomes.Count, GameObject.Find ("TomeSpot").transform.position.z);
		PlaceSpell(newSpell);
		uncastTomes.Add (newSpell);

		Destroy (node1.GetComponent<IngredientHolderScript> ().holding);
		Destroy (node2.GetComponent<IngredientHolderScript> ().holding);
		Destroy (node3.GetComponent<IngredientHolderScript> ().holding);

		//GameObject spelltip = Instantiate (whereSpellTome) as GameObject;
		//spelltip.transform.position = warningSpot.position;
		//Destroy (spelltip, 7.0f);
	}

	public void PredictSpell() {
		spellPreviewString = "";
		if (node2.GetComponent<IngredientHolderScript> ().holding != null && node3.GetComponent<IngredientHolderScript> ().holding == null) {
			if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 0) {
				spellPreviewString += "Enlarge/Shrink ";
			} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 1) {
				spellPreviewString += "Hasten/Slow ";
			} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 2) {
				spellPreviewString += "Toughen/Weaken ";
			}
		}
		if (node3.GetComponent<IngredientHolderScript> ().holding != null) {
			if (node2.GetComponent<IngredientHolderScript> ().holding != null && node3.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript>().objectType == 0) {
				if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 0) {
					spellPreviewString += "Enlarge ";
				} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 1) {
					spellPreviewString += "Hasten ";
				} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 2) {
					spellPreviewString += "Toughen ";
				}
			}
		}
		if (node3.GetComponent<IngredientHolderScript> ().holding != null) {
			if (node2.GetComponent<IngredientHolderScript> ().holding != null && node3.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript>().objectType == 2) {
				if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 0) {
					spellPreviewString += "Shrink ";
				} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 1) {
					spellPreviewString += "Slow ";
				} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 2) {
					spellPreviewString += "Weaken ";
				}
			}
		}

		if (node1.GetComponent<IngredientHolderScript> ().holding != null) {
			if (node1.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 0) {
				spellPreviewString += "Shrubs";
			} else if (node1.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 1) {
				spellPreviewString += "Deer";
			} else if (node1.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 2) {
				spellPreviewString += "Wolves";
			}
		}

		if (node2.GetComponent<IngredientHolderScript> ().holding != null) {
			if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType >= 3) {
				spellPreviewString = "Cleanse Corruption";
			}
		}

		spellPreviewTextbox.text = spellPreviewString;
	}

	private void PlaceSpell(GameObject spellbook) {
		if (GameObject.Find ("ABSlot1").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot1").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot1").transform;
			GameObject.Find ("ABSlot1").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot2").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot2").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot2").transform;
			GameObject.Find ("ABSlot2").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot3").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot3").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot3").transform;
			GameObject.Find ("ABSlot3").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot4").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot4").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot4").transform;
			GameObject.Find ("ABSlot4").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot5").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot5").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot5").transform;
			GameObject.Find ("ABSlot5").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot6").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot6").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot6").transform;
			GameObject.Find ("ABSlot6").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		}
	}

	public void CheckNodes() {
//		Debug.Log ("callonme");
//		if (node2.GetComponent<IngredientHolderScript> ().holding == null
//			&& node3.GetComponent<IngredientHolderScript> ().holding == null
//			&& node4.GetComponent<IngredientHolderScript> ().holding == null) {
//			node2.GetComponent<IngredientHolderScript> ().accepting = 6;
//			node3.GetComponent<IngredientHolderScript> ().accepting = 6;
//			node4.GetComponent<IngredientHolderScript> ().accepting = 6;
//
//		} else if (node2.GetComponent<IngredientHolderScript> ().holding != null){
//			node2.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//			node3.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//			node4.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		} else if (node3.GetComponent<IngredientHolderScript> ().holding != null){
//			node2.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//			node3.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//			node4.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		} else if (node4.GetComponent<IngredientHolderScript> ().holding != null){
//			node2.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//			node3.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//			node4.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		}
	}
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class SpellMenuScript : MonoBehaviour {
//
//	public int target;
//	public int effectType;
//
//	public GameObject spellPrefab;
//
//	public GameObject node1;
//
//	public GameObject node2;
//	public GameObject node3;
//	public GameObject node4;
//	public GameObject node5;
//
//	public GameObject ingWarningPrefab;
//	public GameObject whereSpellTome;
//	public List<GameObject> uncastTomes;
//	public Transform warningSpot;
//
//	//make nodescript with held, every time one gets filled, call a function here that changes the effect type if all were empty, deny fills of wrong type by having nodes check against node type
//	//in nodescript: if effecttype is null, or equals that being dropped
//
//
//	// Use this for initialization
//	void Start () {
//		node1 = gameObject.GetComponentsInChildren<Transform> () [1].gameObject;
//		node2 = gameObject.GetComponentsInChildren<Transform> () [2].gameObject;
//		node3 = gameObject.GetComponentsInChildren<Transform> () [3].gameObject;
//		node4 = gameObject.GetComponentsInChildren<Transform> () [4].gameObject;
//		node5 = gameObject.GetComponentsInChildren<Transform> () [5].gameObject;
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//	}
//
//	public void CreateSpell() {
//
//		if (node4.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType
//			== node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType
//			&& node4.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType
//			== node3.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType) {
//
//			GameObject newSpell = Instantiate (spellPrefab) as GameObject;
//			newSpell.GetComponent<SpellScript> ().target = node1.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType;
//			newSpell.GetComponent<SpellScript> ().strength = 1;
//			//			if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 0
//			//			    || node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 1) {
//			//				newSpell.GetComponent<SpellScript> ().effect = 0;
//			//			} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 1) {
//			//				newSpell.GetComponent<SpellScript> ().effect = 1;
//			//			} else if (node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType == 2) {
//			//				newSpell.GetComponent<SpellScript> ().effect = 1;
//			//			}
//			//
//			newSpell.GetComponent<SpellScript> ().effect = node2.GetComponent<IngredientHolderScript> ().holding.GetComponent<DragDropScript> ().objectType;
//
//
//			newSpell.transform.position = new Vector3 (GameObject.Find ("TomeSpot").transform.position.x, GameObject.Find ("TomeSpot").transform.position.y - uncastTomes.Count, GameObject.Find ("TomeSpot").transform.position.z);
//			uncastTomes.Add (newSpell);
//
//			Destroy (node1.GetComponent<IngredientHolderScript> ().holding);
//			Destroy (node2.GetComponent<IngredientHolderScript> ().holding);
//			Destroy (node3.GetComponent<IngredientHolderScript> ().holding);
//			Destroy (node4.GetComponent<IngredientHolderScript> ().holding);
//
//			GameObject spelltip = Instantiate (whereSpellTome) as GameObject;
//			spelltip.transform.position = warningSpot.position;
//			Destroy (spelltip, 1.0f);
//
//		} else {
//			Debug.Log ("SpellFailed");
//			GameObject ingWarning = Instantiate (ingWarningPrefab) as GameObject;
//			ingWarning.transform.position = warningSpot.position;
//			Destroy (ingWarning, 1.0f);
//		}
//	}
//
//	public void CheckNodes() {
//		//		Debug.Log ("callonme");
//		//		if (node2.GetComponent<IngredientHolderScript> ().holding == null
//		//			&& node3.GetComponent<IngredientHolderScript> ().holding == null
//		//			&& node4.GetComponent<IngredientHolderScript> ().holding == null) {
//		//			node2.GetComponent<IngredientHolderScript> ().accepting = 6;
//		//			node3.GetComponent<IngredientHolderScript> ().accepting = 6;
//		//			node4.GetComponent<IngredientHolderScript> ().accepting = 6;
//		//
//		//		} else if (node2.GetComponent<IngredientHolderScript> ().holding != null){
//		//			node2.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//			node3.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//			node4.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//		} else if (node3.GetComponent<IngredientHolderScript> ().holding != null){
//		//			node2.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//			node3.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//			node4.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//		} else if (node4.GetComponent<IngredientHolderScript> ().holding != null){
//		//			node2.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//			node3.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//			node4.GetComponent<IngredientHolderScript> ().accepting = node2.GetComponent<IngredientHolderScript> ().accepting;
//		//		}
//	}
//}
//
//
//
