using UnityEngine;

public class WinZoneCollider : MonoBehaviour
{
    public static bool winZoneCollision;
    public static string winZoneName;
    public static string colliderName;
    private Color winColor = new Color(0,1,0,0.6f);

    private void Update()
    {
        TurnWinZoneSolidGreen();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Ship") && !GameManager.isGameOver)
        {
            winZoneCollision = true;
            winZoneName = name;
            colliderName = other.gameObject.name;
        }
    }
    
    private void TurnWinZoneSolidGreen()
    {
        if (winZoneCollision)
        {
            GameObject.Find(winZoneName).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", winColor);
        }
    }
}
