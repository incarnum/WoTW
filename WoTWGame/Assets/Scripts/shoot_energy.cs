using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_energy : MonoBehaviour
{
    public GameObject[] energy_type;
    public int[] energy_store;

    public Transform middle_point;
    public Transform release_point;
    [Range(0f, 5f)]
    public float timer_reset;
    [Range(0f, 5f)]
    public int energy;
    public push energy_script;
    public float timer;
    public float energy_dist;
    public float energy_time;
    public int e_type;
    [Range(0, 10)]
    public int E_type;
    [Range(0, 5)]
    public int state;
    public float angle_set;
    [Range(0, 180)]
    public float angle_change;
    public bool on;
    public LineRenderer fore_sight;
    public Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = new Vector3(0, 0, -10);
        fore_sight.SetPosition(0, middle_point.position - offset);

    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                fore_sight.SetPosition(1, release_point.position - offset);

                RE_energy(energy, energy_time, e_type);
                timer = timer_reset;
                switch (state)
                {
                    case 0:
                        setspecificAngle(0f);

                        break;
                    case 1:
                        setRandAngle();

                        break;
                    case 2:
                        setspecificAngle(angle_set);
                        angle_set += angle_change;
                        break;
                    case 3:
                        setspecificAngle(angle_set);
                        angle_set -= angle_change;

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                }

            }
        }




    }


    public int next_energy()
    {
        int test = Random.Range(0, 8);


        return test;
    }
    void RE_energy(int energy, float E_time, int typer)
    {
        GameObject energy_orb = Instantiate(energy_type[energy], release_point.position, release_point.rotation) as GameObject;
        energy_script = energy_orb.GetComponent<push>();
        energy_script.setPoint(middle_point, E_time);
        energy_script.setTime(energy_dist);
        energy_script.setType(typer);
    }
    void setspecificAngle(float angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);

    }
    public void setdist(float dist)
    {
        energy_dist = dist;
    }

    void setRandAngle()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

    }
}
