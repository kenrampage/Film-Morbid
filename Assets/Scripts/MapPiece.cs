using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapPiece : MonoBehaviour
{
    [SerializeField] Material[] colors;
    public bool isBlack;

    GameObject playerCamera;
    public bool puzzleSolved;

    [SerializeField] private UnityEvent onColorToggled;


    // Start is called before the first frame update
    void Start()
    {
        puzzleSolved = false;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //Changing the color of a square;
        if (!puzzleSolved)
        {
            RaycastHit hit;
            if (playerCamera.transform.parent.GetComponent<SmoothIntro>().started)
            {
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 4))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            ToggleColor();
                            onColorToggled?.Invoke();
                        }
                    }
                }
            }
        }
    }

    public void ToggleColor()
    {

        isBlack = !isBlack;
        if (isBlack)
        {
            GetComponent<MeshRenderer>().material = colors[0];
        }
        else
        {
            GetComponent<MeshRenderer>().material = colors[1];
        }
        if(!puzzleSolved)
        {
            transform.parent.GetComponent<MapBoard>().CheckTiles();
        }
    }
}
