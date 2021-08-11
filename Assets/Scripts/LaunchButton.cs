using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    public static bool launchButtonClicked = false;
    public static bool launchButtonClickedFirstTime = false;

    public void LaunchClicked()
    {
        launchButtonClicked = true;
        launchButtonClickedFirstTime = true;
    }
}
