using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class IncreDcreButton : MonoBehaviour
{
    public Slider slider;

    public bool isIncrese;

    void Start()
    {

        if (isIncrese)
        {
            GetComponent<Button>().onClick.AddListener( () => 
            {
                if (slider.value != slider.maxValue)
                {
                    ++slider.value;
                }
            } );
        }
        else
        {
            GetComponent<Button>().onClick.AddListener( () => 
            {
                if (slider.value != slider.minValue)
                {
                    --slider.value;
                }
            } );
        }
    }
}
