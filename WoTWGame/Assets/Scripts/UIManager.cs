using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject shrubUI, deerUI, wolfUI, rabbitUI, owlUI, manager, leftButton, rightButton;
    private List<GameObject> UIRotator;
    public bool shrubOn, deerOn, wolfOn, rabbitOn, owlOn;
    public float X;
    public int currentSelection;
	public float rightX;
	public float leftX;
	public Text size;
	public Text speed;
	public Text toughness;
	public GameObject buffDisplay;
	public basePopulation pop;
	public bool maximized;
	public GameObject deerRightInfluence;
	public GameObject wolfRightInfluence;
	public GameObject rabbitRightIncluence;
	public GameObject antlerUI;
	public GameObject fangUI;
	public GameObject rabbitFootUI;
	public GameObject owlFeatherUI;
	// Use this for initialization
	void Start () {
        UIRotator = new List<GameObject>();
        UIRotator.Insert(0, shrubUI);
        shrubOn = true;
        X = 150;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
			if (maximized)
            RotateLeft();
        }
        if (Input.GetKeyDown("d"))
        {
			if (maximized)
            RotateRight();
        }
		if (Input.GetKeyDown("tab"))
		{
			UpdateMouseoverInfo ();
			if (!maximized) {
				if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().canMove) {
					MaximizeBars ();
					maximized = true;
				}
			} else {
				ShrinkBars ();
				maximized = false;
			}


		}
        if (!deerOn)
        {
            if (deerUI.activeSelf)
            {
				deerOn = true;
				UIRotator.Insert(1, deerUI);
				//leftButton.SetActive(true);
				rightButton.SetActive(true);
				currentSelection = 0;
				for (int i = 0; i < UIRotator.Count; i++)
				{
					UIRotator[i].GetComponent<NewUIScript>().Move(new Vector2(X * (i - currentSelection), 0));
					if (i == currentSelection)
					{
//						UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(true);
					}
					else
					{
						UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(false);
					}
				}
            }
        }
        if (!wolfOn)
        {
            if (wolfUI.activeSelf)
            {
                wolfOn = true;
                UIRotator.Insert(2, wolfUI);
                //leftButton.SetActive(true);
                //rightButton.SetActive(true);
                currentSelection = 0;
                for (int i = 0; i < UIRotator.Count; i++)
                {
                    UIRotator[i].GetComponent<NewUIScript>().Move(new Vector2(X * (i - currentSelection), 0));
                    if (i == currentSelection)
                    {
//                        UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(true);
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
		if (manager.activeSelf && deerOn)
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
		UpdateMouseoverInfo ();
    }

    public void RotateRight()
    {
		if (manager.activeSelf && deerOn)
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
		UpdateMouseoverInfo ();
    }
	public void ActivateDeer() {
		deerUI.SetActive (true);
		antlerUI.SetActive (true);
	}
	public void ActivateWolves() {
		wolfUI.SetActive (true);
		deerRightInfluence.SetActive (true);
		fangUI.SetActive (true);
//		UIRotator.RemoveAll;
//		UIRotator.Add(deerUI);
//		UIRotator.Add(shrubUI);
//		UIRotator.Add (wolfUI);
	}
	public void ActivateRabbits() {
		rabbitUI.SetActive (true);
		wolfRightInfluence.SetActive (true);
		rabbitFootUI.SetActive (true);
	}
	public void ActivateOwls () {
		owlUI.SetActive (true);
		rabbitRightIncluence.SetActive (true);
		owlFeatherUI.SetActive (true);
	}

	public void UpdateMouseoverInfo () {
		
		if (currentSelection == 0) {
			pop = GameObject.Find ("CreatureManager").GetComponent<ShrubPopulation> ();
		} else if (currentSelection == 1) {
			pop = GameObject.Find ("CreatureManager").GetComponent<DeerPopulation> ();
		} else if (currentSelection == 2) {
			pop = GameObject.Find ("CreatureManager").GetComponent<WolfPopulation> ();
		} else if (currentSelection == 3) {
			pop = GameObject.Find ("CreatureManager").GetComponent<RabbitPopulation> ();
		} else if (currentSelection == 4) {
			pop = GameObject.Find ("CreatureManager").GetComponent<OwlPopulation> ();
		}
		print (pop);
		size.text = pop.sizeMod.ToString ();
		speed.text = pop.speedMod.ToString ();
		toughness.text = pop.toughMod.ToString ();
	}

	void OnEnable() {
		UpdateMouseoverInfo ();
	}

	public void MaximizeBars() {
		manager.GetComponent<RectTransform> ().anchorMin = new Vector2 (.5f, .5f);
		manager.GetComponent<RectTransform> ().anchorMax = new Vector2 (.5f, .5f);
		manager.GetComponent<RectTransform> ().pivot = new Vector2 (.5f, .5f);
		manager.GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
		manager.GetComponent<RectTransform> ().localPosition = new Vector2 (0, 0);
		UIRotator[currentSelection].GetComponent<NewUIScript>().UpperLayer.SetActive(true);
		buffDisplay.SetActive (true);
		GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().popBarPaused = true;
		GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
	}

	public void ShrinkBars() {
		manager.GetComponent<RectTransform> ().localScale = new Vector2 (.4f, .4f);
		manager.GetComponent<RectTransform> ().localPosition = new Vector2 (45f, 45f);
		manager.GetComponent<RectTransform> ().anchorMin = new Vector2 (0, 0);
		manager.GetComponent<RectTransform> ().anchorMax = new Vector2 (0, 0);
		manager.GetComponent<RectTransform> ().pivot = new Vector2 (.5f, .5f);
		currentSelection = 0;
		UIRotator[currentSelection].GetComponent<NewUIScript>().UpperLayer.SetActive(false);
		for (int i = 0; i < UIRotator.Count; i++)
		{
			UIRotator[i].GetComponent<NewUIScript>().Move(new Vector2(X * (i - currentSelection), 0));
			if (i == currentSelection)
			{
				UIRotator[i].transform.SetAsLastSibling();
			}
			else
			{
				UIRotator[i].GetComponent<NewUIScript>().UpperLayer.SetActive(false);
			}
		}
		buffDisplay.SetActive (false);
		GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().popBarPaused = false;
		GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().CheckIfICanMove ();
	}
}
