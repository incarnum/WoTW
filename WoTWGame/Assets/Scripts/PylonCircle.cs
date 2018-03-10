using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PylonCircle : MonoBehaviour {
    public IngCircle data;
    private InventoryScript inventory;
    private Text Name;
    private Text countUI;
    private Image Icon;
    private string Info;
    private Text InfoBox;
    private string IngValue;
    public PylonUI UI;
    public PylonScipt parent;
    

	void Start () {
        countUI = gameObject.transform.Find("Amount").GetComponent<Text>();
        Icon = gameObject.transform.Find("Icon").GetComponent<Image>();

        inventory = GameObject.Find("Player").GetComponent<InventoryScript>();
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
        data.Amount = (int)inventory.GetType().GetField(data.IngValue).GetValue(inventory);
        countUI.text = data.Amount.ToString();
        Icon.overrideSprite = data.Icon;
        

    }

    public void onHover()
    {
		if (parent.corrupted == false) {
			UI.HoverText (data.Info);
		} else if (data.IngNum == 0) {
			UI.HoverText ("Cleanses the corruption using energy from shrubs");
			print ("HEYY");
		} else if (data.IngNum == 1) {
			UI.HoverText ("Cleanses the corruption using energy from deer");
		} else if (data.IngNum == 2) {
			UI.HoverText ("Cleanses the corruption using energy from wolves");
		} else if (data.IngNum == 3) {
			UI.HoverText ("Cleanses the corruption using energy from rabbits");
		} else if (data.IngNum == 4) {
			UI.HoverText ("Cleanses the corruption using energy from owls");
		}
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
			if (!parent.corrupted || data.Amount >=3)
            parent.SelectCurrent(data.IngNum);
        }
    }
}
