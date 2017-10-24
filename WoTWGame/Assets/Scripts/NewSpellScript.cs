using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewSpellScript : MonoBehaviour {
    private SpellcraftingAltarScript sac;
    private InventoryScript inv;
    public GameObject cleanseCorruption, toughenShrub, toughenDeer, toughenWolf, weakenShrub, weakenDeer, weakenWolf, enlargeShrub, enlargeDeer, enlargeWolf, shrinkShrub, shrinkDeer, shrinkWolf, hastenShrub, hastenDeer, hastenWolf, slowShrub, slowDeer, slowWolf;
    public GameObject spellPrefab;
    public List<GameObject> uncastTomes;

    // Use this for initialization
    void Start () {
        sac = GameObject.Find("SpellcraftingAltar").GetComponent<SpellcraftingAltarScript>();
        inv = GameObject.Find("Player").GetComponent<InventoryScript>();
    }
	
	// Update is called once per frame

    public void EnlargeShrub ()
    {
        inv.berryNum -= 3;
        CreateSpell(0, 0, 2);
    }
    public void ShrinkShrub()
    {
        inv.berryNum -= 2;
        inv.fangNum -= 1;
        CreateSpell(0, 0, 0);
    }
    public void ToughenShrub()
    {
        inv.berryNum -= 2;
        inv.fangNum -= 1;
        CreateSpell(0, 2, 2);
    }
    public void WeakenShrub()
    {
        inv.berryNum -= 1;
        inv.fangNum -= 2;
        CreateSpell(0, 2, 0);
    }
    public void HastenShrub()
    {
        inv.berryNum -= 2;
        inv.antlerNum -= 1;
        CreateSpell(0, 1, 2);
    }
    public void SlowShrub()
    {
        inv.berryNum -= 1;
        inv.antlerNum -= 1;
        inv.fangNum -= 1;
        CreateSpell(0, 1, 0);
    }

    public void EnlargeDeer()
    {
        inv.berryNum -= 2;
        inv.antlerNum -= 1;
        CreateSpell(1, 0, 2);
    }
    public void ShrinkDeer()
    {
        inv.berryNum -= 1;
        inv.antlerNum -= 1;
        inv.fangNum -= 1;
        CreateSpell(1, 0, 0);
    }
    public void ToughenDeer()
    {
        inv.berryNum -= 1;
        inv.antlerNum -= 1;
        inv.fangNum -= 1;
        CreateSpell(1, 2, 2);
    }
    public void WeakenDeer()
    {
        inv.antlerNum -= 1;
        inv.fangNum -= 2;
        CreateSpell(1, 2, 0);
    }
    public void HastenDeer()
    {
        inv.berryNum -= 1;
        inv.antlerNum -= 2;
        CreateSpell(1, 1, 2);
    }
    public void SlowDeer()
    {
        inv.antlerNum -= 2;
        inv.fangNum -= 1;
        CreateSpell(1, 1, 0);
    }

    public void EnlargeWolf()
    {
        inv.berryNum -= 2;
        inv.fangNum -= 1;
        CreateSpell(2, 0, 2);
    }
    public void ShrinkWolf()
    {
        inv.berryNum -= 1;
        inv.fangNum -= 2;
        CreateSpell(2, 0, 0);
    }
    public void ToughenWolf()
    {
        inv.berryNum -= 1;
        inv.fangNum -= 2;
        CreateSpell(2, 2, 2);
    }
    public void WeakenWolf()
    {
        inv.fangNum -= 3;
        CreateSpell(2, 2, 0);
    }
    public void HastenWolf()
    {
        inv.berryNum -= 1;
        inv.antlerNum -= 1;
        inv.fangNum -= 1;
        CreateSpell(2, 1, 2);
    }
    public void SlowWolf()
    {
        inv.antlerNum -= 1;
        inv.fangNum -= 2;
        CreateSpell(2, 1, 0);
    }

    public void CleanseCorruption()
    {
        for(int i = 3; i > 0; i--)
        {
            if(inv.corrAntlerNum >= inv.corrBerryNum && inv.corrAntlerNum >= inv.corrFangNum)
            {
                inv.corrAntlerNum--;
            }
            else if(inv.corrBerryNum >= inv.corrFangNum)
            {
                inv.corrBerryNum--;
            }
            else
            {
                inv.corrFangNum--;
            }
        }
        CreateSpell(3,3,0);
    }

    public void CreateSpell(int targ, int eff, int mod)
    {
        // targ 0 is shrubs, 1 is deer, 2 is wolves
        // eff 0 is size, 1 is speed, 2 is strength
        // mod 0 is negative, 2 is positive
        GameObject newSpell = Instantiate(spellPrefab) as GameObject;
        newSpell.GetComponent<SpellScript>().target = targ;
        newSpell.GetComponent<SpellScript>().effect = eff;
        if (mod == 0)
        {
            newSpell.GetComponent<SpellScript>().strength = -1;
        }
        else if (mod == 1)
        {
            newSpell.GetComponent<SpellScript>().strength = 0;
        }
        else if (mod == 2)
        {
            newSpell.GetComponent<SpellScript>().strength = 1;
        }
        string spellPreviewString = "";
        if (eff == 0 && mod == 0)
        {
            spellPreviewString += "Shrink ";
        }
        else if (eff == 0 && mod == 1)
        {
            spellPreviewString += "Reset size of ";
        }
        else if (eff == 0 && mod == 2)
        {
            spellPreviewString += "Enlarge ";
        }
        else if (eff == 1 && mod == 0)
        {
            spellPreviewString += "Slow ";
        }
        else if (eff == 1 && mod == 1)
        {
            spellPreviewString += "Reset speed of ";
        }
        else if (eff == 1 && mod == 2)
        {
            spellPreviewString += "Hasten  ";
        }
        else if (eff == 2 && mod == 0)
        {
            spellPreviewString += "Weaken ";
        }
        else if (eff == 2 && mod == 1)
        {
            spellPreviewString += "Reset toughness of ";
        }
        else if (eff == 2 && mod == 2)
        {
            spellPreviewString += "Toughen ";
        }

        if (targ == 0)
        {
            spellPreviewString += "shrubs";
        }
        else if (targ == 1)
        {
            spellPreviewString += "deer";
        }
        else if (targ == 2)
        {
            spellPreviewString += "wolves";
        }
        else if (targ == 3)
        {
            spellPreviewString += "Cleanse Corruption";
        }

        newSpell.GetComponentsInChildren<Text>()[0].text = spellPreviewString;
        PlaceSpell(newSpell);
        uncastTomes.Add(newSpell);
        inv.UpdateNumbers();
    }

    private void PlaceSpell(GameObject spellbook)
    {
        if (GameObject.Find("ABSlot1").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot1").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot1").transform;
            GameObject.Find("ABSlot1").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot2").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot2").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot2").transform;
            GameObject.Find("ABSlot2").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot3").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot3").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot3").transform;
            GameObject.Find("ABSlot3").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot4").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot4").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot4").transform;
            GameObject.Find("ABSlot4").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot5").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot5").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot5").transform;
            GameObject.Find("ABSlot5").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
        else if (GameObject.Find("ABSlot6").GetComponent<SpellbookHolderScript>().holding == null)
        {
            spellbook.transform.SetParent(GameObject.Find("ABSlot6").transform);
            spellbook.GetComponent<RectTransform>().localPosition = Vector2.zero;
            spellbook.GetComponent<SpellScript>().parentToReturnTo = GameObject.Find("ABSlot6").transform;
            GameObject.Find("ABSlot6").GetComponent<SpellbookHolderScript>().holding = spellbook;
        }
    }

    void Update () {
        if (sac.touching)
        {
            if (inv.berryNum > 0)
            {
                if (inv.antlerNum > 0)
                {
                    if(inv.fangNum > 0)
                    {
                        shrinkDeer.SetActive(true);
                        slowShrub.SetActive(true);
                        toughenDeer.SetActive(true);
                        hastenWolf.SetActive(true);
                    }
                    else
                    {
                        shrinkDeer.SetActive(false);
                        slowShrub.SetActive(false);
                        toughenDeer.SetActive(false);
                        hastenWolf.SetActive(false);
                    }
                    if(inv.antlerNum > 1)
                    {
                        hastenDeer.SetActive(true);
                    }
                    else
                    {
                        hastenDeer.SetActive(false);
                    }
                }
                if (inv.berryNum > 1)
                {
                    if(inv.antlerNum > 0)
                    {
                        enlargeDeer.SetActive(true);
                        hastenShrub.SetActive(true);
                    }
                    else
                    {
                        enlargeDeer.SetActive(false);
                        hastenShrub.SetActive(false);
                    }
                    if(inv.fangNum > 0)
                    {
                        enlargeWolf.SetActive(true);
                        toughenShrub.SetActive(true);
                        shrinkShrub.SetActive(true);
                    }
                    else
                    {
                        enlargeWolf.SetActive(false);
                        toughenShrub.SetActive(false);
                        shrinkShrub.SetActive(false);
                    }
                    if(inv.berryNum > 2)
                    {
                        enlargeShrub.SetActive(true);
                    }
                    else
                    {
                        enlargeShrub.SetActive(false);
                    }
                }
                
            }
            if(inv.fangNum > 0)
            {
                if(inv.antlerNum > 1)
                {
                    slowDeer.SetActive(true);
                }
                else
                {
                    slowDeer.SetActive(false);
                }
                if(inv.fangNum > 1)
                {
                    if(inv.berryNum > 0)
                    {
                        shrinkWolf.SetActive(true);
                        weakenShrub.SetActive(true);
                        toughenWolf.SetActive(true);
                    }
                    else
                    {
                        shrinkWolf.SetActive(false);
                        weakenShrub.SetActive(false);
                        toughenWolf.SetActive(false);
                    }
                    if(inv.antlerNum > 0)
                    {
                        slowWolf.SetActive(true);
                        weakenDeer.SetActive(true);
                    }
                    else
                    {
                        slowWolf.SetActive(false);
                        weakenDeer.SetActive(false);
                    }
                }
                if(inv.fangNum > 2)
                {
                    weakenWolf.SetActive(true);
                }
                else
                {
                    weakenWolf.SetActive(false);
                }
            }
            if(inv.corrAntlerNum + inv.corrBerryNum + inv.corrFangNum > 2)
            {
                cleanseCorruption.SetActive(true);
            }
            else
            {
                cleanseCorruption.SetActive(false);
            }
        }
        else
        {
            shrinkDeer.SetActive(false);
            slowShrub.SetActive(false);
            toughenDeer.SetActive(false);
            hastenWolf.SetActive(false);
            hastenDeer.SetActive(false);
            enlargeDeer.SetActive(false);
            hastenShrub.SetActive(false);
            enlargeWolf.SetActive(false);
            toughenShrub.SetActive(false);
            shrinkShrub.SetActive(false);
            enlargeShrub.SetActive(false);
            slowDeer.SetActive(false);
            shrinkWolf.SetActive(false);
            weakenShrub.SetActive(false);
            toughenWolf.SetActive(false);
            slowWolf.SetActive(false);
            weakenDeer.SetActive(false);
            weakenWolf.SetActive(false);
            cleanseCorruption.SetActive(false);
        }
	}
}
