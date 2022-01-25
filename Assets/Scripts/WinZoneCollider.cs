using System;
using UnityEngine;

public class WinZoneCollider : MonoBehaviour
{
    public static bool winZoneCollision;
    public static string winZoneName;
    public static string colliderName;
    private float alpha;

    private void Start()
    {
        alpha = gameObject.GetComponent<MeshRenderer>().material.color.a;

        Debug.Log(gameObject.GetComponent<MeshRenderer>().material.color);
    }

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
            alpha = 1;
        }
    }
}
