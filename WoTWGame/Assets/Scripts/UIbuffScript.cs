using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool shrubSpeedUp;
    public GameObject shrubSpeedUpGUI;
    public bool shrubSpeedDown;
	public GameObject shrubSpeedDownGUI;
	public Text shrubSizeGUI;
	public Text shrubSpeedGUI;
	public Text shrubToughnessGUI;

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
	public Text deerSizeGUI;
	public Text deerSpeedGUI;
	public Text deerToughnessGUI;

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
	public Text wolfSizeGUI;
	public Text wolfSpeedGUI;
	public Text wolfToughnessGUI;

    public SimpleEcologyMasterScript eco;

    // Use this for initialization
    void Start () {
      
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			UpdateUIBuffs ();
		}
	}

	public void UpdateUIBuffs() {
		shrubGrowGUI.SetActive (false);
		shrubShrinkGUI.SetActive (false);
		shrubSpeedUpGUI.SetActive (false);
		shrubSpeedDownGUI.SetActive (false);
		shrubToughGUI.SetActive (false);
		shrubWeakGUI.SetActive (false);
		deerGrowGUI.SetActive (false);
		deerShrinkGUI.SetActive (false);
		deerSpeedUpGUI.SetActive (false);
		deerSpeedDownGUI.SetActive (false);
		deerToughGUI.SetActive (false);
		deerWeakGUI.SetActive (false);
		wolfGrowGUI.SetActive (false);
		wolfShrinkGUI.SetActive (false);
		wolfSpeedUpGUI.SetActive (false);
		wolfSpeedDownGUI.SetActive (false);
		wolfToughGUI.SetActive (false);
		wolfWeakGUI.SetActive (false);
		shrubSizeGUI.text = "";
		shrubSpeedGUI.text = "";
		shrubToughnessGUI.text = "";
		deerSizeGUI.text = "";
		deerSpeedGUI.text = "";
		deerToughnessGUI.text = "";
		wolfSizeGUI.text = "";
		wolfSpeedGUI.text = "";
		wolfToughnessGUI.text = "";

		if (eco.shrubSizeMod > 0) {
			shrubGrowGUI.SetActive (true);
			UpdateText (shrubSizeGUI, eco.shrubSizeMod);
		} else if (eco.shrubSizeMod < 0) {
			shrubShrinkGUI.SetActive (true);
			UpdateText (shrubSizeGUI, eco.shrubSizeMod);
		}
		if (eco.shrubSpeedMod > 0) {
			shrubSpeedUpGUI.SetActive (true);
			UpdateText (shrubSpeedGUI, eco.shrubSpeedMod);
		} else if (eco.shrubSpeedMod < 0) {
			shrubSpeedDownGUI.SetActive (true);
			UpdateText (shrubSpeedGUI, eco.shrubSpeedMod);
		}
		if (eco.shrubToughMod > 0) {
			shrubToughGUI.SetActive (true);
			UpdateText (shrubToughnessGUI, eco.shrubToughMod);
		} else if (eco.shrubToughMod < 0) {
			shrubWeakGUI.SetActive (true);
			UpdateText (shrubToughnessGUI, eco.shrubToughMod);
		}

		if (eco.deerSizeMod > 0) {
			deerGrowGUI.SetActive (true);
			UpdateText (deerSizeGUI, eco.deerSizeMod);
		} else if (eco.deerSizeMod < 0) {
			deerShrinkGUI.SetActive (true);
			UpdateText (deerSizeGUI, eco.deerSizeMod);
		}
		if (eco.deerSpeedMod > 0) {
			deerSpeedUpGUI.SetActive (true);
			UpdateText (deerSpeedGUI, eco.deerSpeedMod);
		} else if (eco.deerSpeedMod < 0) {
			deerSpeedDownGUI.SetActive (true);
			UpdateText (deerSpeedGUI, eco.deerSpeedMod);
		}
		if (eco.deerToughMod > 0) {
			deerToughGUI.SetActive (true);
			UpdateText (deerToughnessGUI, eco.deerToughMod);
		} else if (eco.deerToughMod < 0) {
			deerWeakGUI.SetActive (true);
			UpdateText (deerToughnessGUI, eco.deerToughMod);
		}

		if (eco.wolfSizeMod > 0) {
			wolfGrowGUI.SetActive (true);
			UpdateText (wolfSizeGUI, eco.wolfSizeMod);
		} else if (eco.wolfSizeMod < 0) {
			wolfShrinkGUI.SetActive (true);
			UpdateText (wolfSizeGUI, eco.wolfSizeMod);
		}
		if (eco.wolfSpeedMod > 0) {
			wolfSpeedUpGUI.SetActive (true);
			UpdateText (wolfSpeedGUI, eco.wolfSpeedMod);
		} else if (eco.wolfSpeedMod < 0) {
			wolfSpeedDownGUI.SetActive (true);
			UpdateText (wolfSpeedGUI, eco.wolfSpeedMod);
		}
		if (eco.wolfToughMod > 0) {
			wolfToughGUI.SetActive (true);
			UpdateText (wolfToughnessGUI, eco.wolfToughMod);
		} else if (eco.wolfToughMod < 0) {
			wolfWeakGUI.SetActive (true);
			UpdateText (wolfToughnessGUI, eco.wolfToughMod);
		}

	}

	public void UpdateText(Text textBox, int num) {
		string newText = "x" + Mathf.Abs (num);
			textBox.text = newText;
	}
	
