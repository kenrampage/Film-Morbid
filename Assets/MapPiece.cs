using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    [SerializeField] Material[] colors;
    public bool isBlack;

    GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //Changing the color of a square;
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 4))
        {
            if (Input.GetKeyDown("e"))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleColor();
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
        else if (!isBlack)
        {
            GetComponent<MeshRenderer>().material = colors[1];
        }
        transform.parent.GetComponent<MapBoard>().CheckTiles();
    }
}
