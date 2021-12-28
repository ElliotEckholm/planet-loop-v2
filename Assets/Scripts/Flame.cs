using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public static ParticleSystem flame;
    // Start is called before the first frame update
    void Start()
    {
        flame = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LaunchButton.launchButtonClickedFirstTime)
        {
            flame.Play();
        }
        else
        {
            flame.Stop();
        }
    }
}
