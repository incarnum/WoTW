using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PylonCircle : MonoBehaviour {
    public IngCircle data;
    // Use this for initialization
    private Text Name;
    private Text Amount;
    private Image Icon;
    private string Info;
    private Text InfoBox;
    private int IngValue;
    private PylonUI parent;

	void Start () {
        Amount = gameObject.transform.Find("Amount").GetComponent<Text>();
        Icon = gameObject.transform.Find("Icon").GetComponent<Image>();
        parent = gameObject.transform.parent.GetComponent<PylonUI>();
        Debug.Log(parent);
		
	}
	
	// Update is called once per frame
	void Update () {
        Amount.text = data.Amount.ToString();
        Icon.overrideSprite = data.Icon;
        IngValue = data.IngValue;
		
	}

    public void onHover()
    {
        parent.HoverText(data.Info);
    }
    public void onExit()
    {
        parent.ReturnInfoToDefault();
    }

    public void Use()
    {
        if(data.Amount > 0)
        {

        }
    }
}
