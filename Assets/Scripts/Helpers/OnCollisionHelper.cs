using UnityEngine;
using UnityEngine.Events;

public class OnCollisionHelper : MonoBehaviour
{
    [SerializeField] private string[] collisionTags;
    [SerializeField] private UnityEvent onCollisionEnterEvent;
    [SerializeField] private UnityEvent onCollisionExitEvent;


    private void OnCollisionEnter(Collision other)
    {
        if (CheckTags(other.gameObject))
        {
            onCollisionEnterEvent?.Invoke();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (CheckTags(other.gameObject))
        {
            onCollisionExitEvent?.Invoke();
        }
    }

    private bool CheckTags(GameObject other)
    {
        if (collisionTags.Length == 0)
        {
            return true;
        }

        foreach (string tag in collisionTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

}
