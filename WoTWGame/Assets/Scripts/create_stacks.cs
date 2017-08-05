using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_stacks : MonoBehaviour
{
    public GameObject rune_type;
    public GameObject core_type;
    public shoot_energy generator;
    
    public bool[] mag_runes;//default false
    public Transform note;
    public float E_timer;
    [Range(1, 10)]
    public int stacks;
    [Range(0f, 5f)]
    public float radius;
    [Range(0f, 5f)]
    public float ring_growth;
    [Range(0f, 5f)]
    public float distance;
    public Transform[] points;
    //tidy purpose
    public Transform core_list;

    public float ring_radius;
    // Use this for initialization

    void Start()
    {
        for (int x = 0; x < stacks; x++)
        {
            GameObject runic_orb = Instantiate(rune_type, transform.position, transform.rotation, transform);
            runic_orb.transform.localScale = new Vector3(radius + x * ring_growth, radius + x * ring_growth, radius + x * ring_growth);
            runic_orb.name = "rune_level" + x;
            //runic_orb.transform.position += new Vector3(0, 0, x * distance);
            if (x % 2 == 0)
            {
                runic_orb.GetComponent<rotation>().set_direction(-1);
            }
            else
            {
                runic_orb.GetComponent<rotation>().set_direction(1);

            }
            points = runic_orb.GetComponent<posit_core>().give_nodes();
            for (int y = 0; y < 4; y++)
            {
                GameObject core_orb = Instantiate(core_type, transform.position, transform.rotation, core_list);
                if (points[y] != null)
                {
                    if(mag_runes[x])
                    {
                        core_orb.GetComponent<core_absorb>().set_mag(true);
                    }
                    else
                    {
                        core_orb.GetComponent<core_absorb>().set_mag(false);

                    }
                    core_orb.GetComponent<core_absorb>().setPos(points[y]);
                    core_orb.SetActive(true);

                }
                else
                {
                    Debug.Log("dead node");
                }
                core_orb.SetActive(true);
            }
        }
        ring_radius = stacks * ring_growth;
        note.position = new Vector3(ring_radius, 0, 0);
        generator.setdist(ring_radius);
    }
}
