using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class MagnitudeSlider : MonoBehaviour
{

    public static float magnitudeSliderValue;
    public Slider magnitudeSlider; 
   
    // Update is called once per frame
    void Start()
    {
        magnitudeSlider = this.GetComponent<Slider>();

        magnitudeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        magnitudeSliderValue = magnitudeSlider.value;
    }
}
