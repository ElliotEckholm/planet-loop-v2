using System;
using UnityEngine;

public class WinZoneCollider : MonoBehaviour
{
    public static bool winZoneCollision;
    public static string colliderName;
    private Color winColor = new Color(0,1,0,0.6f);
    private Color defaultColor = new Color(0,1,0,0.1567f);

    public static int numWinZonesHit = 0;

    private void OnTriggerEnter(Collider other)
    {
        // other.gameObject.name.Contains("Ship") && 
        if (!GameManager.isGameOver && !ShipManager.shipCollision && LaunchButton.launchButtonClickedFirstTime)
        {
            winZoneCollision = true;
            colliderName = other.gameObject.name;
            numWinZonesHit++;
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", winColor);

        }
    }

    private void Update()
    {
        if (GameManager.restartClicked)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", defaultColor);
        }
    }

    public static void Reset()
    {
        winZoneCollision = false;
    }
}
