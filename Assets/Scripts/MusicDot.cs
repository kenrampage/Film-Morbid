using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDot : MonoBehaviour
{
    public float initialYPos;
    [SerializeField] float relativeDist;
    Vector3[] pts;
    public int currentPt;

    //Checking for correctness.
    [SerializeField] GameObject musicBoard;
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
        transform.localPosition = new Vector3(transform.localPosition.x, initialYPos + relativeDist*currentPt, transform.localPosition.z);
    }
    
}
