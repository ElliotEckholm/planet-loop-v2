using UnityEngine;
using UnityEngine.UI;

public class PanelPlayUI : MonoBehaviour
{
    public static bool buttonEntered = false;
    public Text launchForceText;

    private void Update()
    {
        // Set launch force text equal to ship's launch magnitude
        launchForceText.text = ShipHelper.launchMagnitude + " Newtons";
    }

    public void EnterUI()
    {
        buttonEntered = true;
    }
    public void ExitUI()
    {
        buttonEntered = false;
    }
}
