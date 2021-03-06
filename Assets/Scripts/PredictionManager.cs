using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PredictionManager : MonoBehaviour
{
    public GameObject realShip;
    public GameObject fakeShipVariant;
    private GameObject fakeShip;
    private GameObject[] planets;
    
    // public GameObject planet0;
    // public GameObject planet1;
    // public GameObject planet2;

    Scene predictionScene;
    PhysicsScene predictionPhysicsScene;
    PhysicsScene currentPhysicsScene;


    public string predicitionSceneName;
    private bool predict;


    // Start is called before the first frame update
    void Start()
    {
        // planets = GameManager.currentLevelObjects;

        predicitionSceneName = "prediction";
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene(predicitionSceneName, parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();
        Physics.autoSimulation = false;

        // TODO only instaintate objects that get selected inside the LEVEL SETUP
        // foreach (GameObject planet in planets)
        // {
        //     GameObject fakePlanet = Instantiate(planet, planet.transform.position, planet.transform.rotation);
        //     SceneManager.MoveGameObjectToScene(fakePlanet, predictionScene);
        // }
        
        // if (GameManager.Level == 1)
        // {
        // Debug.Log(" prediction START planets = " + GameManager.currentLevelObjects[0]);

            planets = GameManager.currentLevelObjects;
        // }
    }

    void FixedUpdate()
    {
        predictionPhysicsScene.Simulate(Time.fixedDeltaTime * 5);
        currentPhysicsScene.Simulate(Time.fixedDeltaTime);

        
        DestroyFakeShipAtExpiration();

        if (fakeShip != null && planets[0] != null)
        {
            // Debug.Log(" prediction fakeShip = " + fakeShip);
            // Debug.Log(" prediction planets = " + planets[0]);

            ShipHelper.applyPlanetForces(fakeShip, planets);

            // Angle ship to forward direction of ship's velocity
            Vector3 shipVelocity = fakeShip.GetComponent<Rigidbody>().velocity;
            if (shipVelocity != new Vector3(0, 0, 0))
            {
                fakeShip.GetComponent<Rigidbody>().transform.forward = shipVelocity;
            }

            if (LaunchButton.launchButtonClicked)
            {
                fakeShip.GetComponent<TrailRenderer>().enabled = false;
            }
        }

        if (ShipManager.fakeShipCollision)
        {
            Destroy(fakeShip, 0f);
            predict = false;
            ShipManager.fakeShipCollision = false;
        }

        if (predict)
        {
            CreateAndLaunchFakeShip();
            predict = false;
        }

        // // TODO: This is hardcoded for only Level 0. FIX ME
        // if (GameManager.Level == 0)
        // {
        //     Level0.SetupLevel();
        // } 
        // else if (GameManager.Level == 1)
        // {
        //     List<GameObject> planets = new List<GameObject>();
        //     planets.Add(planet0);
        //     planets.Add(planet1);
        //     planets.Add(planet2);
        //
        //     Level1.SetupLevel(planets);
        // }
        //
        
    }

    // Update is called once per frame
    void Update()
    {
        ShipHelper.isGamePaused();

        //If mouse is clicked and cursor is not on any buttons
        if (Input.GetMouseButtonUp(0) && !PanelPlayUI.buttonEntered)
        {
            predict = true;
        }
    }
    

    void CreateAndLaunchFakeShip()
    {
        // Add fake ship to prediction
        if (realShip && !LaunchButton.launchButtonClickedFirstTime)
        {
            if (fakeShip) Destroy(fakeShip, 0f); // Destroy previous fakeShip if it exists
            fakeShip = Instantiate(fakeShipVariant, realShip.transform.position, realShip.transform.rotation);
            SceneManager.MoveGameObjectToScene(fakeShip, predictionScene);
            Renderer fakeRenderer = fakeShip.GetComponent<Renderer>();
            fakeRenderer.enabled = false; // Boolean to render the fake ship or not during play mode
            fakeShip.GetComponent<Rigidbody>().AddForce(ShipHelper.launchForce, ForceMode.VelocityChange);
        }
       
    }

    void DestroyFakeShipAtExpiration()
    {
        float maxDistance = 75f;
        
        if (fakeShip && realShip)
        {
            // Destroy fake ship after it is x distance from real ship
            if (Vector3.Distance(realShip.transform.position, fakeShip.transform.position) >= maxDistance)
            {
                Destroy(fakeShip, 0f);
            }
        }

    }
}