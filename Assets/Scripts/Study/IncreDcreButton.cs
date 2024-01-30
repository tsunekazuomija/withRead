using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IncreDcreButton : MonoBehaviour
{
    public Slider slider;

    public bool isIncrease;

    void Start()
    {

        if (isIncrease)
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
