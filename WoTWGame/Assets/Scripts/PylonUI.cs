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

    private InventoryScript invertroy;

    private Text CurrentInfo;

    //List of Cirlces
    private List<GameObject> Elements = new List<GameObject>();

	// Use this for initialization
	void Start () {
        invertroy = GameObject.Find("Player").GetComponent<InventoryScript>();
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
	}
	
	// Update is called once per frame
	void Update () {
        
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
