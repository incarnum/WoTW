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
    // Use this for initialization
    void Start()
    {
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
        if (!ecoManager.corruptingShrubs && !ecoManager.corruptingDeer && !ecoManager.corruptingWolves)
        {
            int rando1 = Random.Range(0, shrubRange);
            int rando2 = Random.Range(0, deerRange);
            int rando3 = Random.Range(0, wolfRange);
            if (rando1 == 0)
            {
                ecoManager.corruptingShrubs = true;
                if (ecoManager.corruptedShrubPop < shrubPopStart && ecoManager.shrubPop > minimumInfectionPop)
                {
                    ecoManager.corruptedShrubPop = shrubPopStart;
                }
            }
            else if (rando2 == 0)
            {
                ecoManager.corruptingDeer = true;
                if (ecoManager.corruptedDeerPop < deerPopStart && ecoManager.deerPop > minimumInfectionPop)
                {
                    ecoManager.corruptedDeerPop = deerPopStart;
                }
            }
            else if (rando3 == 0)
            {
                ecoManager.corruptingWolves = true;
                if (ecoManager.corruptedWolfPop < wolfPopStart && ecoManager.wolfPop > minimumInfectionPop)
                {
                    ecoManager.corruptedWolfPop = wolfPopStart;
                }
            }
        }
    }
}
