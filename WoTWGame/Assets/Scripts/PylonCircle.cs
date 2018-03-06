using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PylonCircle : MonoBehaviour {
    public IngCircle data;
    private InventoryScript invertroy;
    private Text Name;
    private Text countUI;
    private Image Icon;
    private string Info;
    private Text InfoBox;
    private string IngValue;
    private PylonUI UI;
    private PylonScipt parent;

	void Start () {
        countUI = gameObject.transform.Find("Amount").GetComponent<Text>();
        Icon = gameObject.transform.Find("Icon").GetComponent<Image>();
        UI = gameObject.transform.parent.GetComponent<PylonUI>();
        invertroy = GameObject.Find("Player").GetComponent<InventoryScript>();
        parent = gameObject.transform.parent.parent.GetComponent<PylonScipt>();
    }
	
	// Update is called once per frame
	void OnEnable () {
        data.Amount = (int)invertroy.GetType().GetField(data.IngValue).GetValue(invertroy);
        countUI.text = data.Amount.ToString();
        Icon.overrideSprite = data.Icon;
        

    }

    public void onHover()
    {
        UI.HoverText(data.Info);
    }
    public void onExit()
    {
        UI.ReturnInfoToDefault();
    }

    public void Use()
    {
        if(data.Amount > 0)
        {
            data.Amount--;
        }
    }
}
