using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {
    public float speed;
    public int direction;
    public float multiplier;
    public int hold;
    public bool note;
	// Use this for initialization
	void Start () {
        if (direction == 1)
        {
            note = false;
        }
        else
        {
            note = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftShift) && note) {
            direction = 1;
        }
        else if(note)
        {
            direction = -1;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(0, 0, speed * direction * multiplier * Input.GetAxis("Horizontal") * Time.deltaTime);

        }
        else if (Input.GetAxis("Horizontal")  < 0)
        {
            transform.Rotate(0, 0, speed * direction * multiplier * Input.GetAxis("Horizontal") * Time.deltaTime);

        }
    }

    public void set_direction( int dir)
    {
        direction = dir;
    }
}
