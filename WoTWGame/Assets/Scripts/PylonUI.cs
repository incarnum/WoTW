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

    private Text CurrentInfo;
    private int buttonSelected;
    //List of Cirlces
    private List<GameObject> Elements = new List<GameObject>();

	// Use this for initialization
	void Start () {
        CurrentInfo = gameObject.transform.Find("Info").GetComponent<Text>();
        CurrentInfo.text = DefaultInfo;
        int buttonTot = Ingredients.Count;

        //Create and Place Circles from Editor data
        for(int i = 0; i  < Ingredients.Count; i ++)
        {
            GameObject newButton = Instantiate(ItemPrefab) as GameObject;
            newButton.GetComponent<PylonCircle>().data = Ingredients[i];
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / buttonTot) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;
            Elements.Add(newButton);
        }
        if (keyboardControls)
        {
            buttonSelected = 0;
            OnEnter();
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
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (buttonSelected < Elements.Count -1)
                {
                    OnExit();
                    buttonSelected++;
                    OnEnter();
                }
                else
                {
                    OnExit();
                    buttonSelected = 0;
                    OnEnter();
                }

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (buttonSelected > 0)
                {
                    OnExit();
                    buttonSelected--;
                    OnEnter();
                }
                else
                {
                    OnExit();
                    buttonSelected = Elements.Count - 1;
                    OnEnter();
                }

            }
        }

    }
    public void OnEnter()
    {
        Elements[buttonSelected].transform.Find("Ring").GetComponent<Image>().enabled = true;
        Elements[buttonSelected].transform.Find("Amount").GetComponent<Text>().enabled = true;
        Elements[buttonSelected].GetComponent<PylonCircle>().onHover();

    }
    public void OnExit()
    {
        Elements[buttonSelected].transform.Find("Ring").GetComponent<Image>().enabled = false;
        Elements[buttonSelected].transform.Find("Amount").GetComponent<Text>().enabled = false;
        Elements[buttonSelected].GetComponent<PylonCircle>().onExit();
    }

    public void HoverText(string source)
    {
        CurrentInfo.text = source;
    }

    public void ReturnInfoToDefault()
    {
        CurrentInfo.text = DefaultInfo;
    }
}
