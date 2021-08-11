using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0 : MonoBehaviour
{
    // Allows helper classes to access this level's planets
    public static GameObject[] planets;

    Scene currentScene;
    PhysicsScene currentPhysicsScene;

    // Start is called before the first frame update
    private void Start()
    {
        planets = GameManager.currentLevelObjects;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();
        //Physics.autoSimulation = false;

        
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Is game over");
    }

    public static void LevelRotations()
    {

        //Rotate Earth
        GameObject.Find("Earth").transform.Rotate(0, 1, 0);
        // Make Moon1 orbit Earth
        Vector3 moon1OrbitAixs = new Vector3(0, 0, 0.1f);
        GameObject.Find("Moon1").transform.RotateAround(GameObject.Find("Earth").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);

        //// Rotate Planet1
        GameObject.Find("Planet1").transform.Rotate(0, 0, 1);
        // Make Moon2 orbit Planet1
        Vector3 moon2OrbitAixs = new Vector3(0, 1, 0);
        //GameObject.Find("Moon2").transform.RotateAround(GameObject.Find("Planet1").transform.position, moon2OrbitAixs, 100 * Time.deltaTime);



        GameObject.Find("Earth(Clone)").transform.Rotate(0, 1, 0);
        // Make Moon1 orbit Earth
        Vector3 fakeMoon1OrbitAixs = new Vector3(0, 0, 0.1f);
        GameObject.Find("Moon1(Clone)").transform.RotateAround(GameObject.Find("Earth(Clone)").transform.position, fakeMoon1OrbitAixs, 100 * Time.deltaTime);

        //// Rotate Planet1
        GameObject.Find("Planet1(Clone)").transform.Rotate(0, 0, 1);
        // Make Moon2 orbit Planet1
        Vector3 fakeMoon2OrbitAixs = new Vector3(0, 1, 0);

        ////Rotate Earth
        //GameObject.Find("Earth(Clone)").transform.Rotate(0, 1, 0);
        //// Make Moon1 orbit Earth
        //Vector3 cloneMoon1OrbitAixs = new Vector3(0, 0, 0.1f);
        //GameObject.Find("Moon1").transform.RotateAround(GameObject.Find("Earth(Clone)").transform.position, cloneMoon1OrbitAixs, 100 * Time.deltaTime);

        ////// Rotate Planet1
        //GameObject.Find("Planet1(Clone)").transform.Rotate(0, 0, 1);
        //// Make Moon2 orbit Planet1
        //Vector3 cloneMoon2OrbitAixs = new Vector3(0, 1, 0);
        //GameObject.Find("Moon2").transform.RotateAround(GameObject.Find("Planet1").transform.position, moon2OrbitAixs, 100 * Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Is game over");

    //    if (name == "Ship")
    //    {
    //        ShipManager.shipCollision = true;
            
    //    }
    //    if (name.Contains("FakeShip"))
    //    {
    //        ShipManager.fakeShipCollision = true;
    //    }


    //    // Win level by hitting planet 1
    //    if (collision.gameObject.name == "Planet1")
    //    {
    //        GameManager.isGameOver = true;
    //        Debug.Log("Is game over");
    //        //Debug.Log("collision  = " + collision.gameObject.name);
    //        //GameManager.LevelComplete = true;
    //    }

    //    //foreach (ContactPoint contact in collision.contacts)
    //    //{
    //    //    Debug.DrawRay(contact.point, contact.normal, Color.white);
    //    //}
    //    //if (collision.relativeVelocity.magnitude > 2)

    //}
}
