using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corruptionManagerScript : MonoBehaviour
{
    public float infectTime;
    public float minimumInfectionPop;
    public float nextCorruptionTime;
    private SimpleEcologyMasterScript ecoManager;
    public float shrubPopStart, deerPopStart, wolfPopStart;
    public int shrubRange, deerRange, wolfRange, startShrubRange, startDeerRange, startWolfRange;
    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;
	private RabbitPopulation rabbit;
	private OwlPopulation owl;
	public int phase;
	public float currentCorruptionRate;
	public float phase0CorruptionRate;
	public float phase1CorruptionRate;
	public float storedNextCorruptTime;
    // Use this for initialization
    void Start()
    {
        shrub = GameObject.Find("CreatureManager").GetComponent<ShrubPopulation>();
        deer = GameObject.Find("CreatureManager").GetComponent<DeerPopulation>();
        wolf = GameObject.Find("CreatureManager").GetComponent<WolfPopulation>();
		rabbit = GameObject.Find("CreatureManager").GetComponent<RabbitPopulation>();
		owl = GameObject.Find("CreatureManager").GetComponent<OwlPopulation>();
        nextCorruptionTime = Time.time + infectTime;
        ecoManager = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
        shrubPopStart = 10f;
        deerPopStart = 10f;
        wolfPopStart = 10f;
		currentCorruptionRate = phase0CorruptionRate;
    }

    // Update is called once per frame
    void Update()
    {
		if (phase > 0) {
			if (nextCorruptionTime <= Time.time) {
				nextCorruptionTime = Mathf.Infinity;
				Corrupt ();
				print ("Timer's up, corrupting");

				//nextCorruptionTime = Time.time + infectTime;
			}
		}
    }

	public void  TimeStopped() {
		storedNextCorruptTime = nextCorruptionTime - Time.time;
		nextCorruptionTime = Mathf.Infinity;
		print ("timer stopped");
	}

	public void TimeResumed() {
		nextCorruptionTime = storedNextCorruptTime + Time.time;
		print ("timer resumed with " + storedNextCorruptTime.ToString () + " seconds to go");
	}

	public void CheckStartingTimer() {
		if (shrub.corruptedPop <= 0 && deer.corruptedPop <= 0 && wolf.corruptedPop <= 0 && rabbit.corruptedPop <= 0 && owl.corruptedPop <= 0) {
			nextCorruptionTime = Time.time + infectTime;
			print ("Starting timer");
		}
	}

    void Corrupt()
    {
		if (!shrub.corrupting && !deer.corrupting && !wolf.corrupting && !rabbit.corrupting && !owl.corrupting)
        {

			List<basePopulation> possibleTargets = new List<basePopulation>();
			if (shrub.pop > minimumInfectionPop) {
				possibleTargets.Add (shrub);
			}
			if (deer.pop > minimumInfectionPop) {
				possibleTargets.Add (deer);
			}
			if (wolf.pop > minimumInfectionPop) {
				possibleTargets.Add (wolf);
			}
			if (rabbit.pop > minimumInfectionPop) {
				possibleTargets.Add (shrub);
			}
			if (owl.pop > minimumInfectionPop) {
				possibleTargets.Add (shrub);
			}
			print (possibleTargets.Count);
			if (possibleTargets.Count > 0) {
				int target = Random.Range (0, possibleTargets.Count);
				possibleTargets [target].corruptedPop = 15;
				possibleTargets [target].corrupting = true;
			} else {
				print ("all populations too low to corrupt, putting off random corruption for 15 seconds");
				nextCorruptionTime = Time.time + 15f;
			}

//            int rando1 = Random.Range(0, shrubRange);
//            int rando2 = Random.Range(0, deerRange);
//            int rando3 = Random.Range(0, wolfRange);
//            if (rando1 == 0)
//            {
//                shrub.corrupting = true;
//                if (shrub.corruptedPop < shrubPopStart && shrub.pop > minimumInfectionPop)
//                {
//                    shrub.corruptedPop = shrubPopStart;
//                }
//            }
//            else if (rando2 == 0)
//            {
//                deer.corrupting = true;
//                if (deer.corruptedPop < deerPopStart && deer.pop > minimumInfectionPop)
//                {
//                    deer.corruptedPop = deerPopStart;
//                }
//            }
//            else if (rando3 == 0)
//            {
//                wolf.corrupting = true;
//                if (wolf.corruptedPop < wolfPopStart && wolf.pop > minimumInfectionPop)
//                {
//                    wolf.corruptedPop = wolfPopStart;
//                }
//            }
        }
    }
}
