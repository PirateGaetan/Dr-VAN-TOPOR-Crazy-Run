using UnityEngine;
using UnityEngine.UI;

public class PurpleSliderManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void InitPurpleSlider(float value)
    {
        SetPurpleSlider(value);
    }
    public void SetPurpleSlider(float value)
    {
        slider.value = value;
    }
}