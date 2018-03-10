using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PylonUI : MonoBehaviour {

    //Collects variables from Editor
    public List<IngCircle> Ingredients = new List<IngCircle>();
    public string DefaultInfo;
    public GameObject ItemPrefab;
    public bool keyboardControls;
	public RectTransform itemHolder;
	public bool corrupted;
	public int numberOfActiveIngredients;

    private Text CurrentInfo;
    private int buttonSelected;

	private float startRotation;
	public float targetRotation;
	private float startTime;
	private float rotationDuration = .15f;
	private bool rotating;

    //List of Cirlces
    private List<GameObject> Elements = new List<GameObject>();

	// Use this for initialization
	void Start () {
        CurrentInfo = gameObject.transform.Find("Info").GetComponent<Text>();
        //CurrentInfo.text = DefaultInfo;


        if (keyboardControls)
        {
            buttonSelected = 0;
        }
    }
    // Update is called once per frame
    void Update () {
        if (keyboardControls)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Elements[buttonSelected].GetComponent<PylonCircle>().Use();
            }

			if (Input.GetKeyDown(KeyCode.Q))
			{
				OnExit();
			}

			if (Input.GetKeyDown(KeyCode.A))
            {
                if (buttonSelected < Elements.Count -1)
                {
                    buttonSelected++;
                }
                else
                {
                    buttonSelected = 0;
                }
				RotateRight ();
            }
			if (Input.GetKeyDown(KeyCode.D))
            {
                if (buttonSelected > 0)
                {
                    buttonSelected--;
                }
                else
                {
                    buttonSelected = Elements.Count - 1;
                }
				RotateLeft ();
            }
        }
		if (rotating) {
			float newAngle = Mathf.LerpAngle (startRotation, targetRotation, (Time.time - startTime) / rotationDuration);
			itemHolder.rotation = Quaternion.Euler (0, 0, newAngle);
			if (Time.time - startTime >= rotationDuration) {
				rotating = false;
			}
			foreach(RectTransform child in itemHolder) {
				child.rotation = Quaternion.Euler(0,0,0);
			}
		}

    }
    public void OnEnable()
    {
        //Elements[buttonSelected].transform.Find("Ring").GetComponent<Image>().enabled = true;
        //Elements[buttonSelected].transform.Find("Amount").GetComponent<Text>().enabled = true;
		numberOfActiveIngredients = 1;
		if (GameObject.Find ("Deer UI") != null)
			numberOfActiveIngredients += 1;
		if (GameObject.Find ("Wolf UI") != null)
			numberOfActiveIngredients += 1;
		if (GameObject.Find ("Rabbit UI") != null)
			numberOfActiveIngredients += 1;
		if (GameObject.Find ("Owl UI") != null)
			numberOfActiveIngredients += 1;
		int buttonTot = numberOfActiveIngredients;
		itemHolder.rotation = Quaternion.Euler (0, 0, (2 * Mathf.PI / buttonTot) * buttonSelected * Mathf.Rad2Deg);
		targetRotation = (2 * Mathf.PI / buttonTot) * buttonSelected * Mathf.Rad2Deg;
		//Create and Place Circles from Editor data
		for(int i = 0; i  < numberOfActiveIngredients; i ++)
		{
			GameObject newButton = Instantiate(ItemPrefab) as GameObject;
			newButton.GetComponent<PylonCircle>().data = Ingredients[i];
			newButton.GetComponent<PylonCircle> ().UI = this;
			newButton.transform.SetParent(itemHolder, false);
			float theta = (2 * Mathf.PI / buttonTot) * i;
			float xPos = Mathf.Sin(theta) * 150f;
			float yPos = Mathf.Cos(theta) * 150f;
			//newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;
			newButton.GetComponent<SimpleSlideScript> ().Move (new Vector2 (xPos, yPos), .1f);
			Elements.Add(newButton);
		}
		foreach(RectTransform child in itemHolder) {
			child.rotation = Quaternion.Euler(0,0,0);
		}
		transform.Find("SelectionRing").GetComponent<SimpleSlideScript> ().Move (new Vector2 (0f, 149.7f), .1f);
		StartText ();
    }
    public void OnExit()
    {
        //Elements[buttonSelected].transform.Find("Ring").GetComponent<Image>().enabled = false;
        //Elements[buttonSelected].transform.Find("Amount").GetComponent<Text>().enabled = false;
        //Elements[buttonSelected].GetComponent<PylonCircle>().onExit();
		foreach (GameObject ele in Elements) {
			ele.GetComponent<SimpleSlideScript> ().Move (new Vector2 (0, 0), .1f);
			Destroy (ele, .1f);
			StartCoroutine (WaitDisable ());
		}
		Elements.Clear ();
		transform.Find("SelectionRing").GetComponent<SimpleSlideScript> ().Move (new Vector2 (0, 0), .1f);
        GameObject.Find("Player").GetComponent<PlayerControllerScript>().pylonPaused = false;
        GameObject.Find("Player").GetComponent<PlayerControllerScript>().CheckIfICanMove();
    }

	IEnumerator WaitDisable() {
		yield return new WaitForSeconds (.1f);
		gameObject.SetActive (false);
		yield break;
	}


    public void HoverText(string source)
    {
        CurrentInfo.text = source;
		print (source);
    }

	private void StartText() {
		CurrentInfo = gameObject.transform.Find("Info").GetComponent<Text>();

		if (gameObject.transform.parent.GetComponent<PylonScipt>().corrupted == false) {
			CurrentInfo.text = Elements [buttonSelected].GetComponent<PylonCircle> ().data.Info;
		} else if (Elements [buttonSelected].GetComponent<PylonCircle> ().data.IngNum == 0) {
			CurrentInfo.text = "Cleanses the corruption using energy from shrubs";
		} else if (Elements [buttonSelected].GetComponent<PylonCircle> ().data.IngNum == 1) {
			CurrentInfo.text = "Cleanses the corruption using energy from deer";
		} else if (Elements [buttonSelected].GetComponent<PylonCircle> ().data.IngNum == 2) {
			CurrentInfo.text = "Cleanses the corruption using energy from wolves";
		}
	}

    public void ReturnInfoToDefault()
    {
        CurrentInfo.text = DefaultInfo;
    }

	public void OpenMenu() {

	}

	public void RotateRight() {
		rotating = true;
		targetRotation += (2 * Mathf.PI / numberOfActiveIngredients) * Mathf.Rad2Deg;
		startTime = Time.time;
		itemHolder.rotation = Quaternion.Euler (0, 0, itemHolder.rotation.eulerAngles.z + 1f);
		startRotation = itemHolder.eulerAngles.z;

		//Elements[buttonSelected].GetComponent<PylonCircle>().onHover();
		StartText();
	}

	public void RotateLeft() {
		rotating = true;
		targetRotation -= (2 * Mathf.PI / numberOfActiveIngredients) * Mathf.Rad2Deg;
		startTime = Time.time;
		itemHolder.rotation = Quaternion.Euler (0, 0, itemHolder.rotation.eulerAngles.z - 1f);
		startRotation = itemHolder.eulerAngles.z;

		//Elements[buttonSelected].GetComponent<PylonCircle>().onHover();
		StartText();
	}
}
