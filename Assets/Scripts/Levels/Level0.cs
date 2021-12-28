using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0 : MonoBehaviour
{
    
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

}
