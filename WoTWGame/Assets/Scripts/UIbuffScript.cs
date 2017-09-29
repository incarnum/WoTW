using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbuffScript : MonoBehaviour {
    public bool ShowShrub;
    public GameObject shrubGUI;
    public bool ShowDeer;
    public GameObject deerGUI;
    public bool ShowWolf;
    public GameObject wolfGUI;

    public bool shrubGrow;
    public GameObject shrubGrowGUI;
    public bool shrubShrink;
    public GameObject shrubShrinkGUI;
    public bool shrubTough;
    public GameObject shrubToughGUI;
    public bool shrubWeak;
    public GameObject shrubWeakGUI;

    public bool deerGrow;
    public GameObject deerGrowGUI;
    public bool deerShrink;
    public GameObject deerShrinkGUI;
    public bool deerTough;
    public GameObject deerToughGUI;
    public bool deerWeak;
    public GameObject deerWeakGUI;
    public bool deerSpeedUp;
    public GameObject deerSpeedUpGUI;
    public bool deerSpeedDown;
    public GameObject deerSpeedDownGUI;

    public bool wolfGrow;
    public GameObject wolfGrowGUI;
    public bool wolfShrink;
    public GameObject wolfShrinkGUI;
    public bool wolfTough;
    public GameObject wolfToughGUI;
    public bool wolfWeak;
    public GameObject wolfWeakGUI;
    public bool wolfSpeedUp;
    public GameObject wolfSpeedUpGUI;
    public bool wolfSpeedDown;
    public GameObject wolfSpeedDownGUI;
    private SimpleEcologyMasterScript eco;

    // Use this for initialization
    void Start () {
}
	
	// Update is called once per frame
	void Update () {

		// checking shrub
        if(eco.shrubSizeMod != 0)
        {
            if(eco.shrubSizeMod >= 1)
            {
                shrubGrow = true;
                shrubShrink = false;
            }
            else
            {
                shrubShrink = true;
                shrubGrow = false;
            }
        }
        if(eco.shrubToughMod != 0)
        {
            if(eco.shrubToughMod >= 1)
            {
                shrubTough = true;
                shrubWeak = false;
            }
            else
            {
                shrubWeak = true;
                shrubTough = false;
            }
        }
        if (eco.shrubSizeMod != 0 || eco.shrubToughMod != 0)
        {
            ShowShrub = true;
        }
        else
        {
            ShowShrub = false;
        }

        //checking deer
        if (eco.deerSizeMod != 0)
        {
            if (eco.deerSizeMod >= 1)
            {
                deerGrow = true;
                deerShrink = false;
            }
            else
            {
                deerShrink = true;
                deerGrow = false;
            }
        }
        if (eco.deerToughMod != 0)
        {
            if (eco.deerToughMod >= 1)
            {
                deerTough = true;
                deerWeak = false;
            }
            else
            {
                deerWeak = true;
                deerTough = false;
            }
        }
        if (eco.deerSpeedMod != 0)
        {
            if (eco.deerSpeedMod >= 1)
            {
                deerSpeedUp = true;
                deerSpeedDown = false;
            }
            else
            {
                deerSpeedDown = true;
                deerSpeedUp = false;
            }
        }

        if(eco.deerSizeMod !=0 || eco.deerToughMod != 0 || eco.deerSpeedMod != 0)
        {
            ShowDeer = true;
        }
        else
        {
            ShowDeer = false;
        }

        // checking wolves
        if (eco.wolfSizeMod != 0)
        {
            if (eco.wolfSizeMod >= 1)
            {
                wolfGrow = true;
                wolfShrink = false;
            }
            else
            {
                wolfShrink = true;
                wolfGrow = false;
            }
        }
        if (eco.wolfToughMod != 0)
        {
            if (eco.wolfToughMod >= 1)
            {
                wolfTough = true;
                wolfWeak = false;
            }
            else
            {
                wolfWeak = true;
                wolfTough = false;
            }
        }
        if (eco.wolfSpeedMod != 0)
        {
            if (eco.wolfSpeedMod >= 1)
            {
                wolfSpeedUp = true;
                wolfSpeedDown = false;
            }
            else
            {
                wolfSpeedDown = true;
                wolfSpeedUp = false;
            }
        }
        if (eco.wolfSizeMod != 0 || eco.wolfToughMod != 0 || eco.wolfSpeedMod != 0)
        {
            ShowWolf = true;
        }
        else
        {
            ShowWolf = false;
        }

        //Update Icons
        shrubGUI.SetActive(ShowShrub);
        deerGUI.SetActive(ShowDeer);
        wolfGUI.SetActive(ShowWolf);

        shrubGrowGUI.SetActive(shrubGrow);
        shrubShrinkGUI.SetActive(shrubShrink);
        shrubToughGUI.SetActive(shrubTough);
        shrubWeakGUI.SetActive(shrubWeak);

        deerGrowGUI.SetActive(deerGrow);
        deerShrinkGUI.SetActive(deerShrink);
        deerToughGUI.SetActive(deerTough);
        deerWeakGUI.SetActive(deerWeak);
        deerSpeedUpGUI.SetActive(deerSpeedUp);
        deerSpeedDownGUI.SetActive(deerSpeedDown);

        wolfGrowGUI.SetActive(wolfGrow);
        wolfShrinkGUI.SetActive(wolfShrink);
        wolfShrinkGUI.SetActive(wolfShrink);
        wolfToughGUI.SetActive(wolfTough);
        wolfWeakGUI.SetActive(wolfWeak);
        wolfSpeedUpGUI.SetActive(wolfSpeedUp);
        wolfSpeedDownGUI.SetActive(wolfSpeedDown);
    }
}
