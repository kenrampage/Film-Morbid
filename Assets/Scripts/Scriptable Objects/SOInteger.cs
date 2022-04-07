using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Integer_", menuName = "Scriptable Objects/SOInteger")]
public class SOInteger: ScriptableObject
{
    [SerializeField] private int defaultValue;
    [SerializeField] private int value;
    [SerializeField] private int maxValue = 99999;
    [SerializeField] private int minValue;

    public Action<int> onValueChanged;
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

    public int GetValue()
    {
        return value;
    }

    public void SetValue(int i)
    {
        if (i != value)
        {
            value = i;
            onValueChanged?.Invoke(value);
        }

        CheckMinMaxValue();
    }

    public void AddToValue(int i)
    {
        SetValue(value + i);
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
        }

        if (value >= maxValue)
        {
            onMaxValueMet?.Invoke();
        }

    }
}
