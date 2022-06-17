using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level1 : MonoBehaviour
{
    public static int numWinZonesNeededToWin;


    public void FixedUpdate()
    {
        // TODO: ALSO check that all winZones randomly created were completed
        if (ShipManager.shipLanded && WinZoneCollider.numWinZonesHit >= numWinZonesNeededToWin)
        {
            GameManager.LevelComplete = true;
            WinZoneCollider.numWinZonesHit = 0;
        }
    }

    public static void SetupLevel(List<GameObject> planets, GameObject winZone)
    {
        CreateRandomNumberOfPlanets(planets, winZone, 2, 2);
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

    public static void CreateRandomNumberOfPlanets(List<GameObject> planets, GameObject winZone, int min, int max)
    {
        int maxPlanetTypes = 2; // There are only 3 different types of planets at the moment
        int numPlanets = Random.Range(min, max + 1); // randomly choose int between [min, max]
        // TODO: For now add win zone to every planet
        // TODO Randomly choose radius of green zones
        numWinZonesNeededToWin = numPlanets;

        List<GameObject> createdPlanets = new List<GameObject>();
        // Add Planet0 (i.e. Earth) always since that is the launching point 
        GameObject planet0 = planets.Find(o => o.name == "Planet0");
        GameObject basePlanet = Instantiate(
            planet0,
            new Vector3(0.0F, 0.0F, 0.0F),
            new Quaternion(0.0F, 0.0F, 0.0F, 0.0F)
        );
        createdPlanets.Add(basePlanet);
        
        // This list of predicates (0s or 1s) is used to determine if a planet trying to find a location is meeting
        // the radius length condition for all other already created planets
        List<int> predicates = new List<int>();
        
        for (int x = 1; x <= numPlanets; x++)
        {
            // Randomly choose X, Y position
            Vector3 randomPosition = RandomlyPickPosition(createdPlanets, predicates, x);
            
            // Randomly choose rotation
            int randomRX = Random.Range(0, 360);
            int randomRY = Random.Range(0, 360);
            int randomRZ = Random.Range(0, 360);
            Quaternion randomRotation = new Quaternion(randomRX, randomRY, randomRZ, 0.0F);
            
            // Randomly choose planet type
            int planetType = Random.Range(0, maxPlanetTypes + 1); // randomly choose int between [1, maxPlanetTypes]
            GameObject randomPlanet = PickPlanet(planets, planetType);
            
            // Create and place randomly generated planet
            GameObject planet = Instantiate(
                randomPlanet,
                randomPosition,
                randomRotation
            );
            // Randomly Create WinZone around planet
            Instantiate(
                winZone,
                randomPosition,
                randomRotation
            );
            createdPlanets.Add(planet);
        }
    }

    public static Vector3 RandomlyPickPosition(List<GameObject> createdPlanets, List<int> predicates, int planetBeingPlaced)
    {
        // Initialize random ints
        int randomX = 0;
        int randomY = 0;

        predicates.Clear();
        // Will check if all planets created so far are far enough away from each other
        do
        {
            predicates.Clear();
            // Randomly choose a X and Y
            randomX = Random.Range(-15, 70);
            randomY = Random.Range(-20, 20);

            // Loop through each planet created so far and construct a predicate array that says whether 
            // the planet trying to be placed is a radius >= N from all other already created (and placed) planets 
            foreach (GameObject createdPlanet in createdPlanets)
            {
                float radius = Vector2.Distance(
                    new Vector2(createdPlanet.transform.position.x, createdPlanet.transform.position.y),
                    new Vector2(randomX, randomY));

                if (radius >= 30)
                {
                    // Debug.Log(" radius = " + radius + " from already created planet " + createdPlanet.name 
                    //           + " ( " + createdPlanet.transform.position + " ) " + " to new planet " + planetBeingPlaced +
                    //           " ( x: " + randomX + " , y: " + randomY + " ) ");
                    predicates.Add(1);
                }
                else
                {
                    // This means there is a radius that does not meet the length requirement
                    predicates.Add(0);
                }
            }
        } // Keep trying different randomly selected locations if there are ANY radii that do NO meet the length condition 
        while (predicates.Any(predicate => predicate == 0)); // || predicates.Count == 0);

        predicates.Clear();

        Vector3 randomPosition = new Vector3(randomX, randomY, 0.0F);
        // Debug.Log("Planet " + planetBeingPlaced + " being placed at " + randomPosition);

        return randomPosition;
    }

    public static GameObject PickPlanet(List<GameObject> planets, int planetType)
    {
        GameObject matchedPlanet = planets.Find(o => o.name == ("Planet" + planetType));
        return matchedPlanet;
    }
}