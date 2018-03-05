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

        //Create Circles from Editor data
        foreach (IngCircle data in Ingredients)
        {
            GameObject newCircle = Instantiate(ItemPrefab);
            newCircle.transform.parent = transform;
            newCircle.GetComponent<PylonCircle>().data = data;
            Elements.Add(newCircle);
        }
        SpawnCircles();
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
        Debug.Log("Fuck you!");

    }

    public void ReturnInfoToDefault()
    {
        CurrentInfo.text = DefaultInfo;
    }
}
