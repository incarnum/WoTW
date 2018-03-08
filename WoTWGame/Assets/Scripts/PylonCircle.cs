using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PylonCircle : MonoBehaviour {
    public IngCircle data;
    private InventoryScript invertory;
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
		UI = gameObject.transform.parent.parent.GetComponent<PylonUI>();
        invertory = GameObject.Find("Player").GetComponent<InventoryScript>();
		parent = gameObject.transform.parent.parent.parent.GetComponent<PylonScipt>();
    }
	
	// Update is called once per frame
	void Update(){
        if (UI.keyboardControls)
        {
            gameObject.GetComponent<EventTrigger>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<EventTrigger>().enabled = true;
        }
        data.Amount = (int)invertory.GetType().GetField(data.IngValue).GetValue(invertory);
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
            //data.Amount--;
            parent.SelectCurrent(data.IngNum);
        }
    }
}
