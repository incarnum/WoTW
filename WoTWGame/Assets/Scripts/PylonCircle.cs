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

	void Start () {
        Name = gameObject.transform.Find("Name").GetComponent<Text>();
        Amount = gameObject.transform.Find("Amount").GetComponent<Text>();
        Icon = gameObject.transform.Find("Icon").GetComponent<Image>();
		
	}
	
	// Update is called once per frame
	void Update () {
        Name.text = data.Name;
        Amount.text = data.Amount.ToString();
        Icon.overrideSprite = data.Icon;
		
	}

    public void Use()
    {
        if(data.Amount > 0)
        {

        }
    }
}
