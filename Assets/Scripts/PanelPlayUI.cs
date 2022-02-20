using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlayUI : MonoBehaviour
{
    public static bool buttonEntered = false;
    public Text magnitudeSliderText;
    public Text angleSliderText;
    public Button landButton;
    public Button launchButton;

    private void Start()
    {
        landButton.gameObject.SetActive(false);
        launchButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        // Set magnitude slider text equal to ship's launch magnitude (in Newtons)
        magnitudeSliderText.text = MagnitudeSlider.magnitudeSliderValue + " N";
        // Set angle slider text equal to ship's launch angle
        angleSliderText.text = ShipHelper.newValue + " °";
        
        
        if (LaunchButton.launchButtonClickedFirstTime)
        {
            landButton.gameObject.SetActive(true);
            launchButton.gameObject.SetActive(false);
        }
        else
        {
            landButton.gameObject.SetActive(false);
            launchButton.gameObject.SetActive(true);
        }

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
