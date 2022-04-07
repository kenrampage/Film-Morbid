using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MusicPuzzleSFXEvents
{
    public UnityEvent onNote0;
    public UnityEvent onNote1;
    public UnityEvent onNote2;
    public UnityEvent onNote3;
    public UnityEvent onNote4;
    public UnityEvent onNote5;
    public UnityEvent onNote6;
    public UnityEvent onNote7;
    public UnityEvent onNote8;
}

public class MusicDot : MonoBehaviour
{
    public float initialYPos;
    [SerializeField] float relativeDist;
    Vector3[] pts;
    public int currentPt;

    //Checking for correctness.
    [SerializeField] GameObject musicBoard;

    [SerializeField] private MusicPuzzleSFXEvents musicPuzzleSFXEvents;

    // Start is called before the first frame update
    void Start()
    {
        initialYPos = transform.localPosition.y;
        //pts = new Vector3[]{ new Vector3(transform.position.x, initialYPos, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.15f, transform.position.z), new Vector3(transform.position.x,initialYPos + 0.28f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.41f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.52f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.65f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.78f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.935f, transform.position.z), new Vector3(transform.position.x, initialYPos + 1.06f, transform.position.z) };
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ChangeDotPosition(true);
                        musicBoard.GetComponent<MusicPuzzle>().CheckIfCorrect(hit.collider.gameObject);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ChangeDotPosition(false);
                        musicBoard.GetComponent<MusicPuzzle>().CheckIfCorrect(hit.collider.gameObject);
                    }
                }
            }
        }
    }
    void ChangeDotPosition(bool up)
    {
        if (up)
        {
            currentPt++;
            if (currentPt == 9)
            {
                currentPt = 0;
            }
        }
        else if (!up)
        {
            currentPt--;
            if (currentPt == -1)
            {
                currentPt = 8;
            }
        }

        CheckCurrentNote(currentPt);
        transform.localPosition = new Vector3(transform.localPosition.x, initialYPos + relativeDist * currentPt, transform.localPosition.z);
    }


    private void CheckCurrentNote(int position)
    {
        switch (position)
        {
            case 0:
                musicPuzzleSFXEvents.onNote0?.Invoke();
                break;

            case 1:
                musicPuzzleSFXEvents.onNote1?.Invoke();
                break;

            case 2:
                musicPuzzleSFXEvents.onNote2?.Invoke();
                break;

            case 3:
                musicPuzzleSFXEvents.onNote3?.Invoke();
                break;

            case 4:
                musicPuzzleSFXEvents.onNote4?.Invoke();
                break;

            case 5:
                musicPuzzleSFXEvents.onNote5?.Invoke();
                break;

            case 6:
                musicPuzzleSFXEvents.onNote6?.Invoke();
                break;

            case 7:
                musicPuzzleSFXEvents.onNote7?.Invoke();
                break;

            case 8:
                musicPuzzleSFXEvents.onNote8?.Invoke();
                break;

            default: 
                break;
        }
    }
}
