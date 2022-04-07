using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FMODParameterData_", menuName = "Scriptable Objects/SOFMODParameterData", order = 0)]
public class SOFMODParameterData : ScriptableObject
{
    public event Action<float> onValueUpdated;
    private float floatValue;
    public float FloatValue
    {
        get
        {
            return floatValue;
        }
        set
        {
            floatValue = value;
            onValueUpdated?.Invoke(value);
        }
    }

}
