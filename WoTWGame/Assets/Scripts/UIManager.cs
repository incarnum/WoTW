using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject shrubUI, deerUI, wolfUI, rabbitUI, owlUI, manager, leftButton, rightButton;
    private List<GameObject> UIRotator;
    public bool shrubOn, deerOn, wolfOn, rabbitOn, owlOn;
    public float X;
    public int currentSelection;
	public float rightX;
	public float leftX;
	// Use this for initialization
	void Start () {
        UIRotator = new List<GameObject>();
        UIRotator.Insert(0, shrubUI);
        shrubOn = true;
        X = 150;
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


                UIRotator[0].GetComponent<NewUIScript>().Move(new Vector2(-X, 0));
                UIRotator[1].GetComponent<NewUIScript>().Move(new Vector2(X, 0));
                UIRotator [0].GetComponent<NewUIScript> ().UpperLayer.SetActive (true);
				UIRotator [1].GetComponent<NewUIScript> ().UpperLayer.SetActive (true);
            }
        }
        if (!wolfOn)
        {
            if (wolfUI.activeSelf)
            {
                wolfOn = true;
                UIRotator.Insert(2, wolfUI);
                leftButton.SetActive(true);
                rightButton.SetActive(true);
                currentSelection = 1;
                for (int i = 0; i < UIRotator.Count; i++)
                {
                    UIRotator[i].GetComponent<NewUIScript>().Move(new Vector2(X * (i - currentSelection), 0));
                    if (i == currentSelection)
                    {
                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(true);
                    }
                    else
                    {
                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(false);
                    }
                }
            }
        }
        if (!rabbitOn)
        {
            if (rabbitUI.activeSelf)
            {
                rabbitOn = true;
                UIRotator.Insert(3, rabbitUI);

                UIRotator[3].GetComponent<NewUIScript>().Move(new Vector2(X * (3 - currentSelection), 0));
            }
        }
        if (!owlOn)
        {
            if (owlUI.activeSelf)
            {
                owlOn = true;
                UIRotator.Insert(4, owlUI);

                UIRotator[4].GetComponent<NewUIScript>().Move(new Vector2(X * (4 - currentSelection), 0));
            }
        }
	}

    public void RotateLeft()
    {
        if (manager.activeSelf)
        {
            if(currentSelection > 0)
            {
                currentSelection -= 1;
                for (int i = 0; i < UIRotator.Count; i++)
                {
                    UIRotator[i].GetComponent<NewUIScript>().Move(new Vector2(X * (i - currentSelection), 0));
                    if (i == currentSelection)
                    {
                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(true);
                        UIRotator[i].transform.SetAsLastSibling();
                    }
                    else
                    {
                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(false);
                    }
                }
            }
            if(currentSelection == 0)
            {
                leftButton.SetActive(false);
            }
            if (!rightButton.activeSelf)
            {
                rightButton.SetActive(true);
            }
        }
    }

    public void RotateRight()
    {
        if (manager.activeSelf)
        {
            if (currentSelection < UIRotator.Count - 1)
            {
                currentSelection += 1;
                for (int i = 0; i < UIRotator.Count; i++)
                {
                    UIRotator[i].GetComponent<NewUIScript>().Move(new Vector2(X * (i - currentSelection), 0));
                    if (i == currentSelection)
                    {
                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(true);
                        UIRotator[i].transform.SetAsLastSibling();
                    }
                    else
                    {
                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(false);
                    }
                }
            }
            if (currentSelection == UIRotator.Count - 1)
            {
                rightButton.SetActive(false);
            }
            if (!leftButton.activeSelf)
            {
                leftButton.SetActive(true);
            }
        }
    }
	public void ActivateDeer() {
		deerUI.SetActive (true);
	}
	public void ActivateWolves() {
		wolfUI.SetActive (true);
//		UIRotator.RemoveAll;
//		UIRotator.Add(deerUI);
//		UIRotator.Add(shrubUI);
//		UIRotator.Add (wolfUI);
	}
}
