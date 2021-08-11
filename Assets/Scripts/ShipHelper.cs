using System.Collections.Generic;
using UnityEngine;
using System;


public class ShipHelper : MonoBehaviour
{


    // Variable shared with other classes
    public static Vector3 launchForce;
    public static float launchForceDampening = 0.25F;
    public static float launchMagnitude;

    

    public static void isGamePaused()
    {
        if (GameManager.IsGamePaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }


    public static Vector3 calculateLaunchForce(GameObject ship)
    {
        Vector3 initialPosition = ship.GetComponent<Rigidbody>().position;
        Vector3 releasePosition = getCurrentMousePosition().GetValueOrDefault();

        // Get direction of launch aim
        Vector3 launchDirection = releasePosition - initialPosition;
        // Calculate length of launch aim line
        float launchMagnitudeRaw = (float)Math.Pow(Math.Pow(releasePosition.x - initialPosition.x, 2) +
                Math.Pow(releasePosition.y - initialPosition.y, 2), 0.05);
        // Calculate magnitude of launch aiml ine
        launchMagnitude = (float)Mathf.Round(launchMagnitudeRaw * 10.0f) * 0.1f;

        launchForce = launchDirection * launchMagnitude * launchForceDampening;
        return launchForce;
    }


    public static void drawLaunchLine(GameObject ship, LineRenderer launchAimLine)
    {
        // Draw line from ship to cursor
        Vector3 initialLinePosition = ship.GetComponent<Rigidbody>().position;
        launchAimLine.SetPosition(0, initialLinePosition);
        launchAimLine.SetVertexCount(1);
        Vector3 releasePosition = getCurrentMousePosition().GetValueOrDefault();

        launchAimLine.SetVertexCount(2);
        launchAimLine.SetPosition(1, releasePosition);
        launchAimLine.enabled = true;

    }


    public static void launchShip(GameObject ship, LineRenderer launchAimLine)
    {
        launchAimLine.enabled = false;

        ship.GetComponent<Rigidbody>().AddForce(launchForce, ForceMode.VelocityChange);
        LaunchButton.launchButtonClicked = false;
    }

    public static void applyPlanetForces(GameObject ship, GameObject[] planets)
    {
        //apply spherical gravity to selected objects (set the objects in editor)
        foreach (GameObject planet in planets)
        {
            // calculate direction of force: toward the planet
            Vector3 direction = (planet.transform.position - ship.transform.position).normalized;
            // calculate magnitude: gravitationalConstant * shipMass * planetMass
            float magnitude = ship.GetComponent<Rigidbody>().mass * planet.GetComponent<Rigidbody>().mass * ShipManager.gravitationalConstant;
            // calculate radius between ship and planet
            double radius = Math.Pow(Math.Pow(planet.transform.position.x - ship.transform.position.x, 2) +
                Math.Pow(planet.transform.position.y - ship.transform.position.y, 2), 0.05);

            Vector3 force = direction * (magnitude / (float)radius);

            ship.GetComponent<Rigidbody>().AddForce(force);
        }
    }


    public static Vector3? getCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);

        }

        return null;
    }
}
