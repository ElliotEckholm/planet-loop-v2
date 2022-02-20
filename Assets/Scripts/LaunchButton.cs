using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    public static bool launchButtonClicked;
    public static bool launchButtonClickedFirstTime;

    public void LaunchClicked()
    {
        launchButtonClicked = true;
        launchButtonClickedFirstTime = true;

        // enabled = false;
    }
}
