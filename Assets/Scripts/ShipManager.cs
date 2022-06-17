using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipManager : MonoBehaviour
{

    public static GameObject[] planets;
    // Objects to add from Editor
    public GameObject ship;

    // Contstants
    public static float gravitationalConstant = 2.0f;

    // Used in other classes
    public static bool shipCollision;
    public static bool fakeShipCollision;
    public static bool shipLanded;

    // TOOD: reset this on restart
    public static bool applyPlanetForces = true;
    public static bool landing = false;

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
        
        if (!LaunchButton.launchButtonClickedFirstTime && ship != null)
        {
            ShipHelper.calculateLaunchAngle(ship);
            ShipHelper.calculateLaunchForce();
        }
        
        if (Input.GetMouseButton(0) && !PanelPlayUI.buttonEntered)
        {
            GameObject earth = planets[0];
            ShipHelper.rotateShip(ship, earth);
        }
        
    }

    void FixedUpdate()
    {
        
        if(ship != null && !ship.GetComponent<Renderer>().isVisible){
            GameManager.isGameOver = true;
            shipCollision = true;
        }
        
        if (ship != null && LandButton.landButtonClicked)
        {
            // If ship is within land zone, then land ship
            if (LandZoneCollider.landZoneCollision)
            {
                LandShip();
            }
            // Blow up ship otherwise
            else
            {
                GameManager.isGameOver = true;
                shipCollision = true;
            }
        }

        // Add force towards Planet0 in order to land on it
        if (ship != null && landing)
        {
            GameObject earth = planets[0];
            Vector3 earthPosition = earth.transform.position;

            Vector3 direction = (earthPosition - ship.transform.position).normalized;
            // calculate magnitude: gravitationalConstant * shipMass * planetMass
            float magnitude = ship.GetComponent<Rigidbody>().mass * earth.GetComponent<Rigidbody>().mass * 10f;

            Vector3 force = direction * (magnitude);

            ship.GetComponent<Rigidbody>().AddForce(force);
        }
        
        if (ship != null && LaunchButton.launchButtonClicked)
        {
            ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ShipHelper.launchShip(ship);
            LaunchButton.launchButtonClicked = false;
        }
        

        if (ship != null && LaunchButton.launchButtonClickedFirstTime && applyPlanetForces)
        {
            ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            
            ShipHelper.applyPlanetForces(ship, planets);
        }
        
        

        if (ship != null && !landing)
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

    void LandShip() {
        Rigidbody shipBody = ship.GetComponent<Rigidbody>();
        applyPlanetForces = false;
        // Stop rocket
        shipBody.velocity = new Vector3(0, 0, 0);
        // Point towards earth
        Vector3 earthPosition = planets[0].transform.position;
        shipBody.transform.LookAt(earthPosition);

        // Flip ship around so "butt" is facing earth
        shipBody.transform.RotateAround(shipBody.transform.position, shipBody.transform.right, 180f);
            
        LandButton.landButtonClicked = false;
        landing = true;

        // Wait for N seconds before setting shipLanded = true
        // This allows player to watch the ship land and not show the LevelCompleted immediately
        StartCoroutine(waitForShipToLand());
    }

    IEnumerator waitForShipToLand()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(1.5F);
        shipLanded = true;
    }

}
