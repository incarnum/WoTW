using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posit_core : MonoBehaviour
{
    public Transform[] core_nodes;
    public Vector3[] pos_nodes;

    public LineRenderer tr;
    void Start()
    {
        tr = gameObject.GetComponent<LineRenderer>();

    }
    void Update()
    {
        for (int x = 0; x < 4; x++)
        {
            pos_nodes[x] = core_nodes[x].position;
        }
        pos_nodes[4] = core_nodes[0].position;

        if (tr != null)
        {
            tr.SetPositions(pos_nodes);
        }

    }

    // Use this for initialization
    public Transform[] give_nodes()
    {
        return core_nodes;
    }
}
