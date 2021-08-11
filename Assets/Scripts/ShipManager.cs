using System.Collections.Generic;
using UnityEngine;
using System;


public class ShipManager : MonoBehaviour
{

    GameObject[] planets;
    // Objects to add from Editor
    public GameObject ship;

    // Private variable only used in this class
    private LineRenderer launchAimLine;

    // Contstants
    public static float gravitationalConstant = 2.0f;

    // Used in other classes
    public static bool shipCollision = false;
    public static bool fakeShipCollision = false;

    // Start is called before the first frame update
    void Start()
    {

        planets = GameManager.currentLevelObjects;

        launchAimLine = gameObject.AddComponent<LineRenderer>();
        launchAimLine.SetWidth(0.2f, 0.2f);
        launchAimLine.enabled = false;

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

        // If mouse is clicked and cursor is not on any buttons
        if (Input.GetMouseButton(0) && !PanelPlayUI.buttonEntered)
        {
            ShipHelper.drawLaunchLine(ship, launchAimLine);
            ShipHelper.calculateLaunchForce(ship);
           
        }
    }

    void FixedUpdate()
    {

        if (LaunchButton.launchButtonClicked)
        {
            ShipHelper.launchShip(ship, launchAimLine);
            ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        if (ship != null && LaunchButton.launchButtonClickedFirstTime)
        {
            ShipHelper.applyPlanetForces(ship, planets);
            ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
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
