using UnityEngine;

public class Level0 : MonoBehaviour
{
    
    public void FixedUpdate()
    {
        if (WinZoneCollider.winZoneCollision && ShipManager.shipLanded)
        {
            GameManager.LevelComplete = true;
        }
    }

    public static void LevelRotations()
    {
        // Make Moon1 orbit Earth
        // Vector3 moon1OrbitAixs = new Vector3(0, 0, 0.1f);
        // GameObject.Find("Moon1").transform.RotateAround(GameObject.Find("Earth").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);
        // GameObject.Find("Moon1(Clone)").transform.RotateAround(GameObject.Find("Earth(Clone)").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);

        //// Rotate Planet1
        GameObject.Find("Planet1").transform.Rotate(0, 0, 1);
        GameObject.Find("Planet1(Clone)").transform.Rotate(0, 0, 1);
        
        // Make Planet2 orbit Planet1
        // Vector3 planet2OrbitAixs = new Vector3(0, 0, 0.1f);
        // GameObject.Find("Planet2").transform.RotateAround(GameObject.Find("Planet1").transform.position, planet2OrbitAixs, 50 * Time.deltaTime);
        // GameObject.Find("Planet2(Clone)").transform.RotateAround(GameObject.Find("Planet1(Clone)").transform.position, planet2OrbitAixs, 50 * Time.deltaTime);
    }

}
