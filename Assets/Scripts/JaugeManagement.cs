using UnityEngine;
using UnityEngine.UI;

public class JaugeManagement : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void InitSlider(float value)
    {
        SetSlider(value);
    }
    public void SetSlider(float value)
    {
        slider.value = value;
    }
    

}
