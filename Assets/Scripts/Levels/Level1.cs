using System.Collections.Generic;
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
        CreateAndGetRandomNumberOfPlanets(planets, 2,5);
        // foreach (var planet in randomlyGeneratedPlanets)
        // {
        //     Debug.Log("planet position = " + planet.transform.position);
        //
        // }
        // return randomlyGeneratedPlanets;
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

    public static List<GameObject> CreateAndGetRandomNumberOfPlanets(List<GameObject> planets, int min, int max)
    {
        // Debug.Log("GetRandomNumberOfPlanets");
        int maxPlanetTypes = 2; // There are only 3 different types of planets at the moment
        int numPlanets = Random.Range(min, max + 1); // randomly choose int between [min, max]
        Debug.Log("numPlanets = " + numPlanets);

        List<GameObject> createdPlanets = new List<GameObject>();
        // Add planet0 always?
        GameObject planet0 = planets.Find(o => o.name == "Planet0");
        GameObject basePlanet = Instantiate(
            planet0,
            new Vector3(0.0F, 0.0F, 0.0F),
            new Quaternion(0.0F, 0.0F, 0.0F, 0.0F)
        );
        // Debug.Log("EARTH = " + planet0);
        createdPlanets.Add(basePlanet);
        
        for (int x = 1; x <= numPlanets; x++)
        {
            int planetType = Random.Range(0, maxPlanetTypes + 1); // randomly choose int between [1, maxPlanetTypes]
            // Debug.Log("planetType = " + planetType);
        
            // TODO: randomly choose location AND rotation
            int randomX = Random.Range(-10, 50);
            int randomY = Random.Range(-20, 20);

            int randomRX = Random.Range(0, 360);
            int randomRY = Random.Range(0, 360);
            int randomRZ = Random.Range(0, 360);

            GameObject planet = Instantiate(
                PickPlanet(planets, planetType),
                new Vector3(0.0F + (x * randomX), 0.0F + (x * randomY), 0.0F),
                new Quaternion(randomRX, randomRY, randomRZ, 0.0F)
            );
            // GameObject generatedPlanet = planet;
            // generatedPlanet.transform.position = new Vector3(200, 200, 200);

            createdPlanets.Add(planet);
            // SceneManager.MoveGameObjectToScene(planet, predictionScene);
        }
    
        return planets;
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