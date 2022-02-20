using UnityEngine;

public class SpawnChunkedShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject chunkedShip;
    private GameObject _chunckedShip;

    private void FixedUpdate()
    {
        if (ShipManager.shipCollision && ship && LaunchButton.launchButtonClickedFirstTime && 
            !(WinZoneCollider.winZoneCollision && ShipManager.shipLanded))
        {
            Vector3 currentShipVelocity = ship.GetComponent<Rigidbody>().velocity;
            Destroy(ship);
            
            _chunckedShip = Instantiate(chunkedShip, ship.transform.position, ship.transform.rotation);
            
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

        if (GameManager.restartClicked || GameManager.LevelComplete)
        {
            Destroy(GameObject.Find("shipChunked1(Clone)"));
            GameManager.restartClicked = false;
        }
        
    }
}