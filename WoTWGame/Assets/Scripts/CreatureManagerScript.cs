using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureManagerScript : MonoBehaviour {
	//private Transform playerTrans;
	public bool initializeAtStart;

	private SimpleEcologyMasterScript eco;
	public List<GameObject> shrubCreatureList;
	public List<GameObject> berryList;
	public GameObject shrubPrefab;
	public GameObject berryPrefab;
	public float shrubNum;

	public List<GameObject> deerCreatureList;
	public List<GameObject> antlerList;
	public GameObject deerPrefab;
	public GameObject antlerPrefab;
	public float deerNum;

	public List<GameObject> wolfCreatureList;
	public List<GameObject> fangList;
	public GameObject wolfPrefab;
	public GameObject fangPrefab;
	public float wolfNum;

    public List<GameObject> rabbitCreatureList;
    public List<GameObject> rabbitItemList;
    public GameObject rabbitPrefab;
    public GameObject rabbitItemPrefab;
    public float rabbitNum;

    public List<GameObject> owlCreatureList;
    public List<GameObject> featherList;
    public GameObject owlPrefab;
    public GameObject featherPrefab;
    public float owlNum;

    public List<GameObject> corruptedShrubCreatureList;
	public List<GameObject> corruptedBerryList;
	public GameObject corruptedShrubPrefab;
	public float corruptedShrubNum;

	public List<GameObject> corruptedDeerCreatureList;
	public GameObject corruptedDeerPrefab;
	public float corruptedDeerNum;

	public List<GameObject> corruptedWolfCreatureList;
	public GameObject corruptedWolfPrefab;
	public float corruptedWolfNum;

    public List<GameObject> corruptedRabbitCreatureList;
    public GameObject corruptedRabbitPrefab;
    public float corruptedRabbitNum;

    public List<GameObject> corruptedOwlCreatureList;
    public GameObject corruptedOwlPrefab;
    public float corruptedOwlNum;

    public List<GameObject> corruptionNodeList;

	private Transform upperLeftBound;
	private Transform lowerRightBound;

    private ShrubPopulation shrub;
    private DeerPopulation deer;
    private WolfPopulation wolf;
    private RabbitPopulation rabbit;
    private OwlPopulation owl;

    public float ecoToWorldDivision;
    private float lastAdjustment;
    public float adjustmentDelay;

	public List<Transform> region1Positions;
	public List<Transform> region2Positions;
	public List<Transform> region3Positions;
	public List<Transform> region4Positions;
	public List<Transform> region5Positions;

    // Use this for initialization
    void Start () {
		eco = GameObject.Find("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
		upperLeftBound = GetComponentsInChildren<Transform> ()[1];
		lowerRightBound = GetComponentsInChildren<Transform> ()[2];
        shrub = GetComponent<ShrubPopulation>();
        deer = GetComponent<DeerPopulation>();
        wolf = GetComponent<WolfPopulation>();
        rabbit = GetComponent<RabbitPopulation>();
        owl = GetComponent<OwlPopulation>();
        if (initializeAtStart == true) {
			Initialize ();
		}
	}

	void Initialize () {
		AdjustCreatures ();
		AdjustPickips ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > lastAdjustment + adjustmentDelay)
        {
            lastAdjustment = Time.time;

            if (shrub.corruptedPop > 0)
            {
                shrubNum = (shrub.pop - shrub.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                shrubNum = shrub.pop / ecoToWorldDivision;
            }

            if (deer.corruptedPop > 0)
            {
                deerNum = (deer.pop - deer.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                deerNum = deer.pop / ecoToWorldDivision;
            }

            if (wolf.corruptedPop > 0)
            {
                wolfNum = (wolf.pop - wolf.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                wolfNum = wolf.pop / ecoToWorldDivision;
            }

            if (rabbit.corruptedPop > 0)
            {
                rabbitNum = (rabbit.pop - rabbit.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                rabbitNum = rabbit.pop / ecoToWorldDivision;
            }

            if (owl.corruptedPop > 0)
            {
                owlNum = (owl.pop - owl.corruptedPop) / ecoToWorldDivision;
            }
            else
            {
                owlNum = owl.pop / ecoToWorldDivision;
            }

            corruptedShrubNum = shrub.corruptedPop / ecoToWorldDivision;
            corruptedDeerNum = deer.corruptedPop / ecoToWorldDivision;
            corruptedWolfNum = wolf.corruptedPop / ecoToWorldDivision;
            corruptedRabbitNum = rabbit.corruptedPop / ecoToWorldDivision;
            corruptedOwlNum = owl.corruptedPop / ecoToWorldDivision;
            AdjustCreatures();
            AdjustPickips();
        }
    }

    public void AdjustCreatures() {
		//shrubs
		while (shrubCreatureList.Count > shrubNum + 1f) {
			Destroy (shrubCreatureList [0]);
			shrubCreatureList.RemoveAt (0);
//			bool garfBool = false;
//			int garfield = 0;
//			while (garfBool == false) {
//				if ((shrubCreatureList [garfield].transform.position - playerTrans.position).magnitude > 10) {
//					Destroy (shrubCreatureList [garfield]);
//					shrubCreatureList.RemoveAt (garfield);
//					garfBool = true;
//				} else {
//					garfield += 1;
//					if (garfield - 1 > shrubCreatureList.Count) {
//						Destroy (shrubCreatureList [0]);
//						shrubCreatureList.RemoveAt (0);
//					}
//				}
//			}
		}
		while (shrubCreatureList.Count < shrubNum) {
			CreateShrub ();
		}
		//deer
		while (deerCreatureList.Count > deerNum + 1f) {
			Destroy (deerCreatureList [0]);
			deerCreatureList.RemoveAt (0);
		}
		while (deerCreatureList.Count < deerNum) {
			CreateDeer ();
		}
		//wolves
		while (wolfCreatureList.Count > wolfNum + 1f) {
			Destroy (wolfCreatureList [0]);
			wolfCreatureList.RemoveAt (0);
		}
		while (wolfCreatureList.Count < wolfNum) {
			CreateWolf ();
		}
        //rabbits
        while (rabbitCreatureList.Count > rabbitNum + 1f)
        {
            Destroy(rabbitCreatureList[0]);
            rabbitCreatureList.RemoveAt(0);
        }
        while (rabbitCreatureList.Count < rabbitNum)
        {
            CreateRabbit();
        }
        //owls
        while (owlCreatureList.Count > owlNum + 1f)
        {
            Destroy(owlCreatureList[0]);
            owlCreatureList.RemoveAt(0);
        }
        while (owlCreatureList.Count < owlNum)
        {
            CreateOwl();
        }
        //corruptedshrubs
        while (corruptedShrubCreatureList.Count > corruptedShrubNum / 2f + 1f && corruptedShrubNum > 0) {
			Destroy (corruptedShrubCreatureList [0]);
			corruptedShrubCreatureList.RemoveAt (0);
		}
		while (corruptedShrubCreatureList.Count < corruptedShrubNum / 2f) {
			CreateCorruptedShrub ();
		}
		//corrupteddeer
		while (corruptedDeerCreatureList.Count > corruptedDeerNum / 2f + 1f && corruptedDeerNum > 0) {
			Destroy (corruptedDeerCreatureList [0]);
			corruptedDeerCreatureList.RemoveAt (0);
		}
		while (corruptedDeerCreatureList.Count < corruptedDeerNum / 2f) {
			CreateCorruptedDeer ();
		}
		//corruptedwolves
		while (corruptedWolfCreatureList.Count > corruptedWolfNum / 2f + 1f && corruptedWolfNum > 0) {
			Destroy (corruptedWolfCreatureList [0]);
			corruptedWolfCreatureList.RemoveAt (0);
		}
		while (corruptedWolfCreatureList.Count < corruptedWolfNum / 2f) {
			CreateCorruptedWolf ();
		}
        //corruptedrabbits
        while (corruptedRabbitCreatureList.Count > corruptedRabbitNum / 2f + 1f && corruptedRabbitNum > 0)
        {
            Destroy(corruptedRabbitCreatureList[0]);
            corruptedRabbitCreatureList.RemoveAt(0);
        }
        while (corruptedRabbitCreatureList.Count < corruptedRabbitNum / 2f)
        {
            CreateCorruptedRabbit();
        }
        //corruptedowls
        while (corruptedOwlCreatureList.Count > corruptedOwlNum / 2f + 1f && corruptedOwlNum > 0)
        {
            Destroy(corruptedOwlCreatureList[0]);
            corruptedOwlCreatureList.RemoveAt(0);
        }
        while (corruptedOwlCreatureList.Count < corruptedOwlNum / 2f)
        {
            CreateCorruptedOwl();
        }
    }

	public void AdjustPickips() {

//		if (berryList.Count > shrubNum) {
//			while (berryList.Count > shrubNum) {
//				Destroy (berryList [0]);
//				berryList.RemoveAt (0);
//			}
//		}
//		if (berryList.Count < shrubNum) {
//			while (berryList.Count < shrubNum) {
//				CreateBerry ();
//			}
//		}

		while (antlerList.Count > deerNum + 1f) {
			Destroy (antlerList [0]);
			antlerList.RemoveAt (0);
		}

		while (antlerList.Count < deerNum) {
			CreateAntler ();
		}

		while (fangList.Count > wolfNum + 1f) {
			Destroy (fangList [0]);
			fangList.RemoveAt (0);
		}
		if (fangList.Count < wolfNum) {
			while (fangList.Count < wolfNum) {
				CreateFang ();
			}
		}
        while (rabbitItemList.Count > rabbitNum + 1f)
        {
            Destroy(rabbitItemList[0]);
            rabbitItemList.RemoveAt(0);
        }
        if (rabbitItemList.Count < rabbitNum)
        {
            while (rabbitItemList.Count < rabbitNum)
            {
                CreateRabbitItem();
            }
        }
        while (featherList.Count > owlNum + 1f)
        {
            Destroy(featherList[0]);
            featherList.RemoveAt(0);
        }
        if (featherList.Count < owlNum)
        {
            while (featherList.Count < owlNum)
            {
                CreateFeather();
            }
        }
    }

	public void CreateShrub() {
		GameObject newShrub = Instantiate (shrubPrefab) as GameObject;
		shrubCreatureList.Add (newShrub);
//		newShrub.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
//		newShrub.transform.position -= (newShrub.transform.position - GameObject.Find ("ShrubNucleus").transform.position) / 2;
		Place(newShrub, 1, true);
		newShrub.transform.localScale = new Vector3 (shrub.size, shrub.size);
	}

	public void CreateCorruptedShrub() {
		GameObject newCorruptedShrub = Instantiate (corruptedShrubPrefab) as GameObject;
		corruptedShrubCreatureList.Add (newCorruptedShrub);
		Place(newCorruptedShrub, 1, true);
		newCorruptedShrub.transform.localScale = new Vector3 (shrub.size, shrub.size);
	}

	public void CreateDeer() {
		GameObject newDeer = Instantiate (deerPrefab) as GameObject;
		deerCreatureList.Add (newDeer);
		Place(newDeer, 2, false);
		newDeer.transform.localScale = new Vector3 (deer.size, deer.size);
		newDeer.GetComponent<AnimalMovementScript> ().speed2 = deer.speed;
	}

	public void CreateCorruptedDeer() {
		GameObject newCorruptedDeer = Instantiate (corruptedDeerPrefab) as GameObject;
		corruptedDeerCreatureList.Add (newCorruptedDeer);
		Place(newCorruptedDeer, 2, false);
		newCorruptedDeer.transform.localScale = new Vector3 (deer.size, deer.size);
		newCorruptedDeer.GetComponent<AnimalMovementScript> ().speed2 = deer.speed;
	}

	public void CreateAntler() {
		GameObject newAntler = Instantiate (antlerPrefab) as GameObject;
		antlerList.Add (newAntler);
		Place(newAntler, 2, true);
	}

	public void CreateWolf() {
		GameObject newWolf = Instantiate (wolfPrefab) as GameObject;
		wolfCreatureList.Add (newWolf);
		Place(newWolf, 2, false);
		newWolf.transform.localScale = new Vector3 (wolf.size, wolf.size);
		newWolf.GetComponent<AnimalMovementScript> ().speed2 = wolf.speed;
	}

	public void CreateCorruptedWolf() {
		GameObject newWolf = Instantiate (corruptedWolfPrefab) as GameObject;
		corruptedWolfCreatureList.Add (newWolf);
		Place(newWolf, 2, false);
		newWolf.transform.localScale = new Vector3 (wolf.size, wolf.size);
		newWolf.GetComponent<AnimalMovementScript> ().speed2 = wolf.speed;
	}

	public void CreateFang() {
		GameObject newFang = Instantiate (fangPrefab) as GameObject;
		fangList.Add (newFang);
		Place(newFang, 2, true);
	}

    public void CreateRabbit()
    {
        GameObject newRabbit = Instantiate(rabbitPrefab) as GameObject;
        rabbitCreatureList.Add(newRabbit);
        Place(newRabbit, 2, false);
        newRabbit.transform.localScale = new Vector3(rabbit.size, rabbit.size);
        newRabbit.GetComponent<AnimalMovementScript>().speed2 = rabbit.speed;
    }

    public void CreateCorruptedRabbit()
    {
        GameObject newRabbit = Instantiate(corruptedRabbitPrefab) as GameObject;
        corruptedRabbitCreatureList.Add(newRabbit);
        Place(newRabbit, 2, false);
        newRabbit.transform.localScale = new Vector3(rabbit.size, rabbit.size);
        newRabbit.GetComponent<AnimalMovementScript>().speed2 = rabbit.speed;
    }

    public void CreateRabbitItem()
    {
        GameObject newRI = Instantiate(rabbitItemPrefab) as GameObject;
        rabbitItemList.Add(newRI);
        Place(newRI, 2, true);
    }

    public void CreateOwl()
    {
        GameObject newOwl = Instantiate(owlPrefab) as GameObject;
        owlCreatureList.Add(newOwl);
        Place(newOwl, 2, false);
        newOwl.transform.localScale = new Vector3(owl.size, owl.size);
        newOwl.GetComponent<AnimalMovementScript>().speed2 = owl.speed;
    }

    public void CreateCorruptedOwl()
    {
        GameObject newOwl = Instantiate(corruptedOwlPrefab) as GameObject;
        corruptedOwlCreatureList.Add(newOwl);
        Place(newOwl, 2, false);
        newOwl.transform.localScale = new Vector3(owl.size, owl.size);
        newOwl.GetComponent<AnimalMovementScript>().speed2 = owl.speed;
    }

    public void CreateFeather()
    {
        GameObject newFeather = Instantiate(featherPrefab) as GameObject;
        featherList.Add(newFeather);
        Place(newFeather, 2, true);
    }

    public void Place(GameObject obj, float type, bool isStatic) {
		float garf = Random.Range (1, 10);
		float region;
		if (garf <= 5) {
			region = garf;
		} else {
			region = type;
		}


		if (region == 1) {
			int garf2 = Random.Range (0, region1Positions.Count);
			if (region1Positions [garf2].gameObject.activeSelf) {
				obj.transform.position = region1Positions [garf2].position;
				if (isStatic == true) {
					obj.GetComponent<spawnedObject> ().spawnLocation = region1Positions [garf2];
					region1Positions [garf2].gameObject.SetActive (false);
				}
			} else {
				print ("trying to replace");
				Place (obj, type, isStatic);
			}
		}
		if (region == 2) {
			int garf2 = Random.Range (0, region2Positions.Count);
			if (region2Positions [garf2].gameObject.activeSelf) {
				obj.transform.position = region2Positions [garf2].position;
				if (isStatic == true) {
					obj.GetComponent<spawnedObject> ().spawnLocation = region2Positions [garf2];
					region2Positions [garf2].gameObject.SetActive (false);
				}
			} else {
				Place (obj, type, isStatic);
			}
		}
		if (region == 3) {
			int garf2 = Random.Range (0, region3Positions.Count);
			if (region3Positions [garf2].gameObject.activeSelf) {
				obj.transform.position = region3Positions [garf2].position;
				if (isStatic == true) {
					obj.GetComponent<spawnedObject> ().spawnLocation = region3Positions [garf2];
					region3Positions [garf2].gameObject.SetActive (false);
				}
			} else {
				Place (obj, type, isStatic);
			}
		}
		if (region == 4) {
			int garf2 = Random.Range (0, region4Positions.Count);
			if (region4Positions [garf2].gameObject.activeSelf) {
				obj.transform.position = region4Positions [garf2].position;
				if (isStatic == true) {
					obj.GetComponent<spawnedObject> ().spawnLocation = region4Positions [garf2];
					region4Positions [garf2].gameObject.SetActive (false);
				}
			} else {
				Place (obj, type, isStatic);
			}
		}
		if (region == 5) {
			int garf2 = Random.Range (0, region5Positions.Count);
			if (region5Positions [garf2].gameObject.activeSelf) {
				obj.transform.position = region5Positions [garf2].position;
				if (isStatic == true) {
					obj.GetComponent<spawnedObject> ().spawnLocation = region5Positions [garf2];
					region5Positions [garf2].gameObject.SetActive (false);
				}
			} else {
				Place (obj, type, isStatic);
			}
		}
	}
		
}
