using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corruptionManagerScript : MonoBehaviour
{
    public float infectTime;
    public float minimumInfectionPop;
    private float nextCorruptionTime;
    private SimpleEcologyMasterScript ecoManager;
    public float shrubPopStart, deerPopStart, wolfPopStart;
    public int shrubRange, deerRange, wolfRange, startShrubRange, startDeerRange, startWolfRange;
    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;
    // Use this for initialization
    void Start()
    {
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
        nextCorruptionTime = Time.time + infectTime;
        ecoManager = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
        shrubPopStart = 10f;
        deerPopStart = 10f;
        wolfPopStart = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextCorruptionTime <= Time.time)
        {
            Corrupt();
            nextCorruptionTime = Time.time + infectTime;
        }
    }

    void Corrupt()
    {
        if (!shrub.corrupting && !deer.corrupting && !wolf.corrupting)
        {
            int rando1 = Random.Range(0, shrubRange);
            int rando2 = Random.Range(0, deerRange);
            int rando3 = Random.Range(0, wolfRange);
            if (rando1 == 0)
            {
                shrub.corrupting = true;
                if (shrub.corruptedPop < shrubPopStart && shrub.pop > minimumInfectionPop)
                {
                    shrub.corruptedPop = shrubPopStart;
                }
            }
            else if (rando2 == 0)
            {
                deer.corrupting = true;
                if (deer.corruptedPop < deerPopStart && deer.pop > minimumInfectionPop)
                {
                    deer.corruptedPop = deerPopStart;
                }
            }
            else if (rando3 == 0)
            {
                wolf.corrupting = true;
                if (wolf.corruptedPop < wolfPopStart && wolf.pop > minimumInfectionPop)
                {
                    wolf.corruptedPop = wolfPopStart;
                }
            }
        }
    }
}
