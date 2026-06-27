using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class RepitPosition : MonoBehaviour
{
    private Vector3 positioN;
    private float repitinDown;
    public float coordinat;

    void Start()
    {
        positioN = transform.position;
        repitinDown = GetComponent<BoxCollider2D>().size.y / coordinat;    
    }

    
    void Update()
    {
        if (transform.position.y < positioN.y - repitinDown)
        {
            transform.position = positioN;
        }
    }
}
