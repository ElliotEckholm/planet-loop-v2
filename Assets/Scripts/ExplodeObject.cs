using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{

    public float minForce;
    public float maxForce;
    public float radius;


    public void Explode()
    {
        foreach (Transform trans in transform)
        {
            var rb = trans.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
        }
    }


}
