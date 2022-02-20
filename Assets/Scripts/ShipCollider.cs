using UnityEngine;

public class ShipCollider : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        // Blow up ship if it collides with anything other than a WinZone
        if (!collision.gameObject.name.Contains("WinZone") && LaunchButton.launchButtonClickedFirstTime &&
            !(WinZoneCollider.winZoneCollision && ShipManager.shipLanded))
        {
            DestroyShip(name);
        }
    }

    private void DestroyShip(string name)
    {
    
        // Destory fake ship
        if (name.Contains("FakeShip"))
        {
            Destroy(gameObject);
        }
        // Destory real ship 
        else
        {
            GameManager.isGameOver = true;
            ShipManager.shipCollision = true;
        }
    }
}
