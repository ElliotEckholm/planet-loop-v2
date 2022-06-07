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
        CreateRandomNumberOfPlanets(planets, 1, 2);
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

    public static void CreateRandomNumberOfPlanets(List<GameObject> planets, int min, int max)
    {
        int maxPlanetTypes = 2; // There are only 3 different types of planets at the moment
        int numPlanets = Random.Range(min, max + 1); // randomly choose int between [min, max]

        List<GameObject> createdPlanets = new List<GameObject>();
        // Add planet0 always?
        GameObject planet0 = planets.Find(o => o.name == "Planet0");
        GameObject basePlanet = Instantiate(
            planet0,
            new Vector3(0.0F, 0.0F, 0.0F),
            new Quaternion(0.0F, 0.0F, 0.0F, 0.0F)
        );
        createdPlanets.Add(basePlanet);

        for (int x = 1; x <= numPlanets; x++)
        {
            int planetType = Random.Range(0, maxPlanetTypes + 1); // randomly choose int between [1, maxPlanetTypes]
            // Randomly choose a X and Y that is not within N radius of any other created planets
            List<float> radiusToPlanetsList = new List<float>();
            int randomX = 0;
            int randomY = 0;
            
            // if all planets created so far are not far enough away from each other
            while (radiusToPlanetsList.Count != createdPlanets.Count)
            {
                
                radiusToPlanetsList.Clear();
                Debug.Log("============");
                foreach (GameObject createdPlanet in createdPlanets)
                {
                    // GameObject mostRecentlyCreatedPlanet = createdPlanets.LastOrDefault();
                    randomX = Random.Range(-15, 70);
                    randomY = Random.Range(-20, 20);
                    float radius = Vector2.Distance(
                        new Vector2(createdPlanet.transform.position.x, createdPlanet.transform.position.y),
                        new Vector2(randomX, randomY));
                    if (radius > 30)
                    {
                        Debug.Log(" planet = " + createdPlanet.name);
                        radiusToPlanetsList.Add(radius);
                    }
                }
                
                // Debug.Log("createdPlanets first = " + createdPlanets.FirstOrDefault().name);
                // Debug.Log("createdPlanets first planet radius = " + createdPlanets.FirstOrDefault().GetComponent<SphereCollider>().radius);
                // Debug.Log("createdPlanets first planet pos = " + createdPlanets.FirstOrDefault().transform.position);

                // Debug.Log("createdPlanets.Count = " + createdPlanets.Count);
                // Debug.Log("radiusToPlanetsList.Count = " + radiusToPlanetsList.Count);

                foreach (float radius in radiusToPlanetsList)
                {
                    Debug.Log(" radius = " + radius);
                }
            }

            // Randomly choose rotation
            int randomRX = Random.Range(0, 360);
            int randomRY = Random.Range(0, 360);
            int randomRZ = Random.Range(0, 360);
            Quaternion randomRotation = new Quaternion(randomRX, randomRY, randomRZ, 0.0F);
            
            GameObject randomPlanet = PickPlanet(planets, planetType);

            Vector3 randomPosition = new Vector3(randomX,  randomY, 0.0F);
            GameObject planet = Instantiate(
                randomPlanet,
                randomPosition,
                randomRotation
            );
            createdPlanets.Add(planet);
        }
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