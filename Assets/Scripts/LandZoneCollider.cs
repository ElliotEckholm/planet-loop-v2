using UnityEngine;

public class LandZoneCollider : MonoBehaviour
{
    public static bool landZoneCollision;
    public static string landZoneName;
    public static string colliderName;
    private Color landColor = new Color(1,1,0,0.6f);
    private Color defaultColor = new Color(1,0.878f,0,0.1567f);

    private void Update()
    {
        TurnLandZoneSolidGreen();
        
        if (GameManager.restartClicked)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", defaultColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // other.gameObject.name.Contains("Ship") && 
        if (!GameManager.isGameOver && !ShipManager.shipCollision && LaunchButton.launchButtonClickedFirstTime)
        {
            landZoneCollision = true;
            landZoneName = name;
            colliderName = other.gameObject.name;
        }
    }
    
    private void TurnLandZoneSolidGreen()
    {
        if (landZoneCollision)
        {
            GameObject.Find(landZoneName).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", landColor);
        }
    }

    public static void Reset()
    {
        landZoneCollision = false;
    }
}