//	// Update is called once per frame
//	void Update ()
//    {
//        // checking shrub
//        if(eco.shrubSize != 1)
//        {
//            if(eco.shrubSize > 1)
//            {
//                shrubGrow = true;
//                shrubShrink = false;
//            }
//            else
//            {
//                shrubShrink = true;
//            }
//        }
//        else
//        {
//            shrubGrow = false;
//            shrubShrink = false;
//        }
//        if (eco.shrubDown != 0)
//        {
//            if (eco.shrubDown >= 1)
//            {
//                shrubTough = false;
//                shrubWeak = true;
//            }
//            else
//            {
//                shrubWeak = false;
//                shrubTough = true;
//            }
//        }
//        else
//        {
//            shrubWeak = false;
//            shrubTough = false;
//        }
//        if (eco.shrubUp != 0)
//        {
//            if (eco.shrubUp > 0)
//            {
//                deerSpeedUp = true;
//                deerSpeedDown = false;
//            }
//            else
//            {
//                deerSpeedDown = true;
//                deerSpeedUp = false;
//            }
//        }
//        else
//        {
//            deerSpeedUp = false;
//            deerSpeedDown = false;
//        }
//        if (eco.shrubSize != 1 || eco.shrubDown != 0 || eco.shrubUp !=0)
//        {
//            ShowShrub = true;
//        }
//        else
//        {
//            ShowShrub = false;
//        }
//
//        //checking deer
//        if (eco.deerSize != 1)
//        {
//            if (eco.deerSize >= 1)
//            {
//                deerGrow = true;
//                deerShrink = false;
//            }
//            else
//            {
//                deerShrink = true;
//                deerGrow = false;
//            }
//        }
//        else
//        {
//            deerGrow = false;
//            deerShrink = false;
//        }
//        if (eco.deerDown1 != 0)
//        {
//            if (eco.deerDown1 > 1)
//            {
//                deerTough = true;
//                deerWeak = false;
//            }
//            else
//            {
//                deerWeak = true;
//                deerTough = false;
//            }
//        }
//        else
//        {
//            deerWeak = false;
//            deerTough = false;
//        }
//        if (eco.deerSpeed != 2)
//        {
//            if (eco.deerSpeed > 2)
//            {
//                deerSpeedUp = true;
//                deerSpeedDown = false;
//            }
//            else
//            {
//                deerSpeedDown = true;
//                deerSpeedUp = false;
//            }
//        }
//        else
//        {
//            deerSpeedDown = false;
//            deerSpeedUp = false;
//        }
//
//        if (eco.deerSize != 1 || eco.deerDown1 != 0 || eco.deerSpeed != 2)
//        {
//            ShowDeer = true;
//        }
//        else
//        {
//            ShowDeer = false;
//        }
//        // checking wolves
//        if (eco.wolfSize != 1)
//        {
//            if (eco.wolfSize >= 1)
//            {
//                wolfGrow = true;
//                wolfShrink = false;
//            }
//            else
//            {
//                wolfShrink = true;
//                wolfGrow = false;
//            }
//        }
//        else
//        {
//            wolfShrink = false;
//            wolfGrow = false;
//        }
//        if (eco.wolfDown != 0)
//        {
//            if (eco.wolfDown >= 1)
//            {
//                wolfTough = true;
//                wolfWeak = false;
//            }
//            else
//            {
//                wolfWeak = true;
//                wolfTough = false;
//            }
//        }
//        else
//        {
//            wolfWeak = false;
//            wolfTough = false;
//        }
//        if (eco.wolfSpeed != 2)
//        {
//            if (eco.wolfSpeed >= 2)
//            {
//                wolfSpeedUp = true;
//                wolfSpeedDown = false;
//            }
//            else
//            {
//                wolfSpeedDown = true;
//                wolfSpeedUp = false;
//            }
//        }
//        else
//        {
//            wolfSpeedDown = false;
//            wolfSpeedUp = false;
//        }
//        if (eco.wolfSize != 1 || eco.wolfDown != 0 || eco.wolfSpeed != 2)
//        {
//            ShowWolf = true;
//        }
//        else
//        {
//            ShowWolf = false;
//        }
//
//        //Update Icons
//        shrubGUI.SetActive(ShowShrub);
//        deerGUI.SetActive(ShowDeer);
//        wolfGUI.SetActive(ShowWolf);
//
//        shrubGrowGUI.SetActive(shrubGrow);
//        shrubShrinkGUI.SetActive(shrubShrink);
//        shrubToughGUI.SetActive(shrubTough);
//        shrubWeakGUI.SetActive(shrubWeak);
//        shrubSpeedUpGUI.SetActive(shrubSpeedUp);
//        shrubSpeedDownGUI.SetActive(shrubSpeedDown);
//
//        deerGrowGUI.SetActive(deerGrow);
//        deerShrinkGUI.SetActive(deerShrink);
//        deerToughGUI.SetActive(deerTough);
//        deerWeakGUI.SetActive(deerWeak);
//        deerSpeedUpGUI.SetActive(deerSpeedUp);
//        deerSpeedDownGUI.SetActive(deerSpeedDown);
//
//        wolfGrowGUI.SetActive(wolfGrow);
//        wolfShrinkGUI.SetActive(wolfShrink);
//        wolfShrinkGUI.SetActive(wolfShrink);
//        wolfToughGUI.SetActive(wolfTough);
//        wolfWeakGUI.SetActive(wolfWeak);
//        wolfSpeedUpGUI.SetActive(wolfSpeedUp);
//        wolfSpeedDownGUI.SetActive(wolfSpeedDown);
//    }
//
//    private void NewMethod()
//    {
//        if (eco.shrubSize != 0x1)
//        {
//            if (eco.shrubSize >= 1)
//            {
//                shrubGrow = true;
//                shrubShrink = false;
//            }
//            else
//            {
//                shrubShrink = true;
//                shrubGrow = false;
//            }
//        }
//    }
}
