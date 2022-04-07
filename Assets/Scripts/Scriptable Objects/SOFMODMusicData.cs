using UnityEngine;
using FMODUnity;
using System;

[CreateAssetMenu(fileName = "FMODMusicData", menuName = "Scriptable Objects/SOFMODMusicData")]
public class SOFMODMusicData : ScriptableObject
{
    public EventReference[] fmodEvents;
    public int eventIndex;
    public bool shuffle;

    public event Action onPauseToggleTriggered;
    public event Action onPlaybackStartTriggered;
    public event Action onPlaybackStopTriggered;
    public event Action<int> onSetSectionTriggered;
    public event Action onSetHighPassOnTriggered;
    public event Action onSetHighPassOffTriggered;

    public void TogglePause()
    {
        onPauseToggleTriggered?.Invoke();
    }

    public void StartPlayback()
    {
        onPlaybackStartTriggered?.Invoke();
    }

    public void StopPlayback()
    {
        onPlaybackStopTriggered?.Invoke();
    }

    public void SetSection(int value)
    {
        onSetSectionTriggered?.Invoke(value);
    }

    public void SetHighpassOn()
    {
        onSetHighPassOnTriggered?.Invoke();
    }

    public void SetHighPassOff()
    {
        onSetHighPassOffTriggered?.Invoke();
    }

}
