using UnityEngine;
using FMODUnity;


public class FMODPlayOneShot : MonoBehaviour
{
    [SerializeField] private EventReference fmodEvent;
    [SerializeField] private bool soundEffectsOn = true;
    private bool is3D;

    private void Awake()
    {
        RuntimeManager.GetEventDescription(fmodEvent).is3D(out is3D);
    }

    public void PlaySoundEvent()
    {
        if (soundEffectsOn)
        {
            RuntimeManager.PlayOneShot(fmodEvent, gameObject.transform.position);

            // if (is3D)
            // {
            //     PlaySoundEventAttached();
            // }
            // else
            // {
            //     RuntimeManager.PlayOneShot(fmodEvent);
            // }
        }

    }

    private void PlaySoundEventAttached()
    {
        if (soundEffectsOn)
        {
            RuntimeManager.PlayOneShotAttached(fmodEvent.ToString(), gameObject);
        }

    }

    public void DisableSoundEffects()
    {
        soundEffectsOn = false;
    }

    public void EnableSoundEffects()
    {
        soundEffectsOn = true;
    }

}
