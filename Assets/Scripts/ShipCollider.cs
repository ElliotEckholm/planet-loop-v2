using UnityEngine;

public class ShipCollider : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        // Win level by hitting planet 1
        if (collision.gameObject.name.Contains("Planet1"))
        {
            DestroyShip(name);
            //GameManager.LevelComplete = true;
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
