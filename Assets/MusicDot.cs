using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDot : MonoBehaviour
{
    public float initialYPos;
    Vector3[] pts;
    public int currentPt;

    //Checking for correctness.
    [SerializeField] GameObject musicBoard;
    // Start is called before the first frame update
    void Start()
    {
        initialYPos = transform.position.y;
        pts = new Vector3[]{ new Vector3(transform.position.x, initialYPos, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.15f, transform.position.z), new Vector3(transform.position.x,initialYPos + 0.28f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.41f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.52f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.65f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.78f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.935f, transform.position.z), new Vector3(transform.position.x, initialYPos + 1.06f, transform.position.z) };
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
        {
            if (Input.GetKeyDown("e"))
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ChangeDotPosition();
                    }
                    if (hit.collider.gameObject.name == "Treble Clef")
                    {
                        musicBoard.GetComponent<MusicPuzzle>().CheckIfCorrect(hit.collider.gameObject);
                    }
                }
            }
        }
    }
    void ChangeDotPosition()
    {
        currentPt++;
        if (currentPt == pts.Length)
        {
            currentPt = 0;
        }
        transform.position = pts[currentPt];
    }
    
}
