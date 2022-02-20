using System;
using UnityEngine;

public class LandButton : MonoBehaviour
{
    public static bool landButtonClicked;
    public static bool landButtonClickedFirstTime;

    public void Start()
    {
        // gameObject.SetActive(false);

    }
    // public void Update()
    // {
    //     if (LaunchButton.launchButtonClickedFirstTime)
    //     {
    //         Debug.Log("LAUNCH CLICKED");
    //         gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         gameObject.SetActive(false);
    //     }
    //     
    // }

    public void LandClicked()
    {
        landButtonClicked = true;
        landButtonClickedFirstTime = true;
    }
}
