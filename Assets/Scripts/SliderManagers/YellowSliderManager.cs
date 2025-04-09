using UnityEngine;
using UnityEngine.UI;

public class YellowSliderManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void InitYellowSlider(float value)
    {
        SetYellowSlider(value);
    }
    public void SetYellowSlider(float value)
    {
        slider.value = value;
    }
}