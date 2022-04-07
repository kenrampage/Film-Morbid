using UnityEngine;
using FMODUnity;

public class FMODPlay : MonoBehaviour
{
    [SerializeField] private EventReference fmodEvent;
    [SerializeField] private bool ignoreSeekSpeed;
    [SerializeField] private bool startOnEnable;
    [SerializeField] private FMOD.Studio.PLAYBACK_STATE playbackState;

    private bool is3D;

    private FMOD.Studio.EventInstance eventInstance;

    private void Update()
    {
        if (is3D)
        {
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        }
    }

    public void InitializeEvent()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        
        RuntimeManager.GetEventDescription(fmodEvent).is3D(out is3D);

        GetPlaybackState();
    }

    private void OnEnable()
    {
        InitializeEvent();
        if (startOnEnable)
        {
            StartEvent();
        }
    }

    private void OnDisable()
    {
        ReleaseEvent();
    }

    public void StartEvent()
    {
        GetPlaybackState();
        if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            eventInstance.start();
            GetPlaybackState();
        }
        
    }

    public void PauseEvent()
    {
        eventInstance.setPaused(true);
        GetPlaybackState();
    }

    public void UnpauseEvent()
    {
        eventInstance.setPaused(false);
        GetPlaybackState();
    }

    public void StopEventNoFadeout()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        GetPlaybackState();
    }

    public void StopEventWithFadeout()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GetPlaybackState();
    }

    public void ReleaseEvent()
    {
        eventInstance.release();
    }

    public void GetPlaybackState()
    {
        eventInstance.getPlaybackState(out playbackState);
    }
}
