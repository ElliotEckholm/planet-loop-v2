using UnityEngine;


public class ShipManager : MonoBehaviour
{

    GameObject[] planets;
    // Objects to add from Editor
    public GameObject ship;

    // Contstants
    public static float gravitationalConstant = 2.0f;

    // Used in other classes
    public static bool shipCollision;
    public static bool fakeShipCollision;


    // Start is called before the first frame update
    void Start()
    {

        planets = GameManager.currentLevelObjects;
        shipCollision = false;
        fakeShipCollision = false;
        
        ship.GetComponent<Rigidbody>().transform.forward = new Vector3(1, 0, 0);
        ship.GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
    }

    // Update is called once per frame
    void Update()
    {
        ShipHelper.isGamePaused();
        
        if (!LaunchButton.launchButtonClickedFirstTime)
        {
            ShipHelper.calculateLaunchAngle(ship);
            ShipHelper.calculateLaunchForce();
        }
    }

    void FixedUpdate()
    {

        if (ship != null && LaunchButton.launchButtonClicked)
        {
            ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ShipHelper.launchShip(ship);
            LaunchButton.launchButtonClicked = false;
        }

        if (ship != null && LaunchButton.launchButtonClickedFirstTime)
        {
            ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ShipHelper.applyPlanetForces(ship, planets);
        }

        if (ship != null)
        {
            // Angle ship to forward direction of ship's velocity
            // e.g. make ship face the correct way
            Vector3 shipVelocity = ship.GetComponent<Rigidbody>().velocity;
            if (shipVelocity != new Vector3(0,0,0))
            {
                ship.GetComponent<Rigidbody>().transform.forward = shipVelocity;
            }
        }
    }

}
