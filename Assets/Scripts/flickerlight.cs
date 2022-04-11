using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerlight : MonoBehaviour
{
    public float delay;
    public float range1;
    public float range2;
    public float finalValue;
    private Light source;
    bool flickering = true;

    private void Start()
    {
        source = this.gameObject.GetComponent<Light>();
        flickering = true;
        StartCoroutine(flicker());
    }
    public IEnumerator flicker()
    {
        while (flickering)
        {
            finalValue = Random.Range(range1, range2);
            source.intensity = finalValue;
            yield return new WaitForSeconds(delay);
            yield return null;
        }
        yield return null;
    }
}
