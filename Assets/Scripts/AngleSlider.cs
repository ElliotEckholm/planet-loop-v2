using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class AngleSlider : MonoBehaviour
{

    public static float oldValue;
    public static float newValue;
    public static float angleSliderValue;
    public static Slider angleSlider;
    private static GameObject ship;
   
    // Update is called once per frame
    void Start()
    {
        angleSlider = GetComponent<Slider>();
        angleSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void Update()
    {
        if (LaunchButton.launchButtonClickedFirstTime)
        {
            angleSlider.enabled = false;
        }
        else
        {
            angleSlider.enabled = true;
        }
    }

    public void ValueChangeCheck()
    {
        
            
            ship = GameObject.Find("Ship");
            // "Undo" last angle and apply new angle change
            oldValue = newValue;
            newValue = angleSlider.value;
            angleSliderValue = newValue - oldValue;
        
            ShipHelper.rotateShip(ship);
    }
    
    public static void Reset()
    {
        angleSlider.value = 0;
        angleSliderValue = 0;
    }
}
