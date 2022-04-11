using UnityEngine;

public class FMODMusicPlayer : MonoBehaviour
{
    [SerializeField] private SOFMODMusicData fmodMusicData;
    [SerializeField] private FMOD.Studio.EventInstance eventInstance;

    public FMOD.Studio.PLAYBACK_STATE playbackState;
    public bool isPaused;

    private void OnEnable()
    {
        fmodMusicData.onPlaybackStartTriggered += StartPlayback;
        fmodMusicData.onPlaybackStopTriggered += StopPlayback;
        fmodMusicData.onPauseToggleTriggered += TogglePause;
        fmodMusicData.onSetSectionTriggered += SetSection;
    }

    private void OnDisable()
    {
        fmodMusicData.onPlaybackStartTriggered -= StartPlayback;
        fmodMusicData.onPlaybackStopTriggered -= StopPlayback;
        fmodMusicData.onPauseToggleTriggered -= TogglePause;
        fmodMusicData.onSetSectionTriggered -= SetSection;
    }

    private void SetEventInstance()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodMusicData.fmodEvents[fmodMusicData.eventIndex]);
    }

    public void RandomizeEventIndex()
    {
        fmodMusicData.eventIndex = Random.Range(0, fmodMusicData.fmodEvents.Length - 1);
    }

    public void InitializePlayer()
    {
        eventInstance.release();

        if (fmodMusicData.shuffle)
        {
            RandomizeEventIndex();
        }

        SetEventInstance();
        GetPlaybackState();
    }

    private void GetPlaybackState()
    {
        eventInstance.getPlaybackState(out playbackState);
        eventInstance.getPaused(out isPaused);
    }

    public void TogglePause()
    {
        GetPlaybackState();

        if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            StartPlayback();
        }
        else if (playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {


            if (isPaused)
            {
                PausePlayback();
            }
            else
            {
                UnpausePlayback();
            }
        }
    }

    [ContextMenu("Pause Music")]
    public void PausePlayback()
    {
        eventInstance.setPaused(true);
    }

    public void UnpausePlayback()
    {
        eventInstance.setPaused(false);
    }

    public void StartPlayback()
    {
        InitializePlayer();
        eventInstance.start();
    }

    public void StopPlayback()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetSection(int i)
    {
        eventInstance.setParameterByName("Section", i);
    }

    public void SetHighpassOn()
    {
        eventInstance.setParameterByName("HighPass", 1);
    }

    public void SetHighpassOff()
    {
        eventInstance.setParameterByName("HighPass", 0);
    }

    public void SetParameterByName(string parameterName, float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }
}
