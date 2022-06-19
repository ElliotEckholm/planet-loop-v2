using UnityEngine;
using System;
using System.Collections.Generic;


public class ShipHelper : MonoBehaviour
{
    // Variable shared with other classes
    public static Vector3 launchForce;
    
    // public static float launchMagnitude;
    public static Vector3 launchDirection;

    public static float oldValue;
    public static float newValue;
    public static float finalAngle;

    public static void ResetAngle()
    {
        oldValue = 0;
        newValue = 0;
        finalAngle = 0;
    }

    // public static void DestroyShip(GameObject ship)
    // {
    //
    //     // Destory fake ship
    //     if (ship.name.Contains("FakeShip"))
    //     {
    //         Destroy(ship);
    //     }
    //     // Destory real ship 
    //     else
    //     {
    //         GameManager.isGameOver = true;
    //         ShipManager.shipCollision = true;
    //     }
    // }

    public static void isGamePaused()
    {
        if (GameManager.IsGamePaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public static void rotateShip(GameObject ship, GameObject planet)
    {
        if (ship != null && planet != null)
        {
            Vector3 planetVector = planet.transform.position;
            planetVector = getCurrentMousePosition().GetValueOrDefault() - planetVector;
            
            oldValue = newValue;
            newValue = Mathf.Atan2(planetVector.y, planetVector.x) * Mathf.Rad2Deg;
            finalAngle = newValue - oldValue;
            
            ship.transform.RotateAround(planet.transform.position * planet.transform.localScale.x, 
                Vector3.forward, finalAngle);
        }
    }

    public static void calculateLaunchAngle(GameObject ship)
    { 
        launchDirection = ship.transform.forward;//new Vector3(angleX, angleY, 0);
    }


    public static void calculateLaunchForce()
    {
        launchForce = launchDirection * MagnitudeSlider.magnitudeSliderValue;
    }


    public static void launchShip(GameObject ship)
    {
        ship.GetComponent<Rigidbody>().AddForce(launchForce, ForceMode.VelocityChange);
    }

    public static void applyPlanetForces(GameObject ship, GameObject[] planets)
    {
        //apply spherical gravity to selected objects (set the objects in editor)
        foreach (GameObject planet in planets)
        {
            // calculate direction of force: toward the planet
            Vector3 direction = (planet.transform.position - ship.transform.position).normalized;
            // calculate magnitude: gravitationalConstant * shipMass * planetMass
            float magnitude = ship.GetComponent<Rigidbody>().mass * planet.GetComponent<Rigidbody>().mass *
                              ShipManager.gravitationalConstant;

            // calculate radius between ship and planet
            double radius = Math.Pow(Math.Pow(planet.transform.position.x - ship.transform.position.x, 2) +
                                     Math.Pow(planet.transform.position.y - ship.transform.position.y, 2), 0.05);

            Vector3 force = direction * (magnitude / (float) radius);
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