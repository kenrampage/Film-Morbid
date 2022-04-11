using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    [SerializeField] private UnityEvent onEventTriggered;

    public void TriggerEvent()
    {
        onEventTriggered?.Invoke();
    }
}
