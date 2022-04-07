using UnityEngine;
using FMODUnity;

public class FMODPlayWithParameters : MonoBehaviour
{
    [SerializeField] private SOFMODParameterData fmodParameterData;

    [SerializeField] private EventReference fmodEvent;
    [SerializeField] private string parameterName;
    [SerializeField] private bool ignoreSeekSpeed;
    [SerializeField] private bool startOnEnable;
    [SerializeField] private FMOD.Studio.PLAYBACK_STATE playbackState;

    private bool playAttached;

    private FMOD.Studio.EventInstance eventInstance;

    private void Update()
    {
        if (playAttached)
        {
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        }
    }

    public void InitializeEvent()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        bool is3D;
        RuntimeManager.GetEventDescription(fmodEvent).is3D(out is3D);

        playAttached = is3D = true ? true : false;
        GetPlaybackState();
    }

    private void OnEnable()
    {
        InitializeEvent();
        fmodParameterData.onValueUpdated += SetParameterByName;
        if (startOnEnable)
        {
            StartEvent();
        }
    }

    private void OnDisable()
    {
        ReleaseEvent();
        fmodParameterData.onValueUpdated -= SetParameterByName;
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

    public void SetParameterByName(float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }

    public void ReleaseEvent()
    {
        eventInstance.release();
        playAttached = false;
    }

    public void GetPlaybackState()
    {
        eventInstance.getPlaybackState(out playbackState);
    }
}
