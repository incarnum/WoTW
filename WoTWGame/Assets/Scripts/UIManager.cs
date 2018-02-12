using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject shrubUI, deerUI, wolfUI, rabbitUI, owlUI, manager, leftButton, rightButton;
    private List<GameObject> UIRotator;
    public bool shrubOn, deerOn, wolfOn, rabbitOn, owlOn;
	// Use this for initialization
	void Start () {
        UIRotator = new List<GameObject>();
        UIRotator.Insert(0, shrubUI);
        shrubOn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            RotateLeft();
        }
        if (Input.GetKeyDown("l"))
        {
            RotateRight();
        }
        if (!deerOn)
        {
            if (deerUI.activeSelf)
            {
                deerOn = true;
                UIRotator.Insert(1, deerUI);
                leftButton.SetActive(true);
                rightButton.SetActive(true);
            }
        }
        if (!wolfOn)
        {
            if (wolfUI.activeSelf)
            {
                wolfOn = true;
                if(UIRotator[0] == deerUI)
                {
                    UIRotator.Insert(2, wolfUI);
                    UIRotator[1].transform.localPosition = new Vector2(145, 0);
                    UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                } 
                UIRotator.Insert(2, wolfUI);
            }
        }
        if (!rabbitOn)
        {
            if (rabbitUI.activeSelf)
            {
                rabbitOn = true;
                if (UIRotator[0] == shrubUI)
                {
                    UIRotator.Add(rabbitUI);
                    UIRotator[1].transform.localPosition = new Vector2(145, 0);
                    UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                    UIRotator[2].SetActive(false);
                }
                else if(UIRotator[0] == wolfUI)
                {
                    UIRotator.Insert(1, rabbitUI);
                    UIRotator[1].transform.localPosition = new Vector2(145, 0);
                    UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                    UIRotator[2].SetActive(false);
                }
                else
                {
                    UIRotator.Insert(2, rabbitUI);
                    rabbitUI.SetActive(false);
                }
            }
        }
        if (!owlOn)
        {
            if (owlUI.activeSelf)
            {
                owlOn = true;
                UIRotator.Insert(2, owlUI);
                owlUI.SetActive(false);
            }
        }
	}

    public void RotateLeft()
    {
        if (manager.activeSelf)
        {
            if (UIRotator.Count == 2)
            {
                if (UIRotator[0] == shrubUI)
                {
                    UIRotator.Remove(shrubUI);
                    UIRotator.Insert(1, shrubUI);
                    UIRotator[1].transform.localPosition = new Vector2(145, 0);
                    UIRotator[0].transform.localPosition = new Vector2(0, 0);
                    UIRotator[1].transform.SetAsLastSibling();
                    UIRotator[0].transform.SetAsLastSibling();
                }
            }
            else if (UIRotator.Count == 3)
            {
                GameObject oldCenter = UIRotator[0];
                UIRotator.RemoveAt(0);
                UIRotator.Add(oldCenter);
                UIRotator[0].transform.localPosition = new Vector2(0, 0);
                UIRotator[1].transform.localPosition = new Vector2(145, 0);
                UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                UIRotator[1].transform.SetAsLastSibling();
                UIRotator[UIRotator.Count - 1].transform.SetAsLastSibling();
                UIRotator[0].transform.SetAsLastSibling();
            }
            else if (UIRotator.Count == 4)
            {
                GameObject oldCenter = UIRotator[0];
                UIRotator.RemoveAt(0);
                UIRotator.Add(oldCenter);
                UIRotator[0].transform.localPosition = new Vector2(0, 0);
                UIRotator[1].transform.localPosition = new Vector2(145, 0);
                UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                UIRotator[1].transform.SetAsLastSibling();
                UIRotator[UIRotator.Count - 1].transform.SetAsLastSibling();
                UIRotator[0].transform.SetAsLastSibling();
                UIRotator[0].SetActive(true);
                UIRotator[1].SetActive(true);
                UIRotator[UIRotator.Count - 1].SetActive(true);
                UIRotator[2].SetActive(false);
            }
            else if (UIRotator.Count == 5)
            {
                GameObject oldCenter = UIRotator[0];
                UIRotator.RemoveAt(0);
                UIRotator.Add(oldCenter);
                UIRotator[0].transform.localPosition = new Vector2(0, 0);
                UIRotator[1].transform.localPosition = new Vector2(145, 0);
                UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                UIRotator[1].transform.SetAsLastSibling();
                UIRotator[UIRotator.Count - 1].transform.SetAsLastSibling();
                UIRotator[0].transform.SetAsLastSibling();
                UIRotator[0].SetActive(true);
                UIRotator[1].SetActive(true);
                UIRotator[UIRotator.Count - 1].SetActive(true);
                UIRotator[2].SetActive(false);
                UIRotator[3].SetActive(false);
            }
        }
    }

    public void RotateRight()
    {
        if (manager.activeSelf)
        {
            if (UIRotator.Count == 2)
            {
                if (UIRotator[0] == deerUI)
                {
                    UIRotator.Remove(deerUI);
                    UIRotator.Insert(1, deerUI);
                    UIRotator[1].transform.localPosition = new Vector2(145, 0);
                    UIRotator[0].transform.localPosition = new Vector2(0, 0);
                    UIRotator[1].transform.SetAsLastSibling();
                    UIRotator[0].transform.SetAsLastSibling();
                }
            }
            else if (UIRotator.Count == 3)
            {
                GameObject oldLeft = UIRotator[2];
                UIRotator.RemoveAt(2);
                UIRotator.Insert(0, oldLeft);
                UIRotator[0].transform.localPosition = new Vector2(0, 0);
                UIRotator[1].transform.localPosition = new Vector2(145, 0);
                UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                UIRotator[1].transform.SetAsLastSibling();
                UIRotator[UIRotator.Count - 1].transform.SetAsLastSibling();
                UIRotator[0].transform.SetAsLastSibling();
            }
            else if (UIRotator.Count == 4)
            {

                GameObject oldLeft = UIRotator[UIRotator.Count - 1];
                UIRotator.RemoveAt(UIRotator.Count - 1);
                UIRotator.Insert(0, oldLeft);
                UIRotator[0].transform.localPosition = new Vector2(0, 0);
                UIRotator[1].transform.localPosition = new Vector2(145, 0);
                UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                UIRotator[1].transform.SetAsLastSibling();
                UIRotator[UIRotator.Count - 1].transform.SetAsLastSibling();
                UIRotator[0].transform.SetAsLastSibling();
                UIRotator[0].SetActive(true);
                UIRotator[1].SetActive(true);
                UIRotator[UIRotator.Count - 1].SetActive(true);
                UIRotator[2].SetActive(false);
            }
            else if (UIRotator.Count == 5)
            {

                GameObject oldLeft = UIRotator[UIRotator.Count - 1];
                UIRotator.RemoveAt(UIRotator.Count - 1);
                UIRotator.Insert(0, oldLeft);
                UIRotator[0].transform.localPosition = new Vector2(0, 0);
                UIRotator[1].transform.localPosition = new Vector2(145, 0);
                UIRotator[UIRotator.Count - 1].transform.localPosition = new Vector2(-145, 0);
                UIRotator[1].transform.SetAsLastSibling();
                UIRotator[UIRotator.Count - 1].transform.SetAsLastSibling();
                UIRotator[0].transform.SetAsLastSibling();
                UIRotator[0].SetActive(true);
                UIRotator[1].SetActive(true);
                UIRotator[UIRotator.Count - 1].SetActive(true);
                UIRotator[2].SetActive(false);
                UIRotator[3].SetActive(false);
            }
        }
    }
}
