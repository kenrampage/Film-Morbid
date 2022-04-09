using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightningEffect : MonoBehaviour
{

    [SerializeField] private float minTimer;
    [SerializeField] private float maxTimer;

    [SerializeField] private int minFlashes;
    [SerializeField] private int maxFlashes;
    [SerializeField] private float minFlashLength;
    [SerializeField] private float maxFlashLength;
    [SerializeField] private float minBetweenFlashes;
    [SerializeField] private float maxBetweenFlashes;

    [SerializeField] private float minAudioDelay;
    [SerializeField] private float maxAudioDelay;

    [SerializeField] private GameObject visualFlashObject;
    [SerializeField] private UnityEvent onSFXTriggered;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TimerCountdown");
    }

    private IEnumerator TimerCountdown()
    {
        float timer = Random.Range(minTimer, maxTimer);
        print(timer + " sec timer started");
        yield return new WaitForSecondsRealtime(timer);
        StartCoroutine("FlashEffect");
    }

    private IEnumerator FlashEffect()
    {
        float flashCount = Random.Range(minFlashes, maxFlashes);
        print("flashing " + flashCount + " times");

        while (flashCount > 0)
        {
            float flashLength = Random.Range(minFlashLength, maxFlashLength);
            float flashDelay = Random.Range(minBetweenFlashes, maxBetweenFlashes);

            visualFlashObject.SetActive(true);
            yield return new WaitForSecondsRealtime(flashLength);
            visualFlashObject.SetActive(false);
            yield return new WaitForSecondsRealtime(flashDelay);

            flashCount--;
        }
        StartCoroutine("PlaySFX");
        StartCoroutine("TimerCountdown");
    }

    private IEnumerator PlaySFX()
    {
        float sfxDelay = Random.Range(minAudioDelay,maxAudioDelay);
        print("Playing SFX in " + sfxDelay + " seconds");
        yield return new WaitForSecondsRealtime(sfxDelay);
        onSFXTriggered?.Invoke();
    }

    [ContextMenu("Test Flash")]
    private void TestFlashEffect()
    {
        StartCoroutine("FlashEffect");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
