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

    private Text CurrentInfo;

    //List of Cirlces
    private List<GameObject> Elements = new List<GameObject>();

	// Use this for initialization
	void Start () {
        CurrentInfo = gameObject.transform.Find("Info").GetComponent<Text>();
        CurrentInfo.text = DefaultInfo;
        int buttonTot = Ingredients.Count;

        //Create Circles from Editor data
        for(int i = 0; i  < Ingredients.Count; i ++)
        {
            GameObject newButton = Instantiate(ItemPrefab) as GameObject;
            newButton.GetComponent<PylonCircle>().data = Ingredients[i];
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / buttonTot) * i;

        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SpawnCircles()
    {
        foreach (GameObject cir  in Elements)
        {

        }
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
