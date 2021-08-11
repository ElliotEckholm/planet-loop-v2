using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChunkedShip : MonoBehaviour
{

  
    public GameObject ship;
    public GameObject chunckedShip;
    

    private void FixedUpdate()
    {
        if (ShipManager.shipCollision)
        {

            if (ship)
            {
                Vector3 currentShipVelocity = ship.GetComponent<Rigidbody>().velocity;
                Destroy(ship);


                GameObject _chunckedShip = Instantiate(chunckedShip, ship.transform.position, ship.transform.rotation) as GameObject;

                foreach (Transform trans in _chunckedShip.transform)
                {
                    var rb = trans.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.velocity = currentShipVelocity;
                    }
                }


                _chunckedShip.GetComponent<ExplodeObject>().Explode();

                ShipManager.shipCollision = false;
            }
           
        }
        


    }
}
