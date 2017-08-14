﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour {

	public int target;
	public int effect;
	public float strength;
	public bool instaCast;

	private GameObject spellCore;

	private SimpleEcologyMasterScript eco;
	// Use this for initialization
	void Start () {
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		spellCore = GameObject.Find ("Core");
		if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().noChargeMode) {
			instaCast = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Charge() {
		GameObject.Find ("spellCast").transform.position = new Vector3 (100f, 100f, 1f);
		Camera.main.transform.position = new Vector3(spellCore.transform.position.x, spellCore.transform.position.y, Camera.main.transform.position.z);
		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StartShooting ();
		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().nodeType = effect;
		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().nodeType = effect;
		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().nodeType = effect;
		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().nodeType = target;
		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().UpdateColors ();
		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().UpdateColors ();
		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().UpdateColors ();
		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().UpdateColors ();
		//instantiate a prefab of a spellcircle parented to the player
		//change a setting in playerscript to charging mode
	}

	void OnMouseDown() {
		//Cast ();
		if (instaCast) {
			Cast ();
		} else {
			Charge ();
		}
		Destroy (gameObject);
	}

	public void Cast() {
		
		if (effect == 0) {
			//changes size, keeping biomass the same but changing raw population
			if (target == 0) {
				eco.shrubSize += strength * .2f;
			} else if (target == 1) {
				eco.deerSize += strength * .2f;
			} else if (target == 2) {
				eco.wolfSize += strength * .2f;
			} else if (target == 3) {
				eco.shrubSize += strength * .2f;
			} else if (target == 4) {
				eco.deerSize += strength * .2f;
			} else if (target == 5) {
				eco.wolfSize += strength * .2f;
			}
		}

		if (effect == 1) {
			//hasten/slow, change increase; and decrease of food
			if (target == 0) {
				eco.shrubUp += strength;
			} else if (target == 1) {
				eco.deerUp1 += strength;
				eco.shrubDown += strength;
			} else if (target == 2) {
				eco.wolfUp += strength;
				eco.deerDown2 += strength;
			} else if (target == 3) {
				eco.shrubUp += strength;
			} else if (target == 4) {
				eco.deerUp1 += strength;
				eco.shrubDown += strength;
			} else if (target == 5) {
				eco.wolfUp += strength;
				eco.deerDown2 += strength;
			}
		}

		if (effect == 2) {
			//toughen/weaken, change decrease rates
			if (target == 0) {
				eco.shrubDown -= strength;
			} else if (target == 1) {
				eco.deerDown1 -= strength;
				eco.deerDown2 -= strength;
			} else if (target == 2) {
				eco.wolfDown -= strength;
			} else if (target == 3) {
				eco.shrubDown -= strength;
			} else if (target == 4) {
				eco.deerDown1 -= strength;
				eco.deerDown2 -= strength;
			} else if (target == 5) {
				eco.wolfDown -= strength;
			}
				

		} if (effect == 3) {
			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
					Destroy (gunch);
				}
			}
		} if (effect == 4) {
			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
					Destroy (gunch);
				}
			}
		} if (effect == 5) {
			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
					Destroy (gunch);
				}
			}
		}

		GameObject.Find ("SpellMenu").GetComponent<SpellMenuScript> ().uncastTomes.Remove (gameObject);
	}
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class SpellScript : MonoBehaviour {
//
//	public int target;
//	public int effect;
//	public int strength;
//	public bool instaCast;
//
//	private GameObject spellCore;
//
//	private SimpleEcologyMasterScript eco;
//	// Use this for initialization
//	void Start () {
//		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
//		spellCore = GameObject.Find ("Core");
//		if (GameObject.Find ("Player").GetComponent<PlayerControllerScript> ().noChargeMode) {
//			instaCast = true;
//		}
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//	}
//
//	void Charge() {
//		GameObject.Find ("spellCast").transform.position = new Vector3 (100f, 100f, 1f);
//		Camera.main.transform.position = new Vector3(spellCore.transform.position.x, spellCore.transform.position.y, Camera.main.transform.position.z);
//		GameObject.Find ("Spawner").GetComponent<SpawnerScript> ().StartShooting ();
//		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().nodeType = effect;
//		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().nodeType = effect;
//		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().nodeType = effect;
//		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().nodeType = target;
//		GameObject.Find ("JNode1").GetComponent<bNodeScript> ().UpdateColors ();
//		GameObject.Find ("JNode2").GetComponent<bNodeScript> ().UpdateColors ();
//		GameObject.Find ("JNode3").GetComponent<bNodeScript> ().UpdateColors ();
//		GameObject.Find ("JNode4").GetComponent<bNodeScript> ().UpdateColors ();
//		//instantiate a prefab of a spellcircle parented to the player
//		//change a setting in playerscript to charging mode
//	}
//
//	void OnMouseDown() {
//		//Cast ();
//		if (instaCast) {
//			Cast ();
//		} else {
//			Charge ();
//		}
//		Destroy (gameObject);
//	}
//
//	public void Cast() {
//		if (effect == 0) {
//			//population change
//			if (target == 0) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 1) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 2) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			} else if (target == 3) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 4) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 5) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			}
//		}
//
//		if (effect == 1) {
//			//population change
//			if (target == 0) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 1) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 2) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			} else if (target == 3) {
//				eco.shrubPop += strength * 10;
//				eco.corruptedShrubPop += strength * 10 * (eco.corruptedShrubPop / eco.shrubPop);
//			} else if (target == 4) {
//				eco.deerPop += strength * 10;
//				eco.corruptedDeerPop += strength * 10 * (eco.corruptedDeerPop / eco.deerPop);
//			} else if (target == 5) {
//				eco.wolfPop += strength * 10;
//				eco.corruptedWolfPop += strength * 10 * (eco.corruptedWolfPop / eco.wolfPop);
//			}
//		}
//
//		if (effect == 2) {
//			//population change
//			if (target == 0) {
//				eco.shrubPop -= strength * 10;
//				eco.corruptedShrubPop -= strength * 10;
//			} else if (target == 1) {
//				eco.deerPop -= strength * 10;
//				eco.corruptedDeerPop -= strength * 10;
//			} else if (target == 2) {
//				eco.wolfPop -= strength * 10;
//				eco.corruptedWolfPop -= strength * 10;
//			} else if (target == 3) {
//				eco.shrubPop -= strength * 10;
//				eco.corruptedShrubPop -= strength * 10;
//			} else if (target == 4) {
//				eco.deerPop -= strength * 10;
//				eco.corruptedDeerPop -= strength * 10;
//			} else if (target == 5) {
//				eco.wolfPop -= strength * 10;
//				eco.corruptedWolfPop -= strength * 10;
//			}
//
//		} if (effect == 3) {
//			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
//				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
//					Destroy (gunch);
//				}
//			}
//		} if (effect == 4) {
//			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
//				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
//					Destroy (gunch);
//				}
//			}
//		} if (effect == 5) {
//			foreach (GameObject gunch in GameObject.Find ("CreatureManager").GetComponent <CreatureManagerScript> ().corruptionNodeList) {
//				if ((gunch.transform.position - GameObject.Find ("Player").transform.position).magnitude < 6) {
//					Destroy (gunch);
//				}
//			}
//		}
//
//		GameObject.Find ("SpellMenu").GetComponent<SpellMenuScript> ().uncastTomes.Remove (gameObject);
//	}
//}
