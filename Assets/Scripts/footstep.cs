using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstep : MonoBehaviour
{
    public PlayerMovement playerMovementScript;

    public AudioSource speaker_footsteps;

    public AudioClip[] sound_footsteps;
    public int footstepArrayLength;

    public float footstepDelay = 1;
    private bool footStepCoroutineCanExit = false;

    private int indexOld;

    public int index;

    void Start()
    {
        StartCoroutine(footStepCoroutine());
        footstepArrayLength = sound_footsteps.Length;
    }

    public IEnumerator footStepCoroutine()
    {
        while (footStepCoroutineCanExit == false)
        {
            while (playerMovementScript.playerIsNowMoving && playerMovementScript.isGrounded)
            {
                index = Random.Range(0, footstepArrayLength);
                while (index == indexOld)
                {
                    index = Random.Range(0, footstepArrayLength);
                }
                indexOld = index;
                speaker_footsteps.PlayOneShot(sound_footsteps[index]);
                yield return new WaitForSeconds(footstepDelay);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
