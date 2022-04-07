using UnityEngine;
using UnityEngine.Events;

public class OnTriggerHelper : MonoBehaviour
{
    [SerializeField] private string[] triggerTags;
    [SerializeField] private UnityEvent onTriggerEnterEvent;
    [SerializeField] private UnityEvent onTriggerExitEvent;
    

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTags(other.gameObject))
        {
            onTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckTags(other.gameObject))
        {
            onTriggerExitEvent?.Invoke();
        }
    }

    private bool CheckTags(GameObject other)
    {
        if(triggerTags.Length == 0)
        {
            return true;
        }

        foreach (string tag in triggerTags)
        {
            if(other.gameObject.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

}
