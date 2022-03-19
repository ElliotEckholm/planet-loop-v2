﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    // public GameObject planet0;
    // public GameObject planet1;
    // public GameObject planet2;

    public void FixedUpdate()
    {
        if (ShipManager.shipLanded)
        {
            GameManager.LevelComplete = true;
        }
    }

    public static void SetupLevel(List<GameObject> planets)
    {
      
        
        GetRandomNumberOfPlanets(planets, 2,5);
        // Make Moon1 orbit Planet0
        // Vector3 moon1OrbitAixs = new Vector3(0, 0, 0.1f);
        // GameObject.Find("Moon1").transform.RotateAround(GameObject.Find("Planet0").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);
        // GameObject.Find("Moon1(Clone)").transform.RotateAround(GameObject.Find("Planet0(Clone)").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);
        //
        // //// Rotate Planet1
        // GameObject.Find("Planet1").transform.Rotate(0, 0, 1);
        // GameObject.Find("Planet1(Clone)").transform.Rotate(0, 0, 1);
        //
        // // Make Planet2 orbit Planet1
        // Vector3 planet2OrbitAixs = new Vector3(0, 0, 0.1f);
        // GameObject.Find("Planet2").transform.RotateAround(GameObject.Find("Planet1").transform.position, planet2OrbitAixs, 50 * Time.deltaTime);
        // GameObject.Find("Planet2(Clone)").transform.RotateAround(GameObject.Find("Planet1(Clone)").transform.position, planet2OrbitAixs, 50 * Time.deltaTime);
    }

    public static void GetRandomNumberOfPlanets(List<GameObject> planets, int min, int max)
    {
        Debug.Log("GetRandomNumberOfPlanets");
        int maxPlanetTypes = 2; // There are only 3 different types of planets at the moment
        int numPlanets = Random.Range(min, max + 1); // randomly choose int between [min, max]
        Debug.Log("numPlanets = " + numPlanets);

        // List<GameObject> createdPlanets = new List<GameObject>();
        // Add planet0 always?
        GameObject planet0 = planets.Find(o => o.name == "Planet0");
        Instantiate(
            planet0,
            new Vector3(0.0F, 0.0F, 0.0F),
            new Quaternion(0.0F, 0.0F, 0.0F, 0.0F)
        );
        Debug.Log("EARTH = " + planet0);
        // createdPlanets.Add(basePlanet);
        
        for (int x = 1; x <= numPlanets; x++)
        {
            int planetType = Random.Range(1, maxPlanetTypes + 1); // randomly choose int between [1, maxPlanetTypes]
            Debug.Log("planetType = " + planetType);
        
            // TODO: randomly choose location AND rotation
            GameObject planet = Instantiate(
                PickPlanet(planets, planetType),
                new Vector3(0.0F + (x * 20), 0.0F + (x * 15), 0.0F),
                new Quaternion(0.0F + (x * 60), 0.0F + (x * 20), 0.0F + (x * 90), 0.0F)
            );
            Debug.Log("planet = " + planet);
        
        
            // createdPlanets.Add(planet);
            // SceneManager.MoveGameObjectToScene(planet, predictionScene);
        }
    
        // return planets;
    }

    public static GameObject PickPlanet(List<GameObject> planets, int planetType)
    {
        GameObject matchedPlanet = planets.Find(o => o.name == ("Planet" + planetType));
        return matchedPlanet;
        // if (matchedPlanet)
        // {
        //     return matchedPlanet;
        // }
        // else
        // {
        //     return planets[2];
        // }
    }
}