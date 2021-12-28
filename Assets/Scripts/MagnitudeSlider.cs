using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class MagnitudeSlider : MonoBehaviour
{

    public static float magnitudeSliderValue;
    public static Slider magnitudeSlider; 
   
    // Update is called once per frame
    void Start()
    {
        magnitudeSlider = GetComponent<Slider>();
        magnitudeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void Update()
    {
        if (LaunchButton.launchButtonClickedFirstTime)
        {
            magnitudeSlider.enabled = false;
        }
        else
        {
            magnitudeSlider.enabled = true;
        }
    }
    
    public void ValueChangeCheck()
    {
        magnitudeSliderValue = magnitudeSlider.value;
    }
    
    public static void Reset()
    {
        magnitudeSlider.value = 0;
        magnitudeSliderValue = 0;
    }
}
