using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollider : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Is game over");

        //if (name == "Ship")
        //{
        //    ShipManager.shipCollision = true;

        //}

        //Debug.Log(collision.gameObject.name);

        // Win level by hitting planet 1
        if (collision.gameObject.name.Contains("Planet1"))
        {
            
            DestroyShip(name);
            
            //Debug.Log("collision  = " + collision.gameObject.name);
            //GameManager.LevelComplete = true;
        }

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)

    }

    private void DestroyShip(string name)
    {

        // Destory fake ship
        if (name.Contains("FakeShip"))
        {
            Destroy(this.gameObject);
        }
        // Destory real ship 
        else
        {
            GameManager.isGameOver = true;
            ShipManager.shipCollision = true;
        }
    }
}
