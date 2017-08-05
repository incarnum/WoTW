using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push : MonoBehaviour {
    public Transform dest;
    public float speed;
    public float timer;
    public int type;
    public int power;

	// Use this for initialization
	void Start () {
        if (dest != null) {
            //transform.LookAt(dest);
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += -transform.right * speed * Time.deltaTime;
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(gameObject);
        }


    }
    public void setPoint(Transform point, float e_time)
    {
        dest = point;
        timer = e_time;
    }    
    public void setTime(float e_dist)
    {
        timer = e_dist / speed;
    }
    public void add_Power(int pow)
    {
        power += pow;
    }
    public int get_Power()
    {
        return power;
    }
    public void setType(int e_type)
    {
        type = e_type;
    }
    public int get_type()
    {
        return type;
    }
}
