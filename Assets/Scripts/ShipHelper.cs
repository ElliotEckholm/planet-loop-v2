using System.Collections.Generic;
using UnityEngine;
using System;


public class ShipHelper : MonoBehaviour
{


    // Variable shared with other classes
    public static Vector3 launchForce;
    public static float launchForceDampening = 0.25F;
    // public static float launchMagnitude;
    public static Vector3 launchDirection;
    public static Vector3 rotationDirection;

    

    public static void isGamePaused()
    {
        if (GameManager.IsGamePaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public static void rotateShip(GameObject ship)
    {
        // if ()
        // Vector3 mousePosition = getCurrentMousePosition().GetValueOrDefault();

        if (ship != null && !PanelPlayUI.buttonEntered)
        {
            GameObject planet = GameObject.Find("Earth");
            //Debug.Log(planet);
            // Vector3 mousePosition = getCurrentMousePosition().GetValueOrDefault();

            Vector3 ballToCursor = rotationDirection - planet.transform.position;
            ship.transform.rotation = Quaternion.FromToRotation(Vector3.forward, ballToCursor);

            float radius = planet.GetComponent<SphereCollider>().radius * planet.transform.localScale.x;
            ship.transform.position = planet.transform.position + ballToCursor.normalized * radius;
           
        }
    }


    public static Vector3 calculateLaunchForce(GameObject ship)
    {
        // Vector3 initialPosition = ship.GetComponent<Rigidbody>().position;
        // Vector3 releasePosition = getCurrentMousePosition().GetValueOrDefault();
        
        // Debug.Log("launchDirection = " + launchDirection);
        // Get direction of launch aim
        // Vector3 launchDirection = releasePosition - initialPosition;
        // Calculate length of launch aim line
        // float launchMagnitudeRaw = (float)Math.Pow(Math.Pow(launchDirection.x - initialPosition.x, 2) +
        //         Math.Pow(launchDirection.y - initialPosition.y, 2), 0.05);
        // Calculate magnitude of launch aiml ine
        // launchMagnitude = (float)Mathf.Round(launchMagnitudeRaw * 10.0f) * 0.1f * 100.0f;
        
        // Debug.Log("launchDirection = " + launchDirection);
        // Debug.Log("MagnitudeSlider.magnitudeSliderForce = " + MagnitudeSlider.magnitudeSliderForce);
        // Debug.Log("launchForceDampening = " + launchForceDampening);
        launchForce = launchDirection; // * (MagnitudeSlider.magnitudeSliderForce * 0.25f); // * launchForceDampening;

        // Debug.Log(launchForce);

        return launchForce;
    }


    public static void drawLaunchLine(GameObject ship, LineRenderer launchAimLine)
    {
        // Draw line from ship to cursor
        // Start point
        Vector3 startPoint = ship.GetComponent<Rigidbody>().position;
        launchAimLine.SetPosition(0, startPoint);
        launchAimLine.SetVertexCount(1);
        
        // End point
        Vector3 mousePosition = getCurrentMousePosition().GetValueOrDefault();
        // mousePosition = new Vector3(mousePosition.x * 10000, mousePosition.y * 10000, mousePosition.z * 10000);

        float radius = 30.0f;

        Vector3 endpoint =
            Vector3.MoveTowards(
            startPoint, 
            mousePosition,
            1000000000.0f);
        launchAimLine.SetVertexCount(2);
        launchAimLine.SetPosition(1, endpoint);
        
        launchAimLine.enabled = true;
        // launchAimLine.forceRenderingOff = true;
        launchDirection = endpoint;
        rotationDirection = mousePosition;
        Debug.Log("current ship position = " + startPoint );
        Debug.Log("launchDirection = " + launchDirection);
        Debug.Log("mousePosition = " + mousePosition);
        Debug.Log("launchForce = " + launchForce);
        float angle = Vector3.Angle(startPoint, launchDirection);

        Debug.Log("launch angle = " + angle);

    }


    public static void launchShip(GameObject ship, LineRenderer launchAimLine)
    {
        launchAimLine.enabled = false;
        
        Debug.Log("LAUNCHING SHIP = " + launchForce);

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
