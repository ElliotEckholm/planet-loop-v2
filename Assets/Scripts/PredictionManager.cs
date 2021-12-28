using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PredictionManager : MonoBehaviour
{
    public GameObject realShip;
    public GameObject fakeShipVariant;
    private GameObject fakeShip;
    GameObject[] planets;

    Scene predictionScene;
    PhysicsScene predictionPhysicsScene;
    PhysicsScene currentPhysicsScene;


    public string predicitionSceneName;

    private bool predict;


    // Start is called before the first frame update
    void Start()
    {
        planets = GameManager.currentLevelObjects;

        predicitionSceneName = "prediction";
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene(predicitionSceneName, parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();
        Physics.autoSimulation = false;

        foreach (GameObject planet in planets)
        {
            GameObject fakePlanet = Instantiate(planet, planet.transform.position, planet.transform.rotation);
            SceneManager.MoveGameObjectToScene(fakePlanet, predictionScene);
        }
    }

    void FixedUpdate()
    {
        predictionPhysicsScene.Simulate(Time.fixedDeltaTime * 5);
        currentPhysicsScene.Simulate(Time.fixedDeltaTime);


        if (fakeShip)
        {
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
            DestroyFakeShip();
            predict = false;
        }


        Level0.LevelRotations();
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
        if (realShip)
        {
            fakeShip = Instantiate(fakeShipVariant, realShip.transform.position, realShip.transform.rotation);
            SceneManager.MoveGameObjectToScene(fakeShip, predictionScene);
            Renderer fakeRenderer = fakeShip.GetComponent<Renderer>();
            fakeRenderer.enabled = true; // Boolean to render the fake ship or not during play mode
            fakeShip.GetComponent<Rigidbody>().AddForce(ShipHelper.launchForce, ForceMode.VelocityChange);
        }
       
    }

    void DestroyFakeShip()
    {
        float delay = 3f;
        Destroy(fakeShip, delay);
    }
}