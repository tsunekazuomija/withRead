using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusMinusButton : MonoBehaviour
{
    public Slider slider;

    public bool isIncrease;

    private bool buttonDown = false;
    private int count = 0;

    private void FixedUpdate()
    {
        if (buttonDown)
        {
            if ((count % 25 == 0 && count < 75) || (count % 5 == 0 && count >= 75))
            {
                if (isIncrease)
                {
                    if (slider.value != slider.maxValue)
                    {
                        ++slider.value;
                    }
                }
                else
                {
                    if (slider.value != slider.minValue)
                    {
                        --slider.value;
                    }
                }
            }
            if (count == 180000)
            {
                count = 0;
            }
            ++count;
        }
    }

    public void OnButtonDown()
    {
        Debug.Log("Down");
        buttonDown = true;
    }

    public void OnButtonUp()
    {
        Debug.Log("Up");
        buttonDown = false;
        count = 0;
    }
}
