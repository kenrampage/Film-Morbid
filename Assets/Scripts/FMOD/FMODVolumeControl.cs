using UnityEngine;
using FMODUnity;
using TMPro;
using UnityEngine.UI;

public class FMODVolumeControl : MonoBehaviour
{
    public FMOD.Studio.Bus masterBus;
    private Slider slider;

    private void Awake()
    {
        GetBusReferences();
        slider = GetComponent<Slider>();

    }

    private void OnEnable()
    {
        InitializeSlider();
    }

    public void SetVolume(float v)
    {
        masterBus.setVolume(v);
    }

    public float GetVolume()
    {
        float v;
        masterBus.getVolume(out v);
        return v;
    }

    public void GetBusReferences()
    {
        if (masterBus.isValid() == false)
        {
            masterBus = RuntimeManager.GetBus("bus:/");
        }
    }

    [ContextMenu("Test Values")]
    public void TestValues()
    {
        print("FMOD Volume: " + GetVolume());
        print("Slider value is: " + slider.value);
    }

    public void InitializeSlider()
    {
        slider.value = GetVolume();
    }
}
