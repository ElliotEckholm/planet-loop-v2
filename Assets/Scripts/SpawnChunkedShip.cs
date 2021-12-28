using UnityEngine;

public class SpawnChunkedShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject chunkedShip;


    private void FixedUpdate()
    {
        //Debug.Log(ShipManager.shipCollision);
        if (ShipManager.shipCollision && ship)
        {
            Vector3 currentShipVelocity = ship.GetComponent<Rigidbody>().velocity;
            Destroy(ship);

            GameObject _chunckedShip = Instantiate(chunkedShip, ship.transform.position, ship.transform.rotation);

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