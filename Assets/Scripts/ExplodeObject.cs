﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{

    public float minForce;
    public float maxForce;
    public float radius;

    //private void Start()
    //{
    //    Explode();
    //}

    public void Explode()
    {
        //Debug.Log("exploder script");
        foreach (Transform trans in transform)
        {
            //Debug.Log(trans);
            var rb = trans.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
        }
    }


}
