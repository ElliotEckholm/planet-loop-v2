using UnityEngine;
using UnityEngine.UI;

public class PanelPlayUI : MonoBehaviour
{
    public static bool buttonEntered = false;
    public Text magnitudeSliderText;
    public Text angleSliderText;

    private void Update()
    {
        // Set magnitude slider text equal to ship's launch magnitude (in Newtons)
        magnitudeSliderText.text = MagnitudeSlider.magnitudeSliderValue + " N";
        // Set angle slider text equal to ship's launch angle
        angleSliderText.text = MagnitudeSlider.magnitudeSliderValue + " N";
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
