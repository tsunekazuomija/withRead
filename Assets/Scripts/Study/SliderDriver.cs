using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class that is assigned to a set of sliders.
/// This class is used when there are two sliders that have a relationship
/// in which one slider's value is always larger than the other's.
/// </summary>
public class SliderDriver : MonoBehaviour
{
    [SerializeField] private Slider targetSlider;
    [SerializeField] private bool IsLargerSlider;

    private void Start()
    {
        if (IsLargerSlider)
        {
            GetComponent<Slider>().onValueChanged.AddListener( (value) => {
                if (value < targetSlider.value)
                {
                    targetSlider.value = value;
                }
            });
        }
        else
        {
            GetComponent<Slider>().onValueChanged.AddListener( (value) => {
                if (value > targetSlider.value)
                {
                    targetSlider.value = value;
                }
            });
        }
    }
}
