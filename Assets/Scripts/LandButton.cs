using UnityEngine;

public class LandButton : MonoBehaviour
{
    public static bool landButtonClicked;
    public static bool landButtonClickedFirstTime;

    public void LandClicked()
    {
        landButtonClicked = true;
        landButtonClickedFirstTime = true;
    }
}
