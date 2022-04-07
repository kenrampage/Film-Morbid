using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Float_", menuName = "Scriptable Objects/SOFloat")]
public class SOFloat : ScriptableObject
{
    [SerializeField] private float defaultValue;
    [SerializeField] private float value;
    [SerializeField] private float maxValue = 99999;
    [SerializeField] private float minValue;

    public Action<float> onValueChanged;
    public Action onMaxValueMet;
    public Action onMinValueMet;
    public Action onValueReset;


    public void IncrementValue()
    {

        if (value < maxValue)
        {
            value++;
            onValueChanged?.Invoke(value);
        }


        CheckMinMaxValue();
    }

    public void DecrementValue()
    {
        if (value > minValue)
        {
            value--;
            onValueChanged?.Invoke(value);
        }

        CheckMinMaxValue();
    }

    public float GetValue()
    {
        return value;
    }

    public float GetPercentage()
    {
        return value / defaultValue;
    }


    public void SetValue(float f)
    {
        if (f != value)
        {
            value = f;
            onValueChanged?.Invoke(value);
        }

        CheckMinMaxValue();
    }

    public void AddToValue(float f)
    {
        SetValue(value + f);
    }

    public void ResetValue()
    {
        value = defaultValue;
        onValueReset?.Invoke();
    }

    public void CheckMinMaxValue()
    {
        if (value <= minValue)
        {
            onMinValueMet?.Invoke();
            value = minValue;
        }

        if (value >= maxValue)
        {
            onMaxValueMet?.Invoke();
            value = maxValue;
        }

    }

    public void SetDefaultValue(float f)
    {
        defaultValue = f;
    }
}
