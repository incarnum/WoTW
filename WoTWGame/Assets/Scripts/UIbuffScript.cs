using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbuffScript : MonoBehaviour {
    public Texture deerIcon;
    public Texture wolfIcon;
    public Texture shrubIcon;

    public Texture growIcon;
    public Texture shrinkIcon;
    public Texture speedUpIcon;
    public Texture speedDownIcon;
    public Texture toughIcon;
    public Texture weakIcon;

    public bool shrubBuffed;
    public bool deerBuffed;
    public bool wolfBuffed;
    private SimpleEcologyMasterScript eco;

    // Use this for initialization
    void Start () {
        shrubBuffed = false;
        deerBuffed = false;
        wolfBuffed = false;

        
}
	
	// Update is called once per frame
	void Update () {

		// checking shrub
        if(eco.shrubSizeMod != 0)
        {
            if(eco.shrubSizeMod >= 1)
            {

            }
            else
            {

            }
        }
        if(eco.shrubToughMod != 0)
        {
            if(eco.shrubToughMod >= 1)
            {

            }
            else
            {

            }
        }

        //checking deer
        if (eco.deerSizeMod != 0)
        {
            if (eco.deerSizeMod >= 1)
            {

            }
            else
            {

            }
        }
        if (eco.deerToughMod != 0)
        {
            if (eco.deerToughMod >= 1)
            {

            }
            else
            {

            }
        }
        if (eco.deerSpeedMod != 0)
        {
            if (eco.deerSpeedMod >= 1)
            {

            }
            else
            {

            }
        }

        // checking wolves
        if (eco.wolfSizeMod != 0)
        {
            if (eco.wolfSizeMod >= 1)
            {

            }
            else
            {

            }
        }
        if (eco.wolfToughMod != 0)
        {
            if (eco.wolfToughMod >= 1)
            {

            }
            else
            {

            }
        }
        if (eco.wolfSpeedMod != 0)
        {
            if (eco.wolfSpeedMod >= 1)
            {

            }
            else
            {

            }
        }
    }
}
