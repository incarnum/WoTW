using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class core_absorb : MonoBehaviour
{
    public Transform point;
    public float power;
    public GameObject power_orb;
    public GameObject pulse;
    [Range(0, 10)]
    public int E_type;
    public bool magnify;
    public int mag_pow;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(power > 0)
        {
            power_orb.SetActive(true);
        }
        if (point != null)
        {
            transform.position = point.position;
        }
        if (power <= 0)
        {
            power_orb.SetActive(false);
        }
        else if (power > 10)
        {

            power_orb.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            power_orb.transform.localScale = new Vector3(power / 10, power / 10, power / 10);
        }
    }

    public void setPos(Transform pos)
    {
        point = pos;
    }
    public void set_type(int type)
    {
        E_type = type;
    }
    public void set_mag(bool type)
    {
        magnify = type;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("energy") && coll.GetComponent<push>().get_type() == E_type && magnify == false)
        {
            power += coll.GetComponent<push>().get_Power();
            GameObject pulse_orb = Instantiate(pulse, transform.position, transform.rotation) as GameObject;
            Destroy(pulse_orb, 0.5f);
        }
        else if (coll.gameObject.CompareTag("energy") && coll.GetComponent<push>().get_type() == E_type && magnify == true)
        {
            coll.GetComponent<push>().add_Power(mag_pow);
            GameObject pulse_orb = Instantiate(pulse, transform.position + new Vector3(0,0,-4), transform.rotation) as GameObject;
            Destroy(pulse_orb, 1f);
        }
    }
}
