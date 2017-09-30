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
    public SimpleEcologyMasterScript eco;

    // Use this for initialization
    void Start () {
        
}
	
	// Update is called once per frame
	void Update ()
    {
        // checking shrub
        if(eco.shrubSize != 1)
        {
            if(eco.shrubSize > 1)
            {
                shrubGrow = true;
                shrubShrink = false;
            }
            else
            {
                shrubShrink = true;
            }
        }
        if (eco.shrubDown != 0)
        {
            if (eco.shrubDown >= 1)
            {
                shrubTough = false;
                shrubWeak = true;
            }
            else
            {
                shrubWeak = false;
                shrubTough = true;
            }
        }
        if (eco.shrubSize != 1 || eco.shrubDown != 0)
        {
            ShowShrub = true;
        }
        else
        {
            ShowShrub = false;
        }

        //checking deer
        if (eco.deerSize != 1)
        {
            if (eco.deerSize >= 1)
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
        if (eco.deerDown1 != 0)
        {
            if (eco.deerDown1 > 1)
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
        if (eco.deerSpeed != 2)
        {
            if (eco.deerSpeed > 2)
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

        if (eco.deerSize != 1 || eco.deerDown1 != 0 || eco.deerSpeed != 1)
        {
            ShowDeer = true;
        }
        else
        {
            ShowDeer = false;
        }
        // checking wolves
        if (eco.wolfSize != 1)
        {
            if (eco.wolfSize >= 1)
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
        if (eco.wolfDown != 0)
        {
            if (eco.wolfDown >= 1)
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
        if (eco.wolfSpeed != 1)
        {
            if (eco.wolfSpeed >= 1)
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
        if (eco.wolfSize != 1 || eco.wolfDown != 0 || eco.wolfSpeed != 1)
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

    private void NewMethod()
    {
        if (eco.shrubSize != 0x1)
        {
            if (eco.shrubSize >= 1)
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
    }
}
