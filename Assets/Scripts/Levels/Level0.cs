using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{
    
    public void FixedUpdate()
    {
        if (WinZoneCollider.winZoneCollision && ShipManager.shipLanded)
        {
            StartCoroutine(waiter());
        }
    }
    
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1F);
        GameManager.LevelComplete = true;
    }

    public static void SetupLevel()
    {
        // Make Moon1 orbit Planet0
        Vector3 moon1OrbitAixs = new Vector3(0, 0, 0.1f);
        GameObject.Find("Moon1").transform.RotateAround(GameObject.Find("Planet0").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);
        GameObject.Find("Moon1(Clone)").transform.RotateAround(GameObject.Find("Planet0(Clone)").transform.position, moon1OrbitAixs, 100 * Time.deltaTime);
        
        // Rotate Planet1
        GameObject.Find("Planet1").transform.Rotate(0, 0, 1);
        GameObject.Find("Planet1(Clone)").transform.Rotate(0, 0, 1);
        
        // Make Planet2 orbit Planet1
        Vector3 planet2OrbitAixs = new Vector3(0, 0, 0.1f);
        GameObject.Find("Planet2").transform.RotateAround(GameObject.Find("Planet1").transform.position, planet2OrbitAixs, 50 * Time.deltaTime);
        GameObject.Find("Planet2(Clone)").transform.RotateAround(GameObject.Find("Planet1(Clone)").transform.position, planet2OrbitAixs, 50 * Time.deltaTime);
    }
}
