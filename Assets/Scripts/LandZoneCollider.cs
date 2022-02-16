using UnityEngine;

public class LandZoneCollider : MonoBehaviour
{
    public static bool landZoneCollision;
    public static string landZoneName;
    public static string colliderName;
    private Color landColor = new Color(1,1,0,0.6f);

    private void Update()
    {
        TurnLandZoneSolidGreen();
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
}
