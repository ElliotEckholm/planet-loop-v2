using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class AngleSlider : MonoBehaviour
{

    public static float oldValue;
    public static float newValue;
    public static float angleSliderValue;
    public Slider angleSlider;
    private static GameObject ship;
   
    // Update is called once per frame
    void Start()
    {
        
        angleSlider = GetComponent<Slider>();
        angleSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
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
}
