using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
	public GameObject bar1;
	public GameObject bar2;
	public GameObject bar3;
	public GameObject textObject;
	public float highlightSizeChange;
	private Vector2 originalTextSize;
	private Color originalTextColor;
	public Color highlightTextColor;
	public bool selected;
	Vector2 newSize;
	// Use this for initialization
	void Start () {
		originalTextSize = textObject.GetComponent<RectTransform> ().localScale;
		originalTextColor = textObject.GetComponent<Text> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter (PointerEventData pointerEventData) {
		bar1.SetActive (true);
		if (bar3 != null) {
			bar3.SetActive (true);
		}
		//bar2.SetActive (true);
		newSize = new Vector2 (textObject.GetComponent<RectTransform> ().localScale.x * highlightSizeChange, textObject.GetComponent<RectTransform> ().localScale.y * highlightSizeChange);
		textObject.GetComponent<RectTransform> ().localScale = newSize;
		textObject.GetComponent<Text> ().color = highlightTextColor;
	}

	public void OnPointerExit (PointerEventData pointerEventData) {
		bar1.SetActive (false);
		if (bar3 != null) {
			bar3.SetActive (false);
		}
		//bar2.SetActive (false);
		ResetTextSize ();
	}

	public void ResetTextSize() {
		textObject.GetComponent<RectTransform> ().localScale = originalTextSize;
		textObject.GetComponent<Text> ().color = originalTextColor;
	}

	public void Select() {

	}

	public void Deselect() {

	}
}
