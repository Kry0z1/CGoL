using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField inputField;
    
    public float minValue = 0f;
    public float maxValue = 100f;
    public bool wholeNumbers = true;
    public float startValue = 0f;
    
    void Start()
    {
        InitializeUI();
    }
    
    void InitializeUI()
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.wholeNumbers = wholeNumbers;
        
        inputField.contentType = wholeNumbers ? TMP_InputField.ContentType.IntegerNumber : TMP_InputField.ContentType.DecimalNumber;

        slider.value = startValue;
        UpdateInputFieldFromSlider();
        
        slider.onValueChanged.AddListener(OnSliderChanged);
        inputField.onEndEdit.AddListener(OnInputFieldChanged);
        inputField.onSubmit.AddListener(OnInputFieldChanged);
    }
    
    void OnSliderChanged(float value)
    {
        UpdateInputFieldFromSlider();
    }
    
    void OnInputFieldChanged(string text)
    {
        if (float.TryParse(text, out float result))
        {
            result = Mathf.Clamp(result, minValue, maxValue);
            
            if (wholeNumbers)
            {
                result = Mathf.Round(result);
            }
            
            slider.value = result;
            
            UpdateInputFieldFromSlider();
        }
        else
        {
            UpdateInputFieldFromSlider();
        }
    }
    
    void UpdateInputFieldFromSlider()
    {
        if (wholeNumbers)
        {
            inputField.text = Mathf.RoundToInt(slider.value).ToString();
        }
        else
        {
            inputField.text = slider.value.ToString("F2");
        }
    }
}